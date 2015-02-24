using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Monitoring.Helper;

namespace RMS.Monitoring.Device.ThermalPrinter
{
    public class CUSTOM : ThermalPrinter
    {
        public CUSTOM(string model, string deviceManagerName, string deviceManagerID)
            : base("CUSTOM", model, deviceManagerName, deviceManagerID)
        {
        }

        public override int CheckDeviceManager()
        {
            return base.CheckDeviceManager();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Return Paper Status -> 
        /// int[0] = Near End, 
        /// int[1] = Out of Paper
        /// int[2] = Paper Jam
        /// int[3] = Ticket not present in output
        /// int[4] = Cutter error
        /// 
        /// -1 ตรวจสอบไม่ได้
        /// 0 ปกติ
        /// >0  ไม่ปกติ
        /// </returns>
        public override int[] CheckPaperStatus()
        {
            try
            {
                string sVid = deviceManagerID.Substring(deviceManagerID.ToUpper().IndexOf("VID_") + 4, 4);
                int iVid = Int32.Parse(sVid, NumberStyles.HexNumber);
                string sPid = deviceManagerID.Substring(deviceManagerID.ToUpper().IndexOf("PID_") + 4, 4);
                int iPid = Int32.Parse(sPid, NumberStyles.HexNumber);

                byte[] byteArray = new byte[3];
                byteArray[0] = 0x10;
                byteArray[1] = 0x04;
                byteArray[2] = 20;

                byte[] read = null;

                try
                {
                    read = USBCustomThermalPrinter.WriteAndRead(iVid, iPid, byteArray);
                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("Device Not Found") > -1)
                    {
                        //Try to install USB Filter
                        Helper.USBFilter.InstallUSBFilter(sVid, sPid);

                        //Then re-try check paper
                        read = USBCustomThermalPrinter.WriteAndRead(iVid, iPid, byteArray);
                    }
                }

                if (read == null || read.Length == 0)
                    return new int[] {-1, -1, -1, -1, -1};

                int[] ret = new int[5] {0, 0, 0, 0, 0};

                // ตรวจสอบ Near End - Byte ที่ 3, Bit ที่ 2
                if ((read[2] & (1 << 2)) != 0)
                {
                    ret[0] = 1;
                }

                // ตรวจสอบ Out of Paper - Byte ที่ 3, Bit ที่ 0
                if ((read[2] & (1 << 0)) != 0)
                {
                    ret[1] = 1;
                }

                // ตรวจสอบ Paper Jam - Byte ที่ 5, Bit ที่ 6
                if ((read[4] & (1 << 6)) != 0)
                {
                    ret[2] = 1;
                }

                // ตรวจสอบ Ticket not present in output - Byte ที่ 5, Bit ที่ 6
                if ((read[2] & (1 << 6)) != 0)
                {
                    ret[3] = 1;
                }

                // ตรวจสอบ Cutter Error - Byte ที่ 6, Bit ที่ 0
                if ((read[5] & (1 << 0)) != 0)
                {
                    ret[4] = 1;
                }

                return ret;

            }
            catch (Exception ex)
            {
                throw new Exception("CheckPaperStatus failed. " + ex.Message, ex);
            }

        }
    }
}
