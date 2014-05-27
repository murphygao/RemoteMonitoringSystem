using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RMS.Centralize.WebSite.Proxy;
using RMS.Centralize.WebSite.Proxy.ClientProxy;

namespace RMS.Centralize.Website.Areas.Monitoring.Controllers
{
    public class ClientController : Controller
    {

        // GET: /Monitoring/Client/Search/
        public ActionResult Search(JQueryDataTableParamModel param, DateTime? txtAsOfDate, int? dllClientType, string txtClientCode,
            bool? ddlClientStatus,
            string txtIPAddress)
        {
            //JQueryDataTableParamModel param = new JQueryDataTableParamModel();
            //param.sEcho = String.IsNullOrEmpty(Context.Request["sEcho"]) ? "0" : Context.Request["sEcho"];
            //param.sSearch = String.IsNullOrEmpty(Context.Request["sSearch"]) ? "" : Context.Request["sSearch"];
            //param.iDisplayStart = String.IsNullOrEmpty(Context.Request["iDisplayStart"]) ? 0 : Convert.ToInt32(Context.Request["iDisplayStart"]);
            //param.iDisplayLength = String.IsNullOrEmpty(Context.Request["iDisplayLength"]) ? 0 : Convert.ToInt32(Context.Request["iDisplayLength"]);

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"]; // asc or desc
            param.iSortColumn = (Request["mDataProp_" + sortColumnIndex] + "_" + sortDirection).ToLower();

            try
            {
                var cClient = new ClientService().clientService;

                var searchResult = cClient.Search(param, txtAsOfDate, dllClientType, txtClientCode, ddlClientStatus, txtIPAddress);

                int? totalRecords = 0;
                totalRecords = searchResult.TotalRecords;

                var data = new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords,
                    aaData = searchResult.ListClients,
                    isSuccess = searchResult.IsSuccess,
                    errorMessage = searchResult.ErrorMessage
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

        // GET: /Monitoring/Client/ExistingClientCode/
        public ActionResult ExistingClientCode(string clientCode)
        {
            try
            {
                var cClient = new ClientService().clientService;
                var searchResult = cClient.ExistingClientCode(clientCode);
                var data = new
                {
                    aaData = searchResult.TotalRecords,
                    isSuccess = searchResult.IsSuccess,
                    errorMessage = searchResult.ErrorMessage
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

        // GET: /Monitoring/Client/Get/
        public ActionResult GetClient(int? id)
        {
            try
            {
                var cClient = new ClientService().clientService;
                var searchResult = cClient.GetClient(GetClientBy.ClientID, id, null, null, false);
                var data = new
                {
                    data = JsonConvert.SerializeObject(searchResult.Client),
                    status = (searchResult.IsSuccess) ? 1 : 0,
                    errorMessage = searchResult.ErrorMessage
                };

                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var data = new
                {
                    status = 0,
                    errorMessage = ex.Message
                };

                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
    }
}