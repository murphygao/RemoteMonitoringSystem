using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using RMS.Centralize.WebSite.Proxy;
using RMS.Centralize.WebSite.Proxy.ActionProfileProxy;
using RMS.Common.Exception;

namespace RMS.Centralize.Website.Areas.Monitoring.Controllers
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

            try
            {
                var apClient = new ActionProfileService().actionProfileService;

                var searchResult = apClient.Search(param, txtActionProfile, txtEmail, txtSms);

                int? totalRecords = 0;
                totalRecords = searchResult.TotalRecords;

                var data = new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords,
                    aaData = searchResult.ListActionProfiles,
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

                new RMSWebException(this, "0500", "SearchActionList failed. " + ex.Message, ex, true);

                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: /Monitoring/ActionProfile/DeleteActionProfile/
        public ActionResult DeleteActionProfile(int? ActionProfileID)
        {

            if (ActionProfileID == null) throw new ArgumentNullException("ActionProfileID");

            string ret;

            try
            {
                var apClient = new RMS.Centralize.WebSite.Proxy.ActionProfileService().actionProfileService;

                var result = apClient.Delete(ActionProfileID);

                ret = result.IsSuccess ? "1" : "0";

            }
            catch (Exception ex)
            {
                ret = "0";
                new RMSWebException(this, "0500", "DeleteActionProfile failed. " + ex.Message, ex, true);
            }



            return Json(ret);
        }

        // GET: /Monitoring/ActionProfile/GetActionProfile/
        public ActionResult GetActionProfile(int? id)
        {
            if (id == null) throw new ArgumentNullException("ActionProfileID");

            try
            {
                var apClient = new RMS.Centralize.WebSite.Proxy.ActionProfileService().actionProfileService;

                var result = apClient.Get(id);


                    var ret = new
                    {
                        status = (result.IsSuccess) ? 1 : 0,
                        ActionProfileID = result.ActionProfile.ActionProfileId,
                        ActionProfileName = result.ActionProfile.ActionProfileName,
                        data = JsonConvert.SerializeObject(result.ActionProfile, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
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
                new RMSWebException(this, "0500", "GetActionProfile failed. " + ex.Message, ex, true);

                return Json(ret);
            }

        }

        // GET: /Monitoring/ActionProfile/UpdateActionProfile/
        public ActionResult UpdateActionProfile(int? id, string m, string ActionProfileName, string Email, string SMS, bool ActiveList)
        {

            if (string.IsNullOrEmpty(ActionProfileName)) throw new ArgumentNullException("ActionProfileName");

            if (m == "e" && id == null) throw new ArgumentNullException("id");

            try
            {
                var apClient = new ActionProfileService().actionProfileService;
                var result = apClient.Update(id, m, ActionProfileName, Email, SMS, ActiveList);

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

                new RMSWebException(this, "0500", "UpdateActionProfile failed. " + ex.Message, ex, true);

                return Json(ret);
            }
        }

    }
}