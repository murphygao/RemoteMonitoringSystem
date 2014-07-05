using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using RMS.Centralize.DAL;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService.BSL
{
    public class AutoUpdateService
    {
        public RmsLogAutoUpdate AddAutoUpdateLog(string clientCode, string ipAdress, string appName,string currentVersion, string updateVersion, bool isComplete, string errorMessage)
        {
            try
            {
                if (string.IsNullOrEmpty(clientCode) && string.IsNullOrEmpty(ipAdress)) throw new ArgumentNullException("clientCode && ipAddress");
                if (string.IsNullOrEmpty(appName)) throw new ArgumentNullException("appName");

                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    RmsLogAutoUpdate autoUpdate = db.RmsLogAutoUpdates.Create();
                    autoUpdate.IpAddress = ipAdress;
                    autoUpdate.ClientCode = clientCode;
                    autoUpdate.AppName = appName;
                    autoUpdate.CurrentVersion = currentVersion;
                    autoUpdate.UpdateVersion = updateVersion;
                    autoUpdate.IsComplete = isComplete;
                    autoUpdate.ErrorMessage = errorMessage;

                    db.RmsLogAutoUpdates.Add(autoUpdate);
                    db.SaveChanges();

                    return autoUpdate;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Add failed. " + ex.Message, ex, true);
            }
        }
    }
}