using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Monitoring.Device.UPS
{
    public class PowerMatic : UPS
    {
        public PowerMatic(string model, string deviceManagerName, string deviceManagerID)
            : base("PowerMatic", model, deviceManagerName, deviceManagerID)
        {
        }

        public override int CheckDeviceManager()
        {
            return base.CheckDeviceManager();
        }
    }
}
