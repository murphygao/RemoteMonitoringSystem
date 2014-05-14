using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.Centralize.WebService.Model
{
    public class ClientInfo
    {

        public int? ClientID { get; set; }
        public string ClientCode { get; set; }
        public string ClientType { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string LocationCode2 { get; set; }
        public string LocationName2 { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string IPAddress { get; set; }
        public string ProfileName { get; set; }
        public int? State { get; set; }
    }
}