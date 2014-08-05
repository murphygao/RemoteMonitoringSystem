using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Agent.BSL.Monitoring.Models
{
    [DataContract]
    public class DeviceStatus
    {
        [DataMember]
        public int DeviceTypeID { get; set; }
        [DataMember]
        public string DeviceType { get; set; }
        [DataMember]
        public int? DisplayOrder { get; set; }
        [DataMember]
        public string DeviceDescription { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string MessageRemark { get; set; }
        [DataMember]
        public int? MonitoringProfileDeviceID { get; set; }
        [DataMember]
        public DateTime? MessageDateTime { get; set; }

        public DeviceStatus()
        {

        }

        public DeviceStatus(int deviceTypeId, string deviceType, int? displayOrder, string deviceDescription, string message, string messageRemark, int? monitoringProfileDeviceId, DateTime? messageDateTime)
        {
            DeviceTypeID = deviceTypeId;
            DeviceType = deviceType;
            DisplayOrder = displayOrder;
            DeviceDescription = deviceDescription;
            Message = message;
            MessageRemark = messageRemark;
            MonitoringProfileDeviceID = monitoringProfileDeviceId;
            MessageDateTime = messageDateTime;
        }
    }
}
