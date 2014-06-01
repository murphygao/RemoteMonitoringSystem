using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RMS.Centralize.Website.Areas.Monitoring.Models;
using RMS.Centralize.WebSite.Proxy.ClientProxy;
using RMS.Centralize.WebSite.Proxy.SummaryReportProxy;
using RMS.Common.Exception;
using JQueryDataTableParamModel = RMS.Centralize.WebSite.Proxy.SummaryReportProxy.JQueryDataTableParamModel;

namespace RMS.Centralize.Website.Areas.Monitoring.Controllers
{
    public class SummaryReportController : Controller
    {
        //
        // GET: /Monitoring/SummaryReport/SearchMonitoringReport
        public ActionResult SearchMonitoringReport(JQueryDataTableParamModel param, DateTime? txtStartEventDate, DateTime? txtEndEventDate
            , string txtClientCode, string txtLocation, string ddlMessageGroup, string txtMessage, string ddlMessageStatus, int? clientID)
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

            if (!string.IsNullOrEmpty(ddlMessageStatus) && ddlMessageStatus.Split(',').Any(m => m == ""))
            {
                ddlMessageStatus = "";
            }

            try
            {

                var serviceClient = new RMS.Centralize.WebSite.Proxy.SummaryReportService().summaryReportService;
                var result = serviceClient.SearchSummaryMonitoring(param, txtStartEventDate, txtEndEventDate, txtClientCode, txtLocation
                    , ddlMessageGroup, txtMessage, ddlMessageStatus, clientID);



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
                new RMSWebException(this, "0500", "SearchMonitoringReport failed. " + ex.Message, ex, true);

                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }

        // GET: /Monitoring/SummaryReport/GetClientInfo/
        public ActionResult GetClientInfo(int? id)
        {
            try
            {
                if (id == null) throw new ArgumentNullException("ClientID");
            }
            catch (ArgumentNullException ex)
            {
                new RMSWebException(this, "0500", "GetClientInfo failed. " + ex.Message, ex, true);
                throw;
            }
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
                new RMSWebException(this, "0500", "GetClientInfo failed. " + ex.Message, ex, true);
                return Json(ret);
            }

        }

        // GET: /Monitoring/SummaryReport/SearchMonitoringReport
        public ActionResult SearchClientMonitoring(JQueryDataTableParamModel param, DateTime? txtStartEventDate, DateTime? txtEndEventDate
            , string txtClientCode, string txtLocation, string ddlMessageGroup, string txtMessage, string ddlMessageStatus, int? clientID)
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

            if (!string.IsNullOrEmpty(ddlMessageStatus) && ddlMessageStatus.Split(',').Any(m => m == ""))
            {
                ddlMessageStatus = "";
            }

            try
            {

                var service = new RMS.Centralize.WebSite.Proxy.SummaryReportService().summaryReportService;
                var result = service.SearchSummaryMonitoring(param, txtStartEventDate, txtEndEventDate, txtClientCode, txtLocation
                    , ddlMessageGroup, txtMessage, ddlMessageStatus, clientID);



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
                new RMSWebException(this, "0500", "SearchClientMonitoring failed. " + ex.Message, ex, true);

                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }


