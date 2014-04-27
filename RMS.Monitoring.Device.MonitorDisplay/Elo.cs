using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Monitoring.Device.MonitorDisplay
{
    public class Elo : MonitorDisplay
    {
        public Elo(string model, string deviceManagerName, string deviceManagerID)
            : base("Elo", model, deviceManagerName, deviceManagerID)
        {
        }

        public override int CheckDeviceManager()
        {
            return base.CheckDeviceManager();
        }
    }
}
