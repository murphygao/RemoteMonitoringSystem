﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibUsbDotNet;
using LibUsbDotNet.Main;

namespace Test.ThermalPrinter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtResult.Text = "";

                var checkPaperStatus = CheckPaperStatus();


                if (checkPaperStatus != null)
                {
                    if (checkPaperStatus[0] > 0)
                    {
                        if (checkPaperStatus[0] == 500)
                        {
                            txtResult.Text += "Printer Busy";
                        }
                        else
                        {
                            txtResult.Text += "LOW_PAPER";
                        }


                    }

                    if (checkPaperStatus[1] > 0)
                    {
                        if (txtResult.Text == "Printer Busy")
                        {
                            return;
                        }

                        if (txtResult.Text != "") txtResult.Text += ", ";
                        txtResult.Text += "END_PAPER";

                    }
                }

                if (txtResult.Text == "") txtResult.Text = "Normal";
            }
            catch (Exception ex)
            {
                txtResult.Text = ex.Message;
            }
        }

        private int[] CheckPaperStatus()
        {
            try
            {
                string sVid = txtVID.Text;
                int iVid = Int32.Parse(sVid, NumberStyles.HexNumber);
                string sPid = txtPID.Text;
                int iPid = Int32.Parse(sPid, NumberStyles.HexNumber);

                byte[] byteArray = new byte[3];
                byteArray[0] = 0x10;
                byteArray[1] = 0x04;
                byteArray[2] = 4;

                byte[] read = USB.WriteAndRead(iVid, iPid, byteArray);

                if (read == null || read.Length == 0)
                    return new int[] { 500, 500 };

                int[] ret = new int[2] { 0, 0 };
                var bit2 = (read[0] & (1 << 2)) != 0; // Near End
                var bit3 = (read[0] & (1 << 3)) != 0; // Near End
                var bit5 = (read[0] & (1 << 5)) != 0; // Out of Paper
                var bit6 = (read[0] & (1 << 6)) != 0; // Out of Paper

                if (bit2 || bit3)
                    ret[0] = 1;
                if (bit5 || bit6)
                    ret[1] = 1;

                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string CheckA4PrinterStatus()
        {
            try
            {
                string sVid = txtVID.Text;
                int iVid = Int32.Parse(sVid, NumberStyles.HexNumber);
                string sPid = txtPID.Text;
                int iPid = Int32.Parse(sPid, NumberStyles.HexNumber);

                byte[] byteArray = new byte[3];
                byteArray[0] = 0x10;
                byteArray[1] = 0x04;
                byteArray[2] = 4;

                byte[] read = USB.WriteAndRead2(iVid, iPid, byteArray);

                return GetString(read);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnCheckA4PrinterStatus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtResult.Text = "";

                var checkPaperStatus = CheckA4PrinterStatus();

                txtResult.Text = checkPaperStatus;

            }
            catch (Exception ex)
            {
                txtResult.Text = ex.Message;
            }
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }


    }
    public class USB
    {

        public static byte[] WriteAndRead(int vid, int pid, byte[] byteArrayWritten)
        {

            ErrorCode ec = ErrorCode.None;
            UsbDevice MyUsbDevice = null;
            UsbDeviceFinder MyUsbFinder = new UsbDeviceFinder(vid, pid);

            try
            {
                //int counter = 1;
                //while (CheckPrintQueueStatus(0) > 0 && counter < 5)
                //{
                //    System.Threading.Thread.Sleep(2000);
                //    counter++;
                //}

                if (CheckPrintQueueStatus(0) > 0)
                {
                    return null;
                }

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

                                reader.ReadFlush();

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
                throw ex;
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
                    throw ex;
                }
            }
            return null;
        }


        public static byte[] WriteAndRead2(int vid, int pid, byte[] byteArrayWritten)
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
                UsbEndpointReader reader = MyUsbDevice.OpenEndpointReader(ReadEndpointID.Ep02);

                // open write endpoint 1.
                UsbEndpointWriter writer = MyUsbDevice.OpenEndpointWriter(WriteEndpointID.Ep01);

                // Remove the exepath/startup filename text from the begining of the CommandLine.

                if (byteArrayWritten.Length > 0)
                {
                    int bytesWritten;

                    byteArrayWritten = GetBytes("@PJL ECHO wrong sample 3-6-1993 23:12:00");
                    ec = writer.Write(byteArrayWritten, 2000, out bytesWritten);
                    if (ec != ErrorCode.None) throw new Exception(UsbDevice.LastErrorString);

                    byte[] readBuffer = new byte[4096];
                    while (ec == ErrorCode.None)
                    {
                        int bytesRead;

                        // If the device hasn't sent data in the last 100 milliseconds,
                        // a timeout error (ec = IoTimedOut) will occur. 
                        ec = reader.Read(readBuffer, 2000, out bytesRead);
                        if (ec != ErrorCode.None) throw new Exception(UsbDevice.LastErrorString);

                        if (bytesRead == 0) throw new Exception("No more bytes!");

                        // Write that output to the console.
                        //Console.Write(Encoding.Default.GetString(readBuffer, 0, bytesRead));

                        reader.ReadFlush();

                        return readBuffer;
                    }

                    //Console.WriteLine("\r\nDone!\r\n");
                }
                else
                    throw new Exception("Nothing to do.");
            }
            catch (Exception ex)
            {
                throw ex;
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
                                wholeUsbDevice.ResetDevice();
                                // Release interface #0.
                                wholeUsbDevice.ReleaseInterface(0);
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
                    throw ex;
                }
            }
            return null;
        }

        private static void OnRxEndPointData(object sender, EndpointDataEventArgs e)
        {
            //LastDataEventDate = DateTime.Now;
            Console.Write(Encoding.Default.GetString(e.Buffer, 0, e.Count));
        }
        static byte[] GetBytes(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        /// <summary>
        /// Check Printqueue Status
        /// </summary>
        /// <param name="second">จำนวนระยะเวลา (หน่วยวินาที) ที่มี queue ค้างอยู่ใน printer นั้นๆ</param>
        /// <returns>จำนวน queue ที่เกินเวลาที่กำหนดไว้</returns>
        public static int CheckPrintQueueStatus(int? second)
        {
            try
            {
                if (second == null) second = 7;

                int ret = 0;

                using (PrintServer server = new PrintServer())
                {
                    foreach (PrintQueue pq in server.GetPrintQueues())
                    {
                        if (pq.FullName.Trim().ToLower() != "CUSTOM TG2480-H".ToLower()) continue;

                        pq.Refresh();
                        PrintJobInfoCollection jobs = pq.GetPrintJobInfoCollection();
                        foreach (PrintSystemJobInfo job in jobs)
                        {
                            // Since the user may not be able to articulate which job is problematic, 
                            // present information about each job the user has submitted. 
                            //Console.WriteLine((DateTime.UtcNow - job.TimeJobSubmitted).TotalSeconds);

                            if ((DateTime.UtcNow - job.TimeJobSubmitted).TotalSeconds > second)
                                ret++;
                        } // end for each p
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }

}
