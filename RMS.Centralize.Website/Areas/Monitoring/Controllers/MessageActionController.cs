﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RMS.Centralize.WebSite.Proxy;
using RMS.Centralize.WebSite.Proxy.MessageActionProxy;
using RMS.Common.Exception;

namespace RMS.Centralize.Website.Areas.Monitoring.Controllers
{
    public class MessageActionController : Controller
    {

        #region Page: MessageAction

        // GET: /Monitoring/MessageAction/InitDataForMeesageAction/
        public ActionResult InitDataForMeesageAction()
        {

            try
            {
                var serviceClient = new MessageActionService().messsageActionService;
                var result = serviceClient.InitDataForMeesageAction();

                var data = new
                {
                    status = (result.IsSuccess) ? 1 : 0,
                    messageGroup = result.MessageGroup,
                    error = result.ErrorMessage
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
                new RMSWebException(this, "0500", "InitDataForMeesageAction failed. " + ex.Message, ex, true);

                return Json(data);
            }
        }

        // GET: /Monitoring/MessageAction/SearchMessageAction/
        public ActionResult SearchMessageAction(JQueryDataTableParamModel param, int? ddlMessageGroupID, string txtMessage, bool? ddlActiveStatus)
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

                var serviceClient = new RMS.Centralize.WebSite.Proxy.MessageActionService().messsageActionService;
                var result = serviceClient.Search(param, ddlMessageGroupID, txtMessage, ddlActiveStatus);



                int? totalRecords = 0;
                totalRecords = result.TotalRecords;

                var data = new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords,
                    aaData = result.ListMessageActions,
                    status = (result.IsSuccess) ? 1 : 0,
                    error = result.ErrorMessage
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

                return Json(data);
            }

        }


        // GET: /Monitoring/MessageAction/Delete/
        public ActionResult Delete(int? id)
        {

            try
            {
                if (id == null) throw new ArgumentNullException("MessageID");
            }
            catch (ArgumentNullException ex)
            {
                new RMSWebException(this, "0500", "Delete failed. " + ex.Message, ex, true);
                throw;
            }
            string ret;

            try
            {
                var serviceClient = new RMS.Centralize.WebSite.Proxy.MessageActionService().messsageActionService;
                var result = serviceClient.Delete(id);
                ret = result.IsSuccess ? "1" : "0";

            }
            catch (Exception ex)
            {
                ret = "0";
                new RMSWebException(this, "0500", "Delete failed. " + ex.Message, ex, true);
            }

            return Json(ret);
        }


        #endregion


        #region Page : MessageEdit

        // GET: /Monitoring/MessageAction/InitDataForMeesageEdit/
        public ActionResult InitDataForMeesageEdit()
        {

            try
            {
                var serviceClient = new MessageActionService().messsageActionService;
                var result = serviceClient.InitDataForMeesageEdit();

                var data = new
                {
                    status = (result.IsSuccess) ? 1 : 0,
                    messageGroup = result.MessageGroup,
                    severityLevel = result.SeverityLevel,
                    error = result.ErrorMessage
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
                new RMSWebException(this, "0500", "InitDataForMeesageEdit failed. " + ex.Message, ex, true);

                return Json(data);
            }
        }

        // GET: /Monitoring/ActionProfile/UpdateMessage/
        public ActionResult UpdateMessage(int? id, string m, int? messageGroupID, string message, int? severityLevelID, bool activeList, bool activeStatus)
        {

            try
            {
                if (messageGroupID == null) throw new ArgumentNullException("messageGroupID");
                if (string.IsNullOrEmpty(message)) throw new ArgumentNullException("message");

                if (m == "e" && id == null) throw new ArgumentNullException("id");
            }
            catch (ArgumentNullException ex)
            {
                new RMSWebException(this, "0500", "UpdateMessage failed. " + ex.Message, ex, true);
                throw;
            }

            try
            {
                var serviceClient = new MessageActionService().messsageActionService;
                var result = serviceClient.UpdateMessage(id, m, messageGroupID, message, severityLevelID, activeList, activeStatus);

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

                new RMSWebException(this, "0500", "UpdateMessage failed. " + ex.Message, ex, true);

                return Json(ret);
            }
        }


        // GET: /Monitoring/ActionProfile/GetMessage/
        public ActionResult GetMessage(int? id)
        {
            try
            {
                if (id == null) throw new ArgumentNullException("MessageID");
            }
            catch (ArgumentNullException ex)
            {
                new RMSWebException(this, "0500", "GetMessage failed. " + ex.Message, ex, true);
                throw;
            }
            try
            {
                var serviceClient = new MessageActionService().messsageActionService;
                var result = serviceClient.Get(id);

                var ret = new
                {
                    status = (result.IsSuccess) ? 1 : 0,
                    data = JsonConvert.SerializeObject(result.Message)
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
                new RMSWebException(this, "0500", "GetMessage failed. " + ex.Message, ex, true);

                return Json(ret);
            }
        }

        #endregion
    }
}