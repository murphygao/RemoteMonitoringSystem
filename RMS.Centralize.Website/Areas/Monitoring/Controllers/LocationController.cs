using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RMS.Centralize.WebSite.Proxy;
using RMS.Centralize.WebSite.Proxy.LocationProxy;
using RMS.Common.Exception;

namespace RMS.Centralize.Website.Areas.Monitoring.Controllers
{
    public class LocationController : Controller
    {
        // GET: /Monitoring/Location/Search/
        public ActionResult Search(JQueryDataTableParamModel param, string txtLocation)
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
                var service = new LocationService().locationService;

                var searchResult = service.Search(param, txtLocation, null);

                int? totalRecords = 0;
                totalRecords = searchResult.TotalRecords;

                var data = new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords,
                    aaData = searchResult.ListLocationInfos,
                    isSuccess = searchResult.IsSuccess,
                    errorMessage = searchResult.ErrorMessage
                };

                return Json(data);

            }
            catch (Exception ex)
            {
                var data = new
                {
                    isSuccess = false,
                    errorMessage = ex.Message
                };

                new RMSWebException(this, "0500", "Search failed. " + ex.Message, ex, true);

                return Json(data);
            }
        }

        // GET: /Monitoring/Location/Delete/
        public ActionResult Delete(int? id)
        {

            if (id == null) throw new ArgumentNullException("LocationID");

            string ret;

            try
            {
                var service = new RMS.Centralize.WebSite.Proxy.LocationService().locationService;

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

        // GET: /Monitoring/Location/Get/
        public ActionResult Get(int? id)
        {
            if (id == null) throw new ArgumentNullException("LocationID");

            try
            {
                var service = new RMS.Centralize.WebSite.Proxy.LocationService().locationService;

                var result = service.Get(id.Value);


                var ret = new
                {
                    status = (result.IsSuccess) ? 1 : 0,
                    LocationID = result.Location.LocationId,
                    LocationCode = result.Location.LocationCode,
                    Locationname = result.Location.LocationName,
                    data = JsonConvert.SerializeObject(result.Location, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
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

        // GET: /Monitoring/Location/Update/
        public ActionResult Update(int? id, string m, string locationCode, string locationName, bool? mondayEnable, bool? mondayWholeDay, DateTime? mondayStart,
            DateTime? mondayEnd, bool? tuesdayEnable, bool? tuesdayWholeDay, DateTime? tuesdayStart, DateTime? tuesdayEnd, bool? wednesdayEnable,
            bool? wednesdayWholeDay, DateTime? wednesdayStart, DateTime? wednesdayEnd, bool? thursdayEnable, bool? thursdayWholeDay, DateTime? thursdayStart,
            DateTime? thursdayEnd, bool? fridayEnable, bool? fridayWholeDay, DateTime? fridayStart, DateTime? fridayEnd, bool? saturdayEnable,
            bool? saturdayWholeDay, DateTime? saturdayStart, DateTime? saturdayEnd, bool? sundayEnable, bool? sundayWholeDay, DateTime? sundayStart,
            DateTime? sundayEnd, bool? activeList)
        {

            if (string.IsNullOrEmpty(locationCode)) throw new ArgumentNullException("LocationCode");
            if (string.IsNullOrEmpty(locationName)) throw new ArgumentNullException("LocationName");

            if (m == "e" && id == null) throw new ArgumentNullException("id");

            try
            {
                var updatedBy = new BasePage().UserName;

                var service = new RMS.Centralize.WebSite.Proxy.LocationService().locationService;
                var result = service.Update(id, m, locationCode, locationName
                        , mondayEnable ?? false, mondayWholeDay ?? false, mondayStart, mondayEnd
                        , tuesdayEnable ?? false, tuesdayWholeDay ?? false, tuesdayStart, tuesdayEnd
                        , wednesdayEnable ?? false, wednesdayWholeDay ?? false, wednesdayStart, wednesdayEnd
                        , thursdayEnable ?? false, thursdayWholeDay ?? false, thursdayStart, thursdayEnd
                        , fridayEnable ?? false, fridayWholeDay ?? false, fridayStart, fridayEnd
                        , saturdayEnable ?? false, saturdayWholeDay ?? false, saturdayStart, saturdayEnd
                        , sundayEnable ?? false, sundayWholeDay ?? false, sundayStart, sundayEnd
                        , activeList ?? false, updatedBy);

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