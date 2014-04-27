using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Agent.Proxy.ClientProxy;

namespace RMS.Monitoring.Helper
{
    public class Common
    {
        public static List<RmsMonitoringProfileDevice> GetRmsMonitoringProfileDevicebyDeviceCode(ClientResult clientResult, string deviceCode, string deviceTypeCode)
        {
            List<RmsMonitoringProfileDevice> ret = new List<RmsMonitoringProfileDevice>();
            var device = clientResult.ListDevices.FirstOrDefault(d => d.DeviceCode == deviceCode);
            if (device != null)
            {
                var deviceType = clientResult.ListDeviceType.SingleOrDefault(dt => dt.DeviceTypeId == device.DeviceTypeId);
                if (deviceType != null && deviceType.DeviceTypeCode == deviceTypeCode)
                {
                    int? id = device.DeviceId;
                    ret = clientResult.ListMonitoringProfileDevices.Where(p => p.DeviceId == id).ToList();
                }
            }
            return ret;
        }


    }

    public class Models
    {
        public static class DeviceCode
        {
            public const string Client = "CLI";
            public const string Performance = "PERF";
            public const string ATMCardReader = "ACR";
            public const string BarcodeReader = "BR";
            public const string CardDispenser = "CD";
            public const string ElectronicSignaturePad = "ESP";
            public const string IDCardSanner = "IDCS";
            public const string Keyboard = "KB";
            public const string MonitorDisplay = "MD";
            public const string Printer = "PRINT";
            public const string Scanner = "SCAN";
            public const string SmartcardReader = "SCR";
            public const string ThermalPrinter = "TMP";
            public const string WebCamera = "WC";
            public const string UPS = "UPS";
        }
    }
}