        // GET: /Monitoring/SummaryReport/GetCurrentDeviceStatus
        public ActionResult GetCurrentDeviceStatus(JQueryDataTableParamModel param, int? clientID)
        {
            param.iSortColumn = null;

            try
            {
                var serviceClient = new RMS.Centralize.WebSite.Proxy.ClientService().clientService;
                var clientResult = serviceClient.GetClient(GetClientBy.ClientID, clientID, null, null, true, false);
                if (clientResult != null && clientResult.IsSuccess)
                {
                    var service = new RMS.Centralize.WebSite.Proxy.SummaryReportService().summaryReportService;
                    var result = service.SearchSummaryMonitoring(param, null, null, null, null
                        , null, null, "1", clientID);

                    List<DeviceStatus> lDeviceStatuses = new List<DeviceStatus>();

                    foreach (var monitoringProfileDevice in clientResult.ListMonitoringProfileDevices)
                    {
                        RmsMonitoringProfileDevice device = monitoringProfileDevice;

                        if (result.ListSummaryMonitorings == null)
                            result.ListSummaryMonitorings = new List<ReportSummaryMonitoring>();

                        var summaryMonitorings = result.ListSummaryMonitorings.Where(smg => smg.MonitoringProfileDeviceId == device.MonitoringProfileDeviceId);
                        List<ReportSummaryMonitoring> lReports = new List<ReportSummaryMonitoring>(summaryMonitorings.ToList());


                        // ถ้าเจอ แสดงว่า ไม่ปกติ
                        if (lReports.Count > 0)
                        {
                            foreach (var report in lReports)
                            {
                                int? deviceTypeID = FindDeviceTypeIDByDeviceID(clientResult.ListDevices, report.DeviceId);
                                var dt = clientResult.ListDeviceType.First(t => t.DeviceTypeId == deviceTypeID);
                                if (dt != null)
                                {
                                    DeviceStatus ds = new DeviceStatus(report.Id, dt.DeviceTypeId, dt.DeviceType, dt.DisplayOrder,
                                        device.DeviceDescription
                                        , report.Status, report.MessageId, report.Message, report.MessageRemark, report.MonitoringProfileDeviceId
                                        , report.LastActionDateTime, report.LastActionType, report.LevelName
                                        , report.ColorCode, report.ColorTagStart, report.ColorTagEnd, report.MessageDateTime);

                                    lDeviceStatuses.Add(ds);
                                }

                            }

                        }
                        // ถ้าไม่พบ แสดงว่า อุปกรณ์ปกติ ให้ใส่ dummy data
                        else
                        {
                            int? deviceTypeID = FindDeviceTypeIDByDeviceID(clientResult.ListDevices, device.DeviceId);
                            var dt = clientResult.ListDeviceType.First(t => t.DeviceTypeId == deviceTypeID);
                            if (dt != null)
                            {
                                DeviceStatus ds = new DeviceStatus(0, dt.DeviceTypeId, dt.DeviceType, dt.DisplayOrder,
                                    device.DeviceDescription, 0, 0, "Good", null, device.MonitoringProfileDeviceId, null, null, null, "success", "<span class=\"label label-success\">", "</span>", null);

                                lDeviceStatuses.Add(ds);
                            }

                        }


                    }

                    lDeviceStatuses = new List<DeviceStatus>(lDeviceStatuses.OrderBy(o => o.DisplayOrder).ThenBy(o => o.DeviceDescription).ThenBy(o => o.MessageDateTime));

                    string deviceName = "";
                    foreach (var deviceStatus in lDeviceStatuses)
                    {
                        if (deviceStatus.DeviceDescription == deviceName)
                        {
                            deviceStatus.DeviceDescription = "";
                            continue;
                        }
                        deviceName = deviceStatus.DeviceDescription;
                    }

                    int? totalRecords = 0;
                    totalRecords = lDeviceStatuses.Count;
                    var data = new
                    {
                        sEcho = param.sEcho,
                        iTotalRecords = totalRecords,
                        iTotalDisplayRecords = totalRecords,
                        aaData = lDeviceStatuses,
                        isSuccess = result.IsSuccess,
                        errorMessage = result.ErrorMessage
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }



                var data2 = new
                {
                    isSuccess = false,
                    errorMessage = "Client Not Found"
                };

                return Json(data2, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {

                var data = new
                {
                    isSuccess = false,
                    errorMessage = ex.Message
                };
                new RMSWebException(this, "0500", "GetCurrentDeviceStatus failed. " + ex.Message, ex, true);

                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }

        // GET: /Monitoring/SummaryReport/ResendAction
        public ActionResult ResendAction(ActionServiceActionSendType sentType, long? id)
        {
            if (id == null)
            {
                
                var idError = new
                {
                    isSuccess = false,
                    errorMessage = "ID cannot be null."
                };

                return Json(idError, JsonRequestBehavior.AllowGet);

            }
            try
            {
                var service = new RMS.Centralize.WebSite.Proxy.SummaryReportService().summaryReportService;
                var actionRequest = service.ActionRequest(sentType, id.Value);

                var ret = new
                {
                    isSuccess = actionRequest.IsSuccess,
                    errorMessage = actionRequest.ErrorMessage
                };

                return Json(ret, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                var exError = new
                {
                    isSuccess = false,
                    errorMessage = ex.Message
                };
                new RMSWebException(this, "0500", "ResendAction failed. " + ex.Message, ex, true);

                return Json(exError, JsonRequestBehavior.AllowGet);
            }
        }

        private int? FindDeviceTypeIDByDeviceID(List<RmsDevice> devices, int? deviceID)
        {
            try
            {
                if (deviceID == null) return null;
                var rmsDevice = devices.First(d => d.DeviceId == deviceID);
                if (rmsDevice != null) return rmsDevice.DeviceTypeId;
                return null;
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "FindDeviceTypeIDByDeviceID failed. " + ex.Message, ex, false);
            }
        }
    }
}