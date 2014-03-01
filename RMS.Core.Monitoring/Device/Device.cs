using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using RMS.Core.Monitoring.DeviceManager;

namespace RMS.Core.Monitoring.Device
{
    public class Device
    {
        public string brand;
        public string model;

        /// <summary>
        /// Device's name is shown in Device Manager. Normally it's the same as printerName, but not for peripheral which is using COM-USB adapter
        /// </summary>
        public string deviceName;
        public string deviceID;

        protected Device()
        {
        }

        protected Device(string brand, string model, string deviceName, string deviceID)
        {
            this.brand = brand;
            this.deviceID = deviceID;
            this.deviceName = deviceName;
            this.model = model;
        }

        public virtual int CheckDeviceManager()
        {
            try
            {
                ManagementObject device = DeviceManagerService.GetPnPDeviceByName(deviceName);
                if (device != null) return 1;

                if (string.IsNullOrEmpty(deviceID)) return 0;

                device = DeviceManagerService.GetPnPDeviceByID(deviceID);
                if (device != null) return 1;

                return 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
