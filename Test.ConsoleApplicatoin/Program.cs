using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using RMS.Centralize.BSL.MonitoringEngine;
using Test.ConsoleApplication.ClientProxy;
using Test.ConsoleApplication.MonitoringProxy;


namespace Test.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
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
                CallMonitoringAgent();

            } while (Console.ReadKey().KeyChar == 'a');


            Console.ReadKey();
        }
        enum PrinterStatus
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
        static void CheckPrinterStatus()
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


        static void CheckUSBDevice()
        {
            ManagementObjectCollection mbsList = null;
            ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select * From Win32_USBHub");
            mbsList = mbs.Get();

            foreach (ManagementObject mo in mbsList)
            {
                Console.WriteLine("USBHub device Friendly name:{0}", mo["Name"].ToString());
            }
        }
        
        static void CheckUSBDevice2()
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

        static void CheckUSBDevice3()
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

        static void TestWebCamera()
        {
            //WebCameraService wcs = new WebCameraService("LG", "", "Integrated Camera", "");
            //Console.WriteLine("LG Web Camera Status : " + wcs.Monitoring());

            //wcs = new WebCameraService("Lenovo", "", "Integrated Camera", "");
            //Console.WriteLine("LG Web Camera Status : " + wcs.Monitoring());
        }

        static void TestPrinter(string printerName)
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
            buffer = Marshal.AllocHGlobal((int)sizeNeeded);
            if (!Win32.GetPrinter(handle, level, buffer, sizeNeeded, out sizeNeeded))
            {
                System.Console.WriteLine("Fail GetPrinter: {0}",Marshal.GetLastWin32Error());
                return;
            }
            Win32.PRINTER_INFO_2 info = (Win32.PRINTER_INFO_2)
                Marshal.PtrToStructure(buffer, typeof(Win32.PRINTER_INFO_2));
            System.Console.WriteLine("status:\t{0}", info.Status);
            System.Console.WriteLine("type:\t{0}", info.pDriverName);
            System.Console.WriteLine("where:\t{0}", info.pLocation);
            System.Console.WriteLine("comment:\t{0}", info.pComment);
            Marshal.FreeHGlobal(buffer);
            Win32.ClosePrinter(handle);
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
                if (drive.IsReady) Console.WriteLine(Math.Round(drive.TotalFreeSpace / Math.Pow(2, 30), 2) + " GB");
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
            var clientResult = cp.GetClient(GetClientBy.ClientID, 1, null, null, true);

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



        [DllImport("Kernel32.DLL", CharSet = CharSet.Auto, SetLastError = true)]
        private extern static bool GetDevicePowerState(IntPtr hDevice,out bool fOn);

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
