using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RMS.Centralize.WebSite.Proxy;
using RMS.Centralize.WebSite.Proxy.MasterTableProxy;
using RMS.Common.Exception;

namespace RMS.Centralize.Website.Areas.Monitoring.Controllers
{
    public class MasterTableController : Controller
    {
        #region ColorLabel

        // GET: /Monitoring/MasterTable/ListColorLabels/
        public ActionResult ListColorLabels()
        {
            try
            {
                var service = new MasterTableService().masterTableService;

                var result = service.ListColorLabels();

                var ret = new
                {
                    listColorLabels = result.ListColorLabels,

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

        #endregion

        #region MessageMaster

        // GET: /Monitoring/MasterTable/SearchMessageMaster/
        public ActionResult SearchMessageMaster(JQueryDataTableParamModel param, string message)
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
                var service = new MasterTableService().masterTableService;

                var searchResult = service.SearchMessageMaster(param, message);

                int? totalRecords = 0;
                totalRecords = searchResult.TotalRecords;

                var data = new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords,
                    aaData = searchResult.ListMessageMasterInfos,
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

                new RMSWebException(this, "0500", "SearchMessageMaster failed. " + ex.Message, ex, true);

                return Json(data);
            }
        }

        // GET: /Monitoring/MasterTable/DeleteMessageMaster/
        public ActionResult DeleteMessageMaster(string message)
        {

            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException("message");

            string ret;

            try
            {
                var service = new MasterTableService().masterTableService;

                var updatedBy = new BasePage().UserName;

                var result = service.DeleteMessageMaster(message, updatedBy);

                ret = result.IsSuccess ? "1" : "0";

            }
            catch (Exception ex)
            {
                ret = "0";
                new RMSWebException(this, "0500", "DeleteMessageMaster failed. " + ex.Message, ex, true);
            }



            return Json(ret);
        }

        // GET: /Monitoring/MasterTable/GetMessageMaster/
        public ActionResult GetMessageMaster(string message)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException("message");

            try
            {
                var service = new MasterTableService().masterTableService;

                var result = service.GetMessageMaster(message);


                var ret = new
                {
                    status = (result.IsSuccess) ? 1 : 0,
                    Message = result.MessageMaster.Message,
                    data = JsonConvert.SerializeObject(result.MessageMaster, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
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
                new RMSWebException(this, "0500", "GetMessageMaster failed. " + ex.Message, ex, true);

                return Json(ret);
            }

        }


        // GET: /Monitoring/MasterTable/UpdateMessageMaster/
        public ActionResult UpdateMessageMaster(string id, string m, string message, string description, string emailBody, string smsBody, string emailBodySolved, string smsBodySolved)
        {

            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException("message");

            try
            {
                var updatedBy = new BasePage().UserName;

                var service = new MasterTableService().masterTableService;
                var result = service.UpdateMessageMaster(id, m, message, description
                        , emailBody, smsBody, emailBodySolved, smsBodySolved, updatedBy);

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

                new RMSWebException(this, "0500", "UpdateMessageMaster failed. " + ex.Message, ex, true);

                return Json(ret);
            }
        }

        // GET: /Monitoring/MasterTable/ListMessageMaster/
        public ActionResult ListMessageMaster()
        {
            try
            {
                var service = new MasterTableService().masterTableService;

                var result = service.ListMessageMaster();

                var ret = new
                {
                    listMessageMaster = result.ListMessageMasters,

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
                new RMSWebException(this, "0500", "ListMessageMaster failed. " + ex.Message, ex, true);

                return Json(ret);
            }
        }


        #endregion

        #region SystemConfig

        public ActionResult ListSystemConfig(JQueryDataTableParamModel param)
        {
            try
            {
                var service = new MasterTableService().masterTableService;

                var result = service.ListSystemConfig();

                if (result.ListSystemConfigs == null)
                    result.ListSystemConfigs = new List<RmsSystemConfig>();
                
                int? totalRecords = 0;
                totalRecords = result.ListSystemConfigs.Count;

                var data = new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords,
                    aaData = result.ListSystemConfigs,
                    status = (result.IsSuccess) ? 1 : 0,
                    error = result.ErrorMessage
                };

                return Json(data);

            }
            catch (Exception ex)
            {
                var ret = new
                {
                    status = -1,
                    error = ex.Message
                };
                new RMSWebException(this, "0500", "ListSystemConfig failed. " + ex.Message, ex, true);

                return Json(ret);
            }
        }

        public ActionResult GetSystemConfig(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            try
            {
                var service = new MasterTableService().masterTableService;

                var result = service.GetSystemConfig(name);

                if (result.SystemConfig == null)
                {
                    var notFound = new
                    {
                        status = 0,
                        error = "Config ("+name+") Not Found."
                    };
                    new RMSWebException(this, "0500", "GetSystemConfig ("+name+") failed. ", true);

                    return Json(notFound);
                }

                var ret = new
                {
                    status = (result.IsSuccess) ? 1 : 0,
                    name = result.SystemConfig.Name,
                    data = JsonConvert.SerializeObject(result.SystemConfig, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
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
                new RMSWebException(this, "0500", "GetSystemConfig failed. " + ex.Message, ex, true);

                return Json(ret);
            }

        }

        public ActionResult UpdateSystemConfig(string name, string value, string defaultValue, string description)
        {

            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            try
            {
                var updatedBy = new BasePage().UserName;

                var service = new MasterTableService().masterTableService;
                var result = service.UpdateSystemConfig(name, value, defaultValue, description, updatedBy);

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

                new RMSWebException(this, "0500", "UpdateSystemConfig failed. " + ex.Message, ex, true);

                return Json(ret);
            }
        }

        #endregion

    }
}