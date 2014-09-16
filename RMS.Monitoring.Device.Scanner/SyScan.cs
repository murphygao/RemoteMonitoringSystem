﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Monitoring.Device.Scanner
{
    public class Syscan : Scanner
    {
        public Syscan(string model, string deviceManagerName, string deviceManagerID)
            : base("Syscan", model, deviceManagerName, deviceManagerID)
        {
        }

        public override int CheckDeviceManager()
        {
            return base.CheckDeviceManager();
        }
    }
}
