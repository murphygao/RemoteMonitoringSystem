using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Amib.Threading;
using ESN.LicenseManager.Model;
using RMS.Centralize.DAL;
using RMS.Common.Exception;
using RmsReportMonitoringRaw = RMS.Centralize.WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw;

namespace RMS.Centralize.BSL.MonitoringEngine
{
    public class WebsiteMonitoringService
    {

        public SmartThreadPool stp;
        public static bool isRunning = false;
        private List<RmsListOfValue> lListOfValues = new List<RmsListOfValue>();

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

                    var monitoringService = new MonitoringService();

                    var listClients = monitoringService.ListClientWithIPAddress(licenseInfo, out activeClient, null);

                    List<RmsClient> broadcastClient = new List<RmsClient>();

                    foreach (var client in listClients)
                    {
                        if (client.UseLocalInfo == true)
                        {
                            var location = db.RmsLocations.Find(client.LocationId);
                            if (location != null)
                            {
                                if (monitoringService.CanBroadCast(location))
                                    broadcastClient.Add(client);
                            }
                        }
                        else
                        {
                            broadcastClient.Add(client);
                        }
                    }

                    // Log into RMS_Log_Monitoring
                    int refID = AddLogWebsiteMonitoring(activeClient, true, null);


                    #region Initial Thread Pool

                    var maxThreadConfig = db.RmsSystemConfigs.Find("WebsiteMonitoringEngine.MaxThread");

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
                        stpStartInfo.IdleTimeout = 120 * 1000;
                        stpStartInfo.MinWorkerThreads = minThread;
                        stpStartInfo.PerformanceCounterInstanceName = "RMSWebsiteCounter";
                        stpStartInfo.EnableLocalPerformanceCounters = true;
                        stp = new SmartThreadPool(stpStartInfo);

                        var iDbSetLOV = db.RmsListOfValues;
                        lListOfValues = new List<RmsListOfValue>(iDbSetLOV);
                    }

                    #endregion

