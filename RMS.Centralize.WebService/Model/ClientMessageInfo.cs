using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.Centralize.WebService.Model
{
    public class ClientMessageInfo
    {
        /*
         * 
    SELECT c.[ClientID]
	, c.[ClientCode]
	, c.[LocationID]
	, k.[IPAddress]
	, m.[Message]
    , m.[MessageID]
	, mg.[MessageGroupID]
	, mg.[MessageGroupName]
	, m.[SeverityLevelID]
         * 
        */

        public int? ClientID { get; set; }
        public string ClientCode { get; set; }
        public int? LocationID { get; set; }
        public string IPAddress { get; set; }
        public string Message { get; set; }
        public int? MessageID { get; set; }
        public int? MessageGroupID { get; set; }
        public string MessageGroupName { get; set; }
        public int? SeverityLevelID { get; set; }

    }
}