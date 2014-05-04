using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
    }
}