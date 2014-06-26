using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Model;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService.BSL
{
    public class ClientMonitoringService
    {
        public List<ClientMonitoringInfo> ListByClient(int clientID)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.RmsClientMonitorings.Include(i => i.RmsMonitoringProfile).Where(w => w.ClientId == clientID);

                    List<RmsClientMonitoring> lists = new List<RmsClientMonitoring>(listOfType.ToList());

                    List<ClientMonitoringInfo> listClientMonitoringInfo = new List<ClientMonitoringInfo>();

                    foreach (var rmsClientMonitoring in lists)
                    {
                        ClientMonitoringInfo info = new ClientMonitoringInfo(rmsClientMonitoring);
                        info.ProfileName = rmsClientMonitoring.RmsMonitoringProfile.ProfileName;
                        listClientMonitoringInfo.Add(info);
                    }

                    return listClientMonitoringInfo;

                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "List failed. " + ex.Message, ex, false);
            }
        }

        public ClientMonitoringInfo Get(int clientID, int monitoringProfileID)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = true;

                    var profile = db.RmsClientMonitorings.Include("RmsMonitoringProfiles").FirstOrDefault(w => w.ClientId == clientID && w.MonitoringProfileId == monitoringProfileID);

                    if (profile == null) throw new Exception("ClientMonitoring (clientID: " + clientID + ", monitoringProfileID: " + monitoringProfileID + ") Not Found.");


                    ClientMonitoringInfo clientMonitoringInfo = new ClientMonitoringInfo(profile);
                    clientMonitoringInfo.ProfileName = profile.RmsMonitoringProfile.ProfileName;

                    return clientMonitoringInfo;

                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "List failed. " + ex.Message, ex, false);
            }
        }

        public RmsClientMonitoring Update(int clientID, int monitoringProfileID, DateTime effectiveDate, string updatedBy)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    var profile = db.RmsClientMonitorings.Find(clientID, monitoringProfileID);
                    if (profile == null) throw new Exception("ClientMonitoring (clientID: " + clientID + ", monitoringProfileID: " + monitoringProfileID + ") Not Found");

                    profile.EffectiveDate = effectiveDate;

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

        public RmsClientMonitoring Add(int clientID, int monitoringProfileID, DateTime effectiveDate, string updatedBy)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    if (db.RmsClientMonitorings.Any(a => a.ClientId == clientID && a.MonitoringProfileId == monitoringProfileID))
                        throw new Exception("ClientMonitoring (clientID: " + clientID + ", monitoringProfileID: " + monitoringProfileID + ") already exists.");

                    var profile = db.RmsClientMonitorings.Create();

                    profile.ClientId = clientID;
                    profile.MonitoringProfileId = monitoringProfileID;

                    profile.EffectiveDate = effectiveDate;
                    profile.CreatedBy = updatedBy;
                    profile.UpdatedBy = updatedBy;

                    db.RmsClientMonitorings.Add(profile);
                    db.SaveChanges();

                    return profile;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Add failed. " + ex.Message, ex, true);
            }
        }

        public bool Delete(int clientID, int monitoringProfileID, string updatedBy)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    var delete = db.RmsClientMonitorings.Create();
                    delete.ClientId = clientID;
                    delete.MonitoringProfileId = monitoringProfileID;
                    db.RmsClientMonitorings.Attach(delete);
                    db.RmsClientMonitorings.Remove(delete);
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