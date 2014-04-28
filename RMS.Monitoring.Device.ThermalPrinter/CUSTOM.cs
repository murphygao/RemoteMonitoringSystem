﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override int[] CheckPaperStatus()
        {
            string sVid = deviceManagerID.Substring(deviceManagerID.ToUpper().IndexOf("VID_") + 4, 4);
            int iVid = Int32.Parse(sVid, System.Globalization.NumberStyles.HexNumber);
            string sPid = deviceManagerID.Substring(deviceManagerID.ToUpper().IndexOf("PID_") + 4, 4);
            int iPid = Int32.Parse(sPid, System.Globalization.NumberStyles.HexNumber);

            byte[] byteArray = new byte[3];
            byteArray[0] = 0x10;
            byteArray[1] = 0x04;
            byteArray[2] = 4;

            byte[] read = Helper.USB.WriteAndRead(iVid, iPid, byteArray);

            if (read == null || read.Length == 0)
                return new int[]{500, 500};

            int[] ret = new int[2]{0, 0};
            var bit2 = (read[0] & (1 << 2)) != 0; // Near End
            var bit3 = (read[0] & (1 << 3)) != 0; // Near End
            var bit5 = (read[0] & (1 << 5)) != 0; // Out of Paper
            var bit6 = (read[0] & (1 << 6)) != 0; // Out of Paper

            if (bit2 || bit3)
                ret[0] = 1;
            if (bit5 || bit6)
                ret[1] = 1;

            return ret;
        }
    }
}
