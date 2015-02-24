using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.Centralize.WebService.Model
{
    public class RMSAttachment
    {
        public string FileName;
        public byte[] FileBytes;
        public string Remark;

        public int? TypeID;
        public int? MonitoringProfileDeviceId;
        public string Message;
        public string DeviceCode;

        public string TempFileName;
        public string TempFullPath;
    }
}