using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Centralize.WebSite.Proxy.ClientMonitoringProxy;
using RMS.Centralize.WebSite.Proxy.ClientProxy;
using RMS.Common.Exception;
using RmsClientMonitoring = RMS.Centralize.WebSite.Proxy.ClientMonitoringProxy.RmsClientMonitoring;

namespace RMS.Centralize.Website.Areas.Monitoring.Controllers
{
    public class MonitoringProfileController : Controller
    {
        //
        // GET: /Monitoring/MonitoringProfile/List
        public ActionResult List(bool? activeList, int? excludeClientID)
        {
            try
            {
                var service = new RMS.Centralize.WebSite.Proxy.MonitoringProfileService().monitoringProfileService;

                var result = service.List(activeList);

                var listCMs = new List<WebSite.Proxy.ClientMonitoringProxy.ClientMonitoringInfo>();

                if (excludeClientID != null)
                {
                    var cm = new Centralize.WebSite.Proxy.ClientMonitoringService().clientMonitoringService;
                    var listByClient = cm.ListByClient(excludeClientID.Value);
                    listCMs = new List<ClientMonitoringInfo>(listByClient.ListMonitoringProfileInfos.ToList());
                }

                if (result.IsSuccess)
                {
                    var idsToBeRemoved = new HashSet<int>(listCMs.Select(item => item.MonitoringProfileId));
                    result.ListMonitoringProfiles.RemoveAll(item => idsToBeRemoved.Contains(item.MonitoringProfileId));
                }
                else
                {
                    throw new Exception(result.ErrorMessage);
                }

                var ret = new
                {
                    ddlMonitoringProfile = result.ListMonitoringProfiles,

                    status = (result.IsSuccess) ? 1 : 0,
                    error = result.ErrorMessage
                };

                return Json(ret);

            }
            catch (Exception ex)
            {
                var ret = new
                {
                    status = -1,
                    error = ex.Message
                };
                new RMSWebException(this, "0500", "Get failed. " + ex.Message, ex, true);

                return Json(ret);
            }
        }

	}
}