using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Services.Description;
using RMS.Centralize.WebService.Model;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WebsiteMonitoringService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WebsiteMonitoringService.svc or WebsiteMonitoringService.svc.cs at the Solution Explorer and start debugging.
    public class WebsiteMonitoringService : IWebsiteMonitoringService
    {
        public void TestConnection()
        {
        }

        public WebsiteMonitoringResult ListWebsiteMonitoringsByClient(JQueryDataTableParamModel param, int clientID)
        {
            try
            {
                int totalRecord;

                BSL.WebsiteMonitoringService service = new BSL.WebsiteMonitoringService();
                var infos = service.ListWebsiteMonitoringsByClient(clientID);

                var sr = new WebsiteMonitoringResult
                {
                    IsSuccess = true,
                    ListWebsiteMonitoringInfos = infos,
                    TotalRecords = infos.Count
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "ListWebsiteMonitoringsByClient failed. " + ex.Message, ex, true);

                var sr = new WebsiteMonitoringResult
                {
                    IsSuccess = false,
                    ErrorMessage = "ListWebsiteMonitoringsByClient failed. " + ex.Message
                };
                return sr;
            }
        }

        public WebsiteMonitoringResult GetWebsiteMonitoring(int websiteMonitoringID)
        {
            try
            {
                BSL.WebsiteMonitoringService service = new BSL.WebsiteMonitoringService();
                var rms = service.GetWebsiteMonitoring(websiteMonitoringID);

                var sr = new WebsiteMonitoringResult
                {
                    IsSuccess = true,
                    WebsiteMonitoringInfo = rms,
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "GetWebsiteMonitoring failed. " + ex.Message, ex, true);

                var sr = new WebsiteMonitoringResult
                {
                    IsSuccess = false,
                    ErrorMessage = "GetWebsiteMonitoring failed. " + ex.Message
                };
                return sr;
            }
        }

        public Result UpdateWebsiteMonitoring(int? id, string m, int clientID, string websiteMonitoringTypeID, string websiteMonitoringProtocolID,
            int? portNumber, string domainName, bool enable, string updatedBy)
        {
            try
            {
                if (!(string.IsNullOrEmpty(m) || m == "e")) throw new ArgumentException("m parameter (" + m + ") is incorrect format.", "m");


                if (string.IsNullOrEmpty(m))
                {
                    BSL.WebsiteMonitoringService service = new BSL.WebsiteMonitoringService();
                    var rms = service.AddWebsiteMonitoring(clientID, websiteMonitoringTypeID, websiteMonitoringProtocolID, portNumber, domainName, enable, updatedBy);

                    var sr = new Result
                    {
                        IsSuccess = true,
                    };
                    return sr;
                }
                else if (m == "e" && id != null)
                {

                    BSL.WebsiteMonitoringService service = new BSL.WebsiteMonitoringService();
                    var rms = service.UpdateWebsiteMonitoring(id.Value, clientID, websiteMonitoringTypeID, websiteMonitoringProtocolID, portNumber, domainName, enable, updatedBy);

                    var sr = new Result
                    {
                        IsSuccess = true,
                    };
                    return sr;
                }

                var wrongParam = new Result
                {
                    IsSuccess = false,
                    ErrorMessage = "UpdateWebsiteMonitoring errors. Please check m parameter (" + m + ")"
                };
                return wrongParam;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "UpdateWebsiteMonitoring failed. " + ex.Message, ex, true);

                var sr = new Result
                {
                    IsSuccess = false,
                    ErrorMessage = "UpdateWebsiteMonitoring errors. " + ex.Message
                };
                return sr;
            }
        }

        public Result DeleteWebsiteMonitoring(int websiteMonitoringID, string updatedBy)
        {
            try
            {
                BSL.WebsiteMonitoringService service = new BSL.WebsiteMonitoringService();
                var ret = service.DeleteWebsiteMonitoring(websiteMonitoringID, updatedBy);

                var sr = new Result
                {
                    IsSuccess = ret
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "DeleteWebsiteMonitoring failed. " + ex.Message, ex, true);

                var sr = new Result
                {
                    IsSuccess = false,
                    ErrorMessage = "DeleteWebsiteMonitoring errors. " + ex.Message
                };
                return sr;
            }
        }
    }
}
