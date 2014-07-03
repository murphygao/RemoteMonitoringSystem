using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Model;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService.BSL
{
    public class SeverityLevelService : BaseService
    {

        public List<SeverityLevelInfo> Search(JQueryDataTableParamModel param, string severityLevel, bool? activeList, out int totalRecord)
        {
            List<SeverityLevelInfo> lResult = new List<SeverityLevelInfo>();
            SqlParameter[] parameters = new SqlParameter[7];

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
                    SqlParameter p7;

                    if (String.IsNullOrEmpty(severityLevel))
                    {
                        p6 = new SqlParameter("SeverityLevel", DBNull.Value);
                    }
                    else
                    {
                        p6 = new SqlParameter("SeverityLevel", severityLevel);
                    }

                    if (activeList == null)
                    {
                        p7 = new SqlParameter("ActiveList", DBNull.Value);
                    }
                    else
                    {
                        p7 = new SqlParameter("ActiveList", activeList);
                    }

                    parameters[0] = p1;
                    parameters[1] = p2;
                    parameters[2] = p3;
                    parameters[3] = p4;
                    parameters[4] = p5;
                    parameters[5] = p6;
                    parameters[6] = p7;

                    db.Configuration.ProxyCreationEnabled = false;
                    //db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.Database.SqlQuery<SeverityLevelInfo>("RMS_SearchSeverityLevel " +
                                                                            "@SeverityLevel, @ActiveList" +
                                                                            ", @PageNbr, @PageSize, @FirstRec, @SortCol, @TotalRecords OUTPUT"
                        , parameters);

                    lResult = new List<SeverityLevelInfo>(listOfType.ToList());
                    totalRecord = (int)parameters[4].Value;

                    return lResult;
                }

            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Search failed. " + ex.Message, ex, false);
            }
        }

        public List<RmsSeverityLevel> List(bool? activeList)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.RmsSeverityLevels.Include("RmsActionProfle").Where(w => activeList == null || w.ActiveList == activeList);

                    List<RmsSeverityLevel> lists = new List<RmsSeverityLevel>(listOfType.ToList());

                    return lists;

                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "List failed. " + ex.Message, ex, false);
            }
        }

        public RmsSeverityLevel Get(int severityLevelID)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var result = db.RmsSeverityLevels.Find(severityLevelID);

                    if (result == null) throw new Exception("SeverityLevel (" + severityLevelID + ") Not Found.");

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Get (" + severityLevelID + ") failed. " + ex.Message, ex, false);
            }
        }

        public RmsSeverityLevel Update(int id, string levelCode, string levelName, int orderList, bool activeList, int defaultActionProfileID
            , string colorCode, bool actionRepeatable, int actionRepeatInterval, string updatedBy)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    var level = db.RmsSeverityLevels.Find(id);
                    if (level == null) throw new Exception("SeverityLevel (" + id + ") Not Found");

                    level.LevelCode = levelCode;
                    level.LevelName = levelName;
                    level.OrderList = orderList;
                    level.ActiveList = activeList;
                    level.DefaultActionProfileId = defaultActionProfileID;
                    level.ColorCode = colorCode;
                    level.ActionRepeatable = actionRepeatable;
                    level.ActionRepeatInterval = actionRepeatInterval;
                    level.UpdatedBy = updatedBy;

                    db.SaveChanges();

                    return level;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Update failed. " + ex.Message, ex, true);
            }
        }

        public RmsSeverityLevel Add(string levelCode, string levelName, int orderList, bool activeList, int defaultActionProfileID
            , string colorCode, bool actionRepeatable, int actionRepeatInterval, string updatedBy)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    using (var ts = db.Database.BeginTransaction())
                    {
                        //RMS_SeverityLevel
                        var level = db.RmsSeverityLevels.Create();

                        level.LevelCode = levelCode;
                        level.LevelName = levelName;
                        level.OrderList = orderList;
                        level.ActiveList = activeList;
                        level.DefaultActionProfileId = defaultActionProfileID;
                        level.ColorCode = colorCode;
                        level.ActionRepeatable = actionRepeatable;
                        level.ActionRepeatInterval = actionRepeatInterval;
                        level.CreatedBy = updatedBy;
                        level.UpdatedBy = updatedBy;

                        db.RmsSeverityLevels.Add(level);
                        db.SaveChanges();

                        //RMS_ClientSeverityAction
                        var rmsClients = db.RmsClients;
                        foreach (var rmsClient in rmsClients)
                        {
                            var rmsClientSeverityAction = db.RmsClientSeverityActions.Create();
                            rmsClientSeverityAction.ClientId = rmsClient.ClientId;
                            rmsClientSeverityAction.SeverityLevelId = level.SeverityLevelId;
                            rmsClientSeverityAction.CreatedBy = updatedBy;
                            rmsClientSeverityAction.UpdatedBy = updatedBy;

                            db.RmsClientSeverityActions.Add(rmsClientSeverityAction);
                        }
                        db.SaveChanges();

                        ts.Commit();

                        return level;
                    }

                }

            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Add failed. " + ex.Message, ex, true);
            }
        }

        public bool Delete(int id, string updatedBy)
        {
            try
            {
                if (id < 10) throw new ArgumentOutOfRangeException("SeverityLevelID", id, "Cannot delete reserved object.");

                using (var db = new MyDbContext())
                {
                    if (db.RmsMessages.Any(a => a.SeverityLevelId == id))
                        throw new Exception("This SeverityLevel has been using. Cannot delete it.");
                    using (var ts = db.Database.BeginTransaction())
                    {

                        //RMS_ClientSeverityAction
                        //RMS_SeverityLevel
                        db.Database.ExecuteSqlCommand("DELETE FROM RMS_ClientSeverityAction WHERE SeverityLevelID = {0}; DELETE FROM RMS_SeverityLevel WHERE SeverityLevelID = {0}; ", id);

                        db.SaveChanges();

                        ts.Commit();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Delete (" + id + ") failed. " + ex.Message, ex, true);

            }
        }

    }
}