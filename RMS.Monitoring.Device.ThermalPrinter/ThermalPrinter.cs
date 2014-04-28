using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Monitoring.Device.ThermalPrinter
{
    public class ThermalPrinter : Device
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="model"></param>
        /// <param name="deviceManagerName">Device's name is shown in Device Manager.</param>
        /// <param name="deviceManagerID"></param>
        public ThermalPrinter(string brand, string model, string deviceManagerName, string deviceManagerID)
            : base(brand, model, deviceManagerName, deviceManagerID)
        {
        }

        public virtual int[] CheckPaperStatus()
        {
            return null;
        }

        public virtual int CheckPrinterOnline()
        {
            if (string.IsNullOrEmpty(deviceManagerName)) return -1;

            ManagementScope scope = new ManagementScope(@"\root\cimv2");
            scope.Connect();

            // Select Printers from WMI Object Collections
            ManagementObjectSearcher searcher = new
             ManagementObjectSearcher("SELECT * FROM Win32_Printer");

            foreach (ManagementObject printer in searcher.Get())
            {
                if (printer["Name"].ToString().ToLower().IndexOf(deviceManagerName.ToLower().Trim()) > -1)
                {
                    if (printer["WorkOffline"].ToString().ToLower().Equals("true"))
                    {
                        // printer is offline by user
                        return 0;
                    }
                    else
                    {
                        // printer is not offline
                        return 1;
                    }
                }
            }

            return -1;
        }


    }
}
