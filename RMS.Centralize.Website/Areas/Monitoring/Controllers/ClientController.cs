using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RMS.Centralize.WebSite.Proxy;
using RMS.Centralize.WebSite.Proxy.ClientProxy;
using RMS.Common.Exception;

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

                return Json(data);

            }
            catch (Exception ex)
            {
                var data = new
                {
                    isSuccess = false,
                    errorMessage = ex.Message
                };
                new RMSWebException(this, "0500", "ExistingClientCode failed. " + ex.Message, ex, true);

                return Json(data);
            }
        }

        // GET: /Monitoring/Client/GetClient/
        public ActionResult GetClient(int? id)
        {
            try
            {
                var cClient = new ClientService().clientService;
                var searchResult = cClient.GetClient(GetClientBy.ClientID, id, null, null, false, false);
                var data = new
                {
                    data = JsonConvert.SerializeObject(searchResult.Client),
                    status = (searchResult.IsSuccess) ? 1 : 0,
                    errorMessage = searchResult.ErrorMessage
                };

                return Json(data);

            }
            catch (Exception ex)
            {
                var data = new
                {
                    status = 0,
                    errorMessage = ex.Message
                };
                new RMSWebException(this, "0500", "GetClient failed. " + ex.Message, ex, true);

                return Json(data);
            }
        }

        // GET: /Monitoring/Client/UpdateClient/
        public ActionResult UpdateClient(int? id, string m, string clientCode, int? clientTypeID, bool? useLocalInfo, int? referenceClientID, string ipAddress, int? locationID, bool? hasMonitoringAgent, bool? activeList, bool? status, DateTime? effectiveDate, DateTime? expiredDate, int? state)
        {

            if (string.IsNullOrEmpty(clientCode)) throw new ArgumentNullException("clientCode");
            if (clientTypeID == null) throw new ArgumentNullException("clientTypeID");
            if (useLocalInfo == null) throw new ArgumentNullException("useLocalInfo");
            if (useLocalInfo.Value)
            {
                if (ipAddress == null) throw new ArgumentNullException("ipAddress");
                if (locationID == null) throw new ArgumentNullException("locationID");
            }
            else
            {
                if (referenceClientID == null) throw new ArgumentNullException("referenceClientID");
            }
            if (status == null) throw new ArgumentNullException("status");
            if (effectiveDate == null) throw new ArgumentNullException("effectiveDate");

            if (m == "e" && id == null) throw new ArgumentNullException("id");

            try
            {
                var updatedBy = new BasePage().UserName;

                var apClient = new ClientService().clientService;
                var result = apClient.Update(id, m, clientCode, clientTypeID, useLocalInfo, referenceClientID, ipAddress, locationID, hasMonitoringAgent, activeList, status, effectiveDate, expiredDate, state, updatedBy);

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
                new RMSWebException(this, "0500", "UpdateClient failed. " + ex.Message, ex, true);

                return Json(ret);
            }
        }

        // GET: /Monitoring/Client/InitDataForEdit/
        public ActionResult InitDataForEdit()
        {
            try
            {
                var cClient = new ClientService().clientService;
                var listResult = cClient.ListMainAppClient();

                string ddlMainAppClient = string.Empty;

                if (listResult.IsSuccess)
                {
                    ddlMainAppClient = "<option value=\"\">Please Select</option>";
                    foreach (MainAppClient app in listResult.ListMainAppClients)
                    {
                        ddlMainAppClient += "<option value=\"" + app.ClientID + "\">" + app.ClientCode + " - " + app.ClientName + "</option>";
                    }
                }

                var retLocation = cClient.ListLocation();

                string ddlLocation = string.Empty;

                if (listResult.IsSuccess)
                {
                    ddlLocation = "<option value=\"\">Please Select</option>";
                    foreach (RmsLocation loc in retLocation.ListLocations)
                    {
                        ddlLocation += "<option value=\"" + loc.LocationId + "\">" + loc.LocationCode + " - " + loc.LocationName + "</option>";
                    }
                }

                var data = new
                {
                    listMainAppClients = ddlMainAppClient,
                    listLocation = ddlLocation,
                    status = (listResult.IsSuccess) ? 1 : 0,
                    errorMessage = listResult.ErrorMessage
                };

                return Json(data);
            }
            catch (Exception ex)
            {
                var data = new
                {
                    status = 0,
                    errorMessage = ex.Message
                };
                new RMSWebException(this, "0500", "ListMainAppClient failed. " + ex.Message, ex, true);

                return Json(data);
            }
        }



        #region Client Monitoring

        public ActionResult ListClientMonitoring(JQueryDataTableParamModel param, int? clientID)
        {
            if (clientID == null) throw new ArgumentNullException("ClientID");

            try
            {
                var service = new ClientMonitoringService().clientMonitoringService;

                var result = service.ListByClient(clientID.Value);

                var ret = new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = result.TotalRecords,
                    iTotalDisplayRecords = result.TotalRecords,
                    aaData = result.ListMonitoringProfileInfos,
                    isSuccess = result.IsSuccess,
                    errorMessage = result.ErrorMessage
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
                new RMSWebException(this, "0500", "ListClientMonitoring failed. " + ex.Message, ex, true);

                return Json(ret);
            }
        }

        // GET: /Monitoring/Location/Delete/
        public ActionResult DeleteClientMonitoring(int? clientID, int? monitoringProfileID)
        {

            if (clientID == null) throw new ArgumentNullException("ClientID");
            if (monitoringProfileID == null) throw new ArgumentNullException("MonitoringProfileID");

            string ret;

            try
            {
                var service = new ClientMonitoringService().clientMonitoringService;

                var updatedBy = new BasePage().UserName;

                var result = service.Delete(clientID.Value, monitoringProfileID.Value, updatedBy);

                ret = result.IsSuccess ? "1" : "0";

            }
            catch (Exception ex)
            {
                ret = "0";
                new RMSWebException(this, "0500", "DeleteClientMonitoring failed. " + ex.Message, ex, true);
            }

            return Json(ret);
        }

        public ActionResult AddClientMonitoring(int? clientID, int? monitoringProfileID, DateTime? effectiveDate)
        {
            if (clientID == null) throw new ArgumentNullException("ClientID");
            if (monitoringProfileID == null) throw new ArgumentNullException("MonitoringProfileID");
            if (effectiveDate == null) throw new ArgumentNullException("EffectiveDate");

            try
            {
                var service = new ClientMonitoringService().clientMonitoringService;
                var updatedBy = new BasePage().UserName;
                var result = service.Update(clientID.Value, monitoringProfileID.Value, null, effectiveDate.Value, updatedBy);

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

                new RMSWebException(this, "0500", "AddClientMonitoring failed. " + ex.Message, ex, true);

                return Json(ret);
            }

        }


        #endregion

        public ActionResult ListClientSeverityAction(JQueryDataTableParamModel param, int? clientID)
        {
            if (clientID == null) throw new ArgumentNullException("ClientID");

            try
            {
                var service = new ClientService().clientService;

                var result = service.ListClientSeverityActions(clientID.Value);

                var ret = new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = result.TotalRecords,
                    iTotalDisplayRecords = result.TotalRecords,
                    aaData = result.ListClientSeverityActionInfos,
                    isSuccess = result.IsSuccess,
                    errorMessage = result.ErrorMessage
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
                new RMSWebException(this, "0500", "ListClientSeverityAction failed. " + ex.Message, ex, true);

                return Json(ret);
            }
        }

        public ActionResult UpdateClientSeverityAction(int? clientID, int? severityLevelID, bool? overwritenAction, string email, string sms, int? commandLineID)
        {
            try
            {
                if (clientID == null) throw new ArgumentNullException("ClientID");
                if (severityLevelID == null) throw new ArgumentNullException("SeverityLevelID");
                if (overwritenAction == null) throw new ArgumentNullException("OverwritenAction");

                var service = new ClientService().clientService;
                var updatedBy = new BasePage().UserName;
                var result = service.UpdateClientSeverityAction(clientID.Value, severityLevelID.Value, overwritenAction.Value, email, sms, commandLineID, updatedBy);

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

                new RMSWebException(this, "0500", "UpdateClientSeverityAction failed. " + ex.Message, ex, true);

                return Json(ret);
            }

        }

        public ActionResult GetClientSeverityAction(int? clientID, int? severityLevelID)
        {
            if (clientID == null) throw new ArgumentNullException("ClientID");
            if (severityLevelID == null) throw new ArgumentNullException("SeverityLevelID");

            try
            {
                var service = new ClientService().clientService;

                var result = service.GetClientSeverityAction(clientID.Value, severityLevelID.Value);


                var ret = new
                {
                    status = (result.IsSuccess) ? 1 : 0,
                    ClientID = result.Info.ClientId,
                    SeverityLevelID = result.Info.SeverityLevelId,
                    data = JsonConvert.SerializeObject(result.Info, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
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
                new RMSWebException(this, "0500", "GetClientSeverityAction failed. " + ex.Message, ex, true);

                return Json(ret);
            }

        }

    }
}