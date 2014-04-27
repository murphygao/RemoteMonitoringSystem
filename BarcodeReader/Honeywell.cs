using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeReader
{
    public class Honeywell : BarcodeReader
    {
        public Honeywell(string model, string deviceManagerName, string deviceManagerID)
            : base("Honeywell", model, deviceManagerName, deviceManagerID)
        {
        }

        public override int CheckDeviceManager()
        {
            return base.CheckDeviceManager();
        }
    }
}
