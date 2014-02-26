using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RMS.Agent.Entity;

namespace RMS.Agent.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AgentService" in both code and config file together.
    public class AgentService : IAgentService
    {
        private BSL.AutoUpate.AutoUpdateService auoUpdateService;
        private BSL.RemoteCommand.RemoteCommandService commandService;

        public void DoWork()
        {
        }

        public void AutoUpdate(string type)
        {
            auoUpdateService = new BSL.AutoUpate.AutoUpdateService();
            auoUpdateService.Command(type);

        }

        public void RemoteCommand(RemoteCommand remoteCommand)
        {
            commandService = new BSL.RemoteCommand.RemoteCommandService();
            commandService.Command(remoteCommand);
        }

        public void Monitoring()
        {
            throw new NotImplementedException();
        }
    }
}
