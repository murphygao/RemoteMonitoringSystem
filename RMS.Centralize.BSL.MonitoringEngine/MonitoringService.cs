using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization.Configuration;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using RMS.Centralize.BSL.MonitoringEngine.AgentTCPProxy;
using RMS.Centralize.BSL.MonitoringEngine.Model;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Proxy.MonitoringProxy;


namespace RMS.Centralize.BSL.MonitoringEngine
{
    public class MonitoringService
    {
        public void Start()
        {

            /*
             * 1. Get all active clients from db
             * 2. generate multi-thread for boardcasting message
             */

            using (var db = new MyDbContext())
            {
                #region Prepare Parameters

                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;

                var listOfType = db.Database.SqlQuery<RmsClientWithIPAddress>("RMS_ListClientWithIPAddress");

                var listClients = new List<RmsClientWithIPAddress>(listOfType.ToList());

                foreach (var client in listClients)
                {
                    BroadcastAliveMessage(client);
                }

                #endregion
            }
        }

        private void BroadcastAliveMessage(RmsClientWithIPAddress client)
        {
            try
            {
                var asc = new AgentServiceClient();
                string clientEndpiont = "net.tcp://localhost:8081/AgentNetTcp";
                clientEndpiont = clientEndpiont.Replace("localhost", client.IPAddress);
                //asc.Endpoint.Address = new EndpointAddress(clientEndpiont);
                var result = asc.Monitoring(client.ClientCode);

            }
            catch (Exception e)
            {
                try
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

                            RMS.Centralize.WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw rawMessage = new WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw
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
                catch (Exception)
                {
                    
                    throw;
                }



                Console.WriteLine(e);
            }
        }
    }
}
