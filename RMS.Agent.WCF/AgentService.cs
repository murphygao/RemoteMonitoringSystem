using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RMS.Agent.BSL.AutoUpate;
using RMS.Agent.BSL.Monitoring;
using RMS.Agent.BSL.Monitoring.Models;
using RMS.Agent.BSL.RemoteCommand;
using RMS.Agent.Entity;
using RMS.Agent.Helper;
using RMS.Agent.Model;
using RMS.Agent.WCF.Model;
using RMS.Common.Exception;
using EventLog = RMS.Agent.Model.EventLog;

namespace RMS.Agent.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AgentService" in both code and config file together.
    public class AgentService : IAgentService
    {
        private BSL.AutoUpate.AutoUpdateService autoUpdateService;
        private BSL.RemoteCommand.RemoteCommandService commandService;
        private BSL.Monitoring.MonitoringService monitoringService;
        private string tempEventFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\logs\TempEvent.txt";
        public static object _lock = new object();

        public void TestConnection()
        {
            try
            {
                AddLog("Testing", "Testing Agent Service", "");
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "TestConnection failed. " + ex.Message, ex, true);
            }
        }

        public void AutoUpdate(string type)
        {
            try
            {
                AddLog("Auto Update", "Update Agent", "");
                autoUpdateService = new AutoUpdateService();
                autoUpdateService.Command(type);
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "AutoUpdate failed. " + ex.Message, ex, true);
            }
        }

        public void RemoteCommand(RemoteCommand remoteCommand)
        {
            try
            {
                commandService = new RemoteCommandService();
                commandService.Command(remoteCommand);
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "RemoteCommand failed. " + ex.Message, ex, true);
            }
        }

        public Result Monitoring(string clientCode)
        {
            try
            {
                AddLog("Monitoring", "Monitoring Performances & Peripherals", "Client Code: " + clientCode);

                monitoringService = new MonitoringService();
                monitoringService.Command(clientCode);

                return new Result {IsSuccess = true};
            }
            catch (Exception ex)
            {
                AddLog("Monitoring", "Monitoring Failed", ex.Message + (ex.InnerException == null? string.Empty : " " + ex.InnerException.Message));
                throw new RMSAppException(this, "0500", "Monitoring failed. " + ex.Message, ex, true);
            }
        }

        public List<DeviceStatus> SelfMonitoring(string clientCode)
        {
            try
            {
                AddLog("Self Monitoring", "Monitoring Performances & Peripherals", (string.IsNullOrEmpty(clientCode)? string.Empty : "Client Code: " + clientCode));

                monitoringService = new MonitoringService();
                return monitoringService.SelfMonitoring(clientCode);

            }
            catch (Exception ex)
            {
                AddLog("Self Monitoring", "SelfMonitoring Failed", ex.Message + (ex.InnerException == null ? string.Empty : " " + ex.InnerException.Message));
                throw new RMSAppException(this, "0500", "SelfMonitoring failed. " + ex.Message, ex, true);
            }
        }


        private void AddLog(string eventType, string message, string detail)
        {
            try
            {
                lock (_lock)
                {
                    ListEventLogs logs = new ListEventLogs();

                    string strResultList = string.Empty;

                    if (!Directory.Exists(System.IO.Path.GetDirectoryName(tempEventFile)))
                        Directory.CreateDirectory(System.IO.Path.GetDirectoryName(tempEventFile));

                    if (File.Exists(tempEventFile))
                    {

                        using (FileStream fs = File.OpenRead(tempEventFile))
                        {
                            using (TextReader tr = new StreamReader(fs))
                            {
                                strResultList = tr.ReadToEnd();
                                tr.Close();
                            }
                            fs.Close();

                        }
                        if (!string.IsNullOrEmpty(strResultList))
                        {
                            logs = Serializer.XML.DeserializeObject<ListEventLogs>(strResultList);
                        }

                    }

                    logs.Add(new EventLog {EventDateTime = DateTime.Now, EventType = eventType, Message = message, Detail = detail});
                    strResultList = Serializer.XML.SerializeObject(logs);
                    using (TextWriter tw = new StreamWriter(tempEventFile, false)) // Create & open the file
                    {
                        tw.Write(strResultList);
                        tw.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //string eventType, string message, string detail
                throw new RMSAppException(this, "0500", "AddLog failed. eventType=" + eventType + ", message=" + message + ", detail=" + detail + ", " + ex.Message, ex, false);
            }
        }
    }
}
