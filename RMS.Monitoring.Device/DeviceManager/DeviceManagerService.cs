﻿using System;
using System.Management;

namespace RMS.Monitoring.Device.DeviceManager
{
    public class DeviceManagerService
    {

        public static ManagementObject GetPnPDeviceByName(string deviceNames)
        {

            if (string.IsNullOrEmpty(deviceNames)) throw new ArgumentNullException("deviceName");

            // Query the device list trough the WMI. If you want to get
            // all the properties listen in the MSDN article mentioned
            // below, use "select * from Win32_PnPEntity" instead!
            ManagementObjectSearcher deviceList = new ManagementObjectSearcher("Select * from Win32_PnPEntity");
            //ManagementObjectSearcher deviceList = new ManagementObjectSearcher("Select * from Win32_ComputerSystem");

            // Any results? There should be!
            if (deviceList != null)
                // Enumerate the devices
            {

                string[] deviceNameArray = deviceNames.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (ManagementObject device in deviceList.Get())
                {

                    // To make the example more simple,
                    string name = device.GetPropertyValue("Name").ToString();
                    //string status = device.GetPropertyValue("Status").ToString();
                    foreach (var deviceName in deviceNameArray)
                    {
                        if (name.ToLower().IndexOf(deviceName.Trim().ToLower()) >= 0) return device;
                    }

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
            }
            return null;
        }
        public static ManagementObject GetPnPDeviceByID(string deviceIDs)
        {

            if (string.IsNullOrEmpty(deviceIDs)) throw new ArgumentNullException("deviceID");

            // Query the device list trough the WMI. If you want to get
            // all the properties listen in the MSDN article mentioned
            // below, use "select * from Win32_PnPEntity" instead!
            ManagementObjectSearcher deviceList = new ManagementObjectSearcher("Select * from Win32_PnPEntity");
            //ManagementObjectSearcher deviceList = new ManagementObjectSearcher("Select * from Win32_ComputerSystem");

            // Any results? There should be!
            if (deviceList != null)
            {
                string[] deviceIDArray = deviceIDs.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                // Enumerate the devices
                foreach (ManagementObject device in deviceList.Get())
                {

                    foreach (var deviceID in deviceIDArray)
                    {

                        // To make the example more simple,
                        string name = device.GetPropertyValue("Name").ToString();
                        //string status = device.GetPropertyValue("Status").ToString();

                        if (name.ToLower().IndexOf(deviceID.Trim().ToLower()) >= 0) return device;

                        name = device.GetPropertyValue("DeviceID").ToString();
                        if (name.ToLower().IndexOf(deviceID.Trim().ToLower()) >= 0) return device;

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
                }
            }
            return null;
        }
    }
}
