using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Monitoring.Device.CardDespenser
{
    public class MUTEK : CardDispenser
    {
        public MUTEK(string model, string deviceManagerName, string deviceManagerID, bool useCOMPort, string comPort)
            : base("MUTEK", model, deviceManagerName, deviceManagerID, useCOMPort, comPort)
        {
        }

        public override int CheckDeviceManager()
        {
            return base.CheckDeviceManager();
        }

        public override List<string> CheckCardLevel()
        {

            List<string> ret = new List<string>();

            bool checkDispenserCardLevelViaTextFile = (ConfigurationManager.AppSettings["RMS.CheckDispenserCardLevelViaTextFile"] != null && Convert.ToBoolean(ConfigurationManager.AppSettings["RMS.CheckDispenserCardLevelViaTextFile"]));

            if (checkDispenserCardLevelViaTextFile || model.ToLower().EndsWith("_sks"))
            {
                // เครื่อง MUTEK ใช้ 2 ports คือ USB กับ COM
                // โดยที่ COM จะสามารถตรวจสอบ Card Level ได้เท่านั้น
                if (!useCOMPort) return ret;

                string messageStorageFolder = ConfigurationManager.AppSettings["RMS.MessageStorageFolder"];
                messageStorageFolder = (messageStorageFolder.EndsWith(@"\")) ? messageStorageFolder : messageStorageFolder + @"\";

                // Check Low Card
                string lowCard = "LOW_CARD_" + comPort + ".txt";
                if (File.Exists(messageStorageFolder + lowCard))
                {
                    ret.Add("low_card");
                }

                // Check End Card
                string endCard = "END_CARD_" + comPort + ".txt";
                if (File.Exists(messageStorageFolder + endCard))
                {
                    ret.Add("end_card");
                }
            }

            return ret;
        }
    }
}
