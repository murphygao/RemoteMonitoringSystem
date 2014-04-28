using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Agent.Proxy.ClientProxy;
using RMS.Agent.Proxy.MonitoringProxy;

namespace RMS.Monitoring.Device.ThermalPrinter
{
    public class ThermalPrinterService
    {
        private ThermalPrinter _device;
        private ClientResult clientResult;

        public ThermalPrinterService(string brand, string model, string deviceManagerName, string deviceManagerID, ClientResult clientResult)
        {
            this.clientResult = clientResult;

            if (brand.ToLower() == "custom") _device = new CUSTOM(model, deviceManagerName, deviceManagerID);

        }

        public List<RmsReportMonitoringRaw> Monitoring()
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
                    if (arrRet[0] != 0)
                    {
                        RmsReportMonitoringRaw raw = new RmsReportMonitoringRaw();
                        raw.ClientCode = clientResult.Client.ClientCode;
                        raw.DeviceCode = clientResult.ListDevices[0].DeviceCode;

                        raw.Message = "LOW_PAPER";

                        raw.MessageDateTime = DateTime.Now;
                        raw.MonitoringProfileDeviceId = clientResult.ListMonitoringProfileDevices[0].MonitoringProfileDeviceId;

                        lRmsReportMonitoringRaws.Add(raw);
                    }

                    if (arrRet[1] != 0)
                    {
                        RmsReportMonitoringRaw raw = new RmsReportMonitoringRaw();
                        raw.ClientCode = clientResult.Client.ClientCode;
                        raw.DeviceCode = clientResult.ListDevices[0].DeviceCode;

                        raw.Message = "END_PAPER";

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
    }
}
