using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Agent.Proxy.ClientProxy;
using RMS.Agent.Proxy.MonitoringProxy;
using RMS.Common.Exception;

namespace RMS.Monitoring.Device.Alarm
{
    public class AlarmService
    {
        private Alarm _device;
        private ClientResult clientResult;

        public AlarmService(string brand, string model, string deviceManagerName, string deviceManagerID, bool useCOMPort, string comPort, ClientResult clientResult)
        {
            try
            {
                this.clientResult = clientResult;

                if (brand.ToLower() == "dpu") _device = new DPU(model, deviceManagerName, deviceManagerID, useCOMPort, comPort);
                else
                    _device = new Alarm(brand, model, deviceManagerName, deviceManagerID, useCOMPort, comPort);
                    //throw new Exception("Brand Not Found. brand=" + brand);

            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "AlarmService failed. " + ex.Message, ex, false);
            }
        }

        public List<RmsReportMonitoringRaw> Monitoring()
        {
            try
            {
                List<RmsReportMonitoringRaw> lRmsReportMonitoringRaws = new List<RmsReportMonitoringRaw>();

                RmsReportMonitoringRaw raw = new RmsReportMonitoringRaw();
                raw.ClientCode = clientResult.Client.ClientCode;
                raw.DeviceCode = clientResult.ListDevices[0].DeviceCode;

                int ret = _device.CheckDeviceManager();

                raw.MessageDateTime = DateTime.Now;
                raw.MonitoringProfileDeviceId = clientResult.ListMonitoringProfileDevices[0].MonitoringProfileDeviceId;

                bool _ok = true;

                if (ret == 0)
                {
                    raw.Message = "OK";

                    if (clientResult.ListMonitoringProfileDevices[0].BooleanValue == true)
                    {
                        var alarmStatus = _device.CheckAlarmStatus();
                        foreach (string s in alarmStatus)
                        {
                            RmsReportMonitoringRaw monitoringRaw = new RmsReportMonitoringRaw();
                            monitoringRaw.ClientCode = clientResult.Client.ClientCode;
                            monitoringRaw.DeviceCode = clientResult.ListDevices[0].DeviceCode;
                            monitoringRaw.MessageDateTime = DateTime.Now;
                            monitoringRaw.MonitoringProfileDeviceId = clientResult.ListMonitoringProfileDevices[0].MonitoringProfileDeviceId;
                            if (s.ToLower() == "alarm_door")
                            {
                                monitoringRaw.Message = "ALARM_DOOR";
                            }
                            else if (s.ToLower() == "alarm_temperature_external")
                            {
                                monitoringRaw.Message = "ALARM_TEMPERATURE_EXTERNAL";
                            }
                            else if (s.ToLower() == "alarm_temperature")
                            {
                                monitoringRaw.Message = "ALARM_TEMPERATURE";
                            }
                            else if (s.ToLower() == "alarm_vibration")
                            {
                                monitoringRaw.Message = "ALARM_VIBRATION";
                            }
                            else if (s.ToLower() == "alarm_angle")
                            {
                                monitoringRaw.Message = "ALARM_ANGLE";
                            }
                            else if (s.ToLower() == "alarm_power")
                            {
                                monitoringRaw.Message = "ALARM_POWER";
                            }
                            else if (s.ToLower() == "port_cannot_open")
                            {
                                monitoringRaw.Message = "PORT_CANNOT_OPEN";
                            }

                            if (!string.IsNullOrEmpty(monitoringRaw.Message))
                            {
                                lRmsReportMonitoringRaws.Add(monitoringRaw);
                                _ok = false;
                            }
                        }
                    }

                }
                else if (ret == -1)
                {
                    raw.Message = "DEVICE_NOT_FOUND";
                }
                else
                {
                    raw.Message = "DEVICE_NOT_READY";
                }

                if (_ok) //ป้องกันการใส่ OK Message ซ้ำลงไป เมื่อพบว่า Alarm ไม่ปกติ
                {
                    lRmsReportMonitoringRaws.Add(raw);
                }

                // ถ้า BooleanValue เป็น TRUE, สามารถตรวจสอบ ปริมาณการ์ดที่เหลืออยู่ได้
                return lRmsReportMonitoringRaws;
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "Monitoring failed. " + ex.Message, ex, false);
            }
        }
    }
}
