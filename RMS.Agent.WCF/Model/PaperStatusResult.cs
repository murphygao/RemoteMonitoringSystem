using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Agent.WCF.Model
{
    [DataContract]
    public class PaperStatusResult : Result
    {
        public string[] ListStatus;
    }
}
