using System;
using System.Management;
using RMS.Monitoring.Device.DeviceManager;

namespace RMS.Monitoring.Device
{
    public class Device
    {
        public string brand;
        public string model;

        /// <summary>
        /// Device's name is shown in Device Manager. Normally it's the same as printerName, but not for peripheral which is using COM-USB adapter
        /// </summary>
        public string deviceManagerName;
        public string deviceManagerID;

        protected Device()
        {
        }

        protected Device(string brand, string model, string deviceManagerName, string deviceManagerID)
        {
            this.brand = brand;
            this.deviceManagerName = deviceManagerName;
            this.deviceManagerID = deviceManagerID;
            this.model = model;
        }

        public virtual int CheckDeviceManager()
        {
            try
            {
                ManagementObject device = DeviceManagerService.GetPnPDeviceByName(deviceManagerName);
                if (device != null) return 1;

                if (string.IsNullOrEmpty(deviceManagerID)) return 0;

                device = DeviceManagerService.GetPnPDeviceByID(deviceManagerID);
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
