using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Monitoring.Device.IDCardScanner
{
    public class Syscantech : IDCardScanner
    {
        public Syscantech(string model, string deviceManagerName, string deviceManagerID)
            : base("Syscantech", model, deviceManagerName, deviceManagerID)
        {
        }

        public override int CheckDeviceManager()
        {
            return base.CheckDeviceManager();
        }
    }
}
