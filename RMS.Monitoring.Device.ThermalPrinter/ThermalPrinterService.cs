using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Agent.Proxy.ClientProxy;
using RMS.Agent.Proxy.MonitoringProxy;
using RMS.Common.Exception;

namespace RMS.Monitoring.Device.ThermalPrinter
{
    public class ThermalPrinterService
    {
        private ThermalPrinter _device;
        private ClientResult clientResult;

        public ThermalPrinterService(string brand, string model, string deviceManagerName, string deviceManagerID, ClientResult clientResult)
        {
            try
            {
                this.clientResult = clientResult;

                if (brand.ToLower() == "custom") _device = new CUSTOM(model, deviceManagerName, deviceManagerID);
                else
                    throw new Exception("Brand Not Found. brand=" + brand);

            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "ThermalPrinterService failed. " + ex.Message, ex, false);
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
                    int[] arrRet = null;
                    try
                    {
                        // 25AUG14 Modified by Sethawat Th. 
                        // ถ้าไม่มี คิว ใน pool สามารถทำการตรวจสอบ printer status ได้
                        if (_device.CheckPrintQueueStatus(0) == 0)
                        {
                            arrRet = _device.CheckPaperStatus();
                        }
                        else
                        {
                            // กรณีมี คิว อยู่ใน pool

                            int _checkQueueCounter = 0;
                            
                            // วนรอไม่เกิน 5 รอบ หรือจนกว่าไม่มีคิวใน print pool
                            while (_device.CheckPrintQueueStatus(0) > 0 && _checkQueueCounter < 5)
                            {
                                System.Threading.Thread.Sleep(1500);
                                _checkQueueCounter++;
                            }

                            // ตรวจสอบซ้ำอีกรอบ หากมีคิวนานเกิน 7 วินาที แสดงว่า คิวน่าจะค้างอยู่
                            if (_device.CheckPrintQueueStatus(7) == _device.CheckPrintQueueStatus(0))
                            {
                                arrRet = _device.CheckPaperStatus();
                            }
                        }

                        // กรณีที่ได้ผลลัพธ์เป็น 500 ให้ลอง CheckPaperStatus อีกครั้ง
                        if (arrRet != null && arrRet.Length > 0 && arrRet[0] == 500)
                        {
                            System.Threading.Thread.Sleep(1500);
                            arrRet = _device.CheckPaperStatus();
                        }
                    }
                    catch (Exception ex)
                    {
                        new RMSWebException(this, "500", "CheckPaperStatus failed. " + ex.Message, ex, true);
                    }

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
                    }
                    else
                    {
                        RmsReportMonitoringRaw raw = new RmsReportMonitoringRaw();
                        raw.ClientCode = clientResult.Client.ClientCode;
                        raw.DeviceCode = clientResult.ListDevices[0].DeviceCode;

                        raw.Message = "DEVICE_NOT_READY";

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
