using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using RMS.Centralize.DAL;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService.BSL
{
    public class MasterTableService : BaseService
    {
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
                throw new RMSWebException(this, "0500", "ListLocation failed. " + ex.Message, ex, false);
            }

        }
    }
        
}