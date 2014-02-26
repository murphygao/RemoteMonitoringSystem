using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Agent.Entity
{
    public class AutoUpdate
    {
        [DataMember]
        public AutoUpdateType AutoUpdateType;

    }

    public enum AutoUpdateType
    {
        AgentAll,
        AgentDLL,
        Others
    }
}
