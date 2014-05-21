using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Agent.Watchdog.BSL
{
    public class WatchdogService
    {
        public void Start()
        {
            if (CanStartAgentApp())
                Process.Start(ConfigurationManager.AppSettings["RMS.AGENT_FILE_PATH"]);

            if (CanStartOutOfServiceApp())
                Process.Start(ConfigurationManager.AppSettings["RMS.OUT_OF_SERVICE_FILE_PATH"]);
        }

        public void Stop()
        {
            
        }

        private bool CanStartAgentApp()
        {
            if (IsProcessRunning(ConfigurationManager.AppSettings["RMS.AGENT_PROCESS_NAME"])) return false;
            return true;
        }

        private bool IsProcessRunning(string sProcessName)
        {
            System.Diagnostics.Process[] proc = System.Diagnostics.Process.GetProcessesByName(sProcessName);
            if (proc.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanStartOutOfServiceApp()
        {
            if (IsProcessRunning(ConfigurationManager.AppSettings["RMS.BIZ_APP_PROCESS_NAME"])) return false;

            if (IsProcessRunning(ConfigurationManager.AppSettings["RMS.OUT_OF_SERVICE_PROCESS_NAME"])) return false;

            string maFilePath = ConfigurationManager.AppSettings["RMS.MA_FILE_PATH"];
            if (File.Exists(maFilePath))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
