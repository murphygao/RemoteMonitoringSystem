using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Core.Monitoring.Device.Printer
{
    public class PrinterService
    {
        private Printer printer;

        public PrinterService(string brand, string model, string deviceName, string deviceID, string printerName, bool useCOMPort) : this(brand, model, deviceName, deviceID, printerName, useCOMPort, "") {}

        public PrinterService(string brand, string model, string deviceName, string deviceID, string printerName, bool useCOMPort, string portName)
        {
            if (brand.ToLower() == "brother") printer = new Brother(model, deviceName, deviceID, printerName, useCOMPort, portName);
            else if (brand.ToLower() == "winpos") printer = new WinPOS(model, deviceName, deviceID, printerName, useCOMPort, portName);
        }

        public int Monitoring()
        {
            return printer.CheckDeviceManager();
        }
    }
}
