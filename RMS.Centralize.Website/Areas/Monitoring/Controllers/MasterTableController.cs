using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Centralize.WebSite.Proxy;
using RMS.Common.Exception;

namespace RMS.Centralize.Website.Areas.Monitoring.Controllers
{
    public class MasterTableController : Controller
    {
        // GET: /Monitoring/MasterTable/ListColorLabels/
        public ActionResult ListColorLabels()
        {
            try
            {
                var service = new MasterTableService().masterTableService;

                var result = service.ListColorLabels();

                var ret = new
                {
                    listColorLabels = result.ListColorLabels,

                    status = (result.IsSuccess) ? 1 : 0,
                    errorMessage = result.ErrorMessage
                };

                return Json(ret);

            }
            catch (Exception ex)
            {
                var ret = new
                {
                    status = -1,
                    error = ex.Message
                };
                new RMSWebException(this, "0500", "Get failed. " + ex.Message, ex, true);

                return Json(ret);
            }
        }
    }
}