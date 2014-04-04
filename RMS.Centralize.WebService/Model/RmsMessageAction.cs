using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.Centralize.WebService.Model
{
    public partial class RmsMessageAction
    {

        public int MessageId { get; set; } // MessageID (Primary key)
        public string MessageGroupName { get; set; } // MessageGroupName
        public string Message { get; set; } // Message
        public bool? ReadOnly { get; set; } // ReadOnly
        public string LevelName { get; set; } // SeverityLevel.LevelName
        public bool? ActiveStatus { get; set; } // ActiveStatus
        public string ActionProfileName { get; set; }
        public string MessageGroupCode { get; set; }
        public string ColorTagStart { get; set; }
        public string ColorTagEnd { get; set; }
        public long? RowNum { get; set; } // RowNum

        public RmsMessageAction()
        {

        }
    }
}