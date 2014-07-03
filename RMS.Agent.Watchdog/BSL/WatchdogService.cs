using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Common.Exception;

namespace RMS.Agent.Watchdog.BSL
{
    public class WatchdogService
    {
        public void Start()
        {
            try
            {
                if (CanStartAgentApp())
                    Process.Start(ConfigurationManager.AppSettings["RMS.AGENT_FILE_PATH"]);

                if (CanStartOutOfServiceApp())
                    Process.Start(ConfigurationManager.AppSettings["RMS.OUT_OF_SERVICE_FILE_PATH"]);
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "Start failed. " + ex.Message, ex, false);
            }
        }

        public void Stop()
        {
            
        }

        private bool CanStartAgentApp()
        {
            try
            {
                if (IsProcessRunning(ConfigurationManager.AppSettings["RMS.AUTO_UPDATE_PROCESS_NAME"])) return false;

                System.Threading.Thread.Sleep(500);

                if (IsProcessRunning(ConfigurationManager.AppSettings["RMS.AGENT_PROCESS_NAME"])) return false;

                System.Threading.Thread.Sleep(500);

                if (IsProcessRunning(ConfigurationManager.AppSettings["RMS.AUTO_UPDATE_PROCESS_NAME"])) return false;

                return true;
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "CanStartAgentApp failed. " + ex.Message, ex, false);
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

        private bool CanStartOutOfServiceApp()
        {
            try
            {
                if (IsProcessRunning(ConfigurationManager.AppSettings["RMS.BIZ_APP_PROCESS_NAME"])) return false;

                if (IsProcessRunning(ConfigurationManager.AppSettings["RMS.OUT_OF_SERVICE_PROCESS_NAME"])) return false;

                if (IsProcessRunning(ConfigurationManager.AppSettings["RMS.AUTO_UPDATE_PROCESS_NAME"])) return false;

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
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "CanStartOutOfServiceApp failed. " + ex.Message, ex, false);
            }
        }

    }
}
