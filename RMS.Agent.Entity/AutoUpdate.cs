using System.Runtime.Serialization;

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
