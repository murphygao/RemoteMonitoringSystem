using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Core.Monitoring.Device.WebCamera
{
    public class WebCameraService
    {
        private WebCamera wc;

        public WebCameraService(string brand, string model, string deviceName, string deviceID)
        {
            if (brand.ToLower() == "lenovo") wc = new Lenovo(model, deviceName, deviceID);
            else if (brand.ToLower() == "lg") wc = new LG(model, deviceName, deviceID);
        }

        public int Monitoring()
        {
            return wc.CheckDeviceManager();
        }
    }
}
