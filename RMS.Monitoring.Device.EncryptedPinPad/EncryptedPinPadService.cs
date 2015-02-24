using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Agent.Proxy.ClientProxy;
using RMS.Agent.Proxy.MonitoringProxy;
using RMS.Common.Exception;
using RMS.Monitoring.Device.EncryptedPinPad;

namespace RMS.Monitoring.Device.EncryptedPinPad
{
    public class EncryptedPinPadService
    {
        private EncryptedPinPad _device;
        private ClientResult clientResult;

        public EncryptedPinPadService(string brand, string model, string deviceManagerName, string deviceManagerID, bool useCOMPort, string comPort, ClientResult clientResult)
        {
            try
            {
                this.clientResult = clientResult;

                if (brand.ToLower() == "kmy") _device = new KMY(model, deviceManagerName, deviceManagerID, useCOMPort, comPort);
                else
                    _device = new EncryptedPinPad(brand, model, deviceManagerName, deviceManagerID, useCOMPort, comPort);
                    //throw new Exception("Brand Not Found. brand=" + brand);

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
                    string messageStorageFolder = ConfigurationManager.AppSettings["RMS.MessageStorageFolder"];
                    messageStorageFolder = (messageStorageFolder.EndsWith(@"\")) ? messageStorageFolder : messageStorageFolder + @"\";
                    List<string> arrRet = new List<string>();

                    // Port Cannot Open
                    string txtFileName = "PORT_CANNOT_OPEN_" + _device.comPort + ".txt";
                    if (File.Exists(messageStorageFolder + txtFileName))
                    {
                        arrRet.Add("port_cannot_open");
                    }

                    if (arrRet != null)
                    {
                        foreach (var s in arrRet)
                        {
                            RmsReportMonitoringRaw raw = new RmsReportMonitoringRaw();
                            raw.ClientCode = clientResult.Client.ClientCode;
                            raw.DeviceCode = clientResult.ListDevices[0].DeviceCode;

                            if (s.ToLower() == "port_cannot_open")
                                raw.Message = "PORT_CANNOT_OPEN";

                            else
                                continue;

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
