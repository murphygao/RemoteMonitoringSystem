using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RMS.Centralize.WebService.Model
{
    
    [DataContract]
    public class MainAppClient
    {
        [DataMember]
        public int ClientID { get; set; }
        [DataMember]
        public string ClientCode { get; set; }
        [DataMember]
        public string ClientName { get; set; }
    }
}