                    foreach (var client in broadcastClient)
                    {
                        RmsClient _client = client;

                        Amib.Threading.Action checkWebsiteMonitoring = () =>
                        {
                            Check(_client, refID);
                        };

                        stp.QueueWorkItem(checkWebsiteMonitoring);
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
                    AddLogWebsiteMonitoring(activeClient, false, ex.Message);
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



        public void Check(RmsClient client, int refID)
        {

            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;
                    var listofdata = db.RmsWebsiteMonitorings.Where(c => c.ClientId == client.ClientId && c.Enable == true);

                    List<RmsWebsiteMonitoring> lWebMonitorings = new List<RmsWebsiteMonitoring>(listofdata.ToList());
                    List<RmsReportMonitoringRaw> lRmsReportMonitoringRaws = new List<RmsReportMonitoringRaw>();


                    foreach (var websiteMonitoring in lWebMonitorings)
                    {
                        string detail = null;
                        try
                        {
                            var websitMonitoringProtocol = lListOfValues.FirstOrDefault(w => w.ListName == "WebsitMonitoringProtocol" && w.ItemId == websiteMonitoring.WebsiteMonitoringProtocolId);

                            if (websitMonitoringProtocol != null)
                            {
                                detail += websitMonitoringProtocol.ItemValue;
                            }
                            // 1 = Ping, 2 = Telnet
                            if (websiteMonitoring.WebsiteMonitoringProtocolId == "1" || websiteMonitoring.WebsiteMonitoringProtocolId == "2")
                            {
                                detail += " " + client.IpAddress;
                            }
                            else // Http or Https
                            {
                                detail += " " + websiteMonitoring.DomainName;
                            }

                            detail += " " + websiteMonitoring.PortNumber;

                            detail = detail.Trim();
                        }
                        catch (Exception ex)
                        {
                            new RMSWebException(this, "0500", "Generate Monitoring Detail failed [ClientCode=" + client.ClientCode + ", WebsiteMonitoringId=" + websiteMonitoring.WebsiteMonitoringId + ", WebsiteMonitoringProtocolId=" + websiteMonitoring.WebsiteMonitoringProtocolId + "]. " + ex.Message, ex, true);
                        }
                        try
                        {

                            AddLogWebsiteMonitoringClient(refID, client.ClientId, client.ClientCode, client.IpAddress, client.State, detail);
                            
                            switch (websiteMonitoring.WebsiteMonitoringProtocolId)
                            {
                                case "1":
                                    //Uptime.Ping
                                    lRmsReportMonitoringRaws.Add(MyPing(client, websiteMonitoring));
                                    break;
                                case "2":
                                    //Uptime.Telnet
                                    lRmsReportMonitoringRaws.Add(MyTelnet(client, websiteMonitoring));
                                    break;
                                case "3":
                                    //Uptime.HTTP
                                    lRmsReportMonitoringRaws.Add(MyHTTPStatus(client, websiteMonitoring));
                                    break;
                                case "4":
                                    //Uptime.HTTPS
                                    lRmsReportMonitoringRaws.Add(MyHTTPStatus(client, websiteMonitoring));
                                    break;
                                case "5":
                                    //Webpage.HTTP
                                    break;
                                case "6":
                                    //Webpage.HTTPS
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            new RMSWebException(this, "0500", "Each of Website Checking failed [ClientCode=" + client .ClientCode + ", WebsiteMonitoringId=" + websiteMonitoring.WebsiteMonitoringId + ", WebsiteMonitoringProtocolId=" + websiteMonitoring.WebsiteMonitoringProtocolId + "]. " + ex.Message, ex, true);
                        }

                    }

                    if (lRmsReportMonitoringRaws.Count > 0)
                    {
                        var mp = new Centralize.WebService.Proxy.MonitoringService().monitoringService;
                        mp.AddWebsiteMessage(lRmsReportMonitoringRaws);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Check failed. " + ex.Message, ex, false);
            }
        }

        private int AddLogWebsiteMonitoring(int numberOfMonitoring, bool isSuccess, string errorMessage)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    var rmsLogWebsiteMonitoring = db.RmsLogWebsiteMonitorings.Create();
                    rmsLogWebsiteMonitoring.NumberOfMonitoring = numberOfMonitoring;
                    rmsLogWebsiteMonitoring.MonitoringDateTime = DateTime.Now;
                    rmsLogWebsiteMonitoring.IsSuccess = isSuccess;
                    rmsLogWebsiteMonitoring.ErrorMessage = errorMessage;
                    db.RmsLogWebsiteMonitorings.Add(rmsLogWebsiteMonitoring);
                    db.SaveChanges();
                    return rmsLogWebsiteMonitoring.Id;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "AddLogWebsiteMonitoring failed. " + ex.Message, ex, false);
            }
        }
        private void AddLogWebsiteMonitoringClient(int? refID, int? clientID, string clientCode, string clientIPAddress, int? clientState, string detail)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    var log = db.RmsLogWebsiteMonitoringClients.Create();
                    log.MonitoringDateTime = DateTime.Now;
                    log.RefId = refID;
                    log.ClientId = clientID;
                    log.ClientCode = clientCode;
                    log.ClientIpAddress = clientIPAddress;
                    log.ClientState = clientState;
                    log.Detail = detail;
                    db.RmsLogWebsiteMonitoringClients.Add(log);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "AddLogWebsiteMonitoringClient failed. " + ex.Message, ex, false);
            }
        }

        private WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw MyPing(RmsClient client, RmsWebsiteMonitoring websiteMonitoring)
        {

            if (client == null) throw new RMSWebException(this, "500", "MyPing failed. RmsClient cannot be null", true);
            if (websiteMonitoring == null) throw new RMSWebException(this, "500", "MyPing failed. RmsWebsiteMonitoring cannot be null", true);

            try
            {
                RMS.Centralize.WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw rawMessage =
                    new WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw();
                
                rawMessage.ClientCode = client.ClientCode;
                rawMessage.MessageGroupCode = "W001"; //Uptime
                rawMessage.MessageDateTime = DateTime.Now;
                rawMessage.WebsiteMonitoringId = websiteMonitoring.WebsiteMonitoringId;
                
                using (var db = new MyDbContext())
                {

                    RmsTrnPingResult rmsTrnPing = db.RmsTrnPingResults.Create();

                    rmsTrnPing.ClientId = client.ClientId;
                    rmsTrnPing.ClientCode = client.ClientCode;
                    rmsTrnPing.HostName = client.IpAddress;
                    rmsTrnPing.WebsiteMonitoringId = websiteMonitoring.WebsiteMonitoringId;
                    rawMessage.MessageRemark = "Ping: " + client.IpAddress;

                    int _sent = 0;
                    int _received = 0;
                    int _lost = 0;
                    long? _min = null;
                    long? _max = null;
                    long? _avg = null;
                    
                    try
                    {
                        using (var ping = new Ping())
                        {
                            // Create a buffer of 32 bytes of data to be transmitted. 
                            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                            byte[] buffer = Encoding.ASCII.GetBytes(data);

                            // Wait 10 seconds for a reply. 
                            int timeout = 3000;

                            // Set options for transmission: 
                            // The data can go through 64 gateways or routers 
                            // before it is destroyed, and the data packet 
                            // cannot be fragmented.
                            PingOptions options = new PingOptions(64, true);

                            int echoNum = 3;
                            long totalTime = 0;

                            for (int i = 0; i < echoNum; i++)
                            {
                                var reply = ping.Send(client.IpAddress, timeout, buffer, options);

                                _sent++;

                                if (reply != null && reply.Status == IPStatus.Success)
                                {
                                    _received++;

                                    if (_max == null || reply.RoundtripTime > _max) _max = reply.RoundtripTime;
                                    if (_min == null || reply.RoundtripTime < _min) _min = reply.RoundtripTime;

                                    totalTime += reply.RoundtripTime;
                                }
                                else
                                {
                                    _lost++;
                                }

                                if (reply != null && reply.Address != null)
                                    rmsTrnPing.ReplyAddress = reply.Address.ToString();

                            }

                            if (_max != null)
                                _avg = totalTime/_sent;

                        }

                        if (_sent == _lost) // ถ้าจำนวน lost เท่ากับ sent แสดงว่า ping ไม่ถึงทุก echo ถือว่าติดต่อ server ไม่ได้
                        {
                            rawMessage.Message = "PING_FAILED";
                        }
                        else
                        {
                            rawMessage.Message = "OK";
                        }
                    }
                    catch (Exception e)
                    {
                        rawMessage.Message = "PING_FAILED";
                        rawMessage.MessageRemark += " Error: " + e.Message + (e.InnerException != null ? e.InnerException.Message : "");
                        rmsTrnPing.ErrorMessage = "Error: " + e.Message + (e.InnerException != null ? e.InnerException.Message : "");
                    }

                    rmsTrnPing.Sent = _sent;
                    rmsTrnPing.Received = _received;
                    rmsTrnPing.Lost = _lost;
                    if (_max != null)
                        rmsTrnPing.MaximumValue = Convert.ToInt32(_max);
                    if (_max != null)
                        rmsTrnPing.MinimumValue = Convert.ToInt32(_min);
                    if (_avg != null)
                        rmsTrnPing.AverageValue = Convert.ToInt32(_avg);


                    db.RmsTrnPingResults.Add(rmsTrnPing);
                    db.SaveChanges();

                    return rawMessage;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "MyPing failed [Host: " + client.IpAddress + "]. " + ex.Message, ex, true);
            }

        }

        private WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw MyTelnet(RmsClient client, RmsWebsiteMonitoring websiteMonitoring)
        {
            if (client == null) throw new RMSWebException(this, "500", "MyTelnet failed. RmsClient cannot be null", true);
            if (websiteMonitoring == null) throw new RMSWebException(this, "500", "MyTelnet failed. RmsWebsiteMonitoring cannot be null", true);

            try
            {
                RMS.Centralize.WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw rawMessage = new WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw();

                rawMessage.ClientCode = client.ClientCode;
                rawMessage.MessageGroupCode = "W001"; //Uptime
                rawMessage.MessageDateTime = DateTime.Now;
                rawMessage.WebsiteMonitoringId = websiteMonitoring.WebsiteMonitoringId;

                using (var db = new MyDbContext())
                {
                    RmsTrnTelnetResult rmsTrn = db.RmsTrnTelnetResults.Create();
                    rmsTrn.ClientId = client.ClientId;
                    rmsTrn.ClientCode = client.ClientCode;
                    rmsTrn.HostName = client.IpAddress;
                    rmsTrn.WebsiteMonitoringId = websiteMonitoring.WebsiteMonitoringId;
                    rmsTrn.PortNumber = websiteMonitoring.PortNumber ?? 23;
                    rawMessage.MessageRemark = "Telnet: " + client.IpAddress + ", Port: " + websiteMonitoring.PortNumber;

                    try
                    {
                        using (TcpClient tcp = new TcpClient())
                        {
                            int timeout = 2500;
                            tcp.SendTimeout = timeout;
                            tcp.ReceiveTimeout = timeout;

                            int port = websiteMonitoring.PortNumber ?? 23;


                            tcp.Connect(client.IpAddress, port);

                            if (tcp.Connected)
                            {
                                //Logging
                                rmsTrn.IsSuccess = true;
                                rawMessage.Message = "OK";
                            }
                            else
                            {
                                // Send Message to Sever : แจ้งว่า Ping ไม่ผ่าน
                                rmsTrn.IsSuccess = false;
                                rawMessage.Message = "TELNET_FAILED";
                            }

                            tcp.Close();
                        }
                    }
                    catch (Exception e)
                    {
                        rawMessage.Message = "TELNET_FAILED";
                        rawMessage.MessageRemark += " Error: " + e.Message + (e.InnerException != null ? e.InnerException.Message : "");
                        rmsTrn.IsSuccess = false;
                        rmsTrn.ErrorMessage = "Error: " + e.Message + (e.InnerException != null ? e.InnerException.Message : "");
                    }

                    db.RmsTrnTelnetResults.Add(rmsTrn);
                    db.SaveChanges();

                    return rawMessage;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "MyTelnet failed. " + ex.Message, ex, false);
            }

        }

        private WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw MyHTTPStatus(RmsClient client, RmsWebsiteMonitoring websiteMonitoring)
        {
            if (client == null) throw new RMSWebException(this, "500", "MyHTTPStatus failed. RmsClient cannot be null", true);
            if (websiteMonitoring == null) throw new RMSWebException(this, "500", "MyHTTPStatus failed. RmsWebsiteMonitoring cannot be null", true);

            try
            {
                RMS.Centralize.WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw rawMessage = new WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw();

                rawMessage.ClientCode = client.ClientCode;
                rawMessage.MessageGroupCode = "W001"; //Uptime
                rawMessage.MessageDateTime = DateTime.Now;
                rawMessage.WebsiteMonitoringId = websiteMonitoring.WebsiteMonitoringId;

                using (var db = new MyDbContext())
                {
                    RmsTrnHttpResult rmsTrn = db.RmsTrnHttpResults.Create();
                    rmsTrn.ClientId = client.ClientId;
                    rmsTrn.ClientCode = client.ClientCode;
                    rmsTrn.Url = websiteMonitoring.DomainName;
                    rmsTrn.WebsiteMonitoringId = websiteMonitoring.WebsiteMonitoringId;
                    rawMessage.MessageRemark = "URL: " + websiteMonitoring.DomainName;

                    try
                    {
                        if (rmsTrn.Url == null)
                            throw new ArgumentNullException("rmsTrn.Url");

                        if (rmsTrn.Url.ToLower().IndexOf("http:") < 0 && rmsTrn.Url.ToLower().IndexOf("https:") < 0)
                        {
                            // HTTP
                            if (websiteMonitoring.WebsiteMonitoringProtocolId == "3")
                            {
                                rmsTrn.Url = "http://" + rmsTrn.Url.Trim();
                            
                            }
                                // HTTPS
                            else if (websiteMonitoring.WebsiteMonitoringProtocolId == "4")
                            {
                                rmsTrn.Url = "https://" + rmsTrn.Url.Trim();
                            
                            }
                        }

                        rawMessage.MessageRemark = "URL: " + rmsTrn.Url;

                        if (!Uri.IsWellFormedUriString(rmsTrn.Url, UriKind.Absolute))
                            throw new ArgumentException("rmsTrn.Url [" + rmsTrn.Url + "] is invalid format.");

                        var request = (HttpWebRequest) WebRequest.Create(rmsTrn.Url);
                        request.Method = "HEAD";
                        request.Timeout = 5000;

                        using (var response = (HttpWebResponse) request.GetResponse())
                        {
                            rmsTrn.ResponseCode = (int) response.StatusCode;

                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                rawMessage.Message = "OK";
                            }
                            else
                            {
                                rawMessage.Message = "UPTIME_HTTP_FAILED";
                                rawMessage.MessageRemark += " HttpStatusCode: " + (int) response.StatusCode;
                            }

                            response.Close();
                        }
                    }
                    catch (WebException e)
                    {
                        if (e.Status == WebExceptionStatus.ProtocolError &&
                            e.Response != null)
                        {
                            var resp = (HttpWebResponse)e.Response;
                            rmsTrn.ResponseCode = (int)resp.StatusCode;
                        }

                        rawMessage.Message = "UPTIME_HTTP_FAILED";
                        rawMessage.MessageRemark += " Error: " + e.Message + (e.InnerException != null ? e.InnerException.Message : "");
                        rmsTrn.IsSuccess = false;
                        rmsTrn.ErrorMessage = "Error: " + e.Message + (e.InnerException != null ? e.InnerException.Message : "");
                    }
                    catch (Exception e)
                    {
                        rawMessage.Message = "UPTIME_HTTP_FAILED";
                        rawMessage.MessageRemark += " Error: " + e.Message + (e.InnerException != null ? e.InnerException.Message : "");
                        rmsTrn.IsSuccess = false;
                        rmsTrn.ErrorMessage = "Error: " + e.Message + (e.InnerException != null ? e.InnerException.Message : "");
                    }

                    db.RmsTrnHttpResults.Add(rmsTrn);
                    db.SaveChanges();

                    return rawMessage;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "MyHTTPStatus failed. " + ex.Message, ex, false);
            }

        }
    }
}
