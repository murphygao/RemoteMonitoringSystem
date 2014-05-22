using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Transactions;
using RMS.Centralize.BSL.MonitoringEngine.AgentTCPProxy;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Gateway;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SelfTestingService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SelfTestingService.svc or SelfTestingService.svc.cs at the Solution Explorer and start debugging.
    public class SelfTestingService : ISelfTestingService
    {
        public void TestConnection()
        {
            
        }

        public string TestWebConfig()
        {
            string ret = "";
            try
            {
                /*
                    <add key="RMS.OverrideProxy" value="false"/>
                    <add key="RMS.NetTcpBinding_AgentService" value="net.tcp://client_ip_address:8081/AgentNetTcp"/>
                    <add key="RMS.WebServicURL_MonitoringService" value="http://localhost/RMS.Centralize.WebService/MonitoringService.svc"/>
                    <add key="RMS.ActionModeTest" value="true"/>

                    <add key="SKAdapter.SMSGW.URL" value="http://10.217.83.125:2288" />
                    <add key="SKAdapter.SMSGW.CODE" value="CALLBACK" />
                    <add key="SKAdapter.SMSGW.ServiceTimeoutMilliseconds" value="10000" />
                    <add key="SKAdapter.SMSGW.Sender" value="SKS" />
                    <add key="SKAdapter.SMSGW.TestReceiver" value="0860506900" />

                    <add key="SKAdapter.Email.Server" value="10.252.160.41" />
                    <add key="SKAdapter.Email.Port" value="25" />
                    <add key="SKAdapter.Email.SSL" value="False" />
                    <add key="SKAdapter.Email.FromAddress" value="sks@corp.ais900dev.org" />
                    <add key="SKAdapter.Email.FromName" value="AIS Service Kiosk System" />
                    <add key="SKAdapter.Email.ServiceTimeoutMilliseconds" value="10000" />
                    <add key="SKAdapter.Email.TestReceiver" value="satht471@corp.ais900dev.org" />

                 */

                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["RMS.OverrideProxy"]))
                {
                    ret += "<br/>AppSettings[\"RMS.OverrideProxy\"] not found or empty.";
                }

                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["RMS.NetTcpBinding_AgentService"]))
                {
                    ret += "<br/>AppSettings[\"RMS.NetTcpBinding_AgentService\"] not found or empty.";
                }

                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["RMS.WebServicURL_MonitoringService"]))
                {
                    ret += "<br/>AppSettings[\"RMS.WebServicURL_MonitoringService\"] not found or empty.";
                }

                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["RMS.ActionModeTest"]))
                {
                    ret += "<br/>AppSettings[\"RMS.ActionModeTest\"] not found or empty.";
                }

                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SKAdapter.SMSGW.URL"]))
                {
                    ret += "<br/>AppSettings[\"SKAdapter.SMSGW.URL\"] not found or empty.";
                }

                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SKAdapter.SMSGW.CODE"]))
                {
                    ret += "<br/>AppSettings[\"SKAdapter.SMSGW.CODE\"] not found or empty.";
                }

                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SKAdapter.SMSGW.ServiceTimeoutMilliseconds"]))
                {
                    ret += "<br/>AppSettings[\"SKAdapter.SMSGW.ServiceTimeoutMilliseconds\"] not found or empty.";
                }

                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SKAdapter.SMSGW.Sender"]))
                {
                    ret += "<br/>AppSettings[\"SKAdapter.SMSGW.Sender\"] not found or empty.";
                }

                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SKAdapter.SMSGW.TestReceiver"]))
                {
                    ret += "<br/>AppSettings[\"SKAdapter.SMSGW.TestReceiver\"] not found or empty.";
                }

                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SKAdapter.Email.Server"]))
                {
                    ret += "<br/>AppSettings[\"SKAdapter.Email.Server\"] not found or empty.";
                }

                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SKAdapter.Email.Port"]))
                {
                    ret += "<br/>AppSettings[\"SKAdapter.Email.Port\"] not found or empty.";
                }

                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SKAdapter.Email.SSL"]))
                {
                    ret += "<br/>AppSettings[\"SKAdapter.Email.SSL\"] not found or empty.";
                }

                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SKAdapter.Email.FromAddress"]))
                {
                    ret += "<br/>AppSettings[\"SKAdapter.Email.FromAddress\"] not found or empty.";
                }

                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SKAdapter.Email.FromName"]))
                {
                    ret += "<br/>AppSettings[\"SKAdapter.Email.FromName\"] not found or empty.";
                }

                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SKAdapter.Email.ServiceTimeoutMilliseconds"]))
                {
                    ret += "<br/>AppSettings[\"SKAdapter.Email.ServiceTimeoutMilliseconds\"] not found or empty.";
                }

                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SKAdapter.Email.TestReceiver"]))
                {
                    ret += "<br/>AppSettings[\"SKAdapter.Email.TestReceiver\"] not found or empty.";
                }

                return ret;
            }
            catch (Exception ex)
            {
                return ret + "<br/><br/>" + ex.Message;
            }
        }

        public string TestDatabaseConnection()
        {
            string ret = "";
            try
            {
                using (var db = new MyDbContext())
                {

                    using (var ts = db.Database.BeginTransaction())
                    {
                        //db.Configuration.ProxyCreationEnabled = false;
                        //db.Configuration.LazyLoadingEnabled = false;

                        try
                        {
                            var rmsSystemConfig = db.RmsSystemConfigs.Create();
                            rmsSystemConfig.Name = "SelfTestingService";

                            db.RmsSystemConfigs.Add(rmsSystemConfig);
                            db.SaveChanges();

                            db.RmsSystemConfigs.Remove(rmsSystemConfig);
                            db.SaveChanges();

                            ts.Commit();
                        }
                        catch (Exception ex)
                        {
                            ts.Rollback();
                            throw ex;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return "<br/>" + ex.Message;
            }

            return ret;

        }

        public string TestAgentConnection(string ipAddress)
        {
            string ret = "";
            try
            {
                var asc = new AgentServiceClient();
                string clientEndpiont = ConfigurationManager.AppSettings["RMS.NetTcpBinding_AgentService"];
                clientEndpiont = clientEndpiont.Replace("client_ip_address", ipAddress);

                //System.Uri uri = new Uri(clientEndpiont);
                //var endpointAddress = asc.Endpoint.Address;
                //AddressHeaderCollection addressHeaderCollection = endpointAddress.Headers;
                //EndpointIdentity endpointIdentity = endpointAddress.Identity;

                asc.Endpoint.Address = new EndpointAddress(clientEndpiont);
                asc.Endpoint.Binding.OpenTimeout = new TimeSpan(0, 0, 5);
                
                var ar = asc.InnerChannel.BeginOpen(null, null);
                if (!ar.AsyncWaitHandle.WaitOne(asc.Endpoint.Binding.OpenTimeout, true))
                    throw new CommunicationException(ipAddress + " Monitoring Agent is not available.");
                asc.InnerChannel.EndOpen(ar);

                asc.TestConnection();
            }
            catch (Exception ex)
            {
                return "<br/>" + ex.Message;
            }
            return ret;

        }

        public string TestEmailSmsConnection(string email, string sms)
        {
            string ret = "";

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["RMS.ActionModeTest"]))
            {
                ret += "<br/>Please set RMS.ActionModeTest in Web.config to false. ";
                return ret;
            }


            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    string emailFrom = ConfigurationManager.AppSettings["SKAdapter.Email.FromAddress"];
                    WebService.Gateway.ActionGateway ag = new ActionGateway();
                    var actionResult = ag.SendEmail(GatewayName.AIS_SKS, emailFrom, new List<string> { email }, "Testing Email Connection",
                        DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("en-AU")));
                    if (!actionResult.IsSuccess) ret += "<br/>Email Gateway - " + actionResult.ErrorMessage;
                }
                else
                {
                    ret += "<br/>Skip testing Email.";
                }
            }
            catch (Exception ex)
            {

                ret += "<br/>" + ex.Message;
            }

            try
            {
                if (!string.IsNullOrEmpty(sms))
                {
                    string smsSender = ConfigurationManager.AppSettings["SKAdapter.SMSGW.Sender"];
                    WebService.Gateway.ActionGateway ag = new ActionGateway();
                    var actionResult = ag.SendSMS(GatewayName.AIS_SKS, sms, smsSender, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("en-AU")));
                    if (!actionResult.IsSuccess) ret += "<br/>SMS Gateway - " + actionResult.ErrorMessage;
                }
                else
                {
                    ret += "<br/>Skip testing SMS.";
                }
            }
            catch (Exception ex)
            {

                ret += "<br/>" + ex.Message;
            }

            return ret;
        }
    }
}
