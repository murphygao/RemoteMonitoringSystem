using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Agent.Proxy.ClientProxy;
using RMS.Agent.Proxy.MonitoringProxy;
using RMS.Common.Exception;

namespace RMS.Monitoring.Device.ATMCardReader
{
    public class ATMCardReaderService
    {
        private ATMCardReader _device;
        private ClientResult clientResult;

        public ATMCardReaderService(string brand, string model, string deviceManagerName, string deviceManagerID, ClientResult clientResult)
        {
            try
            {
                this.clientResult = clientResult;

                if (brand.ToLower() == "mutek") _device = new MUTEK(model, deviceManagerName, deviceManagerID);

                else
                    throw new Exception("Brand Not Found. brand=" + brand);

            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "ATMCardReaderService failed. " + ex.Message, ex, false);

            }
        }

        public List<RmsReportMonitoringRaw> Monitoring()
        {
            try
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
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "Monitoring failed. " + ex.Message, ex, false);
            }
        }
    }
}
