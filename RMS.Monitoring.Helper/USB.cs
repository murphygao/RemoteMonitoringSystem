using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LibUsbDotNet;
using LibUsbDotNet.Main;
using RMS.Common.Exception;

namespace RMS.Monitoring.Helper
{
    public class USBCustomThermalPrinter
    {

        public static byte[] WriteAndRead(int vid,int pid,byte[] byteArrayWritten)
        {
            ErrorCode ec = ErrorCode.None;
            UsbDevice MyUsbDevice = null;
            UsbDeviceFinder MyUsbFinder = new UsbDeviceFinder(vid, pid);
            
            try
            {

                // Find and open the usb device.
                MyUsbDevice = UsbDevice.OpenUsbDevice(MyUsbFinder);

                // If the device is open and ready
                if (MyUsbDevice == null) throw new Exception("Device Not Found. vid=" + vid + ", pid=" + pid);

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
                using (UsbEndpointReader reader = MyUsbDevice.OpenEndpointReader(ReadEndpointID.Ep01))
                {
                    //reader.Reset();
                    // open write endpoint 1.
                    using (UsbEndpointWriter writer = MyUsbDevice.OpenEndpointWriter(WriteEndpointID.Ep02))
                    {
                        //writer.Reset();
                        // Remove the exepath/startup filename text from the begining of the CommandLine.
                        UsbTransfer usbTransfer = null;

                        try
                        {
                            if (byteArrayWritten.Length > 0)
                            {
                                int bytesWritten;

                                ec = writer.Write(byteArrayWritten, 2000, out bytesWritten);
                                if (ec != ErrorCode.None) throw new Exception(UsbDevice.LastErrorString);

                                byte[] readBuffer = new byte[6];
                                while (ec == ErrorCode.None)
                                {
                                    int bytesRead;

                                    // If the device hasn't sent data in the last 100 milliseconds,
                                    // a timeout error (ec = IoTimedOut) will occur. 
                                    ec = reader.Read(readBuffer, 500, out bytesRead);

                                    if (bytesRead == 0)
                                    {
                                        return readBuffer; 
                                    }
                                    if (ec != ErrorCode.None) throw new Exception(UsbDevice.LastErrorString);


                                    // Write that output to the console.
                                    //Console.Write(Encoding.Default.GetString(readBuffer, 0, bytesRead));
                                    return readBuffer;
                                }

                                //Console.WriteLine("\r\nDone!\r\n");
                            }
                            else
                                throw new Exception("Nothing to do.");
                        }
                        finally
                        {
                            if (usbTransfer != null)
                            {
                                // **** Start of code added to fix ObjectDisposedException
                                if (!usbTransfer.IsCancelled || !usbTransfer.IsCompleted)
                                {
                                    usbTransfer.Cancel();
                                }
                                // **** End of code added to fix ObjectDisposedException
                                usbTransfer.Dispose();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RMSAppException("WriteAndRead failed. " + ex.Message, ex, true);
                //Console.WriteLine();
                //Console.WriteLine((ec != ErrorCode.None ? ec + ":" : String.Empty) + ex.Message);
            }
            finally
            {
                try
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
                                //wholeUsbDevice.ResetDevice();
                            }

                            MyUsbDevice.Close();
                        }
                        MyUsbDevice = null;
                        
                        // Free usb resources
                        UsbDevice.Exit();

                    }
                    System.Threading.Thread.Sleep(750);
                }
                catch (Exception ex)
                {
                    new RMSAppException("WriteAndReadUSB - Closing USB failed. " + ex.Message, ex, true);
                }

            }
            return null;
        }

    }

    public class USBBrotherA4Printer
    {

