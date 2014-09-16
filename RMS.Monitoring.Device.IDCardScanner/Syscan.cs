using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Monitoring.Device.IDCardScanner
{
    public class Syscan : IDCardScanner
    {
        public Syscan(string model, string deviceManagerName, string deviceManagerID)
            : base("Syscan", model, deviceManagerName, deviceManagerID)
        {
        }

        public override int CheckDeviceManager()
        {
            return base.CheckDeviceManager();
        }
    }
}
