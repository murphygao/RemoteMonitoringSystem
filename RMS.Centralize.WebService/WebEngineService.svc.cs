using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Caching;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WebEngineService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WebEngineService.svc or WebEngineService.svc.cs at the Solution Explorer and start debugging.
    public class WebEngineService : IWebEngineService
    {
        public string StartMonitoringEngine()
        {
            try
            {
                RMS.Centralize.BSL.MonitoringEngine.MonitoringService ms = new Centralize.BSL.MonitoringEngine.MonitoringService();
                ms.Start();
                return "OK";
            }
            catch (Exception ex)
            {
                return "Fail: " + ex.Message;
            }
        }
    }
}
