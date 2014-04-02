using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Centralize.DAL
{
    public class MyDbContext : ReverseDbContext
    {
        public override int SaveChanges()
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

                        ts.UpdatedDate = _now;
                        if (item.State == EntityState.Added)
                        {
                            ts.CreatedDate = _now;
                        }
                        else if (item.State == EntityState.Modified)
                        {
                        }
                    }
                }
                catch { }

            }
            return base.SaveChanges();
        }
    }
}
