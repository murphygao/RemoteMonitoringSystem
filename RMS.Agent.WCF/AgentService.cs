using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RMS.Agent.BSL.Monitoring;
using RMS.Agent.Entity;
using RMS.Agent.Helper;
using RMS.Agent.Model;
using RMS.Agent.WCF.Model;
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

        public void AutoUpdate(string type)
        {
            AddLog("Auto Update", "Update Agent", "");
            autoUpdateService = new BSL.AutoUpate.AutoUpdateService();
            autoUpdateService.Command(type);

        }

        public void RemoteCommand(RemoteCommand remoteCommand)
        {
            commandService = new BSL.RemoteCommand.RemoteCommandService();
            commandService.Command(remoteCommand);
        }

        public Result Monitoring(string clientCode)
        {
            AddLog("Monitoring", "Monitoring Performances & Peripherals", "Client Code: " + clientCode);

            monitoringService = new MonitoringService();
            monitoringService.Command(clientCode);

            return new Result {IsSuccess = true};
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
            catch (Exception)
            {

                throw;
            }
        }
    }
}
