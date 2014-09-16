using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RMS.Centralize.WebSite.Proxy;
using RMS.Centralize.WebSite.Proxy.ClientProxy;
using RMS.Common.Exception;
using JQueryDataTableParamModel = RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.JQueryDataTableParamModel;

namespace RMS.Centralize.Website.Areas.Monitoring.Controllers
{
    public class WebsiteMonitoringController : Controller
    {
        // GET: /Monitoring/WebsiteMonitoring/List/
        public ActionResult List(JQueryDataTableParamModel param, int? clientID)
        {

            try
            {
                if (clientID == null) throw new ArgumentNullException("ClientID");

                var clientService = new RMS.Centralize.WebSite.Proxy.ClientService().clientService;

                var clientResult = clientService.GetClient(GetClientBy.ClientID, clientID, null, null, false, false);

                if (clientResult == null || clientResult.Client == null) throw new ArgumentException("ClientID (" + clientID.Value + ") not found.");

                var service = new WebsiteMonitoringService().websiteMonitoringService;

                var result = service.ListWebsiteMonitoringsByClient(param, clientID.Value);

                var ret = new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = result.TotalRecords,
                    iTotalDisplayRecords = result.TotalRecords,
                    aaData = result.ListWebsiteMonitoringInfos.OrderBy(o => o.WebsiteMonitoringTypeId).ThenBy(o => o.WebsiteMonitoringProtocolId),
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
                new RMSWebException(this, "0500", "List failed. " + ex.Message, ex, true);

                return Json(ret);
            }
        }

        // GET: /Monitoring/WebsiteMonitoring/Delete/
        public ActionResult Delete(int? id)
        {

            if (id == null) throw new ArgumentNullException("WebsiteMonitoringID");

            string ret;

            try
            {
                var service = new WebsiteMonitoringService().websiteMonitoringService;

                var updatedBy = new BasePage().UserName;

                var result = service.DeleteWebsiteMonitoring(id.Value, updatedBy);

                ret = result.IsSuccess ? "1" : "0";

            }
            catch (Exception ex)
            {
                ret = "0";
                new RMSWebException(this, "0500", "Delete failed. " + ex.Message, ex, true);
            }

            return Json(ret);
        }

        // GET: /Monitoring/WebsiteMonitoring/Get/
        public ActionResult Get(int? id)
        {
            if (id == null) throw new ArgumentNullException("WebsiteMonitoringID");

            try
            {
                var service = new WebsiteMonitoringService().websiteMonitoringService;

                var result = service.GetWebsiteMonitoring(id.Value);

                var ret = new
                {
                    status = (result.IsSuccess) ? 1 : 0,
                    WebsiteMonitoringID = result.WebsiteMonitoringInfo.WebsiteMonitoringId,
                    data = JsonConvert.SerializeObject(result.WebsiteMonitoringInfo, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
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

        // GET: /Monitoring/WebsiteMonitoring/Update/
        public ActionResult Update(int? id, string m, int? clientID, string websiteMonitoringTypeID, string websiteMonitoringProtocolID,
            int? portNumber, string domainName, bool? enable)
        {

            if (clientID == null) throw new ArgumentNullException("clientID");
            //if (string.IsNullOrEmpty(websiteMonitoringTypeID)) throw new ArgumentNullException("websiteMonitoringTypeID");
            if (string.IsNullOrEmpty(websiteMonitoringProtocolID)) throw new ArgumentNullException("websiteMonitoringProtocolID");
            if (enable == null) throw new ArgumentNullException("enable");

            if (m == "e" && id == null) throw new ArgumentNullException("id");

            try
            {
                var updatedBy = new BasePage().UserName;

                var service = new WebsiteMonitoringService().websiteMonitoringService;
                var result = service.UpdateWebsiteMonitoring(id, m, clientID.Value, websiteMonitoringTypeID
                        , websiteMonitoringProtocolID, portNumber, domainName, enable.Value, updatedBy);

                var ret = new
                {
                    status = (result.IsSuccess) ? 1 : 0,
                    error = (result.IsSuccess) ? "" : result.ErrorMessage
                };

                return Json(ret);

            }
            catch (Exception ex)
            {
                var ret = new
                {
                    status = 0,
                    error = ex.Message
                };

                new RMSWebException(this, "0500", "Update failed. " + ex.Message, ex, true);

                return Json(ret);
            }
        }

    }
}