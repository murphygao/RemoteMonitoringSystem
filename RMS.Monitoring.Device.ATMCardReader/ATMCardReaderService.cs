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

namespace RMS.Monitoring.Device.ATMCardReader
{
    public class ATMCardReaderService
    {
        private ATMCardReader _device;
        private ClientResult clientResult;

        public ATMCardReaderService(string brand, string model, string deviceManagerName, string deviceManagerID, bool useCOMPort, string comPort, ClientResult clientResult)
        {
            try
            {
                this.clientResult = clientResult;

                if (brand.ToLower() == "mutek") _device = new MUTEK(model, deviceManagerName, deviceManagerID, useCOMPort, comPort);
                else
                {
                    _device = new ATMCardReader(brand, model, deviceManagerName, deviceManagerID, useCOMPort, comPort);
                    //throw new Exception("Brand Not Found. brand=" + brand);
                }

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
                    List<string> arrRet = null;

                    string messageStorageFolder = ConfigurationManager.AppSettings["RMS.MessageStorageFolder"];
                    messageStorageFolder = (messageStorageFolder.EndsWith(@"\")) ? messageStorageFolder : messageStorageFolder + @"\";
                    arrRet = new List<string>();

                    string txtFileName = string.Empty;
                    // Check Low Card
                    txtFileName = "LOW_CARD_" + _device.comPort + ".txt";
                    if (File.Exists(messageStorageFolder + txtFileName))
                    {
                        arrRet.Add("low_card");
                    }

                    // Check End Card
                    txtFileName = "END_CARD_" + _device.comPort + ".txt";
                    if (File.Exists(messageStorageFolder + txtFileName))
                    {
                        arrRet.Add("end_card");
                    }

                    // Check Card Jam
                    txtFileName = "CARD_JAM_" + _device.comPort + ".txt";
                    if (File.Exists(messageStorageFolder + txtFileName))
                    {
                        arrRet.Add("card_jam");
                    }

                    // Check End Card
                    txtFileName = "BIN_FULL_" + _device.comPort + ".txt";
                    if (File.Exists(messageStorageFolder + txtFileName))
                    {
                        arrRet.Add("bin_full");
                    }

                    // Port Cannot Open
                    txtFileName = "PORT_CANNOT_OPEN_" + _device.comPort + ".txt";
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

                            if (s.ToLower() == "low_card")
                                raw.Message = "LOW_CARD";

                            else if (s.ToLower() == "end_card")
                                raw.Message = "END_CARD";

                            else if (s.ToLower() == "CARD_JAM")
                                raw.Message = "CARD_JAM";

                            else if (s.ToLower() == "bin_full")
                                raw.Message = "BIN_FULL";

                            else if (s.ToLower() == "port_cannot_open")
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
