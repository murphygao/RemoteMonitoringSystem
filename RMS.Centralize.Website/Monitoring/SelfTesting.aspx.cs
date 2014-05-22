﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMS.Centralize.WebSite.Proxy;

namespace RMS.Centralize.Website.Monitoring
{
    public partial class SelfTesting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DoChecking();
        }

        private void DoChecking()
        {
            string ret = "";

            #region Check Web Server - Web.config

            try
            {
                /*
                    <add key="RMS.OverrideProxy" value="false" />
                    <add key="RMS.WebServicURL_ActionProfileService" value="http://localhost/RMS.Centralize.WebService/ActionProfileService.svc" />
                    <add key="RMS.WebServicURL_MessageActionService" value="http://localhost/RMS.Centralize.WebService/MessageActionService.svc" />
                    <add key="RMS.WebServicURL_ClientService" value="http://localhost/RMS.Centralize.WebService/ClientService.svc" />
                    <add key="RMS.WebServicURL_MonitoringService" value="http://localhost/RMS.Centralize.WebService/MonitoringService.svc" />
                    <add key="RMS.WebServicURL_SummaryReportService" value="http://localhost/RMS.Centralize.WebService/SummaryReportService.svc" />
                    <add key="RMS.WebServicURL_SelfTestingService" value="http://localhost/RMS.Centralize.WebService/SelfTestingService.svc" />

                 */

                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["RMS.OverrideProxy"]))
                {
                    ret += "<br/>AppSettings[\"RMS.OverrideProxy\"] not found or empty.";
                }
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["RMS.WebServicURL_ActionProfileService"]))
                {
                    ret += "<br/>AppSettings[\"RMS.WebServicURL_ActionProfileService\"] not found or empty.";
                }
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["RMS.WebServicURL_MessageActionService"]))
                {
                    ret += "<br/>AppSettings[\"RMS.WebServicURL_MessageActionService\"] not found or empty.";
                }
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["RMS.WebServicURL_ClientService"]))
                {
                    ret += "<br/>AppSettings[\"RMS.WebServicURL_ClientService\"] not found or empty.";
                }
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["RMS.WebServicURL_MonitoringService"]))
                {
                    ret += "<br/>AppSettings[\"RMS.WebServicURL_MonitoringService\"] not found or empty.";
                }
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["RMS.WebServicURL_SummaryReportService"]))
                {
                    ret += "<br/>AppSettings[\"RMS.WebServicURL_SummaryReportService\"] not found or empty.";
                }
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["RMS.WebServicURL_SelfTestingService"]))
                {
                    ret += "<br/>AppSettings[\"RMS.WebServicURL_SelfTestingService\"] not found or empty.";
                }

                if (ret == "")
                {
                    rWebConfig.InnerHtml = "<span class=\"txt-green\">Passed</span>";
                }
                else
                {
                    rWebConfig.InnerHtml = "<span class=\"txt-red\">Failed</span>" + ret;
                }
            }
            catch (Exception ex)
            {
                rWebConfig.InnerHtml = "Internal error while checking Web.config. " + ex.Message;
            }

            #endregion

            #region Check Web Server -> Web Service Server Connection

            ret = "";

            try
            {
                var service = new ActionProfileService();
                service.actionProfileService.TestConnection();
            }
            catch (Exception ex)
            {
                ret += "<br/>ActionProfileService failed. " + ex.Message;
            }
            try
            {
                var service = new ClientService();
                service.clientService.TestConnection();
            }
            catch (Exception ex)
            {
                ret += "<br/>ClientService failed. " + ex.Message;
            }
            try
            {
                var service = new MessageActionService();
                service.messsageActionService.TestConnection();
            }
            catch (Exception ex)
            {
                ret += "<br/>MessageActionService failed. " + ex.Message;
            }
            try
            {
                var service = new MonitoringService();
                service.monitoringService.TestConnection();
            }
            catch (Exception ex)
            {
                ret += "<br/>MonitoringService failed. " + ex.Message;
            }
            try
            {
                var service = new SummaryReportService();
                service.summaryReportService.TestConnection();
            }
            catch (Exception ex)
            {
                ret += "<br/>SummaryReportService failed. " + ex.Message;
            }
            try
            {
                var service = new SelfTestingService();
                service.selfTestingService.TestConnection();
            }
            catch (Exception ex)
            {
                ret += "<br/>SelfTestingService failed. " + ex.Message;
            }
            if (ret == "")
            {
                rWebToWsConnection.InnerHtml = "<span class=\"txt-green\">Passed</span>";
            }
            else
            {
                rWebToWsConnection.InnerHtml = "<span class=\"txt-red\">Failed</span>" + ret;
            }


            #endregion

            #region Check Web Service Server - Web.Config

            ret = "";

            try
            {
                var service = new SelfTestingService();
                ret = service.selfTestingService.TestWebConfig();

                if (ret == "")
                {
                    rWebServiceConfig.InnerHtml = "<span class=\"txt-green\">Passed</span>";
                }
                else
                {
                    rWebServiceConfig.InnerHtml = "<span class=\"txt-red\">Failed</span>" + ret;
                }
            }
            catch (Exception ex)
            {
                rWebServiceConfig.InnerHtml = "Internal error while checking Web Service Web.config. " + ex.Message;
            }

            #endregion

            #region Check Web Service Server -> Database Server Connection

            ret = "";

            try
            {
                var service = new SelfTestingService();
                ret = service.selfTestingService.TestDatabaseConnection();

                if (ret == "")
                {
                    rWSToDbConnection.InnerHtml = "<span class=\"txt-green\">Passed</span>";
                }
                else
                {
                    rWSToDbConnection.InnerHtml = "<span class=\"txt-red\">Failed</span>" + ret;
                }
            }
            catch (Exception ex)
            {
                rWSToDbConnection.InnerHtml = "Internal error while checking Database Connection. " + ex.Message;
            }

            #endregion

            #region Check Web Service Server -> Monitoring Agent Connection

            ret = "";

            try
            {
                var ipAddress = Request["ipAddress"];

                if (string.IsNullOrEmpty(ipAddress))
                {
                    rWsToAgentConnection.InnerHtml = "Please specify agent IP address via URL.<br/>eg. SelfTesting.aspx?ipaddress=127.0.0.1&email=name@domain.com&sms=0812345678";
                }
                else
                {
                    var service = new SelfTestingService();
                    ret = service.selfTestingService.TestAgentConnection(ipAddress);

                    if (ret == "")
                    {
                        rWsToAgentConnection.InnerHtml = "<span class=\"txt-green\">Passed</span>";
                    }
                    else
                    {
                        rWsToAgentConnection.InnerHtml = "<span class=\"txt-red\">Failed</span>" + ret;
                    }
                }

            }
            catch (Exception ex)
            {
                rWsToAgentConnection.InnerHtml = "Internal error while checking Monitoring Agent Connection. " + ex.Message;
            }

            #endregion

            #region Check Web Service Server -> Email & SMS Server Connection

            ret = "";

            try
            {
                var email = Request["email"];
                var sms = Request["sms"];

                if (string.IsNullOrEmpty(email))
                {
                    rWsToEmailConnection.InnerHtml = "Please specify email via URL.";
                }

                if (string.IsNullOrEmpty(sms))
                {
                    if (rWsToEmailConnection.InnerText != "") rWsToEmailConnection.InnerHtml += "<br/>";
                    rWsToEmailConnection.InnerHtml += "Please specify sms via URL.";
                }

                if (rWsToEmailConnection.InnerText != "")
                {
                    rWsToEmailConnection.InnerHtml += "<br/>eg. SelfTesting.aspx?ipaddress=127.0.0.1&email=name@domain.com&sms=0812345678";
                }

                else
                {
                    var service = new SelfTestingService();
                    ret = service.selfTestingService.TestEmailSmsConnection(email, sms);

                    if (ret == "")
                    {
                        rWsToEmailConnection.InnerHtml = "<span class=\"txt-green\">Passed</span>";
                    }
                    else
                    {
                        rWsToEmailConnection.InnerHtml = "<span class=\"txt-red\">Failed</span>" + ret;
                    }
                }

            }
            catch (Exception ex)
            {
                rWsToEmailConnection.InnerHtml = "Internal error while checking Email & SMS Server Connection. " + ex.Message;
            }

            #endregion

        }
    }
}