        public static string WriteAndRead(int vid, int pid, byte[] byteArrayWritten)
        {
            ErrorCode ec = ErrorCode.None;
            UsbDevice MyUsbDevice = null;
            UsbDeviceFinder MyUsbFinder = new UsbDeviceFinder(vid, pid);

            try
            {

                // Find and open the usb device.
                MyUsbDevice = UsbDevice.OpenUsbDevice(MyUsbFinder);

                // If the device is open and ready
                if (MyUsbDevice == null) throw new Exception("Device Not Found. vid=" + vid + ", pid=" + pid);

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
                using (UsbEndpointReader reader = MyUsbDevice.OpenEndpointReader(ReadEndpointID.Ep02))
                {
                    reader.Reset();
                    // open write endpoint 1.
                    using (UsbEndpointWriter writer = MyUsbDevice.OpenEndpointWriter(WriteEndpointID.Ep01))
                    {
                        UsbTransfer usbTransfer = null;

                        try
                        {
                            writer.Reset();
                            // Remove the exepath/startup filename text from the begining of the CommandLine.

                            if (byteArrayWritten.Length > 0)
                            {
                                int bytesWritten;

                                ec = writer.Write(byteArrayWritten, 2000, out bytesWritten);
                                if (ec != ErrorCode.None) throw new Exception(UsbDevice.LastErrorString);

                                byte[] readBuffer = new byte[256];
                                string retString = "";
                                while (ec == ErrorCode.None)
                                {
                                    Thread.Sleep(1000);

                                    int bytesRead;

                                    // If the device hasn't sent data in the last 100 milliseconds,
                                    // a timeout error (ec = IoTimedOut) will occur. 
                                    ec = reader.Read(readBuffer, 1000, out bytesRead);
                                    if (ec != ErrorCode.None) throw new Exception(UsbDevice.LastErrorString);

                                    if (bytesRead == 0)
                                    {
                                        return retString;
                                        //throw new Exception("No more bytes!");
                                    }

                                    // Write that output to the console.
                                    //Console.Write(Encoding.Default.GetString(readBuffer, 0, bytesRead));

                                    retString += Encoding.Default.GetString(readBuffer, 0, bytesRead);
                                }

                                //Console.WriteLine("\r\nDone!\r\n");
                            }
                            else
                                throw new Exception("Nothing to do.");
                        }
                        finally
                        {
                            if (usbTransfer != null)
                            {
                                // **** Start of code added to fix ObjectDisposedException
                                if (!usbTransfer.IsCancelled || !usbTransfer.IsCompleted)
                                {
                                    usbTransfer.Cancel();
                                }
                                // **** End of code added to fix ObjectDisposedException
                                usbTransfer.Dispose();
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new RMSAppException("WriteAndRead failed. " + ex.Message, ex, true);
                //Console.WriteLine();
                //Console.WriteLine((ec != ErrorCode.None ? ec + ":" : String.Empty) + ex.Message);
            }
            finally
            {
                try
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
                                //wholeUsbDevice.ResetDevice();
                            }

                            MyUsbDevice.Close();
                        }
                        MyUsbDevice = null;

                        // Free usb resources
                        UsbDevice.Exit();

                    }
                    System.Threading.Thread.Sleep(750);

                }
                catch (Exception ex)
                {
                    new RMSAppException("WriteAndReadUSB - Closing USB failed. " + ex.Message, ex, true);
                }

            }
            return null;
        }

    }

    public class USBFilter
    {
        public static List<string> ListUSBFilter()
        {
            try
            {
                Process p = new Process();
                // Redirect the output stream of the child process.
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.FileName = @"C:\Program Files\LibUSB-Win32\bin\install-filter.exe";
                p.StartInfo.Arguments = "list";
                p.Start();
                // Do not wait for the child process to exit before
                // reading to the end of its redirected stream.
                string output = p.StandardOutput.ReadToEnd();
                List<string> listAllDevices = new List<string>(output.Split(new string[] {"\n"}, StringSplitOptions.None));

                List<string> listInstalledDevices = new List<string>();
                for (int i = 0; i < listAllDevices.Count; i++)
                {
                    if (listAllDevices[i].ToLower().IndexOf("device upper filters:libusb0") > -1)
                    {
                        string usbDeviceID = listAllDevices[i - 1].Substring(0, listAllDevices[i - 1].IndexOf("PID_") + 8).Trim();
                        listInstalledDevices.Add(usbDeviceID);
                    }
                }
                p.WaitForExit();
                return listInstalledDevices;
            }
            catch (Exception ex)
            {
                new RMSAppException("ListUSBFilter failed " + ex.Message, ex, true);
            }
            return new List<string>();
        }

        public static void ReinstallUSBFilter()
        {
            try
            {
                ReinstallUSBFilter(ListUSBFilter());
            }
            catch (Exception ex)
            {
                new RMSAppException("ReinstallUSBFilter failed " + ex.Message, ex, true);
            }
        }

        public static void ReinstallUSBFilter(List<string> devices)
        {
            try
            {
                Process p = new Process();
                // Redirect the output stream of the child process.
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.FileName = @"C:\Program Files\LibUSB-Win32\bin\install-filter.exe";

                foreach (var device in devices)
                {
                    p.StartInfo.Arguments = string.Format("uninstall --device=\"{0}\"", device);
                    p.Start();
                    p.WaitForExit();
                    p.StartInfo.Arguments = string.Format("install --device=\"{0}\"", device);
                    p.Start();
                    p.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                new RMSAppException("ReinstallUSBFilter failed " + ex.Message, ex, true);
            }
        }

        public static void InstallUSBFilter(List<string> devices)
        {
            try
            {
                Process p = new Process();
                // Redirect the output stream of the child process.
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.FileName = @"C:\Program Files\LibUSB-Win32\bin\install-filter.exe";

                foreach (var device in devices)
                {
                    p.StartInfo.Arguments = string.Format("install --device=\"{0}\"", device);
                    p.Start();
                    p.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                new RMSAppException("InstallUSBFilter failed " + ex.Message, ex, true);
            }
        }

        public static void InstallUSBFilter(string vid, string pid)
        {
            try
            {
                Process p = new Process();
                // Redirect the output stream of the child process.
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.FileName = @"C:\Program Files\LibUSB-Win32\bin\install-filter.exe";

                string deviceID = string.Format("USB\\VID_{0}&PID_{1}", vid, pid);

                p.StartInfo.Arguments = string.Format("install --device=\"{0}\"", deviceID);
                p.Start();
                p.WaitForExit();
            }
            catch (Exception ex)
            {
                new RMSAppException("InstallUSBFilter failed " + ex.Message, ex, true);
            }
        }

    }

}
