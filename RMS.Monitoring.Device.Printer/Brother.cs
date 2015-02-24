
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using RMS.Common.Exception;
using RMS.Monitoring.Helper;

namespace RMS.Monitoring.Device.Printer
{
    public class Brother : Printer
    {
        public Brother(string model, string deviceManagerName, string deviceManagerID, string printerName, bool useCOMPort)
            : base("Brother", model, deviceManagerName, deviceManagerID, printerName, useCOMPort)
        {
        }

        public Brother(string model, string deviceManagerName, string deviceManagerID, string printerName, bool useCOMPort, string comPort)
            : base("Brother", model, deviceManagerName, deviceManagerID, printerName, useCOMPort, comPort)
        {
        }

        public override string[] CheckPaperStatus()
        {
            int counter = 3;

            do
            {
                try
                {
                    string sVid = deviceManagerID.Substring(deviceManagerID.ToUpper().IndexOf("VID_") + 4, 4);
                    int iVid = Int32.Parse(sVid, NumberStyles.HexNumber);
                    string sPid = deviceManagerID.Substring(deviceManagerID.ToUpper().IndexOf("PID_") + 4, 4);
                    int iPid = Int32.Parse(sPid, NumberStyles.HexNumber);

                    string pjl = "";
                    pjl += (char)27 + "%-12345X@PJL " + Environment.NewLine;
                    pjl += "@PJL INFO STATUS " + Environment.NewLine;
                    pjl += (char)27 + "%-12345X ";
                    byte[] b1 = System.Text.Encoding.ASCII.GetBytes(pjl);

                    string strResult = string.Empty;
                    try
                    {
                        strResult = USBBrotherA4Printer.WriteAndRead(iVid, iPid, b1);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.IndexOf("Device Not Found") > -1)
                        {
                            //Try to install USB Filter
                            Helper.USBFilter.InstallUSBFilter(sVid, sPid);

                            //Then re-try check paper
                            strResult = USBBrotherA4Printer.WriteAndRead(iVid, iPid, b1);
                        }
                    }
                    List<string> strRet = new List<string>();

                    if (string.IsNullOrEmpty(strResult) || strResult.IndexOf("PJL INFO STATUS") < 0)
                    {
                        // need to check printer status again
                    }
                    else
                    {
                        if (strResult.ToUpper().IndexOf("NO PAPER") > -1)
                        {
                            strRet.Add("end_paper");
                        }
                        if (strResult.ToUpper().IndexOf("JAM ") > -1)
                        {
                            strRet.Add("paper_jam");
                        }
                        if (strResult.ToUpper().IndexOf("TONER LOW") > -1)
                        {
                            strRet.Add("low_toner");
                        }
                        if (strResult.ToUpper().IndexOf("TONER ENDED") > -1 || strResult.ToUpper().IndexOf("REPLACE TONER") > -1)
                        {
                            strRet.Add("end_toner");
                        }
                        if (strResult.ToUpper().IndexOf("DRUM END SOON") > -1)
                        {
                            strRet.Add("low_drum");
                        }
                        if (strResult.ToUpper().IndexOf("DRUM STOP") > -1 || strResult.ToUpper().IndexOf("REPLACE DRUM") > -1)
                        {
                            strRet.Add("end_drum");
                        }
                        if (strResult.ToUpper().IndexOf("DRUM ERROR") > -1)
                        {
                            strRet.Add("drum_error");
                        }
                        if (strResult.ToUpper().IndexOf("COVER OPEN") > -1)
                        {
                            strRet.Add("cover_open");
                        }

                        // ถ้า strRet.Count == 0 แสดงว่า ไม่พบ error ใดตาม message ที่กำหนดไว้
                        // ให้ตรวจสอบดูว่า printer ยังพร้อมใช้งานอยู่หรือไม่
                        if (strRet.Count == 0)
                        {
                            if (strResult.ToUpper().IndexOf("SLEEP") > -1 || strResult.ToUpper().IndexOf("READY") > -1 ||
                                strResult.ToUpper().IndexOf("PRINTING") > -1 || strResult.ToUpper().IndexOf("WARMING UP") > -1 ||
                                strResult.ToUpper().IndexOf("COOLING DOWN") > -1 || strResult.ToUpper().IndexOf("RECEIVING DATA") > -1)
                            {
                                // printer ปกติ
                                strRet.Add("ok");

                                // เพิ่ม Message Status กรณี OK
                                if (strResult.ToUpper().IndexOf("SLEEP") > -1)
                                    strRet.Add("sleep");
                                if (strResult.ToUpper().IndexOf("WARMING UP") > -1)
                                    strRet.Add("warming_up");
                                if (strResult.ToUpper().IndexOf("PRINTING") > -1)
                                    strRet.Add("printing");

                                return strRet.ToArray();
                            }
                            else
                            {
                                strRet.Add("printer_error");
                            }
                        }
                        else
                        {
                            // เพิ่ม Message Status กรณี NOT OK
                            
                            if (strResult.ToUpper().IndexOf("SLEEP") > -1)
                                strRet.Add("sleep");
                            if (strResult.ToUpper().IndexOf("WARMING UP") > -1)
                                strRet.Add("warming_up");
                            if (strResult.ToUpper().IndexOf("PRINTING") > -1)
                                strRet.Add("printing");
                        }

                        return strRet.ToArray();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Brother CheckPaperStatus while loop failed. " + ex.Message, ex);
                }

                counter--;
                if (counter > 0)
                    Thread.Sleep(1500);
            } while (counter > 0);

            return null;
        }

    }
}
