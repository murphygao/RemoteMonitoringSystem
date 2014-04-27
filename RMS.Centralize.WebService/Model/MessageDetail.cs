using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.Centralize.WebService.Model
{
    public class MessageDetail
    {
        //dm.DeviceID, m.MessageID, m.Message, mg.MessageGroupID, mg.MessageGroupCode, mg.MessageGroupName

        public int DeviceID { get; set; }
        public string DeviceDescription { get; set; }
        public int MessageID { get; set; }
        public string Message { get; set; } // MessageGroupName
        public int MessageGroupID { get; set; }
        public string MessageGroupCode { get; set; } // Message
        public string MessageGroupName { get; set; } // Message

    }
}