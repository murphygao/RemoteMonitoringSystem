using System;
using System.Management;
using System.Printing;
using RMS.Common.Exception;

namespace RMS.Monitoring.Device.Printer
{
    public class Printer : Device
    {
        /// <summary>
        /// Printer's name is shown in "Devices and Printers"
        /// </summary>
        protected string printerName;

        /// <summary>
        /// If using COM-USB adapter, useCOMPort is true;
        /// </summary>
        protected bool useCOMPort;
        protected string comPort;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="model"></param>
        /// <param name="deviceManagerName">Device's name is shown in Device Manager.</param>
        /// <param name="deviceManagerID"></param>
        /// <param name="printerName">Printer's name is shown in "Devices and Printers"</param>
        /// <param name="useCOMPort">If using COM-USB adapter, useCOMPort is true;</param>
        public Printer(string brand, string model, string deviceManagerName, string deviceManagerID, string printerName, bool useCOMPort)
            : base(brand, model, deviceManagerName, deviceManagerID)
        {
            this.brand = brand;
            this.model = model;
            this.deviceManagerName = deviceManagerName;
            this.deviceManagerID = deviceManagerID;
            this.printerName = printerName;
            this.useCOMPort = useCOMPort;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="model"></param>
        /// <param name="deviceManagerName">Device's name is shown in Device Manager.</param>
        /// <param name="deviceManagerID"></param>
        /// <param name="printerName">Printer's name is shown in "Devices and Printers"</param>
        /// <param name="useCOMPort">If using COM-USB adapter, useCOMPort is true;</param>
        /// <param name="comPort">COM1, COM2, COM3, COM4, ...</param>
        public Printer(string brand, string model, string deviceManagerName, string deviceManagerID, string printerName, bool useCOMPort, string comPort)
            : base(brand, model, deviceManagerName, deviceManagerID)
        {
            this.brand = brand;
            this.model = model;
            this.deviceManagerName = deviceManagerName;
            this.deviceManagerID = deviceManagerID;
            this.printerName = printerName;
            this.useCOMPort = useCOMPort;
            this.comPort = comPort;
        }

        public virtual int[] CheckPaperStatus()
        {
            return null;
        }

        public virtual int CheckPrinterOnline()
        {
            try
            {
                if (string.IsNullOrEmpty(printerName)) return -1;

                ManagementScope scope = new ManagementScope(@"\root\cimv2");
                scope.Connect();

                // Select Printers from WMI Object Collections
                ManagementObjectSearcher searcher = new
                    ManagementObjectSearcher("SELECT * FROM Win32_Printer");

                foreach (ManagementObject printer in searcher.Get())
                {
                    if (printer["Name"].ToString().ToLower().IndexOf(printerName.ToLower().Trim()) > -1)
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
