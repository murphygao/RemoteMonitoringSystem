using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Monitoring.Device.ThermalPrinter
{
    public class Customs : ThermalPrinter
    {
        public Customs(string model, string deviceManagerName, string deviceManagerID)
            : base("Customs", model, deviceManagerName, deviceManagerID)
        {
        }

        public override int CheckDeviceManager()
        {
            return base.CheckDeviceManager();
        }
    }
}
