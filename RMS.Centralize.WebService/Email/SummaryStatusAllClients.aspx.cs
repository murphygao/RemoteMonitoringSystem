using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.BSL;
using RMS.Centralize.WebService.Model;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService.Email
{
    public partial class SummaryStatusAllClients : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<SummaryStatusAllClientsInfo> lstatusList = new List<SummaryStatusAllClientsInfo>();
                using (var db = new MyDbContext())
                {

                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.Database.SqlQuery<SummaryStatusAllClientsInfo>("RMS_Report_SummaryStatusAllClients");

                    lstatusList = new List<SummaryStatusAllClientsInfo>(listOfType.ToList());

                    // ถ้าไม่เจอ ให้จบการทำงาน

                    Repeater1.DataSource = lstatusList;
                    Repeater1.DataBind();
                }

            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Page_Load failed. " + ex.Message, ex, true);
            }

        }
    }
}