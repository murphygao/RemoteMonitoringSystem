using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using RMS.Agent.Proxy.ClientProxy;
using RMS.Agent.Proxy.MonitoringProxy;
using RMS.Common.Exception;

namespace RMS.Monitoring.Device.CardDespenser
{
    public class CardDispenserService
    {
        private CardDispenser _device;
        private ClientResult clientResult;

        public CardDispenserService(string brand, string model, string deviceManagerName, string deviceManagerID, bool useCOMPort, string comPort, ClientResult clientResult)
        {
            try
            {
                this.clientResult = clientResult;

                if (brand.ToLower() == "mutek") _device = new MUTEK(model, deviceManagerName, deviceManagerID, useCOMPort, comPort);
                else
                    throw new Exception("Brand Not Found. brand=" + brand);

            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "CardDispenserService failed. " + ex.Message, ex, false);
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

                raw.MessageDateTime = DateTime.Now;
                raw.MonitoringProfileDeviceId = clientResult.ListMonitoringProfileDevices[0].MonitoringProfileDeviceId;

                if (ret == 0)
                {
                    raw.Message = "OK";

                    if (clientResult.ListMonitoringProfileDevices[0].BooleanValue == true)
                    {
                        var cardLevel = _device.CheckCardLevel();
                        foreach (string s in cardLevel)
                        {
                            RmsReportMonitoringRaw cardLevelRaw = new RmsReportMonitoringRaw();
                            cardLevelRaw.ClientCode = clientResult.Client.ClientCode;
                            cardLevelRaw.DeviceCode = clientResult.ListDevices[0].DeviceCode;
                            cardLevelRaw.MessageDateTime = DateTime.Now;
                            cardLevelRaw.MonitoringProfileDeviceId = clientResult.ListMonitoringProfileDevices[0].MonitoringProfileDeviceId;
                            if (s.ToLower() == "low_card")
                            {
                                cardLevelRaw.Message = "LOW_CARD";
                            }
                            else if (s.ToLower() == "end_card")
                            {
                                cardLevelRaw.Message = "END_CARD";
                            }

                            if (!string.IsNullOrEmpty(cardLevelRaw.Message))
                                lRmsReportMonitoringRaws.Add(cardLevelRaw);
                        }
                    }


                }
                else if (ret == -1)
                {
                    raw.Message = "DEVICE_NOT_FOUND";
                }
                else
                {
                    raw.Message = "DEVICE_NOT_READY";
                }


                lRmsReportMonitoringRaws.Add(raw);



                // ถ้า BooleanValue เป็น TRUE, สามารถตรวจสอบ ปริมาณการ์ดที่เหลืออยู่ได้
                return lRmsReportMonitoringRaws;
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "Monitoring failed. " + ex.Message, ex, false);
            }
        }
    }
}
