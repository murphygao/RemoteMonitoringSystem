using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.Centralize.WebService.Model
{
    public class SummaryStatusAllClientsInfo
    {
        public int ClientID { get; set; }
        public string ClientCode { get; set; }
        public string IPAddress { get; set; }
        public int LocationID { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public int CounterOK { get; set; }
        public int CounterNotOK { get; set; }
        public int CounterBizMessage { get; set; }
        public int AgentNotAlive { get; set; }
        public int iRMSStatus { get; set; }
        public string sRMSStatus { get; set; }
        public int ClientTypeID { get; set; }
        public bool HasMonitoringAgent { get; set; }
        public DateTime? OldestErrorMessageDateTime { get; set; }
    }
}