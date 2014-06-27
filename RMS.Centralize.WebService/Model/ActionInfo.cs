using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;

namespace RMS.Centralize.WebService.Model
{
    public class ActionInfo
    {
        public string To { get; set; }
        public string ClientCode { get; set; }
        public string LevelCode { get; set; }
        public string Message { get; set; }
        public string MessageRemark { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string MessageGroupName { get; set; }
        public DateTime? MessageDateTime { get; set; }
        public string DeviceCode { get; set; }
        public string DeviceDescription { get; set; }
        public long SummaryMonitoringReportID { get; set; }

        /// <summary>
        /// Error Message,
        /// Solved Message
        /// </summary>
        public MessageType MessageType { get; set; }


        public string ToPlainTextHeader()
        {
            string ret = string.Empty;

            ret += "ClientCode | ";
            ret += "DeviceCode | ";
            ret += "DeviceDescription | ";
            ret += "Message | ";
            ret += "MessageGroupName | ";
            ret += "MessageRemark | ";
            ret += "LocationCode | ";
            ret += "LocationName | ";
            ret += "MessageDateTime";

            return ret;
        }
        public string ToPlainText()
        {
            string ret = string.Empty;

            ret += ClientCode + " | ";
            ret += DeviceCode + " | ";
            ret += DeviceDescription + " | ";
            ret += Message + " | ";
            ret += MessageGroupName + " | ";
            ret += MessageRemark + " | ";
            ret += LocationCode + " | ";
            ret += LocationName + " | ";
            if (MessageDateTime != null) ret += MessageDateTime.Value.ToString("dd/MM/yyyy HH:mm:ss");
            else ret += "N/A";

            return ret;
        }

        public string ToSMSText()
        {

            return "[" + (string.IsNullOrEmpty(DeviceDescription) ? DeviceCode : DeviceDescription) + ", " + Message + "]";


        }
    }

    public enum MessageType
    {
        ErrorMessage,
        SolvedMessage
    }

}