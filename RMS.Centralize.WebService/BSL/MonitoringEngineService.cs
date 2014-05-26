﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.Centralize.WebService.BSL
{
    public class MonitoringEngineService : BaseService
    {
        public string Start()
        {
            try
            {
                RMS.Centralize.BSL.MonitoringEngine.MonitoringService ms = new Centralize.BSL.MonitoringEngine.MonitoringService();
                ms.Start(licenseInfo);
                return "OK";
            }
            catch (Exception ex)
            {
                return "Fail: " + ex.Message;
            }

        }
    }
}