using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    reader.Reset();
                    // open write endpoint 1.
                    using (UsbEndpointWriter writer = MyUsbDevice.OpenEndpointWriter(WriteEndpointID.Ep02))
                    {
                        writer.Reset();
                        // Remove the exepath/startup filename text from the begining of the CommandLine.

                        if (byteArrayWritten.Length > 0)
                        {
                            int bytesWritten;

                            ec = writer.Write(byteArrayWritten, 2000, out bytesWritten);
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

                                // Write that output to the console.
                                //Console.Write(Encoding.Default.GetString(readBuffer, 0, bytesRead));
                                return readBuffer;
                            }

                            //Console.WriteLine("\r\nDone!\r\n");
                        }
                        else
                            throw new Exception("Nothing to do.");

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
                                wholeUsbDevice.ResetDevice();
                            }

                            MyUsbDevice.Close();
                        }
                        MyUsbDevice = null;
                        
                        // Free usb resources
                        UsbDevice.Exit();

                    }
                }
                catch (Exception ex)
                {
                    new RMSAppException("WriteAndReadUSB - Closing USB failed. " + ex.Message, ex, true);
                }
            }
            return null;
        }

    }
}
