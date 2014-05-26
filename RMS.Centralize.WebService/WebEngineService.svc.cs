﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Web.Caching;
using RMS.Centralize.WebService.BSL;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WebEngineService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WebEngineService.svc or WebEngineService.svc.cs at the Solution Explorer and start debugging.
    public class WebEngineService : IWebEngineService
    {
        public void TestConnection()
        {

        }

        public string StartMonitoringEngine()
        {
            try
            {
                BSL.MonitoringEngineService mes = new MonitoringEngineService();
                return mes.Start();
            }
            catch (Exception ex)
            {
                return "Failed: " + ex.Message;
            }
        }
    }
}
