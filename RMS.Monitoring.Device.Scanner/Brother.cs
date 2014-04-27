using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Monitoring.Device.Scanner
{
    public class Brother : Scanner
    {
        public Brother(string model, string deviceManagerName, string deviceManagerID)
            : base("Brother", model, deviceManagerName, deviceManagerID)
        {
        }

        public override int CheckDeviceManager()
        {
            return base.CheckDeviceManager();
        }
    }
}
