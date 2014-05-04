using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RMS.Centralize.WebSite.Proxy.SummaryReportProxy;

namespace RMS.Centralize.Website.Areas.Monitoring.Controllers
{
    public class SummaryReportController : Controller
    {
        //
        // GET: /Monitoring/SummaryReport/SearchMonitoringReport
        public ActionResult SearchMonitoringReport(JQueryDataTableParamModel param, DateTime? txtStartMessageDate, DateTime? txtEndMessageDate
            , string txtClientCode, string txtLocation, string ddlMessageGroup, string txtMessage, bool? ddlMessageStatus)
        {
            //JQueryDataTableParamModel param = new JQueryDataTableParamModel();
            //HttpContext Context = System.Web.HttpContext.Current;
            //param.sEcho = String.IsNullOrEmpty(Context.Request["sEcho"]) ? "0" : Context.Request["sEcho"];
            //param.sSearch = String.IsNullOrEmpty(Context.Request["sSearch"]) ? "" : Context.Request["sSearch"];
            //param.iDisplayStart = String.IsNullOrEmpty(Context.Request["iDisplayStart"]) ? 0 : Convert.ToInt32(Context.Request["iDisplayStart"]);
            //param.iDisplayLength = String.IsNullOrEmpty(Context.Request["iDisplayLength"]) ? 0 : Convert.ToInt32(Context.Request["iDisplayLength"]);

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"]; // asc or desc
            param.iSortColumn = (Request["mDataProp_" + sortColumnIndex] + "_" + sortDirection).ToLower();
            try
            {

                var serviceClient = new RMS.Centralize.WebSite.Proxy.SummaryReportService().summaryReportService;
                var result = serviceClient.SearchSummaryMonitoring(param, txtStartMessageDate, txtEndMessageDate, txtClientCode, txtLocation
                    , ddlMessageGroup, txtMessage, ddlMessageStatus);



                int? totalRecords = 0;
                totalRecords = result.TotalRecords;

                var data = new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords,
                    aaData = result.ListSummaryMonitorings,
                    isSuccess = result.IsSuccess,
                    errorMessage = result.ErrorMessage
                };

                return Json(data, JsonRequestBehavior.AllowGet);

            }

            catch (Exception ex)
            {

                var data = new
                {
                    isSuccess = false,
                    errorMessage = ex.Message
                };

                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }

        // GET: /Monitoring/SummaryReport/GetClientInfo/
        public ActionResult GetClientInfo(int? id)
        {
            if (id == null) throw new ArgumentNullException("ClientID");

            try
            {
                var service = new RMS.Centralize.WebSite.Proxy.SummaryReportService().summaryReportService;

                var result = service.GetClientInfo(id.Value);

                var ret = new
                {
                    status = (result.IsSuccess) ? 1 : 0,
                    ClientID = result.Client.ClientID,
                    data = JsonConvert.SerializeObject(result.Client, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
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
                return Json(ret);
            }

        }

    }
}