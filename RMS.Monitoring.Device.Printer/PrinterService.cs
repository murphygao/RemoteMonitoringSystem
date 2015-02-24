using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using RMS.Agent.Proxy.ClientProxy;
using RMS.Agent.Proxy.MonitoringProxy;
using RMS.Common.Exception;

namespace RMS.Monitoring.Device.Printer
{
    public class PrinterService
    {
        private Printer _device;
        private ClientResult clientResult;
        private static int[] counterRelay = new int[2] { 0, 0 };

        public PrinterService(string brand, string model, string deviceManagerName, string deviceManagerID, string printerName, bool useCOMPort, ClientResult clientResult) : this(brand, model, deviceManagerName, deviceManagerID, printerName, useCOMPort, "", clientResult) { }

        public PrinterService(string brand, string model, string deviceManagerName, string deviceManagerID, string printerName, bool useCOMPort, string portName, ClientResult clientResult)
        {
            try
            {
                this.clientResult = clientResult;

                if (brand.ToLower() == "brother") _device = new Brother(model, deviceManagerName, deviceManagerID, printerName, useCOMPort, portName);
                else if (brand.ToLower() == "winpos") _device = new WinPOS(model, deviceManagerName, deviceManagerID, printerName, useCOMPort, portName);
                else
                    _device = new Printer(brand, model, deviceManagerName, deviceManagerID, printerName, useCOMPort, portName);
                    //throw new Exception("Brand Not Found. brand=" + brand);
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
                    bool checkA4PaperViaTextFile = (ConfigurationManager.AppSettings["RMS.CheckA4PaperViaTextFile"] != null && Convert.ToBoolean(ConfigurationManager.AppSettings["RMS.CheckA4PaperViaTextFile"]));
                    List<string> arrRet = null;

                    if (checkA4PaperViaTextFile)
                    {
                        string messageStorageFolder = ConfigurationManager.AppSettings["RMS.MessageStorageFolder"];
                        messageStorageFolder = (messageStorageFolder.EndsWith(@"\")) ? messageStorageFolder : messageStorageFolder + @"\";
                        arrRet = new List<string>();

                        string txtFileName = string.Empty;
                        // Check Low Paper
                        txtFileName = "LOW_PAPER_" + _device.brand + ".txt";
                        if (File.Exists(messageStorageFolder + txtFileName))
                        {
                            arrRet.Add("low_paper");
                        }

                        // Check End Paper
                        txtFileName = "END_PAPER_" + _device.brand + ".txt";
                        if (File.Exists(messageStorageFolder + txtFileName))
                        {
                            arrRet.Add("end_paper");
                        }

                        // Check Paper Jam
                        txtFileName = "PAPER_JAM_" + _device.brand + ".txt";
                        if (File.Exists(messageStorageFolder + txtFileName))
                        {
                            arrRet.Add("paper_jam");
                        }

                        // Check Low Toner
                        txtFileName = "LOW_TONER_" + _device.brand + ".txt";
                        if (File.Exists(messageStorageFolder + txtFileName))
                        {
                            arrRet.Add("low_toner");
                        }

                        // Check End Toner
                        txtFileName = "END_TONER_" + _device.brand + ".txt";
                        if (File.Exists(messageStorageFolder + txtFileName))
                        {
                            arrRet.Add("end_toner");
                        }

                        // Check Low Drum
                        txtFileName = "LOW_DRUM_" + _device.brand + ".txt";
                        if (File.Exists(messageStorageFolder + txtFileName))
                        {
                            arrRet.Add("low_drum");
                        }

                        // Check End Drum
                        txtFileName = "END_DRUM_" + _device.brand + ".txt";
                        if (File.Exists(messageStorageFolder + txtFileName))
                        {
                            arrRet.Add("end_drum");
                        }

                        // Check Drum Error
                        txtFileName = "DRUM_ERROR_" + _device.brand + ".txt";
                        if (File.Exists(messageStorageFolder + txtFileName))
                        {
                            arrRet.Add("drum_error");
                        }

                        // Check Drum Error
                        txtFileName = "DRUM_ERROR_" + _device.brand + ".txt";
                        if (File.Exists(messageStorageFolder + txtFileName))
                        {
                            arrRet.Add("cover_oepn");
                        }

                        // Check Printer Error
                        txtFileName = "PRINTER_ERROR_" + _device.brand + ".txt";
                        if (File.Exists(messageStorageFolder + txtFileName))
                        {
                            arrRet.Add("printer_error");
                        }

                    }
                    else
                    {
                        try
                        {
                            string[] checkPaperStatus = _device.CheckPaperStatus();
                            if (checkPaperStatus != null)
                                arrRet = new List<string>(checkPaperStatus);
                        }
                        catch (Exception ex)
                        {
                            new RMSAppException(this, "500", "CheckPaperStatus failed. " + ex.Message, ex, true);
                        }
                    }

                    if (arrRet != null)
                    {
                        foreach (var s in arrRet)
                        {
                            RmsReportMonitoringRaw raw = new RmsReportMonitoringRaw();
                            raw.ClientCode = clientResult.Client.ClientCode;
                            raw.DeviceCode = clientResult.ListDevices[0].DeviceCode;

                            if (s.ToLower() == "low_paper")
                                raw.Message = "LOW_PAPER";

                            else if (s.ToLower() == "end_paper")
                                raw.Message = "END_PAPER";

                            else if (s.ToLower() == "paper_jam")
                                raw.Message = "PAPER_JAM";

                            else if (s.ToLower() == "low_toner")
                                raw.Message = "LOW_TONER";

                            else if (s.ToLower() == "end_toner")
                                raw.Message = "END_TONER";

                            else if (s.ToLower() == "low_drum")
                                raw.Message = "LOW_DRUM";

                            else if (s.ToLower() == "end_drum")
                                raw.Message = "END_DRUM";

                            else if (s.ToLower() == "drum_error")
                                raw.Message = "DRUM_ERROR";

                            else if (s.ToLower() == "cover_open")
                                raw.Message = "COVER_OPEN";

                            else if (s.ToLower() == "printer_error")
                                raw.Message = "PRINTER_ERROR";

                            else
                                continue;

                            raw.MessageDateTime = DateTime.Now;
                            raw.MonitoringProfileDeviceId = clientResult.ListMonitoringProfileDevices[0].MonitoringProfileDeviceId;

                            lRmsReportMonitoringRaws.Add(raw);
                        }



                        try
                        {
                            var NumOfQueues = _device.CheckPrintQueueStatus(12);
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
                        catch (Exception ex)
                        {
                            //Console.WriteLine(ex);
                        }
                    }

                    // ถ้าไม่มี error msg ใน list และพบว่าเกิด error ระหว่างการตรวจสอบ paper status
                    // จะทำอย่างไร? ยังต้องส่ง OK กลับไป server หรือไม่?

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
