using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using RMS.Core.Monitoring.DeviceManager;

namespace RMS.Core.Monitoring.Device.WebCamera
{
    public class WebCamera : Device
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="model"></param>
        /// <param name="deviceName">Device's name is shown in Device Manager.</param>
        /// <param name="deviceID"></param>
        public WebCamera(string brand, string model, string deviceName, string deviceID) : base(brand, model, deviceName, deviceID)
        {
        }

    }
}
