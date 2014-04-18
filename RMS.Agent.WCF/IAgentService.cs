using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RMS.Agent.Entity;
using RMS.Agent.WCF.Model;

namespace RMS.Agent.WCF
{
    [ServiceContract]
    public interface IAgentService
    {

        [OperationContract]
        void AutoUpdate(string type);

        [OperationContract]
        void RemoteCommand(RemoteCommand remoteCommand);

        [OperationContract]
        Result Monitoring(string clientCode);
    }

}
