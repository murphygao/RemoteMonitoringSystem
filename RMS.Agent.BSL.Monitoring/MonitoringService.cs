using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;
using RMS.Agent.Entity;
using RMS.Agent.Proxy;
using RMS.Agent.Proxy.ClientProxy;
using RMS.Agent.Proxy.MonitoringProxy;
using RMS.Common.Exception;
using RMS.Monitoring.Helper;

namespace RMS.Agent.BSL.Monitoring
{
    public class MonitoringService
    {
        private delegate void ExecuteCommandAsync(string clientCode);
        private delegate void SetMonitoringStateAsync(string clientCode, ClientState clientState);

        private SetMonitoringStateAsync stateAsync;
        public MonitoringService()
        {
            stateAsync = new SetMonitoringStateAsync(SetMonitoringState);
        }

        public void Command(string clientCode)
        {
            try
            {
                if (string.IsNullOrEmpty(clientCode)) return;

                UpdateClientCode(clientCode);
                var caller = new ExecuteCommandAsync(ExecuteCommand);
                caller.BeginInvoke(clientCode, null, null);
            }
            catch (Exception ex)
            {
                throw new RMSAppException("Command failed. clientCode=" + clientCode + "  " + ex.Message, ex, false);
            }
        }


        private void ExecuteCommand(string clientCode)
        {
            /*
             * 
             * 1. Call Centralize to Get Client & Device Info for Monitoring 
             * 2. Send Alive Message
             * 3. Check Maintenance State
             * 4. Check Device Monitoring
             * 
             */


            try
            {
                #region 1. Call Centralize to Get Client & Device Info for Monitoring

                ClientServiceClient cs = new ClientService().clientService;
                var clientResult = cs.GetClient(GetClientBy.ClientCode, null, clientCode, null, true, true);

                int? deviceId = null;
                int? monitoringProfileDeviceId = null;

                var rmsMonitoringProfileDevices = RMS.Monitoring.Helper.Common.GetRmsMonitoringProfileDevicebyDeviceCode(clientResult, "CLIENT", Models.DeviceCode.Client);
                // for CLIENT code, there are only one rmsMonitoringProfileDevices
                if (rmsMonitoringProfileDevices.Count > 0)
                    monitoringProfileDeviceId = rmsMonitoringProfileDevices[0].MonitoringProfileDeviceId;

                if (Convert.ToBoolean(ConfigurationManager.AppSettings["RMS.DebugLogEnable"] ?? "false"))
                {
                    string log = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " Call Centerlize - ClientResult " + Helper.Serializer.XML.SerializeObject(clientResult);
                    new RMSDebugLog(log, true);
                    log = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " Call Centerlize - Monitoring Profile " + Helper.Serializer.XML.SerializeObject(rmsMonitoringProfileDevices);
                    new RMSDebugLog(log, true);
                }


                #endregion

                MonitoringServiceClient mp = new Proxy.MonitoringService().monitoringService;

                #region 2. Send Alive Message

                try
                {
                    var rawMessage = new RmsReportMonitoringRaw();
                    rawMessage.ClientCode = clientResult.Client.ClientCode;
                    rawMessage.DeviceCode = "CLIENT";

                    rawMessage.Message = "OK";
                    rawMessage.MessageDateTime = DateTime.Now;
                    rawMessage.MonitoringProfileDeviceId = monitoringProfileDeviceId;

                    mp.AddMessage(rawMessage);

                    if (Convert.ToBoolean(ConfigurationManager.AppSettings["RMS.DebugLogEnable"] ?? "false"))
                    {
                        string log = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " Send Alive Message " +  Helper.Serializer.XML.SerializeObject(rawMessage);
                        new RMSDebugLog(log, true);
                    }

                }
                catch (Exception ex)
                {
                    throw new RMSAppException(this, "0500", "Send Alive Message failed. " + ex.Message, ex, false);
                }
                #endregion


                #region 3. Check Maintenance State

                try
                {
                    string maFilePath = ConfigurationManager.AppSettings["RMS.MA_FILE_PATH"];

                    // MA State?
                    if (File.Exists(maFilePath))
                    {
                        if (clientResult.Client.State == (int) ClientState.Normal)
                        {
                            var client = new ClientServiceClient();
                            client.SetClientState(clientResult.Client.ClientId, ClientState.Maintenance);
                        }
                        return;
                    }
                        // Normal State
                    else
                    {
                        if (clientResult.Client.State == (int)ClientState.Maintenance)
                        {
                            var client = new ClientServiceClient();
                            client.SetClientState(clientResult.Client.ClientId, ClientState.Normal);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new RMSAppException(this, "0500", "Check Maintenance State failed. " + ex.Message, ex, false);
                }

                #endregion

                #region 4. Check Device Monitoring

                try
                {
                    var monitoringService = new RMS.Monitoring.Core.MonitoringService();

                    // Performance
                    var monitoringRaws = monitoringService.Monitoring("performance", clientResult);

                    // Device
                    monitoringRaws.AddRange(monitoringService.Monitoring("device", clientResult));

                    if (monitoringRaws.Count > 0)
                    {
                        if (Convert.ToBoolean(ConfigurationManager.AppSettings["RMS.DebugLogEnable"] ?? "false"))
                        {
                            string log = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " Check Device Monitoring " + Helper.Serializer.XML.SerializeObject(monitoringRaws);
                            new RMSDebugLog(log, true);
                        }

                        mp.AddMessages(monitoringRaws);
                    }
                }
                catch (Exception ex)
                {
                    throw new RMSAppException(this, "0500", "Check Device Monitoring failed. " + ex.Message, ex, false);
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "ExecuteCommand failed. " + ex.Message, ex, true);
            }
        }


        public void SetMonitoringState(string clientCode, ClientState clientState)
        {
            try
            {
                var cs = new ClientService().clientService;
                var clientResult = cs.GetClient(GetClientBy.ClientCode, null, clientCode, null, false, false);

                if (clientResult.Client.State == (int) ClientState.Normal && clientState == ClientState.Maintenance)
                {
                    cs.SetClientState(clientResult.Client.ClientId, ClientState.Maintenance);
                }
                // Normal State
                else if (clientResult.Client.State == (int) ClientState.Maintenance && clientState == ClientState.Normal)
                {
                    cs.SetClientState(clientResult.Client.ClientId, ClientState.Normal);
                }
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "SetMonitoringState failed. ClientCode="+ clientCode + "   " + ex.Message, ex, true);
            }

        }

        private void UpdateClientCode(string clientCode)
        {
            try
            {
                if (ConfigurationManager.AppSettings["CLIENT_CODE"] != clientCode)
                {
                    Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    configuration.AppSettings.Settings["CLIENT_CODE"].Value = clientCode;
                    configuration.Save();

                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "UpdateClientCode failed. ClientCode=" + clientCode + "   " + ex.Message, ex, true);
            }
        }

    }
}
