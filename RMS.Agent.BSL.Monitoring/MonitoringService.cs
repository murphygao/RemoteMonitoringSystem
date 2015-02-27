using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using RMS.Agent.BSL.Monitoring.Models;
using RMS.Agent.BSL.Monitoring.Models;
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
        private delegate bool SetMonitoringStateAsync(string clientCode, ClientState clientState);

        private static object _lockClientFile = new object();

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

                string localStorage = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\LocalStorage";
                localStorage = (ConfigurationManager.AppSettings["RMS.LocalStorage"] ?? localStorage);

                try
                {
                    string localClientFile = localStorage + @"\clientResult.xml";

                    var directoryName = Path.GetDirectoryName(localClientFile);
                    if (directoryName != null && !Directory.Exists(directoryName))
                        Directory.CreateDirectory(directoryName);

                    string strResultList = Serializer.XML.SerializeObject(clientResult);
                    lock (_lockClientFile)
                    {
                        using (TextWriter tw = new StreamWriter(localClientFile, false)) // Create & open the file
                        {
                            tw.Write(strResultList);
                            tw.Close();
                        }
                        
                    }
                }
                catch (Exception myEx2)
                {
                    new RMSAppException(this, "0500", "SelfMonitoring failed. " + myEx2.Message, myEx2, true);
                }


                int? deviceId = null;
                int? monitoringProfileDeviceId = null;

                var rmsMonitoringProfileDevicebyDeviceCode = RMS.Monitoring.Helper.Common.GetRmsMonitoringProfileDevicebyDeviceCode(clientResult, "CLIENT", RMS.Monitoring.Helper.Models.DeviceCode.Client);
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

                #region 2. Check Maintenance State

                bool MAMode = false;

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
                        MAMode = true;
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

                #region 3. Check Application Running

                bool checkedAgent = false;
                foreach (var monitoringClient in rmsMonitoringProfileDevices)
                {
                    if (monitoringClient.StringValue == null) monitoringClient.StringValue = string.Empty;

                    List<string> appNameList = new List<string>(monitoringClient.StringValue.Split('|'));

                    // ถ้า device string ไม่มีค่า หรือมีค่าเท่ากับ agent process name แสดงว่า ให้ตรวจสอบ agent ว่ายังทำงานอยู่หรือไม่
                    if (!checkedAgent && appNameList.Any(s => string.IsNullOrEmpty(s) || s.ToLower().Trim() == ConfigurationManager.AppSettings["RMS.AGENT_PROCESS_NAME"].ToLower().Trim()))
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
                    else if (!MAMode) // หากเข้า case นี้แสดงว่า ระบบอยากให้ตรวจสอบการทำงานของ application ภายนอก และไม่ต้องอยู่ใน MA Mode
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

                if (MAMode) return; //ถ้าอยู่ใน MA Mode ให้หยุดการ Monitoring

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

                        List<RMSAttachment> lRMSAttachment = new List<RMSAttachment>();
                        if (monitoringRaws.Exists(w => w.DeviceCode == "CLIENT" && w.Message == "APPLICATION_NOT_RUNNING"))
                        {
                            if (Convert.ToBoolean(ConfigurationManager.AppSettings["RMS.EnableAttachEventLog"] ?? "false"))
                            {
                                try
                                {
                                    var query = "*[System[TimeCreated[@SystemTime >= '" + DateTime.Now.AddHours(-1).ToUniversalTime().ToString("o") +
                                                "']]]";
                                    string eventLogFileName = "EventLog.evtx";

                                    if (Convert.ToBoolean(ConfigurationManager.AppSettings["RMS.EventLog.NeedImpersonate"] ?? "false"))
                                    {
                                        var userName = ConfigurationManager.AppSettings["RMS.EventLog.UserName"];
                                        var password = ConfigurationManager.AppSettings["RMS.EventLog.Password"];
                                        Helper.Common.ExtractLog(query, localStorage, eventLogFileName, clientCode, clientCode, userName, password);
                                    }
                                    else
                                    {
                                        Helper.Common.ExtractLog(query, localStorage, eventLogFileName);
                                    }
                                    byte[] byteEventLog = File.ReadAllBytes(localStorage + @"\" + eventLogFileName);

                                    RMSAttachment rmsAttachment = new RMSAttachment();
                                    rmsAttachment.FileBytes = byteEventLog;
                                    rmsAttachment.Message = "APPLICATION_NOT_RUNNING";
                                    rmsAttachment.DeviceCode = "CLIENT";
                                    rmsAttachment.FileName = eventLogFileName;
                                    lRMSAttachment.Add(rmsAttachment);
                                }
                                catch (Exception ex)
                                {
                                    new RMSAppException(this, "0500", "Check Device Monitoring - Prepare EventLog Attachfile failed. " + ex.Message, ex, true);
                                }
                            }

                            //MainAppTempLog
                            if (Convert.ToBoolean(ConfigurationManager.AppSettings["RMS.EnableAttachMainAppLog"] ?? "false"))
                            {
                                try
                                {
                                    string mainAppTempLog = ConfigurationManager.AppSettings["RMS.MainAppTempLog"];
                                    if (!string.IsNullOrEmpty(mainAppTempLog))
                                    {
                                        if (File.Exists(mainAppTempLog))
                                        {
                                            byte[] byteMainAppTempLog = File.ReadAllBytes(mainAppTempLog);
                                            RMSAttachment rmsAttachment = new RMSAttachment();
                                            rmsAttachment.FileBytes = byteMainAppTempLog;
                                            rmsAttachment.Message = "APPLICATION_NOT_RUNNING";
                                            rmsAttachment.DeviceCode = "CLIENT";
                                            rmsAttachment.FileName = Path.GetFileName(mainAppTempLog);
                                            lRMSAttachment.Add(rmsAttachment);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    new RMSAppException(this, "0500", "Check Device Monitoring - Prepare AppLog Attachfile failed. " + ex.Message, ex, true);
                                }
                            }
                        }

                        if (lRMSAttachment.Count == 0)
                        {
                            mp.AddMessages(monitoringRaws);
                        }
                        else
                        {
                            mp.AddMessagesWithAttachFiles(monitoringRaws, lRMSAttachment);
                        }

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

        public List<DeviceStatus> SelfMonitoring(string clientCode)
        {
            /*
             * 
             * 1. Call Centralize or Local to Get Client & Device Info for Monitoring 
             * 2. Check Device Monitoring
             * 
             */

            try
            {
                #region 1. Call Centralize to Get Client & Device Info for Monitoring
                string localStorage = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\LocalStorage";
                localStorage = (ConfigurationManager.AppSettings["RMS.LocalStorage"] ?? localStorage);
                string localClientFile = localStorage + @"\clientResult.xml";
                string xmlLocalClient = string.Empty;
                if (File.Exists(localClientFile))
                {
                    lock (_lockClientFile)
                    {
                        xmlLocalClient = File.ReadAllText(localClientFile);
                    }
                }

                ClientResult clientResult = null;

                try
                {
                    if (xmlLocalClient == string.Empty) throw new Exception("Local Client Data Not Found. Try to call server.");
                    clientResult = Helper.Serializer.XML.DeserializeObject<ClientResult>(xmlLocalClient);
                }
                catch (Exception myEx1)
                {
                    new RMSAppException(this, "0500", "SelfMonitoring failed. " + myEx1.Message, myEx1, true);

                    Configuration config;

                    ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
                    configFile.ExeConfigFilename = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Local.config";
                    config = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);

                    if (config.AppSettings.Settings["RMS.CLIENT_CODE"] != null)
                        clientCode = config.AppSettings.Settings["RMS.CLIENT_CODE"].Value;


                    ClientServiceClient cs = new ClientService().clientService;
                    clientResult = cs.GetClient(GetClientBy.ClientCode, null, clientCode, null, true, true);

                    try
                    {
                        var directoryName = Path.GetDirectoryName(localClientFile);
                        if (directoryName != null && !Directory.Exists(directoryName))
                            Directory.CreateDirectory(directoryName);

                        string strResultList = Serializer.XML.SerializeObject(clientResult);
                        lock (_lockClientFile)
                        {
                            using (TextWriter tw = new StreamWriter(localClientFile, false)) // Create & open the file
                            {
                                tw.Write(strResultList);
                                tw.Close();
                            }

                        }
                    }
                    catch (Exception myEx2)
                    {
                        new RMSAppException(this, "0500", "SelfMonitoring failed. " + myEx2.Message, myEx2, true);
                    }
                }

                if (clientResult == null) throw new Exception("Unable to restore clientResult object.");

                int? deviceId = null;
                int? monitoringProfileDeviceId = null;

                var rmsMonitoringProfileDevicebyDeviceCode = RMS.Monitoring.Helper.Common.GetRmsMonitoringProfileDevicebyDeviceCode(clientResult, "CLIENT", RMS.Monitoring.Helper.Models.DeviceCode.Client);
                var rmsMonitoringProfileDevices = rmsMonitoringProfileDevicebyDeviceCode;

                // for CLIENT code, there are only one rmsMonitoringProfileDevices
                if (rmsMonitoringProfileDevices.Count > 0)
                    monitoringProfileDeviceId = rmsMonitoringProfileDevices[0].MonitoringProfileDeviceId;

                #endregion

                List<RmsReportMonitoringRaw> monitoringRaws = new List<RmsReportMonitoringRaw>();

                #region 2. Check Device Monitoring

                try
                {
                    var monitoringService = new RMS.Monitoring.Core.MonitoringService();

                    // Performance
                    monitoringRaws.AddRange(monitoringService.Monitoring("performance", clientResult));

                    // Device
                    monitoringRaws.AddRange(monitoringService.Monitoring("device", clientResult));


                    //return monitoringRaws;
                }
                catch (Exception ex)
                {
                    throw new RMSAppException(this, "0500", "Check Local Device Monitoring failed. " + ex.Message, ex, false);
                }

                #endregion

                List<DeviceStatus> lDeviceStatuses = new List<DeviceStatus>();

                #region Reorder Process

                foreach (var raw in monitoringRaws)
                {
                    RmsMonitoringProfileDevice mpd =
                        clientResult.ListMonitoringProfileDevices.FirstOrDefault(a => a.MonitoringProfileDeviceId == raw.MonitoringProfileDeviceId);

                    if (mpd == null) continue; //Something wrong

                    RmsDevice rd = clientResult.ListDevices.FirstOrDefault(a => a.DeviceId == mpd.DeviceId);

                    if (rd == null) continue; //Something wrong

                    RmsDeviceType rdt = clientResult.ListDeviceType.FirstOrDefault(a => a.DeviceTypeId == rd.DeviceTypeId);

                    if (rdt == null) continue; //Something wrong

                    DeviceStatus ds = new DeviceStatus(rdt.DeviceTypeId, rdt.DeviceType, rdt.DisplayOrder, mpd.DeviceDescription, raw.Message,
                        raw.MessageRemark, raw.MonitoringProfileDeviceId, raw.MessageDateTime);
                    
                    lDeviceStatuses.Add(ds);

                }

                lDeviceStatuses = new List<DeviceStatus>(lDeviceStatuses.OrderBy(o => o.DisplayOrder).ThenBy(o => o.DeviceDescription).ThenBy(o => o.MessageDateTime));

                string deviceName = "";
                foreach (var deviceStatus in lDeviceStatuses)
                {
                    deviceStatus.MonitoringProfileDeviceID = null;

                    if (deviceStatus.DeviceDescription == deviceName)
                    {
                        deviceStatus.DeviceDescription = "";
                        continue;
                    }
                    deviceName = deviceStatus.DeviceDescription;
                }


                #endregion

                return lDeviceStatuses;
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "SelfMonitoring failed. " + ex.Message, ex, true);
            }
        }

        public bool SetMonitoringState(string clientCode, ClientState clientState)
        {
            try
            {
                var cs = new ClientService().clientService;
                var clientResult = cs.GetClient(GetClientBy.ClientCode, null, clientCode, null, false, false);

                if (clientResult == null || clientResult.Client == null)
                {
                    List<string> listLocalIPs = GetLocalIPAddress();
                    foreach (var localIP in listLocalIPs)
                    {
                        clientResult = cs.GetClient(GetClientBy.IPAddress, null, null, localIP, false, false);
                        if (clientResult != null && clientResult.Client != null)
                        {
                            UpdateClientCode(clientResult.Client.ClientCode);
                            break;
                        }
                    }
                }

                if (clientResult != null && clientResult.Client != null)
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
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "SetMonitoringState failed. ClientCode="+ clientCode + "   " + ex.Message, ex, true);
                return false;
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

        private List<string> GetLocalIPAddress()
        {
            IPHostEntry host;
            List<string> listLocalIPs = new List<string>();
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    listLocalIPs.Add(ip.ToString());
                }
            }
            return listLocalIPs;
        }
    }
}
