using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Monitoring.Device.SignaturePad
{
    public class Signotec : SignaturePad
    {
        public Signotec(string model, string deviceManagerName, string deviceManagerID)
            : base("Signotec", model, deviceManagerName, deviceManagerID)
        {
        }

        public override int CheckDeviceManager()
        {
            return base.CheckDeviceManager();
        }
    }
}
