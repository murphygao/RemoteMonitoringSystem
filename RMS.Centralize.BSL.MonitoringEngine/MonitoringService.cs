using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using RMS.Centralize.BSL.MonitoringEngine.AgentTCPProxy;
using RMS.Centralize.BSL.MonitoringEngine.Model;
using RMS.Centralize.DAL;

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
                Console.WriteLine(e);
            }
        }
    }
}
