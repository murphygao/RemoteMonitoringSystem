using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Printing;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using LibUsbDotNet;
using LibUsbDotNet.Info;
using LibUsbDotNet.Main;
using RMS.Centralize.BSL.MonitoringEngine;
using Test.ConsoleApplication.ClientProxy;
using Test.ConsoleApplication.MonitoringProxy;


namespace Test.ConsoleApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //CheckPrinterStatus();

            /*
            var wc = new WebCamera("LG", "", "Integrated Camera", "");
            Console.WriteLine(wc.CheckDeviceManager());
            Console.WriteLine();

            var pr = new Performance("Brother", "", "Brother MFC-7450 Performance", "", "Brother MFC-7450 Performance", false);
            Console.WriteLine(pr.CheckDeviceManager());
            Console.WriteLine(pr.CheckPrinterOnline());
            */

            do
            {
                Console.WriteLine();
                //TestPerformance();
                //TestMonitor();
                //PowerSupplyState();
                //CheckUSBDevice3();

                //TestClientProxy();
                //CallMonitoringAgent();
                LocalIPAddress();
                //TestPrinter("Brother MFC-7450 Printer");
                //TestCustomThermalPrinter();
                //TestPrintingStatus();
                //string x = @"USB\VID_0DD4&PID_01A8\TG2480-H_Num.:_0";

                //string sVid = x.ToUpper().Substring(x.IndexOf("VID_") + 4, 4);
                //int iVid = Int32.Parse(sVid, System.Globalization.NumberStyles.HexNumber);
                //string sPid = x.ToUpper().Substring(x.IndexOf("PID_") + 4, 4);
                //int iPid = Int32.Parse(sPid, System.Globalization.NumberStyles.HexNumber);


                //TestUSBLib();
                //TestReadWriteUSBLib();
            } while (Console.ReadKey().KeyChar == 'a');


            Console.ReadKey();
        }

        private enum PrinterStatus
        {
            Other = 1,
            Unknown,
            Idle,
            Printing,
            Warmup,
            Stopped,
            printing,
            Offline
        }

        private static void CheckPrinterStatus()
        {
            // Set management scope
            ManagementScope scope = new ManagementScope(@"\root\cimv2");
            scope.Connect();

            // Select Printers from WMI Object Collections
            ManagementObjectSearcher searcher = new
                ManagementObjectSearcher("SELECT * FROM Win32_Printer");

            string printerName = "";
            foreach (ManagementObject printer in searcher.Get())
            {
                printerName = printer["Name"].ToString().ToLower();
                Console.WriteLine("Performance = " + printer["Name"]);
                if (printer["WorkOffline"].ToString().ToLower().Equals("true"))
                {
                    // printer is offline by user
                    Console.WriteLine("not connected.");
                }
                else
                {
                    // printer is not offline
                    Console.WriteLine("connected.");
                }
                Console.WriteLine();
            }
        }


        private static void CheckUSBDevice()
        {
            ManagementObjectCollection mbsList = null;
            ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select * From Win32_USBHub");
            mbsList = mbs.Get();

            foreach (ManagementObject mo in mbsList)
            {
                Console.WriteLine("USBHub device Friendly name:{0}", mo["Name"].ToString());
            }
        }

        private static void CheckUSBDevice2()
        {
            ObjectQuery query =
                new ObjectQuery("Select * from Win32_USBHub");

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection result = searcher.Get();

            foreach (ManagementObject obj in result)
            {
                if (obj["Description"] != null) Console.WriteLine("Description:\t" + obj["Description"].ToString());
                if (obj["DeviceID"] != null) Console.WriteLine("DeviceID:\t" + obj["DeviceID"].ToString());
                if (obj["PNPDeviceID"] != null) Console.WriteLine("PNPDeviceID:\t" + obj["PNPDeviceID"].ToString());
            }
        }

        private static void CheckUSBDevice3()
        {
            ObjectQuery query =
                new ObjectQuery("Select * from Win32_PnPEntity");

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection result = searcher.Get();

            foreach (ManagementObject obj in result)
            {
                //if (obj["Description"] != null) Console.WriteLine("Description:\t" + obj["Description"].ToString());
                //if (obj["DeviceID"] != null) Console.WriteLine("DeviceID:\t" + obj["DeviceID"].ToString());
                //if (obj["PNPDeviceID"] != null) Console.WriteLine("PNPDeviceID:\t" + obj["PNPDeviceID"].ToString());

                //if (obj["ClassGuid"] == null) continue;
                //if (obj["ClassGuid"].ToString() != "{6bdd1fc6-810f-11d0-bec7-08002be2092f}") continue;
                //if (obj["ClassGuid"].ToString() != "{50dd5230-ba8a-11d1-bf5d-0000f805f530}") continue;
                if (obj["Description"].ToString() != "HID-compliant device") continue;


                foreach (PropertyData property in obj.Properties)
                {
                    if (property.Name == "InstallDate")
                    {
                        object val = property.Value;
                    }
                    Console.WriteLine(property.Name + " " + property.Value);
                }
                Console.WriteLine();
                Console.WriteLine();
            }

        }

        private static void TestWebCamera()
        {
            //WebCameraService wcs = new WebCameraService("LG", "", "Integrated Camera", "");
            //Console.WriteLine("LG Web Camera Status : " + wcs.Monitoring());

            //wcs = new WebCameraService("Lenovo", "", "Integrated Camera", "");
            //Console.WriteLine("LG Web Camera Status : " + wcs.Monitoring());
        }

        private static void TestPrinter(string printerName)
        {
            string printer = printerName;
            IntPtr handle = IntPtr.Zero;
            if (!Win32.OpenPrinter(printer, out handle, IntPtr.Zero))
            {
                System.Console.WriteLine("Fail OpenPrinter: {0}", Marshal.GetLastWin32Error());
                return;
            }
            UInt32 level = 2;
            UInt32 sizeNeeded = 0;
            IntPtr buffer = IntPtr.Zero;
            Win32.GetPrinter(handle, level, buffer, 0, out sizeNeeded);
            buffer = Marshal.AllocHGlobal((int) sizeNeeded);
            if (!Win32.GetPrinter(handle, level, buffer, sizeNeeded, out sizeNeeded))
            {
                System.Console.WriteLine("Fail GetPrinter: {0}", Marshal.GetLastWin32Error());
                return;
            }
            Win32.PRINTER_INFO_2 info = (Win32.PRINTER_INFO_2)
                Marshal.PtrToStructure(buffer, typeof (Win32.PRINTER_INFO_2));
            System.Console.WriteLine("status:\t{0}", info.Status);
            System.Console.WriteLine("type:\t{0}", info.pDriverName);
            System.Console.WriteLine("where:\t{0}", info.pLocation);
            System.Console.WriteLine("comment:\t{0}", info.pComment);
            Marshal.FreeHGlobal(buffer);
            Win32.ClosePrinter(handle);
        }

        private static void TestPrintingStatus()
        {
            PrintServer server = new PrintServer();

            foreach (PrintQueue pq in server.GetPrintQueues())
            {
                if (pq.FullName != "Brother MFC-7450 Printer") continue;

                pq.Refresh();
                PrintJobInfoCollection jobs = pq.GetPrintJobInfoCollection();
                foreach (PrintSystemJobInfo job in jobs)
                {
                    // Since the user may not be able to articulate which job is problematic, 
                    // present information about each job the user has submitted. 
                    Console.WriteLine((DateTime.UtcNow - job.TimeJobSubmitted).TotalSeconds);
                } // end for each p
            }
        }

        private static void TestPerformance()
        {
            PerformanceCounter cpuCounter;
            PerformanceCounter ramCounter;

            cpuCounter = new PerformanceCounter {CategoryName = "Processor", CounterName = "% Processor Time", InstanceName = "_Total"};

            ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            cpuCounter.NextValue();
            System.Threading.Thread.Sleep(500);
            Console.WriteLine(cpuCounter.NextValue() + "%");

            Console.WriteLine(ramCounter.NextValue() + "MB");


            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.DriveType == DriveType.CDRom) continue;
                //There are more attributes you can use.
                //Check the MSDN link for a complete example.
                Console.WriteLine(drive.Name);
                if (drive.IsReady) Console.WriteLine(Math.Round(drive.TotalFreeSpace/Math.Pow(2, 30), 2) + " GB");
            }

        }

        private static void TestMonitor()
        {
            // ...

            var query = "select * from WmiMonitorBasicDisplayParams";
            using (var wmiSearcher = new ManagementObjectSearcher("\\root\\wmi", query))
            {
                var results = wmiSearcher.Get();
                foreach (ManagementObject wmiObj in results)
                {

                    foreach (PropertyData property in wmiObj.Properties)
                    {
                        Console.WriteLine(property.Name + " " + property.Value);
                    }
                    // get the "Active" property and cast to a boolean, which should 
                    // tell us if the display is active. I've interpreted this to mean "on"
                    //var active = (Boolean) wmiObj["Active"];
                }
            }
        }

        private static void TestClientProxy()
        {
            ClientProxy.ClientServiceClient cp = new ClientServiceClient();
            var clientResult = cp.GetClient(GetClientBy.ClientID, 1, null, null, true, true);

            MonitoringProxy.MonitoringServiceClient mp = new MonitoringServiceClient();

            List<RmsReportMonitoringRaw> lRawMessage = new List<RmsReportMonitoringRaw>();

            foreach (var mpd in clientResult.ListMonitoringProfileDevices)
            {
                var rawMessage = new RmsReportMonitoringRaw();
                rawMessage.ClientCode = clientResult.Client.ClientCode;
                rawMessage.DeviceCode = clientResult.ListDevices.First(d => d.DeviceId == mpd.DeviceId).DeviceCode;

                if (mpd.MonitoringProfileDeviceId != 16)
                    rawMessage.Message = "OK";
                else
                    rawMessage.Message = "DEVICE_NOT_READY";
                rawMessage.MessageDateTime = DateTime.Now;
                rawMessage.MonitoringProfileDeviceId = mpd.MonitoringProfileDeviceId;

                lRawMessage.Add(rawMessage);
            }

            mp.AddMessages(lRawMessage);

        }

        private static void TestCustomThermalPrinter()
        {
            //byte[] byteArray = new byte[6];
            //byteArray[0] = 0x10;
            //byteArray[1] = 0x04;
            //byteArray[2] = 20;
            byte[] byteArray = new byte[1];
            byteArray[0] = 0x0A;

            GCHandle pinnedArray = GCHandle.Alloc(byteArray, GCHandleType.Pinned);
            IntPtr pointer = pinnedArray.AddrOfPinnedObject();
            //do your stuff

            bool ret = RawPrinterHelper.SendBytesToPrinter("CUSTOM TG2480-H", pointer, 1);

            byte[] managedArray = new byte[1];
            Marshal.Copy(pointer, managedArray, 0, 1);

            pinnedArray.Free();

        }

        [DllImport("Kernel32.DLL", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDevicePowerState(IntPtr hDevice, out bool fOn);

        public class PlatformInvokeUSER32
        {
            #region Class Variables

            public const int SM_CXSCREEN = 0;
            public const int SM_CYSCREEN = 1;

            #endregion

            #region Class Functions

            [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
            public static extern IntPtr GetDesktopWindow();

            [DllImport("user32.dll", EntryPoint = "GetDC")]
            public static extern IntPtr GetDC(IntPtr ptr);

            [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
            public static extern int GetSystemMetrics(int abc);

            [DllImport("user32.dll", EntryPoint = "GetWindowDC")]
            public static extern IntPtr GetWindowDC(Int32 ptr);

            [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

            #endregion
        }

        private static void getDisplayStatus()
        {
            bool fOn;
            IntPtr hDC = PlatformInvokeUSER32.GetDC(PlatformInvokeUSER32.GetDesktopWindow());
            if (GetDevicePowerState(hDC, out fOn))
                Console.WriteLine("Monitor is Off.");
            else
            {
                Console.WriteLine("Monitor is on.");
            }
        }

        private static void CallMonitoringAgent()
        {
            MonitoringProxy.MonitoringServiceClient msc = new MonitoringServiceClient();
            msc.StartMonitoringEngine();
            //RMS.Centralize.BSL.MonitoringEngine.MonitoringService ms = new MonitoringService();
            //ms.Start();
        }



        public static UsbDevice MyUsbDevice;

        public static void TestUSBLib()
        {
            // Dump all devices and descriptor information to console output.
            UsbRegDeviceList allDevices = UsbDevice.AllDevices;
            foreach (UsbRegistry usbRegistry in allDevices)
            {
                if (usbRegistry.Open(out MyUsbDevice))
                {
                    Console.WriteLine(MyUsbDevice.Info.ToString());
                    for (int iConfig = 0; iConfig < MyUsbDevice.Configs.Count; iConfig++)
                    {
                        UsbConfigInfo configInfo = MyUsbDevice.Configs[iConfig];
                        Console.WriteLine(configInfo.ToString());

                        ReadOnlyCollection<UsbInterfaceInfo> interfaceList = configInfo.InterfaceInfoList;
                        for (int iInterface = 0; iInterface < interfaceList.Count; iInterface++)
                        {
                            UsbInterfaceInfo interfaceInfo = interfaceList[iInterface];
                            Console.WriteLine(interfaceInfo.ToString());

                            ReadOnlyCollection<UsbEndpointInfo> endpointList = interfaceInfo.EndpointInfoList;
                            for (int iEndpoint = 0; iEndpoint < endpointList.Count; iEndpoint++)
                            {
                                Console.WriteLine(endpointList[iEndpoint].ToString());
                            }
                        }
                    }
                }
            }


            // Free usb resources.
            // This is necessary for libusb-1.0 and Linux compatibility.
            UsbDevice.Exit();

        }

        #region SET YOUR USB Vendor and Product ID!

        public static UsbDeviceFinder MyUsbFinder = new UsbDeviceFinder(0x0DD4, 0x01A8);

        #endregion

        public static void TestReadWriteUSBLib()
        {
            ErrorCode ec = ErrorCode.None;

            try
            {
                // Find and open the usb device.
                MyUsbDevice = UsbDevice.OpenUsbDevice(MyUsbFinder);

                // If the device is open and ready
                if (MyUsbDevice == null) throw new Exception("Device Not Found.");

                // If this is a "whole" usb device (libusb-win32, linux libusb)
                // it will have an IUsbDevice interface. If not (WinUSB) the 
                // variable will be null indicating this is an interface of a 
                // device.
                IUsbDevice wholeUsbDevice = MyUsbDevice as IUsbDevice;
                if (!ReferenceEquals(wholeUsbDevice, null))
                {
                    // This is a "whole" USB device. Before it can be used, 
                    // the desired configuration and interface must be selected.

                    // Select config #1
                    wholeUsbDevice.SetConfiguration(1);

                    // Claim interface #0.
                    wholeUsbDevice.ClaimInterface(0);
                }

                // open read endpoint 1.
                UsbEndpointReader reader = MyUsbDevice.OpenEndpointReader(ReadEndpointID.Ep01);

                // open write endpoint 1.
                UsbEndpointWriter writer = MyUsbDevice.OpenEndpointWriter(WriteEndpointID.Ep02);

                // Remove the exepath/startup filename text from the begining of the CommandLine.
                string cmdLine = Regex.Replace(
                    Environment.CommandLine, "^\".+?\"^.*? |^.*? ", "", RegexOptions.Singleline);

                if (!String.IsNullOrEmpty(cmdLine))
                {
                    int bytesWritten;


                    byte[] byteArray = new byte[1];
                    byteArray[0] = 0x0A;

                    byteArray = new byte[6];
                    byteArray[0] = 0x10;
                    byteArray[1] = 0x04;
                    byteArray[2] = 4;



                    ec = writer.Write(byteArray, 2000, out bytesWritten);
                    if (ec != ErrorCode.None) throw new Exception(UsbDevice.LastErrorString);

                    byte[] readBuffer = new byte[8];
                    while (ec == ErrorCode.None)
                    {
                        int bytesRead;

                        // If the device hasn't sent data in the last 100 milliseconds,
                        // a timeout error (ec = IoTimedOut) will occur. 
                        ec = reader.Read(readBuffer, 100, out bytesRead);
                        if (ec != ErrorCode.None) throw new Exception(UsbDevice.LastErrorString);

                        if (bytesRead == 0) throw new Exception("No more bytes!");


                        var bit2 = (readBuffer[0] & (1 << 2)) != 0;
                        var bit3 = (readBuffer[0] & (1 << 3)) != 0;
                        var bit5 = (readBuffer[0] & (1 << 5)) != 0;
                        var bit6 = (readBuffer[0] & (1 << 6)) != 0;

                        BitArray bits = new BitArray(readBuffer[0]);

                        // Write that output to the console.
                        Console.Write(Encoding.Default.GetString(readBuffer, 0, bytesRead));
                    }

                    Console.WriteLine("\r\nDone!\r\n");
                }
                else
                    throw new Exception("Nothing to do.");
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine((ec != ErrorCode.None ? ec + ":" : String.Empty) + ex.Message);
            }
            finally
            {
                if (MyUsbDevice != null)
                {
                    if (MyUsbDevice.IsOpen)
                    {
                        // If this is a "whole" usb device (libusb-win32, linux libusb-1.0)
                        // it exposes an IUsbDevice interface. If not (WinUSB) the 
                        // 'wholeUsbDevice' variable will be null indicating this is 
                        // an interface of a device; it does not require or support 
                        // configuration and interface selection.
                        IUsbDevice wholeUsbDevice = MyUsbDevice as IUsbDevice;
                        if (!ReferenceEquals(wholeUsbDevice, null))
                        {
                            // Release interface #0.
                            wholeUsbDevice.ReleaseInterface(0);
                        }

                        MyUsbDevice.Close();
                    }
                    MyUsbDevice = null;

                    // Free usb resources
                    UsbDevice.Exit();

                }

                // Wait for user input..
                Console.ReadKey();
            }
        }


        private static void LocalIPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            var addresses = host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            var listIP = new List<IPAddress>(addresses.ToList());
            foreach (var ipAddress in listIP)
            {
                Console.WriteLine(ipAddress);
            }
        }
    }

    public enum PowerMgmt
    {
        StandBy,
        Off,
        On
    };

    public class ScreenPowerMgmtEventArgs
    {
        private PowerMgmt _PowerStatus;
        public ScreenPowerMgmtEventArgs(PowerMgmt powerStat)
        {
            this._PowerStatus = powerStat;
        }
        public PowerMgmt PowerStatus
        {
            get { return this._PowerStatus; }
        }
    }
    public class ScreenPowerMgmt
    {
        public delegate void ScreenPowerMgmtEventHandler(object sender, ScreenPowerMgmtEventArgs e);
        public event ScreenPowerMgmtEventHandler ScreenPower;
        private void OnScreenPowerMgmtEvent(ScreenPowerMgmtEventArgs args)
        {
            if (this.ScreenPower != null) this.ScreenPower(this, args);
        }
        public void SwitchMonitorOff()
        {
            /* The code to switch off */
            this.OnScreenPowerMgmtEvent(new ScreenPowerMgmtEventArgs(PowerMgmt.Off));
        }
        public void SwitchMonitorOn()
        {
            /* The code to switch on */
            this.OnScreenPowerMgmtEvent(new ScreenPowerMgmtEventArgs(PowerMgmt.On));
        }
        public void SwitchMonitorStandby()
        {
            /* The code to switch standby */
            this.OnScreenPowerMgmtEvent(new ScreenPowerMgmtEventArgs(PowerMgmt.StandBy));
        }

    }

    public class Win32
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool OpenPrinter(string printer, out IntPtr
    handle, IntPtr printerDefaults);
        [DllImport("winspool.drv")]
        public static extern bool ClosePrinter(IntPtr handle);
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetPrinter(IntPtr handle, UInt32 level,
    IntPtr buffer,
            UInt32 size, out UInt32 sizeNeeded);
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct PRINTER_INFO_2
        {
            public string pServerName;
            public string pPrinterName;
            public string pShareName;
            public string pPortName;
            public string pDriverName;
            public string pComment;
            public string pLocation;
            public IntPtr pDevMode;
            public string pSepFile;
            public string pPrintProcessor;
            public string pDatatype;
            public string pParameters;
            public IntPtr pSecurityDescriptor;
            public UInt32 Attributes;
            public UInt32 Priority;
            public UInt32 DefaultPriority;
            public UInt32 StartTime;
            public UInt32 UntilTime;
            public UInt32 Status;
            public UInt32 cJobs;
            public UInt32 AveragePPM;
        }

    }



}
