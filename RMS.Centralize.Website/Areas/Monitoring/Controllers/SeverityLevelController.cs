using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RMS.Centralize.WebSite.Proxy;
using RMS.Centralize.WebSite.Proxy.SeverityLevelProxy;
using RMS.Common.Exception;

namespace RMS.Centralize.Website.Areas.Monitoring.Controllers
{
    public class SeverityLevelController : Controller
    {
        // GET: /Monitoring/SeverityLevel/Search/
        public ActionResult Search(JQueryDataTableParamModel param, string severityLevel, bool? activeList)
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
                var service = new SeverityLevelService().severityLevelService;

                var searchResult = service.Search(param, severityLevel, activeList);

                int? totalRecords = 0;
                totalRecords = searchResult.TotalRecords;

                var data = new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords,
                    aaData = searchResult.ListSeverityLevelInfos,
                    status = (searchResult.IsSuccess) ? 1 : 0,
                    error = searchResult.ErrorMessage
                };

                return Json(data);

            }
            catch (Exception ex)
            {
                var data = new
                {
                    status = 0,
                    error = ex.Message
                };

                new RMSWebException(this, "0500", "Search failed. " + ex.Message, ex, true);

                return Json(data);
            }
        }

        // GET: /Monitoring/SeverityLevel/Delete/
        public ActionResult Delete(int? id)
        {

            if (id == null) throw new ArgumentNullException("SeverityLevelID");

            string ret;

            try
            {
                var service = new SeverityLevelService().severityLevelService;

                var updatedBy = new BasePage().UserName;

                var result = service.Delete(id.Value, updatedBy);

                ret = result.IsSuccess ? "1" : "0";

            }
            catch (Exception ex)
            {
                ret = "0";
                new RMSWebException(this, "0500", "Delete failed. " + ex.Message, ex, true);
            }



            return Json(ret);
        }

        // GET: /Monitoring/SeverityLevel/Get/
        public ActionResult Get(int? id)
        {
            if (id == null) throw new ArgumentNullException("SeverityLevelID");

            try
            {
                var service = new SeverityLevelService().severityLevelService;

                var result = service.Get(id.Value);


                var ret = new
                {
                    status = (result.IsSuccess) ? 1 : 0,
                    SeverityLevelID = result.SeverityLevel.SeverityLevelId,
                    data = JsonConvert.SerializeObject(result.SeverityLevel, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
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


        // GET: /Monitoring/SeverityLevel/Update/
        public ActionResult Update(int? id, string m, string levelCode, string levelName, int? orderList, bool? activeList, int? defaultActionProfileID
            , string colorCode, bool? repeatable, int? repeatableInterval)
        {

            if (string.IsNullOrEmpty(levelCode)) throw new ArgumentNullException("LevelCode");
            if (string.IsNullOrEmpty(levelName)) throw new ArgumentNullException("LevelName");
            if (orderList == null) throw new ArgumentNullException("OrderList");
            if (activeList == null) throw new ArgumentNullException("ActiveList");
            if (defaultActionProfileID == null) throw new ArgumentNullException("DefaultActionProfileID");
            if (string.IsNullOrEmpty(colorCode)) throw new ArgumentNullException("ColorCode");

            if (m == "e" && id == null) throw new ArgumentNullException("id");

            try
            {
                var updatedBy = new BasePage().UserName;

                var service = new SeverityLevelService().severityLevelService;
                var result = service.Update(id, m, levelCode, levelName
                        , orderList.Value, activeList.Value, defaultActionProfileID.Value
                        , colorCode, repeatable ?? false, repeatableInterval ?? 0, updatedBy);

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

        // GET: /Monitoring/SeverityLevel/List/
        public ActionResult List(bool? activeList)
        {
            try
            {
                var service = new SeverityLevelService().severityLevelService;

                var result = service.List(activeList);

                var ret = new
                {
                    ddlMonitoringProfile = result.ListSeverityLevels,

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


    }

}