using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Web;

namespace RMS.Centralize.Website.Areas.Monitoring.Models
{
    public class DeviceStatus
    {
        public long ReportID { get; set; }
        public int DeviceTypeID { get; set; }
        public string DeviceType { get; set; }
        public int? DisplayOrder { get; set; }
        public string DeviceDescription { get; set; }
        public int? status { get; set; }
        public int? MessageID { get; set; }
        public string Message { get; set; }
        public string MessageRemark { get; set; }
        public int? MonitoringProfileDeviceID { get; set; }
        public DateTime? LastActionDateTime { get; set; }
        public string LastActionType { get; set; }
        public string LevelName { get; set; }
        public string ColorCode { get; set; }
        public string ColorTagStart { get; set; } 
        public string ColorTagEnd { get; set; }

        public DateTime? MessageDateTime { get; set; }

        public DeviceStatus()
        {
            
        }

        public DeviceStatus(long reportId, int deviceTypeId, string deviceType, int? displayOrder, string deviceDescription, int? status, int? messageId, string message, string messageRemark, int? monitoringProfileDeviceId, DateTime? lastActionDateTime, string lastActionType, string levelName, string colorCode, string colorTagStart, string colorTagEnd, DateTime? messageDateTime)
        {
            ReportID = reportId;
            DeviceTypeID = deviceTypeId;
            DeviceType = deviceType;
            DisplayOrder = displayOrder;
            DeviceDescription = deviceDescription;
            this.status = status;
            MessageID = messageId;
            Message = message;
            MessageRemark = messageRemark;
            MonitoringProfileDeviceID = monitoringProfileDeviceId;
            LastActionDateTime = lastActionDateTime;
            LastActionType = lastActionType;
            LevelName = levelName;
            ColorCode = colorCode;
            ColorTagStart = colorTagStart;
            ColorTagEnd = colorTagEnd;
            MessageDateTime = messageDateTime;
        }
    }
}