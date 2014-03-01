using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Core.Monitoring.DeviceManager
{
    public class DeviceManagerService
    {

        public static ManagementObject GetPnPDeviceByName(string deviceName)
        {

            if (string.IsNullOrEmpty(deviceName)) throw new ArgumentNullException("deviceName");

            // Query the device list trough the WMI. If you want to get
            // all the properties listen in the MSDN article mentioned
            // below, use "select * from Win32_PnPEntity" instead!
            ManagementObjectSearcher deviceList = new ManagementObjectSearcher("Select * from Win32_PnPEntity");
            //ManagementObjectSearcher deviceList = new ManagementObjectSearcher("Select * from Win32_ComputerSystem");

            // Any results? There should be!
            if (deviceList != null)
                // Enumerate the devices
                foreach (ManagementObject device in deviceList.Get())
                {

                    // To make the example more simple,
                    string name = device.GetPropertyValue("Name").ToString();
                    string status = device.GetPropertyValue("Status").ToString();

                    if (name.ToLower().IndexOf(deviceName.ToLower()) >= 0) return device;

                    //// Uncomment these lines and use the "select * query" if you 
                    //// want a VERY verbose list
                    //// foreach (PropertyData prop in device.Properties)
                    ////    Console.WriteLine( "\t" + prop.Name + ": " + prop.Value);

                    //// More details on the valid properties:
                    //// http://msdn.microsoft.com/en-us/library/aa394353(VS.85).aspx
                    //Console.WriteLine("Device name: {0}", name);
                    //Console.WriteLine("\tStatus: {0}", status);

                    //// Part II, Evaluate the device status.
                    //bool working = ((status == "OK") || (status == "Degraded")
                    //    || (status == "Pred Fail"));

                    //Console.WriteLine("\tWorking?: {0}", working);

                    //foreach (PropertyData propertyData in device.Properties)
                    //{
                    //    Console.WriteLine("Property:  {0}, Value: {1}", propertyData.Name, propertyData.Value);
                    //}

                    //Console.WriteLine();
                }
            return null;
        }
        public static ManagementObject GetPnPDeviceByID(string deviceID)
        {

            if (string.IsNullOrEmpty(deviceID)) throw new ArgumentNullException("deviceID");

            // Query the device list trough the WMI. If you want to get
            // all the properties listen in the MSDN article mentioned
            // below, use "select * from Win32_PnPEntity" instead!
            ManagementObjectSearcher deviceList = new ManagementObjectSearcher("Select * from Win32_PnPEntity");
            //ManagementObjectSearcher deviceList = new ManagementObjectSearcher("Select * from Win32_ComputerSystem");

            // Any results? There should be!
            if (deviceList != null)
                // Enumerate the devices
                foreach (ManagementObject device in deviceList.Get())
                {

                    // To make the example more simple,
                    string name = device.GetPropertyValue("Name").ToString();
                    string status = device.GetPropertyValue("Status").ToString();

                    if (name.ToLower().IndexOf(deviceID.ToLower()) >= 0) return device;

                    //// Uncomment these lines and use the "select * query" if you 
                    //// want a VERY verbose list
                    //// foreach (PropertyData prop in device.Properties)
                    ////    Console.WriteLine( "\t" + prop.Name + ": " + prop.Value);

                    //// More details on the valid properties:
                    //// http://msdn.microsoft.com/en-us/library/aa394353(VS.85).aspx
                    //Console.WriteLine("Device name: {0}", name);
                    //Console.WriteLine("\tStatus: {0}", status);

                    //// Part II, Evaluate the device status.
                    //bool working = ((status == "OK") || (status == "Degraded")
                    //    || (status == "Pred Fail"));

                    //Console.WriteLine("\tWorking?: {0}", working);

                    //foreach (PropertyData propertyData in device.Properties)
                    //{
                    //    Console.WriteLine("Property:  {0}, Value: {1}", propertyData.Name, propertyData.Value);
                    //}

                    //Console.WriteLine();
                }
            return null;
        }
    }
}
