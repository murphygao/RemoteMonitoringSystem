using System.Management;

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
        {
            this.brand = brand;
            this.model = model;
            this.deviceManagerName = deviceManagerName;
            this.deviceManagerID = deviceManagerID;
            this.printerName = printerName;
            this.useCOMPort = useCOMPort;
            this.comPort = comPort;
        }

        public virtual int CheckPaperStatus()
        {
            return -1;
        }

        public virtual int CheckPrinterOnline()
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

    }
}
