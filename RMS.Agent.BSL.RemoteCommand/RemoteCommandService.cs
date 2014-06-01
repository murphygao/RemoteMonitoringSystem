using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RMS.Common.Exception;

namespace RMS.Agent.BSL.RemoteCommand
{
    public class RemoteCommandService
    {
        public void Command(Entity.RemoteCommand remoteCommand)
        {
            ExecuteCommand(remoteCommand);
        }

        private string ExecuteCommand(Entity.RemoteCommand remoteCommand)
        {
            try
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.RedirectStandardOutput = true;
                startInfo.UseShellExecute = false; 
                startInfo.CreateNoWindow = true;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C " + remoteCommand.CommandLine;
                process.StartInfo = startInfo;
                process.Start();
                return process.StandardOutput.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "ExecuteCommand failed. " + ex.Message, ex, false);
            }
        }
    }
}
