using System.Runtime.Serialization;

namespace RMS.Agent.Entity
{
    [DataContract]
    public class Monitoring
    {

        [DataMember]
        public string ClientCode { get; set; }


    }
}
