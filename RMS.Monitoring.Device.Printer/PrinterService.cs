using System;
using System.Collections.Generic;
using RMS.Agent.Proxy.ClientProxy;
using RMS.Agent.Proxy.MonitoringProxy;
using RMS.Common.Exception;

namespace RMS.Monitoring.Device.Printer
{
    public class PrinterService
    {
        private Printer _device;
        private ClientResult clientResult;

        public PrinterService(string brand, string model, string deviceManagerName, string deviceManagerID, string printerName, bool useCOMPort, ClientResult clientResult) : this(brand, model, deviceManagerName, deviceManagerID, printerName, useCOMPort, "", clientResult) { }

        public PrinterService(string brand, string model, string deviceManagerName, string deviceManagerID, string printerName, bool useCOMPort, string portName, ClientResult clientResult)
        {
            try
            {
                this.clientResult = clientResult;

                if (brand.ToLower() == "brother") _device = new Brother(model, deviceManagerName, deviceManagerID, printerName, useCOMPort, portName);
                else if (brand.ToLower() == "winpos") _device = new WinPOS(model, deviceManagerName, deviceManagerID, printerName, useCOMPort, portName);

                throw new Exception("Brand Not Found. brand=" + brand);
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "PrinterService failed. " + ex.Message, ex, false);
            }
        }

        public List<RmsReportMonitoringRaw> Monitoring()
        {
            try
            {
                List<RmsReportMonitoringRaw> lRmsReportMonitoringRaws = new List<RmsReportMonitoringRaw>();

                int ret = _device.CheckDeviceManager();

                if (ret != 0)
                {
                    RmsReportMonitoringRaw raw = new RmsReportMonitoringRaw();
                    raw.ClientCode = clientResult.Client.ClientCode;
                    raw.DeviceCode = clientResult.ListDevices[0].DeviceCode;

                    if (ret == -1)
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
                }
                else
                {
                    int[] arrRet = _device.CheckPaperStatus();
                    if (arrRet != null)
                    {
                        if (arrRet[0] > 0)
                        {
                            RmsReportMonitoringRaw raw = new RmsReportMonitoringRaw();
                            raw.ClientCode = clientResult.Client.ClientCode;
                            raw.DeviceCode = clientResult.ListDevices[0].DeviceCode;

                            raw.Message = "LOW_PAPER";

                            raw.MessageDateTime = DateTime.Now;
                            raw.MonitoringProfileDeviceId = clientResult.ListMonitoringProfileDevices[0].MonitoringProfileDeviceId;

                            lRmsReportMonitoringRaws.Add(raw);
                        }

                        if (arrRet[1] > 0)
                        {
                            RmsReportMonitoringRaw raw = new RmsReportMonitoringRaw();
                            raw.ClientCode = clientResult.Client.ClientCode;
                            raw.DeviceCode = clientResult.ListDevices[0].DeviceCode;

                            raw.Message = "END_PAPER";

                            raw.MessageDateTime = DateTime.Now;
                            raw.MonitoringProfileDeviceId = clientResult.ListMonitoringProfileDevices[0].MonitoringProfileDeviceId;

                            lRmsReportMonitoringRaws.Add(raw);
                        }


                        var NumOfQueues = _device.CheckPrintQueueStatus(7);
                        if (NumOfQueues > 0)
                        {
                            RmsReportMonitoringRaw raw = new RmsReportMonitoringRaw();
                            raw.ClientCode = clientResult.Client.ClientCode;
                            raw.DeviceCode = clientResult.ListDevices[0].DeviceCode;

                            raw.Message = "CANNOT_PRINT";

                            raw.MessageDateTime = DateTime.Now;
                            raw.MonitoringProfileDeviceId = clientResult.ListMonitoringProfileDevices[0].MonitoringProfileDeviceId;

                            lRmsReportMonitoringRaws.Add(raw);
                        }
                    }
                }

                if (lRmsReportMonitoringRaws.Count == 0)
                {
                    RmsReportMonitoringRaw raw = new RmsReportMonitoringRaw();
                    raw.ClientCode = clientResult.Client.ClientCode;
                    raw.DeviceCode = clientResult.ListDevices[0].DeviceCode;

                    raw.Message = "OK";

                    raw.MessageDateTime = DateTime.Now;
                    raw.MonitoringProfileDeviceId = clientResult.ListMonitoringProfileDevices[0].MonitoringProfileDeviceId;

                    lRmsReportMonitoringRaws.Add(raw);
                }

                return lRmsReportMonitoringRaws;
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "Monitoring failed. " + ex.Message, ex, false);
            }
        }
    }
}
