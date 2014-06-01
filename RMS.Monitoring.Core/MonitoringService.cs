using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Agent.Proxy.ClientProxy;
using RMS.Agent.Proxy.MonitoringProxy;
using RMS.Common.Exception;
using RMS.Monitoring.Device.CardDespenser;
using RMS.Monitoring.Device.Keyboard;
using RMS.Monitoring.Device.MonitorDisplay;
using RMS.Monitoring.Device.PerformanceCounter;
using RMS.Monitoring.Device.SignaturePad;
using RMS.Monitoring.Device.SmartCardReader;
using RMS.Monitoring.Device.ThermalPrinter;
using RMS.Monitoring.Device.WebCamera;
using RMS.Monitoring.Helper;

namespace RMS.Monitoring.Core
{
    public class MonitoringService
    {
        public List<RmsReportMonitoringRaw> Monitoring(string mType, ClientResult clientResult)
        {
            try
            {
                var ret = new List<RmsReportMonitoringRaw>();
                if (string.IsNullOrEmpty(mType))
                    return ret;
                if (clientResult == null)
                    return ret;

                if (mType.ToLower() == "performance")
                {
                    var ps = new PerformanceService();
                    return ps.Monitoring(clientResult);
                }
                else if (mType.ToLower() == "device")
                {
                    ret = CheckDevice(clientResult);
                }
                else
                {
                    throw new Exception("Monitoring Type Not Match. mType=" + mType);
                }


                return ret;
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "Monitoring failed. " + ex.Message, ex, false);
            }
        }

        private List<RmsReportMonitoringRaw> CheckDevice(ClientResult clientResult)
        {
            try
            {
                List<RmsReportMonitoringRaw> lRmsReportMonitoringRaws = new List<RmsReportMonitoringRaw>();

                var listMonitoringProfileDevices = clientResult.ListMonitoringProfileDevices.OrderBy(o => o.DeviceId);
                foreach (var mpd in listMonitoringProfileDevices)
                {
                    ClientResult cr = new ClientResult();

                    cr.Client = clientResult.Client;

                    cr.ListMonitoringProfileDevices = new List<RmsMonitoringProfileDevice>();
                    cr.ListMonitoringProfileDevices.Add(mpd);
                
                    // Get DeviceTypeId
                    var device = clientResult.ListDevices.FirstOrDefault(d => d.DeviceId == mpd.DeviceId);

                    cr.ListDevices = new List<RmsDevice>();
                    cr.ListDevices.Add(device);

                    if (device != null)
                    {
                        int? deviceTypeID = device.DeviceTypeId;
                        var deviceType = clientResult.ListDeviceType.FirstOrDefault(dt => dt.DeviceTypeId == deviceTypeID);

                        cr.ListDeviceType = new List<RmsDeviceType>();
                        cr.ListDeviceType.Add(deviceType);

                        if (deviceType != null)
                        {
                            string deviceTypeCode = deviceType.DeviceTypeCode;


                            if (deviceTypeCode == Models.DeviceCode.ATMCardReader)
                            {
                            

                            }
                            else if (deviceTypeCode == Models.DeviceCode.BarcodeReader)
                            {


                            }
                            else if (deviceTypeCode == Models.DeviceCode.CardDispenser)
                            {
                                var ds = new CardDispenserService(device.Brand, device.Model, mpd.DeviceManagerName, mpd.DeviceManagerId, cr);
                                var rmsReportMonitoringRaws = ds.Monitoring();
                                lRmsReportMonitoringRaws.AddRange(rmsReportMonitoringRaws);
                            }
                            else if (deviceTypeCode == Models.DeviceCode.ElectronicSignaturePad)
                            {
                                var ds = new SignaturePadService(device.Brand, device.Model, mpd.DeviceManagerName, mpd.DeviceManagerId, cr);
                                var rmsReportMonitoringRaws = ds.Monitoring();
                                lRmsReportMonitoringRaws.AddRange(rmsReportMonitoringRaws);
                            }
                            else if (deviceTypeCode == Models.DeviceCode.Keyboard)
                            {
                                var ds = new KeyboardService(device.Brand, device.Model, mpd.DeviceManagerName, mpd.DeviceManagerId, cr);
                                var rmsReportMonitoringRaws = ds.Monitoring();
                                lRmsReportMonitoringRaws.AddRange(rmsReportMonitoringRaws);
                            }
                            else if (deviceTypeCode == Models.DeviceCode.MonitorDisplay)
                            {
                                var ds = new MonitorDisplayService(device.Brand, device.Model, mpd.DeviceManagerName, mpd.DeviceManagerId, cr);
                                var rmsReportMonitoringRaws = ds.Monitoring();
                                lRmsReportMonitoringRaws.AddRange(rmsReportMonitoringRaws);
                            }
                            else if (deviceTypeCode == Models.DeviceCode.SmartcardReader)
                            {
                                var ds = new SmartCardReaderService(device.Brand, device.Model, mpd.DeviceManagerName, mpd.DeviceManagerId, cr);
                                var rmsReportMonitoringRaws = ds.Monitoring();
                                lRmsReportMonitoringRaws.AddRange(rmsReportMonitoringRaws);

                            }
                            else if (deviceTypeCode == Models.DeviceCode.ThermalPrinter)
                            {
                                var ds = new ThermalPrinterService(device.Brand, device.Model, mpd.DeviceManagerName, mpd.DeviceManagerId, cr);
                                var rmsReportMonitoringRaws = ds.Monitoring();
                                lRmsReportMonitoringRaws.AddRange(rmsReportMonitoringRaws);

                            }
                            else if (deviceTypeCode == Models.DeviceCode.WebCamera)
                            {
                                var ds = new WebCameraService(device.Brand, device.Model, mpd.DeviceManagerName, mpd.DeviceManagerId, cr);
                                var rmsReportMonitoringRaws = ds.Monitoring();
                                lRmsReportMonitoringRaws.AddRange(rmsReportMonitoringRaws);
                            }


                        }
                    }
                }
                return lRmsReportMonitoringRaws;
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "CheckDevice failed. " + ex.Message, ex, false);
            }
        }
    }
}
