using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Core.Monitoring.Device.WebCamera
{
    public class Lenovo : WebCamera
    {
        public Lenovo(string model, string deviceName, string deviceID)
            : base("Lenovo", model, deviceName, deviceID)
        {
        }

    }
}
