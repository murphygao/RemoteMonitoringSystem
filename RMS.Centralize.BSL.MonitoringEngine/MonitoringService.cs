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
using RMS.Centralize.BSL.MonitoringEngine.Model;
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

            try
            {
                using (var db = new MyDbContext())
                {
                    #region Prepare Parameters

                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var listClients = ListClientWithIPAddress(licenseInfo);

                    foreach (var client in listClients)
                    {
                        BroadcastAliveMessage(client);
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Start failed. " + ex.Message, ex);
            }
        }

        private void BroadcastAliveMessage(RmsClientWithIPAddress client)
        {
            try
            {
                var asc = new AgentServiceClient();
                string clientEndpiont = ConfigurationManager.AppSettings["RMS.NetTcpBinding_AgentService"];
                clientEndpiont = clientEndpiont.Replace("client_ip_address", client.IPAddress);
                asc.Endpoint.Address = new EndpointAddress(clientEndpiont);
                var result = asc.Monitoring(client.ClientCode);

            }
            catch (Exception e)
            {
                try
                {
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

                Console.WriteLine(e);
            }
        }

        public List<RmsClientWithIPAddress> ListClientWithIPAddress(LicenseInfo licenseInfo)
        {

            using (var db = new MyDbContext())
            {
                #region Prepare Parameters

                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;

                var listOfType = db.Database.SqlQuery<RmsClientWithIPAddress>("RMS_ListClientWithIPAddress");

                var listClients = new List<RmsClientWithIPAddress>(listOfType.ToList());

                if (!licenseInfo.ValidateLicense(listClients.Count, null, null, null, null, null))
                {
                    throw new Exception("License is invalid or exceed active client's quota or expired. Please contact product owner.");
                }

                return listClients;

                #endregion
            }

        }
    }
}
