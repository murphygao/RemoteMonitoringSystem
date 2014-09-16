using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Monitoring.Device.EncryptedPinPad
{
    public class KMY : EncryptedPinPad
    {
        public KMY(string model, string deviceManagerName, string deviceManagerID, bool useCOMPort, string comPort)
            : base("KMY", model, deviceManagerName, deviceManagerID, useCOMPort, comPort)
        {
        }

        public override int CheckDeviceManager()
        {
            return base.CheckDeviceManager();
        }
    }
}
