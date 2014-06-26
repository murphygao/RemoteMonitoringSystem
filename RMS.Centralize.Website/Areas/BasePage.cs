using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace RMS.Centralize.Website.Areas
{
    public class BasePage : System.Web.UI.Page
    {

        public BasePage()
        {
            //
            // TODO: Add constructor logic here
            //

            this.MasterPageFile = HttpContext.Current.Request.ApplicationPath + "/SmartAdmin.Master";
        }

        public string UserName
        {
            get
            {
                return (string)Session["__UserName"];
            }
        }

    }

}