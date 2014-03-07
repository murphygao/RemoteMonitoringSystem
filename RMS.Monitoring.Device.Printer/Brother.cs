namespace RMS.Monitoring.Device.Printer
{
    public class Brother : Printer
    {
        public Brother(string model, string deviceManagerName, string deviceManagerID, string printerName, bool useCOMPort)
            : base("Brother", model, deviceManagerName, deviceManagerID, printerName, useCOMPort)
        {
        }

        public Brother(string model, string deviceManagerName, string deviceManagerID, string printerName, bool useCOMPort, string comPort)
            : base("Brother", model, deviceManagerName, deviceManagerID, printerName, useCOMPort, comPort)
        {
        }

        public override int CheckPaperStatus()
        {
            return -1;
        }
    }
}
