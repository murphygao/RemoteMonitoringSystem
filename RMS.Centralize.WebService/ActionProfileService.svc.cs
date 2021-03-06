﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Model;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ActionProfileService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ActionProfileService.svc or ActionProfileService.svc.cs at the Solution Explorer and start debugging.
    public class ActionProfileService : IActionProfileService
    {
        public void TestConnection()
        {
            
        }

        public ActionProfileResult List(bool? activeList)
        {
            try
            {
                BSL.ActionProfileService service = new BSL.ActionProfileService();
                var lists = service.List(activeList);

                var sr = new ActionProfileResult
                {
                    IsSuccess = true,
                    ListActionProfiles = lists,
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "List failed. " + ex.Message, ex, true);

                var sr = new ActionProfileResult
                {
                    IsSuccess = false,
                    ErrorMessage = "List errors. " + ex.Message
                };
                return sr;
            }
        }

        public ActionProfileResult Search(JQueryDataTableParamModel param, string txtActionProfile, string txtEmail, string txtSms)
        {

            List<RmsActionProfile> listRmsActionProfile = new List<RmsActionProfile>();
            SqlParameter[] parameters = new SqlParameter[8];

            try
            {
                using (var db = new MyDbContext())
                {
                    SqlParameter p1;
                    SqlParameter p2;
                    SqlParameter p3;

                    if (String.IsNullOrEmpty(txtActionProfile))
                    {
                        p1 = new SqlParameter("ActionProfileName", DBNull.Value);
                    }
                    else
                    {
                        p1 = new SqlParameter("ActionProfileName", txtActionProfile);
                    }

                    if (String.IsNullOrEmpty(txtEmail))
                    {
                        p2 = new SqlParameter("Email", DBNull.Value);
                    }
                    else
                    {
                        p2 = new SqlParameter("Email", txtEmail);
                    }

                    if (String.IsNullOrEmpty(txtSms))
                    {
                        p3 = new SqlParameter("Sms", DBNull.Value);
                    }
                    else
                    {
                        p3 = new SqlParameter("Sms", txtSms);
                    }
                    SqlParameter p4 = new SqlParameter("PageNbr", DBNull.Value);
                    SqlParameter p5 = new SqlParameter("PageSize", param.iDisplayLength);
                    SqlParameter p6 = new SqlParameter("FirstRec", param.iDisplayStart);
                    SqlParameter p7 = new SqlParameter("SortCol", param.iSortColumn);
                    SqlParameter p8 = new SqlParameter("TotalRecords", SqlDbType.Int);
                    p8.Direction = ParameterDirection.Output;

                    parameters[0] = p1;
                    parameters[1] = p2;
                    parameters[2] = p3;
                    parameters[3] = p4;
                    parameters[4] = p5;
                    parameters[5] = p6;
                    parameters[6] = p7;
                    parameters[7] = p8;

                    db.Configuration.ProxyCreationEnabled = false;
                    //db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.Database.SqlQuery<RmsActionProfile>("RMS_SearchActionProfile " +
                                                                            "@ActionProfileName, @Email, @Sms, @PageNbr, @PageSize, @FirstRec, @SortCol, @TotalRecords OUTPUT"
                        , parameters);

                    listRmsActionProfile = new List<RmsActionProfile>(listOfType.ToList());

                    ActionProfileResult sr = new ActionProfileResult
                    {
                        IsSuccess = true,
                        ListActionProfiles = listRmsActionProfile, 
                        TotalRecords = (int) parameters[7].Value
                    };
                    return sr;
                }

            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "Search failed. " + ex.Message, ex, true);

                return new ActionProfileResult
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }

        }

        public Result Delete(int? actionProfileID)
        {
            if (actionProfileID == null) throw new ArgumentNullException("actionProfileID");

            try
            {
                using (var db = new MyDbContext())
                {
                    var actionProfile = db.RmsActionProfiles.Create();
                    actionProfile.ActionProfileId = actionProfileID.Value;
                    db.RmsActionProfiles.Attach(actionProfile);
                    db.RmsActionProfiles.Remove(actionProfile);
                    db.SaveChanges();

                    return new Result {IsSuccess = true};
                }
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "Delete failed. " + ex.Message, ex, true);

                return new Result { IsSuccess = false };
            }

        }

        public ActionProfileResult Get(int? id)
        {
            if (id == null) throw new ArgumentNullException("ActionProfileID");

            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;
                   
                    RmsActionProfile actionProfile = db.RmsActionProfiles.Find(id);

                    var sr = new ActionProfileResult
                    {
                        IsSuccess = true,
                        ActionProfile = actionProfile,
                    };
                    return sr;

                }
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "Get failed. " + ex.Message, ex, true);

                var sr = new ActionProfileResult
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
                return sr;
            }
        }

        public Result Update(int? id, string m, string ActionProfileName, string Email, string SMS, bool ActiveList)
        {
            try
            {
                if (string.IsNullOrEmpty(ActionProfileName)) throw new ArgumentNullException("ActionProfileName");

                if (m == "e" && id == null) throw new ArgumentNullException("id");
            }
            catch (ArgumentNullException ex)
            {
                new RMSWebException(this, "0500", "Update failed. " + ex.Message, ex, true);
                throw;
            }
            if (string.IsNullOrEmpty(m))
            {
                try
                {
                    using (var db = new MyDbContext())
                    {
                        var actionProfile = db.RmsActionProfiles.Create();
                        actionProfile.ActionProfileName = ActionProfileName;
                        actionProfile.Email = string.IsNullOrEmpty(Email)? null : Email.Trim();
                        actionProfile.Sms = string.IsNullOrEmpty(SMS) ? null : SMS.Trim();
                        actionProfile.ActiveList = ActiveList;
                        db.RmsActionProfiles.Add(actionProfile);

                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    new RMSWebException(this, "0500", "Update failed. " + ex.Message, ex, true);

                    return new Result
                    {
                        IsSuccess = false,
                        ErrorMessage = ex.Message
                    };
                }
            }
            else if (m == "e")
            {
                try
                {
                    using (var db = new MyDbContext())
                    {
                        var actionProfile = db.RmsActionProfiles.Find(id);
                        actionProfile.ActionProfileName = ActionProfileName;
                        actionProfile.Email = string.IsNullOrEmpty(Email) ? null : Email.Trim();
                        actionProfile.Sms = string.IsNullOrEmpty(SMS) ? null : SMS.Trim();
                        actionProfile.ActiveList = ActiveList;

                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    new RMSWebException(this, "0500", "Update failed. " + ex.Message, ex, true);

                    return new Result
                    {
                        IsSuccess = false,
                        ErrorMessage = ex.Message
                    };

                }
            }
            else
            {
                new RMSWebException(this, "0500", "Update failed. Missing Parameter m=" + m, true);

                return new Result
                {
                    IsSuccess = false,
                    ErrorMessage = "Missing Parameter"
                };

            }

            return new Result
            {
                IsSuccess = true
            };
        }
    }
}
