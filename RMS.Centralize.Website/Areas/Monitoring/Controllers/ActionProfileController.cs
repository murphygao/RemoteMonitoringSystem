using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RMS.Centralize.Website.ActionProfileProxy;
using JQueryDataTableParamModel = RMS.Centralize.Website.ActionProfileProxy.JQueryDataTableParamModel;

namespace Bootstrap.Areas.Monitoring.Controllers
{
    public class ActionProfileController : Controller
    {
        //
        // GET: /Monitoring/ActionProfile/SearchActionList/
        public ActionResult SearchActionList(JQueryDataTableParamModel param, string txtActionProfile, string txtEmail, string txtSms)
        {
            //JQueryDataTableParamModel param = new JQueryDataTableParamModel();
            //param.sEcho = String.IsNullOrEmpty(Context.Request["sEcho"]) ? "0" : Context.Request["sEcho"];
            //param.sSearch = String.IsNullOrEmpty(Context.Request["sSearch"]) ? "" : Context.Request["sSearch"];
            //param.iDisplayStart = String.IsNullOrEmpty(Context.Request["iDisplayStart"]) ? 0 : Convert.ToInt32(Context.Request["iDisplayStart"]);
            //param.iDisplayLength = String.IsNullOrEmpty(Context.Request["iDisplayLength"]) ? 0 : Convert.ToInt32(Context.Request["iDisplayLength"]);

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"]; // asc or desc
            param.iSortColumn = (Request["mDataProp_" + sortColumnIndex] + "_" + sortDirection).ToLower();

            ActionProfileServiceClient apClient = new ActionProfileServiceClient();
            apClient.Endpoint.Address = new EndpointAddress("http://localhost/RMS.Centralize.WebService/ActionProfileService.svc");
            var searchResult = apClient.Search(param, txtActionProfile, txtEmail, txtSms);

            int? totalRecords = 0;
            totalRecords = searchResult.TotalRecords;

            var data = new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = searchResult.ListActionProfile
            };

            return Json(data, JsonRequestBehavior.AllowGet); ;
        }



    }
}