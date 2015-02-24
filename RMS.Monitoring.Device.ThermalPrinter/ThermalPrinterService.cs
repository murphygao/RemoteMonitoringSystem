using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
        private static int[] counterRelay = new int[2]{0, 0};

        public ThermalPrinterService(string brand, string model, string deviceManagerName, string deviceManagerID, ClientResult clientResult)
        {
            try
            {
                this.clientResult = clientResult;

                if (brand.ToLower() == "custom") _device = new CUSTOM(model, deviceManagerName, deviceManagerID);
                else
                    _device = new ThermalPrinter(brand, model, deviceManagerName, deviceManagerID);
                    //throw new Exception("Brand Not Found. brand=" + brand);

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

                if (ret == -1)
                {
                    // ถ้า Device Not Found ให้หน่วงแล้วลอง check device manager อีกครั้ง
                    System.Threading.Thread.Sleep(1500);
                    ret = _device.CheckDeviceManager();
                }

                if (ret != 0)
                {
                    RmsReportMonitoringRaw raw = new RmsReportMonitoringRaw();
                    raw.ClientCode = clientResult.Client.ClientCode;
                    raw.DeviceCode = clientResult.ListDevices[0].DeviceCode;
                    raw.MessageDateTime = DateTime.Now;
                    raw.MonitoringProfileDeviceId = clientResult.ListMonitoringProfileDevices[0].MonitoringProfileDeviceId;

                    if (ret == -1)
                    {
                        raw.Message = "DEVICE_NOT_FOUND";
                        counterRelay[0]++;

                        if (counterRelay[0] >= 2) //ถ้าพบ Device Not Found ตั้งแต่ 2 ครั้งติดกันขึ้นไป จึงส่ง Issue Message ไปยัง Server
                        {
                            lRmsReportMonitoringRaws.Add(raw);
                            counterRelay[0] = 0; //ทำการ reset Not Found counter ค่าเป็น 0 เพราะส่ง Issue Message แจ้งเตือนแล้ว
                        }
                        else // ถ้าเจอ Device Not Found 1 ครั้ง ให้จบการทำงานได้เลย
                        {
                            return lRmsReportMonitoringRaws;
                        }
                    }
                    else
                    {
                        counterRelay[0] = 0; //ทำการ reset Not Found counter ค่าเป็น 0 เพราะเจออุปกรณ์แล้ว

                        raw.Message = "DEVICE_NOT_READY";
                        lRmsReportMonitoringRaws.Add(raw);
                    }

                }
                else
                {
                    counterRelay[0] = 0; //ทำการ reset Not Found counter ค่าเป็น 0 เพราะเจออุปกรณ์แล้ว

                    int[] arrRet = null;
                    bool checkThermalPaperViaTextFile = (ConfigurationManager.AppSettings["RMS.CheckThermalPaperViaTextFile"] != null &&
                                                         Convert.ToBoolean(ConfigurationManager.AppSettings["RMS.CheckThermalPaperViaTextFile"]));
                    try
                    {

                        if (checkThermalPaperViaTextFile) // ถ้าตรวจสอบ paper status จาก text file, สามารถเข้าไปตรวจสอบได้ทันที
                        {
                            //arrRet = _device.CheckPaperStatus();

                            var _ret = new int[] { 0, 0, 0, 0, 0 };
                            string messageStorageFolder = ConfigurationManager.AppSettings["RMS.MessageStorageFolder"];
                            messageStorageFolder = (messageStorageFolder.EndsWith(@"\")) ? messageStorageFolder : messageStorageFolder + @"\";

                            // Check Low Paper
                            string lowPaper = "LOW_PAPER_" + _device.brand + ".txt";
                            if (File.Exists(messageStorageFolder + lowPaper))
                            {
                                _ret[0] = 1;
                            }

                            // Check End Paper
                            string endPaper = "END_PAPER_" + _device.brand + ".txt";
                            if (File.Exists(messageStorageFolder + endPaper))
                            {
                                _ret[1] = 1;
                            }

                            // Check Paper Jam
                            string paperJam = "PAPER_JAM_" + _device.brand + ".txt";
                            if (File.Exists(messageStorageFolder + paperJam))
                            {
                                _ret[2] = 1;
                            }

                            // Check Ticket Not Present
                            string ticketNotPresent = "TICKET_NOT_PRESENT_" + _device.brand + ".txt";
                            if (File.Exists(messageStorageFolder + ticketNotPresent))
                            {
                                _ret[3] = 1;
                            }

                            // Check Cutter Error
                            string cutterError = "CUTTER_ERROR_" + _device.brand + ".txt";
                            if (File.Exists(messageStorageFolder + cutterError))
                            {
                                _ret[4] = 1;
                            }

                            arrRet = _ret;

                        }
                        else // ถ้าตรวจสอบ paper status ผ่าน usb, ต้องทำการ check printer pool เสียก่อน
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

                                // ตรวจสอบซ้ำอีกรอบ หากมีคิวนานเกิน 7 วินาที แสดงว่า คิวน่าจะค้างอยู่
                                if (_device.CheckPrintQueueStatus(7) == _device.CheckPrintQueueStatus(0))
                                {
                                    arrRet = _device.CheckPaperStatus();
                                }
                                else
                                {
                                    // วนรอไม่เกิน 5 รอบ หรือจนกว่าไม่มีคิวใน print pool
                                    while (_device.CheckPrintQueueStatus(0) > 0 && _checkQueueCounter < 5)
                                    {
                                        Thread.Sleep(1500);
                                        _checkQueueCounter++;
                                    }
                                }
                            }

                            // กรณีที่ได้ผลลัพธ์เป็น 500 ให้ลอง CheckPaperStatus อีกครั้ง
                            if (arrRet != null && arrRet.Length > 0 && arrRet[0] == 500)
                            {
                                Thread.Sleep(1500);
                                arrRet = _device.CheckPaperStatus();
                            }                        
                        }

                    }
                    catch (Exception ex)
                    {
                        new RMSWebException(this, "500", "CheckPaperStatus failed. " + ex.Message, ex, true);
                    }

                    if (arrRet != null)
                    {
                        if (arrRet.Length >= 1 && arrRet[0] > 0)
                        {
                            RmsReportMonitoringRaw raw = new RmsReportMonitoringRaw();
                            raw.ClientCode = clientResult.Client.ClientCode;
                            raw.DeviceCode = clientResult.ListDevices[0].DeviceCode;

                            raw.Message = "LOW_PAPER";

                            raw.MessageDateTime = DateTime.Now;
                            raw.MonitoringProfileDeviceId = clientResult.ListMonitoringProfileDevices[0].MonitoringProfileDeviceId;

                            lRmsReportMonitoringRaws.Add(raw);
                        }

                        if (arrRet.Length >= 2 && arrRet[1] > 0)
                        {
                            RmsReportMonitoringRaw raw = new RmsReportMonitoringRaw();
                            raw.ClientCode = clientResult.Client.ClientCode;
                            raw.DeviceCode = clientResult.ListDevices[0].DeviceCode;

                            raw.Message = "END_PAPER";

                            raw.MessageDateTime = DateTime.Now;
                            raw.MonitoringProfileDeviceId = clientResult.ListMonitoringProfileDevices[0].MonitoringProfileDeviceId;

                            lRmsReportMonitoringRaws.Add(raw);
                        }

                        if (arrRet.Length >= 3 && arrRet[2] > 0)
                        {
                            RmsReportMonitoringRaw raw = new RmsReportMonitoringRaw();
                            raw.ClientCode = clientResult.Client.ClientCode;
                            raw.DeviceCode = clientResult.ListDevices[0].DeviceCode;

                            raw.Message = "PAPER_JAM";

                            raw.MessageDateTime = DateTime.Now;
                            raw.MonitoringProfileDeviceId = clientResult.ListMonitoringProfileDevices[0].MonitoringProfileDeviceId;

                            lRmsReportMonitoringRaws.Add(raw);
                        }

                        if (arrRet.Length >= 4 && arrRet[3] > 0)
                        {
                            RmsReportMonitoringRaw raw = new RmsReportMonitoringRaw();
                            raw.ClientCode = clientResult.Client.ClientCode;
                            raw.DeviceCode = clientResult.ListDevices[0].DeviceCode;

                            raw.Message = "TICKET_NOT_PRESENT";

                            raw.MessageDateTime = DateTime.Now;
                            raw.MonitoringProfileDeviceId = clientResult.ListMonitoringProfileDevices[0].MonitoringProfileDeviceId;

                            lRmsReportMonitoringRaws.Add(raw);
                        }

                        if (arrRet.Length >= 5 && arrRet[4] > 0)
                        {
                            RmsReportMonitoringRaw raw = new RmsReportMonitoringRaw();
                            raw.ClientCode = clientResult.Client.ClientCode;
                            raw.DeviceCode = clientResult.ListDevices[0].DeviceCode;

                            raw.Message = "CUTTER_ERROR";

                            raw.MessageDateTime = DateTime.Now;
                            raw.MonitoringProfileDeviceId = clientResult.ListMonitoringProfileDevices[0].MonitoringProfileDeviceId;

                            lRmsReportMonitoringRaws.Add(raw);
                        }

                    }
                    else if (arrRet == null && checkThermalPaperViaTextFile)
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
