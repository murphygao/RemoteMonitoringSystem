using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Agent.Proxy.ClientProxy;

namespace RMS.Monitoring.Helper
{
    public class Common
    {
        public static List<RmsMonitoringProfileDevice> GetRmsMonitoringProfileDevicebyDeviceCode(ClientResult clientResult, string deviceCode)
        {
            List<RmsMonitoringProfileDevice> ret = new List<RmsMonitoringProfileDevice>();
            var device = clientResult.ListDevices.FirstOrDefault(d => d.DeviceCode == deviceCode);
            if (device != null)
            {
                int? id = device.DeviceId;
                ret = clientResult.ListMonitoringProfileDevices.Where(p => p.DeviceId == id).ToList();
            }
            return ret;
        }
    }
}
