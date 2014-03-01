using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Core.Monitoring.Device.WebCamera
{
    public class LG : WebCamera
    {
        public LG(string model, string deviceName, string deviceID)
            : base("LG", model, deviceName, deviceID)
        {
        }

        public override int CheckDeviceManager()
        {
            return base.CheckDeviceManager();
        }
    }
}
