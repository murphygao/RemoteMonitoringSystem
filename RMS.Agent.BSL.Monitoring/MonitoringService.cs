using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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
             * 2. Check Application Running
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

                var rmsMonitoringProfileDevicebyDeviceCode = RMS.Monitoring.Helper.Common.GetRmsMonitoringProfileDevicebyDeviceCode(clientResult, "CLIENT", Models.DeviceCode.Client);
                var rmsMonitoringProfileDevices = rmsMonitoringProfileDevicebyDeviceCode;
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

                List<RmsReportMonitoringRaw> monitoringRaws = new List<RmsReportMonitoringRaw>();
                MonitoringServiceClient mp = new Proxy.MonitoringService().monitoringService;

                #region 2. Check Application Running

                bool checkedAgent = false;
                foreach (var monitoringClient in rmsMonitoringProfileDevices)
                {
                    List<string> appNameList = new List<string>(monitoringClient.StringValue.Split('|'));

                    // ถ้า device string ไม่มีค่า หรือมีค่าเท่ากับ agent process name แสดงว่า ให้ตรวจสอบ agent ว่ายังทำงานอยู่หรือไม่
                    if (!checkedAgent && appNameList.Any(s => string.IsNullOrEmpty(s) || s == ConfigurationManager.AppSettings["RMS.AGENT_PROCESS_NAME"].ToLower().Trim()))
                    {
                        checkedAgent = true;
                        // การที่สามารถทำ process ต่างๆ ได้อยู่ในนี้ แสดงว่า agent ทำงาได้ปกติ
                        // ให้ทำการส่ง message OK ได้ทันที
                        try
                        {
                            var rawMessage = new RmsReportMonitoringRaw();
                            rawMessage.ClientCode = clientResult.Client.ClientCode;
                            rawMessage.DeviceCode = "CLIENT";

                            rawMessage.Message = "OK";
                            rawMessage.MessageDateTime = DateTime.Now;
                            rawMessage.MonitoringProfileDeviceId = monitoringClient.MonitoringProfileDeviceId;

                            monitoringRaws.Add(rawMessage);
                            //mp.AddMessage(rawMessage);
                        }
                        catch (Exception ex)
                        {
                            new RMSAppException(this, "0500", "Send Alive Message failed. " + ex.Message, ex, true);
                        }

                    }
                    else // หากเข้า case นี้แสดงว่า ระบบอยากให้ตรวจสอบการทำงานของ application ภายนอก
                    {
                        try
                        {
                            if (appNameList.Any(s => IsProcessRunning(s.Trim())))
                            {
                                var rawMessage = new RmsReportMonitoringRaw();
                                rawMessage.ClientCode = clientResult.Client.ClientCode;
                                rawMessage.DeviceCode = "CLIENT";

                                rawMessage.Message = "OK";
                                rawMessage.MessageDateTime = DateTime.Now;
                                rawMessage.MonitoringProfileDeviceId = monitoringClient.MonitoringProfileDeviceId;

                                monitoringRaws.Add(rawMessage);
                            }
                            else
                            {
                                var rawMessage = new RmsReportMonitoringRaw();
                                rawMessage.ClientCode = clientResult.Client.ClientCode;
                                rawMessage.DeviceCode = "CLIENT";

                                rawMessage.Message = "APPLICATION_NOT_RUNNING";
                                rawMessage.MessageDateTime = DateTime.Now;
                                rawMessage.MonitoringProfileDeviceId = monitoringClient.MonitoringProfileDeviceId;

                                monitoringRaws.Add(rawMessage);

                            }
                        }
                        catch (Exception ex)
                        {
                            new RMSAppException(this, "0500", "Check application (" + monitoringClient.StringValue + ") running failed. " + ex.Message,
                                ex, true);
                        }
                    }
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
                            var client = new ClientService().clientService;
                            client.SetClientState(clientResult.Client.ClientId, ClientState.Maintenance);
                        }
                        return;
                    }
                        // Normal State
                    else
                    {
                        if (clientResult.Client.State == (int)ClientState.Maintenance)
                        {
                            var client = new ClientService().clientService;
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
                    monitoringRaws.AddRange(monitoringService.Monitoring("performance", clientResult));

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

                if (clientResult != null)
                {
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
                else
                {
                    new RMSAppException(this, "0500", "SetMonitoringState failed. ClientCode=" + clientCode + " not found.", true);
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
                // Initial Local Configuration
                Configuration config;

                ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
                configFile.ExeConfigFilename = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Local.config";
                config = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);

                if (config.AppSettings.Settings["RMS.CLIENT_CODE"].Value != clientCode)
                {
                    config.AppSettings.Settings["RMS.CLIENT_CODE"].Value = clientCode;
                    config.Save();
                }
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "UpdateClientCode failed. ClientCode=" + clientCode + "   " + ex.Message, ex, true);
            }
        }

        private bool IsProcessRunning(string sProcessName)
        {
            try
            {
                Process[] proc = Process.GetProcessesByName(sProcessName);
                if (proc.Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "IsProcessRunning failed. " + ex.Message, ex, false);
            }
        }
    }
}
