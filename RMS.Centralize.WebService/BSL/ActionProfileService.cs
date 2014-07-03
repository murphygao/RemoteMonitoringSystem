using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMS.Centralize.DAL;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService.BSL
{
    public class ActionProfileService : BaseService
    {
        public List<RmsActionProfile> List(bool? activeList)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.RmsActionProfiles.Where(w => activeList == null || w.ActiveList == activeList);

                    List<RmsActionProfile> lists = new List<RmsActionProfile>(listOfType.ToList());

                    return lists;

                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "List failed. " + ex.Message, ex, false);
            }
        }
    }
}