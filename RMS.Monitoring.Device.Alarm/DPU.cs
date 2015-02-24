using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Monitoring.Device.Alarm
{
    public class DPU : Alarm
    {
        public DPU(string model, string deviceManagerName, string deviceManagerID, bool useCOMPort, string comPort)
            : base("DPU", model, deviceManagerName, deviceManagerID, useCOMPort, comPort)
        {
        }

        public override int CheckDeviceManager()
        {
            return base.CheckDeviceManager();
        }

        public override List<string> CheckAlarmStatus()
        {

            List<string> ret = new List<string>();

            bool checkAlarmStatusViaTextFile = (ConfigurationManager.AppSettings["RMS.CheckAlarmStatusViaTextFile"] != null && Convert.ToBoolean(ConfigurationManager.AppSettings["RMS.CheckAlarmStatusViaTextFile"]));

            if (checkAlarmStatusViaTextFile)
            {
                string messageStorageFolder = ConfigurationManager.AppSettings["RMS.MessageStorageFolder"];
                messageStorageFolder = (messageStorageFolder.EndsWith(@"\")) ? messageStorageFolder : messageStorageFolder + @"\";

                // Port Cannot Open
                string portOpen = "PORT_CANNOT_OPEN_" + comPort + ".txt";
                if (File.Exists(messageStorageFolder + portOpen))
                {
                    ret.Add("port_cannot_open");
                }

                // Alarm Door
                string alarmDoor = "ALARM_DOOR_" + comPort + ".txt";
                if (File.Exists(messageStorageFolder + alarmDoor))
                {
                    ret.Add("alarm_door");
                }

                // Alarm Temperature External
                string alarmTemperatureExternal = "ALARM_TEMPERATURE_EXTERNAL_" + comPort + ".txt";
                if (File.Exists(messageStorageFolder + alarmTemperatureExternal))
                {
                    ret.Add("alarm_temperature_external");
                }

                // Alarm Temperature
                string alarmTemperature = "ALARM_TEMPERATURE_" + comPort + ".txt";
                if (File.Exists(messageStorageFolder + alarmTemperature))
                {
                    ret.Add("alarm_temperature");
                }

                // Alarm Vibration
                string alarmVibration = "ALARM_VIBRATION_" + comPort + ".txt";
                if (File.Exists(messageStorageFolder + alarmVibration))
                {
                    ret.Add("alarm_vibration");
                }

                // Alarm Angle
                string alarmAngle = "ALARM_ANGLE_" + comPort + ".txt";
                if (File.Exists(messageStorageFolder + alarmAngle))
                {
                    ret.Add("alarm_angle");
                }

                // Alarm Power
                string alarmPower = "ALARM_POWER_" + comPort + ".txt";
                if (File.Exists(messageStorageFolder + alarmPower))
                {
                    ret.Add("ALARM_POWER");
                }
            }

            return ret;
        }
    }
}
