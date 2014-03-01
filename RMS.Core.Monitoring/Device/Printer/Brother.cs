using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Core.Monitoring.Device.Printer
{
    public class Brother : Printer
    {
        public Brother(string model, string deviceName, string deviceID, string printerName, bool useCOMPort) : base("Brother", model, deviceName, deviceID, printerName, useCOMPort)
        {
        }

        public Brother(string model, string deviceName, string deviceID, string printerName, bool useCOMPort, string comPort) : base("Brother", model, deviceName, deviceID, printerName, useCOMPort, comPort)
        {
        }

        public override int CheckPaperStatus()
        {
            return -1;
        }
    }
}
