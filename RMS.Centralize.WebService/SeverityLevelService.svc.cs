using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Model;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SeverityLevelService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SeverityLevelService.svc or SeverityLevelService.svc.cs at the Solution Explorer and start debugging.
    public class SeverityLevelService : ISeverityLevelService
    {
        public void TestConnection()
        {
        }

        public SeverityLevelResult Search(JQueryDataTableParamModel param, string severityLevel, bool? activeList)
        {
            try
            {
                int totalRecord;

                BSL.SeverityLevelService service = new BSL.SeverityLevelService();
                var infos = service.Search(param, severityLevel, activeList, out totalRecord);

                var sr = new SeverityLevelResult
                {
                    IsSuccess = true,
                    ListSeverityLevelInfos = infos,
                    TotalRecords = totalRecord
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "Search failed. " + ex.Message, ex, true);

                var sr = new SeverityLevelResult
                {
                    IsSuccess = false,
                    ErrorMessage = "Search errors. " + ex.Message
                };
                return sr;
            }
        }

        public SeverityLevelResult List(bool? activeList)
        {
            try
            {
                BSL.SeverityLevelService service = new BSL.SeverityLevelService();
                var lists = service.List(activeList);

                var sr = new SeverityLevelResult
                {
                    IsSuccess = true,
                    ListSeverityLevels = lists,
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "List failed. " + ex.Message, ex, true);

                var sr = new SeverityLevelResult
                {
                    IsSuccess = false,
                    ErrorMessage = "List errors. " + ex.Message
                };
                return sr;
            }
        }

        public SeverityLevelResult Get(int severityLevelID)
        {
            try
            {
                BSL.SeverityLevelService service = new BSL.SeverityLevelService();
                var level = service.Get(severityLevelID);

                var sr = new SeverityLevelResult
                {
                    IsSuccess = true,
                    SeverityLevel = level,
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "Get failed. " + ex.Message, ex, true);

                var sr = new SeverityLevelResult
                {
                    IsSuccess = false,
                    ErrorMessage = "Get errors. " + ex.Message
                };
                return sr;
            }
        }

        public Result Update(int? id, string m, string levelCode, string levelName, int orderList, bool activeList, int defaultActionProfileID, string colorCode,
            bool actionRepeatable, int actionRepeatInterval, string updatedBy)
        {
            try
            {
                if (!(string.IsNullOrEmpty(m) || m == "e")) throw new ArgumentException("m parameter (" + m + ") is incorrect format.", "m");

                if (string.IsNullOrEmpty(m))
                {
                    BSL.SeverityLevelService service = new BSL.SeverityLevelService();
                    var profile = service.Add(levelCode, levelName, orderList, activeList
                        , defaultActionProfileID, colorCode, actionRepeatable, actionRepeatInterval, updatedBy);

                    var sr = new Result
                    {
                        IsSuccess = true,
                    };
                    return sr;
                }
                else if (m == "e")
                {
                    if (id == null) throw new ArgumentNullException("SeverityLevelID");

                    BSL.SeverityLevelService service = new BSL.SeverityLevelService();
                    var profile = service.Update(id.Value, levelCode, levelName, orderList, activeList
                        , defaultActionProfileID, colorCode, actionRepeatable, actionRepeatInterval, updatedBy);

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

        public Result Delete(int id, string updatedBy)
        {
            try
            {
                BSL.SeverityLevelService service = new BSL.SeverityLevelService();
                var ret = service.Delete(id, updatedBy);

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
