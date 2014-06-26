using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RMS.Centralize.WebService.Model;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ClientMonitoringService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ClientMonitoringService.svc or ClientMonitoringService.svc.cs at the Solution Explorer and start debugging.
    public class ClientMonitoringService : IClientMonitoringService
    {
        public void TestConnection()
        {

        }

        public ClientMonitoringResult ListByClient(int clientID)
        {
            try
            {
                BSL.ClientMonitoringService service = new BSL.ClientMonitoringService();
                var lists = service.ListByClient(clientID);

                var sr = new ClientMonitoringResult
                {
                    IsSuccess = true,
                    ListMonitoringProfileInfos = lists,
                    TotalRecords = lists.Count
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "ListByClient (" + clientID + ") failed. " + ex.Message, ex, true);

                var sr = new ClientMonitoringResult
                {
                    IsSuccess = false,
                    ErrorMessage = "List errors. " + ex.Message
                };
                return sr;
            }
        }

        public ClientMonitoringResult Get(int clientID, int monitoringProfileID)
        {
            try
            {
                BSL.ClientMonitoringService service = new BSL.ClientMonitoringService();
                var monitoring = service.Get(clientID, monitoringProfileID);

                var sr = new ClientMonitoringResult
                {
                    IsSuccess = true,
                    MonitoringProfileInfo = monitoring,
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "Get failed. " + ex.Message, ex, true);

                var sr = new ClientMonitoringResult
                {
                    IsSuccess = false,
                    ErrorMessage = "Get errors. " + ex.Message
                };
                return sr;
            }
        }

        public Result Update(int clientID, int monitoringProfileID, string m, DateTime effectiveDate, string updatedBy)
        {
            try
            {
                if (!(string.IsNullOrEmpty(m) || m == "e")) throw new ArgumentException("m parameter (" + m + ") is incorrect format.", "m");

                if (string.IsNullOrEmpty(m))
                {

                    BSL.ClientMonitoringService service = new BSL.ClientMonitoringService();
                    var cm = service.Add(clientID, monitoringProfileID, effectiveDate, updatedBy);

                    var sr = new Result
                    {
                        IsSuccess = true,
                    };
                    return sr;
                }
                else if (m == "e")
                {

                    BSL.ClientMonitoringService service = new BSL.ClientMonitoringService();
                    var profile = service.Update(clientID, monitoringProfileID, effectiveDate, updatedBy);

                    var sr = new Result
                    {
                        IsSuccess = true,
                    };
                    return sr;
                }
                var wrongParam = new Result
                {
                    IsSuccess = false,
                    ErrorMessage = "Update errors. Please check m parameter (" + m + ")"
                };
                return wrongParam;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "Update failed. " + ex.Message, ex, true);

                var sr = new Result
                {
                    IsSuccess = false,
                    ErrorMessage = "Update errors. " + ex.Message
                };
                return sr;
            }
        }

        public Result Delete(int clientID, int monitoringProfileID, string updatedBy)
        {
            try
            {
                BSL.ClientMonitoringService service = new BSL.ClientMonitoringService();
                var ret = service.Delete(clientID, monitoringProfileID, updatedBy);

                var sr = new Result
                {
                    IsSuccess = ret
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "Delete failed. " + ex.Message, ex, true);

                var sr = new Result
                {
                    IsSuccess = false,
                    ErrorMessage = "Delete errors. " + ex.Message
                };
                return sr;
            }
        }
    }
}
