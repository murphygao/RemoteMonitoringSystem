using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Centralize.DAL;

namespace RMS.Centralize.BSL.MonitoringEngine.Model
{
    public class MonitoringProfileDeviceInfo : RmsMonitoringProfileDevice
    {
        public string DeviceCode { get; set; }
        public string DeviceTypeCode { get; set; }
    }
}
