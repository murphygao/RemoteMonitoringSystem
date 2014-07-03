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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MonitoringProfileService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MonitoringProfileService.svc or MonitoringProfileService.svc.cs at the Solution Explorer and start debugging.
    public class MonitoringProfileService : IMonitoringProfileService
    {
        public void TestConnection()
        {
        }

        public MonitoringProfileResult List(bool? activeList)
        {
            try
            {
                BSL.MonitoringProfileService service = new BSL.MonitoringProfileService();
                var monitoringProfiles = service.List(activeList);

                var sr = new MonitoringProfileResult
                {
                    IsSuccess = true,
                    ListMonitoringProfiles = monitoringProfiles,
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "List failed. " + ex.Message, ex, true);

                var sr = new MonitoringProfileResult
                {
                    IsSuccess = false,
                    ErrorMessage = "List errors. " + ex.Message
                };
                return sr;
            }
        }

        public MonitoringProfileResult Get(int monitoringProfileID)
        {
            try
            {
                BSL.MonitoringProfileService service = new BSL.MonitoringProfileService();
                var monitoringProfile = service.Get(monitoringProfileID);

                var sr = new MonitoringProfileResult
                {
                    IsSuccess = true,
                    MonitoringProfile = monitoringProfile,
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "Get failed. " + ex.Message, ex, true);

                var sr = new MonitoringProfileResult
                {
                    IsSuccess = false,
                    ErrorMessage = "Get errors. " + ex.Message
                };
                return sr;
            }
        }

        public MonitoringProfileResult Search(JQueryDataTableParamModel param, string name, bool? activeList)
        {
            try
            {
                int totalRecord;
                BSL.MonitoringProfileService service = new BSL.MonitoringProfileService();
                List<MonitoringProfileInfo> monitoringProfileInfos = service.Search(param, name, activeList, out totalRecord);

                var sr = new MonitoringProfileResult
                {
                    IsSuccess = true,
                    ListMonitoringProfileInfo = monitoringProfileInfos,
                    TotalRecords = totalRecord
                };

                return sr;

            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "Search failed. " + ex.Message, ex, true);

                var sr = new MonitoringProfileResult
                {
                    IsSuccess = false,
                    ErrorMessage = "Search errors. " + ex.Message
                };
                return sr;
            }
        }

        public Result Update(int? id, string m, string profileName, bool activeList, string updatedBy)
        {
            try
            {
                if (!(string.IsNullOrEmpty(m) || m == "e")) throw new ArgumentException("m parameter (" + m + ") is incorrect format.", "m");

                if (string.IsNullOrEmpty(m))
                {
                    BSL.MonitoringProfileService service = new BSL.MonitoringProfileService();
                    var profile = service.Add(profileName, activeList, updatedBy);

                    var sr = new Result
                    {
                        IsSuccess = true,
                    };
                    return sr;
                }
                else if (m == "e")
                {
                    if (id == null) throw new ArgumentNullException("MonitoringProfileID");

                    BSL.MonitoringProfileService service = new BSL.MonitoringProfileService();
                    var profile = service.Update(id.Value, profileName, activeList, updatedBy);

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

        public Result Delete(int monitoringProfileID, string updatedBy)
        {
            try
            {
                BSL.MonitoringProfileService service = new BSL.MonitoringProfileService();
                var ret = service.Delete(monitoringProfileID, updatedBy);

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
