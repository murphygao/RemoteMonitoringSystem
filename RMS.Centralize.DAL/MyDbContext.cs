using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Common.Exception;

namespace RMS.Centralize.DAL
{
    public class MyDbContext : ReverseDbContext
    {
        public override int SaveChanges()
        {

            try
            {
                this.ChangeTracker.DetectChanges();
                foreach (var item in this.ChangeTracker.Entries())
                {
                    try
                    {
                        dynamic ts = item.Entity;
                        if (ts != null)
                        {
                            var _now = DateTime.Now;


                            if (item.State == EntityState.Added)
                            {
                                ts.CreatedDate = _now;
                                ts.UpdatedDate = _now;
                            }
                            else if (item.State == EntityState.Modified)
                            {
                                ts.UpdatedDate = _now;
                            }
                        }
                    }
                    catch { }

                }
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                string err = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    err += "Entity of type \"" + eve.Entry.Entity.GetType().Name + "\" in state \"" + eve.Entry.State +
                           "\" has the following validation errors:" + Environment.NewLine;
                    foreach (var ve in eve.ValidationErrors)
                    {
                        err += "- Property: \"" + ve.PropertyName + "\", Error: \"" + ve.ErrorMessage + "\"" + Environment.NewLine;
                    }
                }
                throw new RMSWebException(this, "0500", "SaveChanges failed. " + e.Message + err, e, false);
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "SaveChanges failed. " + ex.Message, ex, false);
            }
        }
    }
}
