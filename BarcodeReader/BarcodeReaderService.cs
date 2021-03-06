﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Agent.Proxy.ClientProxy;
using RMS.Agent.Proxy.MonitoringProxy;

namespace BarcodeReader
{
    class BarcodeReaderService
    {
        private BarcodeReader _device;
        private ClientResult clientResult;

        public BarcodeReaderService(string brand, string model, string deviceManagerName, string deviceManagerID, ClientResult clientResult)
        {
            this.clientResult = clientResult;

            if (brand.ToLower() == "honeywell") _device = new Honeywell(model, deviceManagerName, deviceManagerID);

        }

        public List<RmsReportMonitoringRaw> Monitoring()
        {
            List<RmsReportMonitoringRaw> lRmsReportMonitoringRaws = new List<RmsReportMonitoringRaw>();

            RmsReportMonitoringRaw raw = new RmsReportMonitoringRaw();
            raw.ClientCode = clientResult.Client.ClientCode;
            raw.DeviceCode = clientResult.ListDevices[0].DeviceCode;

            int ret = _device.CheckDeviceManager();

            if (ret == 0)
            {
                raw.Message = "OK";
            }
            else if (ret == -1)
            {
                raw.Message = "DEVICE_NOT_FOUND";
            }
            else
            {
                raw.Message = "DEVICE_NOT_READY";
            }
            raw.MessageDateTime = DateTime.Now;
            raw.MonitoringProfileDeviceId = clientResult.ListMonitoringProfileDevices[0].MonitoringProfileDeviceId;

            lRmsReportMonitoringRaws.Add(raw);

            return lRmsReportMonitoringRaws;
        }
    }
}
