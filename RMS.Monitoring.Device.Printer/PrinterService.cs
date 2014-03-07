namespace RMS.Monitoring.Device.Printer
{
    public class PrinterService
    {
        private Printer printer;

        public PrinterService(string brand, string model, string deviceManagerName, string deviceManagerID, string printerName, bool useCOMPort) : this(brand, model, deviceManagerName, deviceManagerID, printerName, useCOMPort, "") { }

        public PrinterService(string brand, string model, string deviceManagerName, string deviceManagerID, string printerName, bool useCOMPort, string portName)
        {
            if (brand.ToLower() == "brother") printer = new Brother(model, deviceManagerName, deviceManagerID, printerName, useCOMPort, portName);
            else if (brand.ToLower() == "winpos") printer = new WinPOS(model, deviceManagerName, deviceManagerID, printerName, useCOMPort, portName);
        }

        public int Monitoring()
        {
            return printer.CheckDeviceManager();
        }
    }
}
