using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Monitoring.Device.Keyboard
{
    public class KeyTechnology : Keyboard
    {
        public KeyTechnology(string model, string deviceManagerName, string deviceManagerID)
            : base("KeyTechnology", model, deviceManagerName, deviceManagerID)
        {
        }

        public override int CheckDeviceManager()
        {
            return base.CheckDeviceManager();
        }
    }
}
