using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Model;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MasterTableService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MasterTableService.svc or MasterTableService.svc.cs at the Solution Explorer and start debugging.
    public class MasterTableService : IMasterTableService
    {
        public void TestConnection()
        {

        }

        #region Color Label

        public MasterTableResult ListColorLabels()
        {
            try
            {
                BSL.MasterTableService service = new BSL.MasterTableService();
                var lists = service.ListColorLabels();

                var sr = new MasterTableResult
                {
                    IsSuccess = true,
                    ListColorLabels = lists,
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "List failed. " + ex.Message, ex, true);

                var sr = new MasterTableResult
                {
                    IsSuccess = false,
                    ErrorMessage = "List errors. " + ex.Message
                };
                return sr;
            }
        }

        #endregion

        #region Message Master

        public MasterTableResult SearchMessageMaster(JQueryDataTableParamModel param, string message)
        {
            try
            {
                int totalRecord;

                BSL.MasterTableService service = new BSL.MasterTableService();
                var infos = service.SearchMessageMaster(param, message, out totalRecord);

                var sr = new MasterTableResult
                {
                    IsSuccess = true,
                    ListMessageMasterInfos = infos,
                    TotalRecords = totalRecord
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "SearchMessageMaster failed. " + ex.Message, ex, true);

                var sr = new MasterTableResult
                {
                    IsSuccess = false,
                    ErrorMessage = "SearchMessageMaster errors. " + ex.Message
                };
                return sr;
            }
        }

        public MasterTableResult ListMessageMaster()
        {
            try
            {
                BSL.MasterTableService service = new BSL.MasterTableService();
                var lists = service.ListMessageMaster();

                var sr = new MasterTableResult
                {
                    IsSuccess = true,
                    ListMessageMasters = lists,
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "ListMessageMaster failed. " + ex.Message, ex, true);

                var sr = new MasterTableResult
                {
                    IsSuccess = false,
                    ErrorMessage = "ListMessageMaster errors. " + ex.Message
                };
                return sr;
            }
        }

        public MasterTableResult GetMessageMaster(string message)
        {
            try
            {
                BSL.MasterTableService service = new BSL.MasterTableService();
                var rms = service.GetMessageMaster(message);

                var sr = new MasterTableResult
                {
                    IsSuccess = true,
                    MessageMaster = rms,
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "GetMessageMaster failed. " + ex.Message, ex, true);

                var sr = new MasterTableResult
                {
                    IsSuccess = false,
                    ErrorMessage = "GetMessageMaster errors. " + ex.Message
                };
                return sr;
            }
        }

        public Result UpdateMessageMaster(string id, string m, string message, string description, string emailBody, string smsBody,
            string emailBodySolved, string smsBodySolved, string updatedBy)
        {
            try
            {
                if (!(string.IsNullOrEmpty(m) || m == "e")) throw new ArgumentException("m parameter (" + m + ") is incorrect format.", "m");


                if (string.IsNullOrEmpty(m))
                {
                    if (string.IsNullOrEmpty(message)) throw new ArgumentNullException("message");

                    BSL.MasterTableService service = new BSL.MasterTableService();
                    var rms = service.AddMessageMaster(message, description, emailBody, smsBody, emailBodySolved, smsBodySolved, updatedBy);

                    var sr = new Result
                    {
                        IsSuccess = true,
                    };
                    return sr;
                }
                else if (m == "e" && !string.IsNullOrEmpty(id))
                {

                    BSL.MasterTableService service = new BSL.MasterTableService();
                    var rms = service.UpdateMessageMaster(id, description, emailBody, smsBody, emailBodySolved, smsBodySolved, updatedBy);

                    var sr = new Result
                    {
                        IsSuccess = true,
                    };
                    return sr;
                }

                var wrongParam = new Result
                {
                    IsSuccess = false,
                    ErrorMessage = "UpdateMessageMaster errors. Please check m parameter (" + m + ")"
                };
                return wrongParam;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "Update failed. " + ex.Message, ex, true);

                var sr = new Result
                {
                    IsSuccess = false,
                    ErrorMessage = "UpdateMessageMaster errors. " + ex.Message
                };
                return sr;
            }
        }

        public Result DeleteMessageMaster(string message, string updatedBy)
        {
            try
            {
                if (string.IsNullOrEmpty(message)) throw new ArgumentNullException("message");

                BSL.MasterTableService service = new BSL.MasterTableService();
                var ret = service.DeleteMessageMaster(message, updatedBy);

                var sr = new Result
                {
                    IsSuccess = ret
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "DeleteMessageMaster failed. " + ex.Message, ex, true);

                var sr = new Result
                {
                    IsSuccess = false,
                    ErrorMessage = "DeleteMessageMaster errors. " + ex.Message
                };
                return sr;
            }
        }

        #endregion

        #region System Config

        public MasterTableResult ListSystemConfig()
        {
            try
            {
                BSL.MasterTableService service = new BSL.MasterTableService();
                var lists = service.ListSystemConfig();

                var sr = new MasterTableResult
                {
                    IsSuccess = true,
                    ListSystemConfigs = lists,
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "ListSystemConfig failed. " + ex.Message, ex, true);

                var sr = new MasterTableResult
                {
                    IsSuccess = false,
                    ErrorMessage = "ListSystemConfig errors. " + ex.Message
                };
                return sr;
            }
        }

        public MasterTableResult GetSystemConfig(string name)
        {
            try
            {
                BSL.MasterTableService service = new BSL.MasterTableService();
                var rms = service.GetSystemConfig(name);

                var sr = new MasterTableResult
                {
                    IsSuccess = true,
                    SystemConfig = rms,
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "GetSystemConfig failed. " + ex.Message, ex, true);

                var sr = new MasterTableResult
                {
                    IsSuccess = false,
                    ErrorMessage = "GetSystemConfig errors. " + ex.Message
                };
                return sr;
            }
        }

        public Result UpdateSystemConfig(string name, string value, string defaultValue, string description, string updatedBy)
        {
            try
            {

                BSL.MasterTableService service = new BSL.MasterTableService();
                var rms = service.UpdateSystemConfig(name, value, defaultValue, description, updatedBy);

                var sr = new Result
                {
                    IsSuccess = true,
                };
                return sr;

            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "UpdateSystemConfig failed. " + ex.Message, ex, true);

                var sr = new Result
                {
                    IsSuccess = false,
                    ErrorMessage = "UpdateSystemConfig errors. " + ex.Message
                };
                return sr;
            }
        }

        public MasterTableResult ListLOVByListName(string listName)
        {
            try
            {
                BSL.MasterTableService service = new BSL.MasterTableService();
                var rms = service.ListLOVByListName(listName);

                var sr = new MasterTableResult
                {
                    IsSuccess = true,
                    ListListOfValueInfos = rms,
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "ListLOVByListName failed. " + ex.Message, ex, true);

                var sr = new MasterTableResult
                {
                    IsSuccess = false,
                    ErrorMessage = "ListLOVByListName errors. " + ex.Message
                };
                return sr;
            }
        }

        #endregion

    }
}
