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
    public class MonitoringProfileService
    {
        public List<RmsMonitoringProfile> List(bool? activeList)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.RmsMonitoringProfiles.Include("RmsMonitoringProfileDevices").Where(w => activeList == null || w.ActiveList == activeList);

                    List<RmsMonitoringProfile> lists = new List<RmsMonitoringProfile>(listOfType.ToList());

                    return lists;

                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "List failed. " + ex.Message, ex, false);
            }
        }

        public RmsMonitoringProfile Get(int monitoringProfileID)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var profile =
                        db.RmsMonitoringProfiles.Include("RmsMonitoringProfileDevices").FirstOrDefault(f => f.MonitoringProfileId == monitoringProfileID);

                    if (profile == null) throw new Exception("MonitoringProfile (" + monitoringProfileID + ") Not Found.");

                    
                    return profile;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Get (" + monitoringProfileID + ") failed. " + ex.Message, ex, false);
            }
        }

        public List<MonitoringProfileInfo> Search(JQueryDataTableParamModel param, string name, bool? activeList, out int totalRecord)
        {
            List<MonitoringProfileInfo> lProfiles = new List<MonitoringProfileInfo>();
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

                    if (String.IsNullOrEmpty(name))
                    {
                        p6 = new SqlParameter("ProfileName", DBNull.Value);
                    }
                    else
                    {
                        p6 = new SqlParameter("ProfileName", name);
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

                    var listOfType = db.Database.SqlQuery<MonitoringProfileInfo>("RMS_SearchMonitoringProfile " +
                                                                            "@ProfileName, @ActiveList" +
                                                                            ", @PageNbr, @PageSize, @FirstRec, @SortCol, @TotalRecords OUTPUT"
                        , parameters);

                    lProfiles = new List<MonitoringProfileInfo>(listOfType.ToList());
                    totalRecord = (int)parameters[4].Value;

                    return lProfiles;
                }

            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Search failed. " + ex.Message, ex, false);
            }
        }

        public RmsMonitoringProfile Update(int id, string profileName, bool activeList, string updatedBy)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    var profile = db.RmsMonitoringProfiles.Find(id);
                    if (profile == null) throw new Exception("MonitoringProfile (" + id + ") Not Found");

                    profile.ProfileName = profileName;

                    profile.ActiveList = activeList;
                    profile.UpdatedBy = updatedBy;

                    db.SaveChanges();

                    return profile;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Update failed. " + ex.Message, ex, true);
            }
        }
        public RmsMonitoringProfile Add(string profileName, bool activeList, string updatedBy)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    var profile = db.RmsMonitoringProfiles.Create();

                    profile.ProfileName = profileName;

                    profile.ActiveList = activeList;
                    profile.CreatedBy = updatedBy;
                    profile.UpdatedBy = updatedBy;

                    db.RmsMonitoringProfiles.Add(profile);
                    db.SaveChanges();

                    return profile;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Add failed. " + ex.Message, ex, true);
            }
        }

        public bool Delete(int monitoringProfileID, string updatedBy)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    if (db.RmsClientMonitorings.Any(a => a.MonitoringProfileId == monitoringProfileID))
                        throw new Exception("This MonitoringProfile is already used. Cannot delete it.");

                    var delete = db.RmsMonitoringProfiles.Create();
                    delete.MonitoringProfileId = monitoringProfileID;
                    db.RmsMonitoringProfiles.Attach(delete);
                    db.RmsMonitoringProfiles.Remove(delete);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Delete failed. " + ex.Message, ex, true);

            }
        }

    
    }
}