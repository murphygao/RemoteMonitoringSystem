using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Monitoring.Device.ThermalPrinter;

namespace RMS.Monitoring.API
{
    public class Brother
    {
        /// <summary>
        /// ตรวจสอบกระดาษ - Brother Printer
        /// </summary>
        /// <param name="model">Printer Model</param>
        /// <param name="vid">VID value</param>
        /// <param name="pid">PID value</param>
        /// <returns>สถานะกระกาษและปริ๊นเตอร์ในรูปแบบ array. 
        /// กรณีปกติ จะได้ ok 
        /// กรณีผิดปกติ จะเป็นข้อความอื่นๆ 
        /// กรณีตรวจสอบไม่ได้ จะไม่มีข้อความใดๆ</returns>
        public string[] CheckPaperStatus(string model, string vid, string pid)
        {
            try
            {
                string deviceID = string.Format("VID_{0}&PID_{1}", vid, pid);
                Device.Printer.Brother printer = new Device.Printer.Brother(model, "", deviceID, "", false);
                string[] paperStatus = printer.CheckPaperStatus();
                return paperStatus;
            }
            catch (Exception ex)
            {
                throw new Exception("Brother - CheckPaperStatus failed. " + ex.Message, ex);
            }
            finally
            {
                USBFilter.ReinstallUSBFilter();
            }
        }
    }

    public class Custom
    {
        /// <summary>
        /// ตรวจสอบกระดาษ - Custom Thermal Printer
        /// </summary>
        /// <param name="model">Printer Model</param>
        /// <param name="vid">VID value</param>
        /// <param name="pid">PID value</param>
        /// <returns>สถานะกระกาษและปริ๊นเตอร์ในรูปแบบ array. 
        /// กรณีปกติ จะได้ ok 
        /// กรณีผิดปกติ จะเป็นข้อความอื่นๆ 
        /// กรณีตรวจสอบไม่ได้ จะไม่มีข้อความใดๆ</returns>
        public string[] CheckPaperStatus(string model, string vid, string pid)
        {
            try
            {
                string deviceID = string.Format("VID_{0}&PID_{1}", vid, pid);
                CUSTOM printer = new CUSTOM(model, "", deviceID);
                int[] arrRet = printer.CheckPaperStatus();

                List<string> paperStatus = new List<string>();

                if (arrRet != null)
                {
                    if (arrRet.Length >= 1 && arrRet[0] > 0)
                    {
                        paperStatus.Add("low_paper");
                    }

                    if (arrRet.Length >= 2 && arrRet[1] > 0)
                    {
                        paperStatus.Add("end_paper");
                    }

                    if (arrRet.Length >= 3 && arrRet[2] > 0)
                    {
                        paperStatus.Add("paper_jam");
                    }

                    if (arrRet.Length >= 4 && arrRet[3] > 0)
                    {
                        paperStatus.Add("ticket_not_present");
                    }

                    if (arrRet.Length >= 5 && arrRet[4] > 0)
                    {
                        paperStatus.Add("cuter_error");
                    }

                    if (paperStatus.Count == 0) paperStatus.Add("ok");

                }

                return paperStatus.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception("Custom - CheckPaperStatus failed. " + ex.Message, ex);
            }
            finally
            {
                USBFilter.ReinstallUSBFilter();
            }
        }
    }

    public class USBFilter
    {
        private static DateTime startTime = new DateTime();

        public static void ReinstallUSBFilter()
        {
            if (startTime.AddMinutes(15) > DateTime.Now)
                Helper.USBFilter.ReinstallUSBFilter();
        }
        public static void ReinstallUSBFilter(bool force)
        {
            if (force || startTime.AddMinutes(15) > DateTime.Now)
                Helper.USBFilter.ReinstallUSBFilter();
        }
    }
}
