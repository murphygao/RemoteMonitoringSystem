using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using Amib.Threading;
using ESN.LicenseManager.Model;
using RMS.Centralize.BSL.MonitoringEngine.AgentTCPProxy;
using RMS.Centralize.BSL.MonitoringEngine.Model;
using RMS.Centralize.DAL;
using RMS.Common.Exception;
using RMS.Monitoring.Helper;


namespace RMS.Centralize.BSL.MonitoringEngine
{
    public class MonitoringService
    {
        public SmartThreadPool stp;
        public static bool isRunning = false ;

        public void Start(LicenseInfo licenseInfo)
        {
            if (isRunning) return;

            /*
             * 1. Get all active clients from db
             * 2. generate multi-thread for boardcasting message
             */

            int activeClient = 0;
            try
            {
                isRunning = true;

                using (var db = new MyDbContext())
                {
                    #region Prepare Parameters

                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var listClients = ListClientWithIPAddress(licenseInfo, out activeClient, true);

                    List<RmsClient> broadcastClient = new List<RmsClient>();

                    foreach (var client in listClients)
                    {
                        if (client.UseLocalInfo == true)
                        {
                            var location = db.RmsLocations.Find(client.LocationId);
                            if (location != null)
                            {
                                if (CanBroadCast(location))
                                    broadcastClient.Add(client);
                            }
                        }
                        else
                        {
                            broadcastClient.Add(client);
                        }
                    }

                    // Log into RMS_Log_Monitoring
                    int refID = AddLogMonitoring(activeClient, true, null);


                    #region Initial Thread Pool

                    var maxThreadConfig = db.RmsSystemConfigs.Find("MonitoringEngine.MaxThread");

                    int minThread = 1;
                    int maxThread = 1;

                    if (maxThreadConfig != null)
                    {
                        maxThread = Convert.ToInt32(maxThreadConfig.Value ?? maxThreadConfig.DefaultValue);

                        if (maxThread > broadcastClient.Count)
                        {
                            maxThread = broadcastClient.Count;
                        }
                    }

                    if (broadcastClient.Count > 0)
                    {
                        STPStartInfo stpStartInfo = new STPStartInfo();
                        stpStartInfo.MaxWorkerThreads = maxThread;
                        stpStartInfo.IdleTimeout = 60*1000;
                        stpStartInfo.MinWorkerThreads = minThread;
                        stpStartInfo.PerformanceCounterInstanceName = "RMSCounter";
                        stpStartInfo.EnableLocalPerformanceCounters = true;
                        stp = new SmartThreadPool(stpStartInfo);
                    }

                    #endregion

                    foreach (var client in broadcastClient)
                    {
                        RmsClient _client = client;

                        Amib.Threading.Action broadcastAliveMessage = () =>
                        {
                            BroadcastAliveMessage(_client, refID);
                        };

                        stp.QueueWorkItem(broadcastAliveMessage);
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                string exLicense = "License is invalid";
                if (ex.Message.ToLower().IndexOf(exLicense.ToLower()) > -1)
                {
                    // Log into RMS_Log_Monitoring
                    AddLogMonitoring(activeClient, false, ex.Message);
                }
                throw new RMSWebException(this, "0500", "Start failed. " + ex.Message, ex, false);
            }
            finally
            {
                if (stp != null)
                {
                    stp.WaitForIdle();
                    System.Threading.Thread.Sleep(1000);
                    if (!stp.IsShuttingdown) stp.Shutdown();
                    stp.Dispose();
                }
                isRunning = false;
            }
        }

        private void BroadcastAliveMessage(RmsClient client, int refID)
        {
            try
            {
                var asc = new AgentServiceClient();
                string clientEndpiont = ConfigurationManager.AppSettings["RMS.NetTcpBinding_AgentService"];
                clientEndpiont = clientEndpiont.Replace("client_ip_address", client.IpAddress);
                asc.Endpoint.Address = new EndpointAddress(clientEndpiont);
                AddLogMonitoringClient(refID, client.ClientId, client.ClientCode, client.IpAddress, client.State, null);
                var result = asc.Monitoring(client.ClientCode);

            }
            catch (Exception e)
            {
                try
                {
                    if (e.Message.IndexOf("AddLogMonitoringClient") > -1)
                    {
                        throw new RMSWebException(this, "0500", "BroadcastAliveMessage failed. " + e.Message, e, false);
                    }


                    // ถ้า Cleint State เป็น 1 (Normal) แสดงว่า Agent ผิดปกติ
                    if (client.State == 1)
                    {
                        using (var db = new MyDbContext())
                        {
                            var rmsSystemConfig = db.RmsSystemConfigs.Find("AgentProcessName");
                            var agentProcessName = rmsSystemConfig.Value ?? rmsSystemConfig.DefaultValue;

                            SqlParameter[] parameters = new SqlParameter[1];
                            SqlParameter p1 = new SqlParameter("ClientID", client.ClientId);
                            parameters[0] = p1;

                            var lists = db.Database.SqlQuery<MonitoringProfileDeviceInfo>("RMS_GetMonitoringProfileDeviceByClientID "
                                                                                         + "@ClientID", parameters);
                            var device = lists.First(f => f.DeviceCode == "CLIENT" && f.DeviceTypeCode == Models.DeviceCode.Client 
                                    && (string.IsNullOrEmpty(f.StringValue) || f.StringValue == agentProcessName));
                            
                            if (device != null)
                            {
                                var mp = new Centralize.WebService.Proxy.MonitoringService().monitoringService;

                                RMS.Centralize.WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw rawMessage = new WebService.Proxy.
                                    MonitoringProxy.RmsReportMonitoringRaw
                                {
                                    ClientCode = client.ClientCode,
                                    DeviceCode = "CLIENT",
                                    Message = "AGENT_NOT_ALIVE",
                                    MessageDateTime = DateTime.Now,
                                    MonitoringProfileDeviceId = device.MonitoringProfileDeviceId
                                };

                                mp.AddMessage(rawMessage);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new RMSWebException(this, "0500", "BroadcastAliveMessage failed. " + ex.Message, ex, false);
                  }
            }
        }

        public List<RmsClient> ListClientWithIPAddress(LicenseInfo licenseInfo, out int activeClient, bool? hasMonitoringAgent)
        {

            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.Database.SqlQuery<RmsClient>("RMS_ListClientWithIPAddress");

                    List<RmsClient> listClients = new List<RmsClient>(listOfType.ToList());

                    var numOfHasAgent = listClients.Count(c => c.HasMonitoringAgent == true);
                    var numOfNoAgent = listClients.Count(c => c.HasMonitoringAgent == false || c.HasMonitoringAgent == null);


                    if (!licenseInfo.ValidateLicense(numOfHasAgent, numOfNoAgent, null, null, null, null, null, null))
                    {
                        new RMSLicenseException("ValidateLicense failed. numOfHasAgent: " + numOfHasAgent + "/" + licenseInfo.quota1 + ", numOfNoAgent:" + numOfNoAgent + "/" + licenseInfo.quota2, true);
                        throw new Exception("License is invalid or exceed active client's quota or expired. Please contact product owner.");
                    }

                    if (hasMonitoringAgent == null)
                    {
                        listClients = new List<RmsClient>(listOfType.ToList());
                    }
                    else
                    {
                        listClients = new List<RmsClient>(listOfType.Where(c => c.HasMonitoringAgent == hasMonitoringAgent));
                    }

                    activeClient = listClients.Count;

                    return listClients;


                }

            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "ListClientWithIPAddress failed. " + ex.Message, ex, false);

            }
        }

        public int AddLogMonitoring(int numberOfMonitoring, bool isSuccess, string errorMessage)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    var rmsLogMonitoring = db.RmsLogMonitorings.Create();
                    rmsLogMonitoring.NumberOfMonitoring = numberOfMonitoring;
                    rmsLogMonitoring.MonitoringDateTime = DateTime.Now;
                    rmsLogMonitoring.IsSuccess = isSuccess;
                    rmsLogMonitoring.ErrorMessage = errorMessage;
                    db.RmsLogMonitorings.Add(rmsLogMonitoring);
                    db.SaveChanges();
                    return rmsLogMonitoring.Id;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "AddLogMonitoring failed. " + ex.Message, ex, false);
            }
        }
        public void AddLogMonitoringClient(int? refID, int? clientID, string clientCode, string clientIPAddress, int? clientState, string detail)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    var log = db.RmsLogMonitoringClients.Create();
                    log.MonitoringDateTime = DateTime.Now;
                    log.RefId = refID;
                    log.ClientId = clientID;
                    log.ClientCode = clientCode;
                    log.ClientIpAddress = clientIPAddress;
                    log.ClientState = clientState;
                    log.Detail = detail;
                    db.RmsLogMonitoringClients.Add(log);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "AddLogMonitoringClient failed. " + ex.Message, ex, false);
            }
        }

        public bool CanBroadCast(RmsLocation location)
        {
            try
            {
                //ครวจสอบว่า วันและเวลาตอนนี้ ต้อง broadcast หรือไม่
                DateTime myToday = DateTime.Now;

                if ((int) myToday.DayOfWeek == 1) // Monday
                {
                    if (location.MondayEnable == null || location.MondayEnable == false) return false;
                    if (location.MondayWholeDay == true) return true;
                    if (location.MondayStart == null || location.MondayEnd == null) return true;

                    int sHH = location.MondayStart.Value.Hour;
                    int smm = location.MondayStart.Value.Minute;
                    DateTime startTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, sHH, smm, 0);
                    int eHH = location.MondayEnd.Value.Hour;
                    int emm = location.MondayEnd.Value.Minute;
                    DateTime endTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, eHH, emm, 0);
                    if (startTime <= myToday && myToday <= endTime) return true;
                }

                else if ((int) myToday.DayOfWeek == 2) // Tuesday
                {
                    if (location.TuesdayEnable == null || location.TuesdayEnable == false) return false;
                    if (location.TuesdayWholeDay == true) return true;
                    if (location.TuesdayStart == null || location.TuesdayEnd == null) return true;

                    int sHH = location.TuesdayStart.Value.Hour;
                    int smm = location.TuesdayStart.Value.Minute;
                    DateTime startTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, sHH, smm, 0);
                    int eHH = location.TuesdayEnd.Value.Hour;
                    int emm = location.TuesdayEnd.Value.Minute;
                    DateTime endTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, eHH, emm, 0);
                    if (startTime <= myToday && myToday <= endTime) return true;
                }

                else if ((int) myToday.DayOfWeek == 3) // Wednesday
                {
                    if (location.WednesdayEnable == null || location.WednesdayEnable == false) return false;
                    if (location.WednesdayWholeDay == true) return true;
                    if (location.WednesdayStart == null || location.WednesdayEnd == null) return true;

                    int sHH = location.WednesdayStart.Value.Hour;
                    int smm = location.WednesdayStart.Value.Minute;
                    DateTime startTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, sHH, smm, 0);
                    int eHH = location.WednesdayEnd.Value.Hour;
                    int emm = location.WednesdayEnd.Value.Minute;
                    DateTime endTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, eHH, emm, 0);
                    if (startTime <= myToday && myToday <= endTime) return true;
                }

                else if ((int) myToday.DayOfWeek == 4) // Thursday
                {
                    if (location.ThursdayEnable == null || location.ThursdayEnable == false) return false;
                    if (location.ThursdayWholeDay == true) return true;
                    if (location.ThursdayStart == null || location.ThursdayEnd == null) return true;

                    int sHH = location.ThursdayStart.Value.Hour;
                    int smm = location.ThursdayStart.Value.Minute;
                    DateTime startTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, sHH, smm, 0);
                    int eHH = location.ThursdayEnd.Value.Hour;
                    int emm = location.ThursdayEnd.Value.Minute;
                    DateTime endTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, eHH, emm, 0);
                    if (startTime <= myToday && myToday <= endTime) return true;
                }

                else if ((int) myToday.DayOfWeek == 5) // Friday
                {
                    if (location.FridayEnable == null || location.FridayEnable == false) return false;
                    if (location.FridayWholeDay == true) return true;
                    if (location.FridayStart == null || location.FridayEnd == null) return true;

                    int sHH = location.FridayStart.Value.Hour;
                    int smm = location.FridayStart.Value.Minute;
                    DateTime startTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, sHH, smm, 0);
                    int eHH = location.FridayEnd.Value.Hour;
                    int emm = location.FridayEnd.Value.Minute;
                    DateTime endTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, eHH, emm, 0);
                    if (startTime <= myToday && myToday <= endTime) return true;
                }

                else if ((int) myToday.DayOfWeek == 6) // Saturday
                {
                    if (location.SaturdayEnable == null || location.SaturdayEnable == false) return false;
                    if (location.SaturdayWholeDay == true) return true;
                    if (location.SaturdayStart == null || location.SaturdayEnd == null) return true;

                    int sHH = location.SaturdayStart.Value.Hour;
                    int smm = location.SaturdayStart.Value.Minute;
                    DateTime startTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, sHH, smm, 0);
                    int eHH = location.SaturdayEnd.Value.Hour;
                    int emm = location.SaturdayEnd.Value.Minute;
                    DateTime endTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, eHH, emm, 0);
                    if (startTime <= myToday && myToday <= endTime) return true;
                }

                else if ((int) myToday.DayOfWeek == 7) // Sunday
                {
                    if (location.SundayEnable == null || location.SundayEnable == false) return false;
                    if (location.SundayWholeDay == true) return true;
                    if (location.SundayStart == null || location.SundayEnd == null) return true;

                    int sHH = location.SundayStart.Value.Hour;
                    int smm = location.SundayStart.Value.Minute;
                    DateTime startTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, sHH, smm, 0);
                    int eHH = location.SundayEnd.Value.Hour;
                    int emm = location.SundayEnd.Value.Minute;
                    DateTime endTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, eHH, emm, 0);
                    if (startTime <= myToday && myToday <= endTime) return true;
                }


                return false;
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "CanBroadCast failed. " + ex.Message, ex, false);
            }

        }
    }
}
