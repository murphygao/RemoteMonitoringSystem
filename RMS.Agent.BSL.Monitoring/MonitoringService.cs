using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Agent.Entity;
using RMS.Agent.Proxy;
using RMS.Agent.Proxy.ClientProxy;
using RMS.Agent.Proxy.MonitoringProxy;
using RMS.Monitoring.Helper;

namespace RMS.Agent.BSL.Monitoring
{
    public class MonitoringService
    {
        private delegate void ExecuteCommandAsync(string clientCode);

        public void Command(string clientCode)
        {
            var caller = new ExecuteCommandAsync(ExecuteCommand);
            caller.BeginInvoke(clientCode, null, null);
        }


        private void ExecuteCommand(string clientCode)
        {
            /*
             * 
             * 1. Call Centralize to Get Client & Device Info for Monitoring 
             * 2. Send Alive Message
             * 3. Check Device Monitoring
             * 
             */


            #region 1. Call Centralize to Get Client & Device Info for Monitoring

            ClientServiceClient cs = new ClientServiceClient();
            var clientResult = cs.GetClient(GetClientBy.ClientCode, null, clientCode, null, true);

            int? deviceId = null;
            int? monitoringProfileDeviceId = null;

            var rmsMonitoringProfileDevices = Common.GetRmsMonitoringProfileDevicebyDeviceCode(clientResult, "CLIENT", Models.DeviceCode.Client);
            // for CLIENT code, there are only one rmsMonitoringProfileDevices
            if (rmsMonitoringProfileDevices.Count > 0)
                monitoringProfileDeviceId = rmsMonitoringProfileDevices[0].MonitoringProfileDeviceId;

            #endregion


            #region 2. Send Alive Message

            RMS.Agent.Proxy.MonitoringProxy.MonitoringServiceClient mp = new MonitoringServiceClient();

            var rawMessage = new RmsReportMonitoringRaw();
            rawMessage.ClientCode = clientResult.Client.ClientCode;
            rawMessage.DeviceCode = "CLIENT";

            rawMessage.Message = "OK";
            rawMessage.MessageDateTime = DateTime.Now;
            rawMessage.MonitoringProfileDeviceId = monitoringProfileDeviceId;

            mp.AddMessage(rawMessage); 

            #endregion


            #region 3. Check Device Monitoring

            var monitoringService = new RMS.Monitoring.Core.MonitoringService();

            // Performance
            var monitoringRaws = monitoringService.Monitoring("performance", clientResult);
            if (monitoringRaws.Count > 0)
                mp.AddMessages(monitoringRaws);

            // Device
            monitoringRaws = monitoringService.Monitoring("device", clientResult);
            if (monitoringRaws.Count > 0)
                mp.AddMessages(monitoringRaws);


            #endregion

        }

    }
}
