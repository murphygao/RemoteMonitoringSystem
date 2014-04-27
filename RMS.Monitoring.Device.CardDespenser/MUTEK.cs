using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Monitoring.Device.CardDespenser
{
    public class MUTEK : CardDispenser
    {
        public MUTEK(string model, string deviceManagerName, string deviceManagerID)
            : base("MUTEK", model, deviceManagerName, deviceManagerID)
        {
        }

        public override int CheckDeviceManager()
        {
            return base.CheckDeviceManager();
        }

        public override int CheckCardLevel()
        {
            return -1;
        }
    }
}
