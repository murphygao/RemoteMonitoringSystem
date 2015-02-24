using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Monitoring.Device.ATMCardReader
{
    public class ATMCardReader : Device
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="model"></param>
        /// <param name="deviceManagerName">Device's name is shown in Device Manager.</param>
        /// <param name="deviceManagerID"></param>
        public ATMCardReader(string brand, string model, string deviceManagerName, string deviceManagerID, bool useCOMPort, string comPort)
            : base(brand, model, deviceManagerName, deviceManagerID, useCOMPort, comPort)
        {
        }
    }
}
