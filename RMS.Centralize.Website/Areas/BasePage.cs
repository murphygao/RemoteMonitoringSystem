using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.Centralize.Website.Areas
{
    public class BasePage : System.Web.UI.Page
    {
        public string pageName;

        public BasePage()
        {
            //
            // TODO: Add constructor logic here
            //

            _Logger = new Logger();
            this.MasterPageFile = HttpContext.Current.Request.ApplicationPath + "/SmartAdmin.Master";
            AuthorizePage();
        }

        private Logger _Logger;

        //note that Derived class cannt create instance of it
        //this is also a kind of controling
        public Logger Logger
        {
            get { return _Logger; }

        }

        private SessionData _CurrentSession;

        public SessionData CurrentSession
        {
            get { return _CurrentSession; }
            set { _CurrentSession = value; }
        }

        protected bool AuthorizePage()
        {
            string appPath = HttpContext.Current.Request.ApplicationPath;
            string pageLogicalPathName = HttpContext.Current.Request.CurrentExecutionFilePath.Substring(appPath.Length);
            //if (string.IsNullOrEmpty("")) HttpContext.Current.Response.Redirect("http://www.google.co.th");
            return true;
        }

        public string GetPageName()
        {
            string path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo fi = new System.IO.FileInfo(path);
            return fi.Name;
        }
    }
    /// <summary>
    /// Responsible for logging
    /// Please note that you can also use Microsoft Enterprise Logging Block
    /// </summary>
    public class Logger
    {
        public void LogWarning(object Message)
        {
            //write some logic here
        }
        public void LogError(object Message)
        {
            //write some logic here
        }
        public void LogTrace(object Message)
        {
            //write some logic here
        }
    }

    public class SessionData
    {
        public int UserId
        {
            get { return int.Parse(HttpContext.Current.Session["UserId"].ToString()); }
            set { HttpContext.Current.Session["UserId"] = value; }
        }

        public List<int> ItemIds
        {
            get { return (List<int>)HttpContext.Current.Session["Items"]; }
            set { HttpContext.Current.Session["Items"] = value; }
        }

    }
}