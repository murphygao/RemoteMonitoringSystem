using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Model;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService.BSL
{
    public class MasterTableService : BaseService
    {

        #region ColorLabel

        public List<RmsColorLabel> ListColorLabels()
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.RmsColorLabels;

                    List<RmsColorLabel> listLocations = new List<RmsColorLabel>(listOfType.ToList());

                    return listLocations;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "ListColorLabels failed. " + ex.Message, ex, false);
            }
        }

        #endregion

        #region MessageMaster

        public List<RmsMessageMaster> ListMessageMaster()
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.RmsMessageMasters;

                    List<RmsMessageMaster> listLocations = new List<RmsMessageMaster>(listOfType.ToList());

                    return listLocations;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "ListMessageMaster failed. " + ex.Message, ex, false);
            }

        }

        public List<MessageMasterInfo> SearchMessageMaster(JQueryDataTableParamModel param, string message, out int totalRecord)
        {
            List<MessageMasterInfo> lResult = new List<MessageMasterInfo>();
            SqlParameter[] parameters = new SqlParameter[6];

            try
            {
                using (var db = new MyDbContext())
                {
                    SqlParameter p1 = new SqlParameter("PageNbr", DBNull.Value);
                    SqlParameter p2 = new SqlParameter("PageSize", param.iDisplayLength);
                    SqlParameter p3 = new SqlParameter("FirstRec", param.iDisplayStart);

                    SqlParameter p4;
                    if (String.IsNullOrEmpty(param.iSortColumn))
                    {
                        p4 = new SqlParameter("SortCol", DBNull.Value);
                    }
                    else
                    {
                        p4 = new SqlParameter("SortCol", param.iSortColumn);
                    }

                    SqlParameter p5 = new SqlParameter("TotalRecords", SqlDbType.Int);
                    p5.Direction = ParameterDirection.Output;

                    SqlParameter p6;

                    if (String.IsNullOrEmpty(message))
                    {
                        p6 = new SqlParameter("Message", DBNull.Value);
                    }
                    else
                    {
                        p6 = new SqlParameter("Message", message);
                    }

                    parameters[0] = p1;
                    parameters[1] = p2;
                    parameters[2] = p3;
                    parameters[3] = p4;
                    parameters[4] = p5;
                    parameters[5] = p6;

                    db.Configuration.ProxyCreationEnabled = false;
                    //db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.Database.SqlQuery<MessageMasterInfo>("RMS_SearchMessageMaster " +
                                                                             "@Message" +
                                                                             ", @PageNbr, @PageSize, @FirstRec, @SortCol, @TotalRecords OUTPUT"
                        , parameters);

                    lResult = new List<MessageMasterInfo>(listOfType.ToList());
                    totalRecord = (int) parameters[4].Value;

                    return lResult;
                }

            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "SearchMessageMaster failed. " + ex.Message, ex, false);
            }
        }

        public RmsMessageMaster GetMessageMaster(string message)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    return db.RmsMessageMasters.Find(message);

                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "GetMessageMaster failed. " + ex.Message, ex, false);
            }

        }

        public RmsMessageMaster AddMessageMaster(string message, string description, string emailBody, string smsBody, string emailBodySolved,
            string smsBodySolved, string updatedBy)
        {
            try
            {
                if (GetMessageMaster(message) != null) throw new Exception("Message (" + message + ") exists already");

                using (var db = new MyDbContext())
                {
                    //RMS_MessageMaster
                    var rms = db.RmsMessageMasters.Create();

                    rms.Message = message;
                    rms.Description = description;
                    rms.EmailBody = emailBody;
                    rms.SmsBody = smsBody;
                    rms.EmailBodySolved = emailBodySolved;
                    rms.SmsBodySolved = smsBodySolved;
                    rms.CreatedBy = updatedBy;
                    rms.UpdatedBy = updatedBy;

                    db.RmsMessageMasters.Add(rms);
                    db.SaveChanges();

                    return rms;

                }

            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "AddMessageMaster failed. " + ex.Message, ex, true);
            }
        }

        public RmsMessageMaster UpdateMessageMaster(string message, string description, string emailBody, string smsBody, string emailBodySolved,
            string smsBodySolved, string updatedBy)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    //RMS_MessageMaster
                    var rms = db.RmsMessageMasters.FirstOrDefault(w => w.Message == message);
                    if (rms == null) throw new Exception("MessageMaster (" + message + ") Not Found");

                    rms.Description = description;
                    rms.EmailBody = emailBody;
                    rms.SmsBody = smsBody;
                    rms.EmailBodySolved = emailBodySolved;
                    rms.SmsBodySolved = smsBodySolved;
                    rms.UpdatedBy = updatedBy;

                    db.SaveChanges();

                    return rms;
                }

            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "UpdateMessageMaster failed. " + ex.Message, ex, true);
            }
        }

        public bool DeleteMessageMaster(string message, string updatedBy)
        {
            try
            {
                if (string.IsNullOrEmpty(message)) throw new ArgumentNullException("message");

                using (var db = new MyDbContext())
                {
                    var rms = db.RmsMessageMasters.Create();
                    rms.Message = message;
                    db.RmsMessageMasters.Attach(rms);
                    db.RmsMessageMasters.Remove(rms);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "DeleteMessageMaster (" + message + ") failed. " + ex.Message, ex, true);

            }
        }

        #endregion


        #region System Configuration

        public List<RmsSystemConfig> ListSystemConfig()
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.RmsSystemConfigs;

                    List<RmsSystemConfig> listRMS = new List<RmsSystemConfig>(listOfType.ToList());

                    return listRMS;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "ListSystemConfig failed. " + ex.Message, ex, false);
            }

        }

        public RmsSystemConfig GetSystemConfig(string name)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    return db.RmsSystemConfigs.Find(name);

                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "GetSystemConfig failed. " + ex.Message, ex, false);
            }

        }

        public RmsSystemConfig UpdateSystemConfig(string name, string value, string defaultValue, string description, string updatedBy)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    //RMS_MessageMaster
                    var rms = db.RmsSystemConfigs.Find(name);
                    if (rms == null) throw new Exception("SystemConfig (" + name + ") Not Found");

//                    rms.Name = name;
                    rms.Value = string.IsNullOrEmpty(value) ? null : value.Trim();
//                    rms.DefaultValue = defaultValue;
                    rms.Description = string.IsNullOrEmpty(description) ? null : description.Trim();
                    rms.UpdatedBy = updatedBy;

                    db.SaveChanges();

                    return rms;
                }

            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "UpdateSystemConfig failed. " + ex.Message, ex, true);
            }
        }


        #endregion


        #region List Of Value

        public List<ListOfValueInfo> ListLOVByListName(string listName)
        {
            List<ListOfValueInfo> lResult = new List<ListOfValueInfo>();
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                using (var db = new MyDbContext())
                {
                    SqlParameter p1;

                    if (String.IsNullOrEmpty(listName))
                    {
                        p1 = new SqlParameter("ListName", DBNull.Value);
                    }
                    else
                    {
                        p1 = new SqlParameter("ListName", listName);
                    }

                    parameters[0] = p1;

                    db.Configuration.ProxyCreationEnabled = false;
                    //db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.Database.SqlQuery<ListOfValueInfo>("RMS_ListListOfValueByListName " +
                                                                             "@ListName"
                        , parameters);

                    lResult = new List<ListOfValueInfo>(listOfType.ToList());

                    return lResult;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "ListLOVByListName failed. " + ex.Message, ex, false);
            }

        }


        #endregion

    }
        
}