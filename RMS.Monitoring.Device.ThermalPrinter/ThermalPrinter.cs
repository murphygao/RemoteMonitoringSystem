using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using RMS.Common.Exception;

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Return Paper Status -> int[0] = Near End, int [1] = Out of Paper
        /// -1 ตรวจสอบไม่ได้
        /// 0 ปกติ
        /// >0  ไม่ปกติ
        /// </returns>
        public virtual int[] CheckPaperStatus()
        {
            return null;
        }

        public virtual int CheckPrinterOnline()
        {
            try
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
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "CheckPrinterOnline failed. " + ex.Message, ex, false);
            }
        }

        /// <summary>
        /// Check Printqueue Status
        /// </summary>
        /// <param name="second">จำนวนระยะเวลา (หน่วยวินาที) ที่มี queue ค้างอยู่ใน printer นั้นๆ</param>
        /// <returns>จำนวน queue ที่เกินเวลาที่กำหนดไว้</returns>
        public virtual int CheckPrintQueueStatus(int? second)
        {
            try
            {
                if (second == null) second = 7;

                int ret = 0;

                PrintServer server = new PrintServer();

                foreach (PrintQueue pq in server.GetPrintQueues())
                {
                    if (pq.FullName.Trim().ToLower() != deviceManagerName.Trim().ToLower()) continue;

                    pq.Refresh();
                    PrintJobInfoCollection jobs = pq.GetPrintJobInfoCollection();
                    foreach (PrintSystemJobInfo job in jobs)
                    {
                        // Since the user may not be able to articulate which job is problematic, 
                        // present information about each job the user has submitted. 
                        //Console.WriteLine((DateTime.UtcNow - job.TimeJobSubmitted).TotalSeconds);

                        if ((DateTime.UtcNow - job.TimeJobSubmitted).TotalSeconds > second)
                            ret++;
                    }// end for each p
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "CheckPrintQueueStatus failed. " + ex.Message, ex, false);
            }
        }

    }
}
