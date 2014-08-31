using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMS.Common.Exception;

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
                throw new RMSWebException(this, "0500", "Start failed. " + ex.Message, ex, false);
            }
        }
    }

    public class WebsiteMonitoringEngineService : BaseService
    {
        public string Start()
        {
            try
            {
                RMS.Centralize.BSL.MonitoringEngine.WebsiteMonitoringService wms = new Centralize.BSL.MonitoringEngine.WebsiteMonitoringService();
                wms.Start(licenseInfo);
                return "OK";
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Start failed. " + ex.Message, ex, false);
            }
        }
    }

}