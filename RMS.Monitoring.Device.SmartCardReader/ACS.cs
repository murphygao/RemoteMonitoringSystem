using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Monitoring.Device.SmartCardReader
{
    public class ACS : SmartCardReader
    {
        public ACS(string model, string deviceManagerName, string deviceManagerID)
            : base("ACS", model, deviceManagerName, deviceManagerID)
        {
        }

        public override int CheckDeviceManager()
        {
            return base.CheckDeviceManager();
        }
    }
}
