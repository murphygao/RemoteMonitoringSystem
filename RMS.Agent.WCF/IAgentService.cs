using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RMS.Agent.Entity;

namespace RMS.Agent.WCF
{
    [ServiceContract]
    public interface IAgentService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        void AutoUpdate(string type);

        [OperationContract]
        void RemoteCommand(RemoteCommand remoteCommand);

        [OperationContract]
        void Monitoring();
    }

}
