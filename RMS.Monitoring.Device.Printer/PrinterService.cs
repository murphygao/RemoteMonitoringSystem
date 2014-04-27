using System;
using System.Collections.Generic;
using RMS.Agent.Proxy.ClientProxy;
using RMS.Agent.Proxy.MonitoringProxy;

namespace RMS.Monitoring.Device.Printer
{
    public class PrinterService
    {
        private Printer _device;
        private ClientResult clientResult;

        public PrinterService(string brand, string model, string deviceManagerName, string deviceManagerID, string printerName, bool useCOMPort, ClientResult clientResult) : this(brand, model, deviceManagerName, deviceManagerID, printerName, useCOMPort, "", clientResult) { }

        public PrinterService(string brand, string model, string deviceManagerName, string deviceManagerID, string printerName, bool useCOMPort, string portName, ClientResult clientResult)
        {
            this.clientResult = clientResult;

            if (brand.ToLower() == "brother") _device = new Brother(model, deviceManagerName, deviceManagerID, printerName, useCOMPort, portName);
            else if (brand.ToLower() == "winpos") _device = new WinPOS(model, deviceManagerName, deviceManagerID, printerName, useCOMPort, portName);
        }

        public List<RmsReportMonitoringRaw> Monitoring()
        {
            List<RmsReportMonitoringRaw> lRmsReportMonitoringRaws = new List<RmsReportMonitoringRaw>();

            RmsReportMonitoringRaw raw = new RmsReportMonitoringRaw();
            raw.ClientCode = clientResult.Client.ClientCode;
            raw.DeviceCode = clientResult.ListDevices[0].DeviceCode;

            int ret = _device.CheckDeviceManager();

            if (ret == 0)
            {
                raw.Message = "OK";
            }
            else if (ret == -1)
            {
                raw.Message = "DEVICE_NOT_FOUND";
            }
            else
            {
                raw.Message = "DEVICE_NOT_READY";
            }
            raw.MessageDateTime = DateTime.Now;
            raw.MonitoringProfileDeviceId = clientResult.ListMonitoringProfileDevices[0].MonitoringProfileDeviceId;

            lRmsReportMonitoringRaws.Add(raw);

            return lRmsReportMonitoringRaws;
        }
    }
}
