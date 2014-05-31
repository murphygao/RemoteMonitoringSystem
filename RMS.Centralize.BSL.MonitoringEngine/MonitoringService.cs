using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization.Configuration;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ESN.LicenseManager.Model;
using RMS.Centralize.BSL.MonitoringEngine.AgentTCPProxy;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Proxy.MonitoringProxy;


namespace RMS.Centralize.BSL.MonitoringEngine
{
    public class MonitoringService
    {
        public void Start(LicenseInfo licenseInfo)
        {

            /*
             * 1. Get all active clients from db
             * 2. generate multi-thread for boardcasting message
             */

            int activeClient = 0;
            try
            {
                using (var db = new MyDbContext())
                {
                    #region Prepare Parameters

                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var listClients = ListClientWithIPAddress(licenseInfo, out activeClient, true);

                    // Log into RMS_Log_Monitoring
                    int refID = AddLogMonitoring(activeClient, true, null);

                    foreach (var client in listClients)
                    {
                        BroadcastAliveMessage(client, refID);
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

                throw new Exception("Start failed. " + ex.Message, ex);
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
                        throw;


                    // ถ้า Cleint State เป็น 1 (Normal) แสดงว่า Agent ผิดปกติ
                    if (client.State == 1)
                    {
                        using (var db = new MyDbContext())
                        {
                            SqlParameter[] parameters = new SqlParameter[1];
                            SqlParameter p1 = new SqlParameter("ClientID", client.ClientId);
                            parameters[0] = p1;

                            var lists = db.Database.SqlQuery<RmsMonitoringProfileDevice>("RMS_GetMonitoringProfileDeviceByClientID "
                                                                                         + "@ClientID", parameters);
                            var device = lists.First(f => f.DeviceId == 1 || f.DeviceDescription == "Alive");
                            if (device != null)
                            {
                                RMS.Centralize.WebService.Proxy.MonitoringProxy.MonitoringServiceClient mp = new MonitoringServiceClient();

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
                    throw new Exception("BroadcastAliveMessage failed. " + ex.Message, ex);
                }
            }
        }

        public List<RmsClient> ListClientWithIPAddress(LicenseInfo licenseInfo, out int activeClient, bool? hasMonitoringAgent)
        {

            using (var db = new MyDbContext())
            {
                #region Prepare Parameters

                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;

                var listOfType = db.Database.SqlQuery<RmsClient>("RMS_ListClientWithIPAddress");

                List<RmsClient> listClients = new List<RmsClient>(listOfType.ToList());

                var numOfHasAgent = listClients.Count(c => c.HasMonitoringAgent == true);
                var numOfNoAgent = listClients.Count(c => c.HasMonitoringAgent == false || c.HasMonitoringAgent == null);


                if (!licenseInfo.ValidateLicense(numOfHasAgent, numOfNoAgent, null, null, null, null, null, null))
                {
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

                #endregion
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
                throw new Exception("AddLogMonitoring failed. " + ex.Message, ex);
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
                throw new Exception("AddLogMonitoringClient failed. " + ex.Message, ex);
            }
        }
    }
}
