using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Agent.WCF.Model
{
    [DataContract]
    public class Result
    {
        [DataMember]
        public bool IsSuccess { get; set; }

        [DataMember]
        public string ErrorCode { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }

    }
}
