using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using RMS.Centralize.BSL.MonitoringEngine.Model;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Gateway;
using RMS.Centralize.WebService.Model;

namespace RMS.Centralize.WebService.BSL
{
    public class ActionService : BaseService
    {
        internal string destinationPath = @"c:\monitoring\email\";

        public void ActionRequest(List<RmsReportSummaryMonitoring> lRmsReportSummaryMonitorings)
        {

        
        }

        [DataContract]
        public enum ActionSendType
        {
            [EnumMember] 
            ManualSending,
            [EnumMember]
            SummaryTechnicalSending,
            [EnumMember]
            SummaryHighLevelSending
        }

        public void ActionSend(ActionSendType actionSendType, List<RmsReportSummaryMonitoring> lRmsReportSummaryMonitorings)
        {
            try
            {
                switch (actionSendType)
                {
                        case ActionSendType.ManualSending:
                        ManualSending(lRmsReportSummaryMonitorings);
                        break;

                        case ActionSendType.SummaryTechnicalSending:
                        TechnicalSending();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ActionSend errors. " + ex.Message, ex);
            }
        }

        private void ManualSending(List<RmsReportSummaryMonitoring> lRmsReportSummaryMonitorings, bool updateLastAction = false)
        {
            if (lRmsReportSummaryMonitorings == null || lRmsReportSummaryMonitorings.Count == 0) return;

            int activeClients;
            try
            {
                List<ActionInfo> lEmails = new List<ActionInfo>();
                List<ActionInfo> lSMSs = new List<ActionInfo>();
                Dictionary<string, string> distinctEmail = new Dictionary<string, string>();
                Dictionary<string, string> distinctSMS = new Dictionary<string, string>();

                using (var db = new MyDbContext())
                {

                    #region Check License Validity by Calling ListClientWithIPAddress

                    var ms = new Centralize.BSL.MonitoringEngine.MonitoringService();
                    ms.ListClientWithIPAddress(licenseInfo, out activeClients);

                    #endregion

                    SqlParameter[] parameters = new SqlParameter[1];
                    SqlParameter p1 = new SqlParameter("ClientID", lRmsReportSummaryMonitorings[0].ClientId);
                    parameters[0] = p1;

                    var details = db.Database.SqlQuery<ClientMessageAction>("RMS_ListClientMessageAction " +
                                                                            "@ClientID", parameters);
                    List<ClientMessageAction> lClientMessageActions = new List<ClientMessageAction>(details.ToList());


                    foreach (var monitoring in lRmsReportSummaryMonitorings)
                    {
                        RmsReportSummaryMonitoring monitoring1 = monitoring;
                        bool useEmail = false;
                        bool useSMS = false;

                        var clientMessageAction = lClientMessageActions.First(cma => cma.MessageID == monitoring1.MessageId);
                        
                        if (clientMessageAction == null) continue;


                        #region //EMAIL SECTION

                        //เตรียมว่าจะต้องส่ง Email ไปหาใครบ้าง
                        string mEmails = string.Empty;

                        //ตรวจตสอบ flag Overwritten
                        if (clientMessageAction.OverwritenAction == true)
                        {
                            mEmails = clientMessageAction.Email2;
                        }
                        else
                        {
                            mEmails = clientMessageAction.Email + ";" + clientMessageAction.Email2;
                        }

                        mEmails = mEmails.Replace(',', ';');

                        var splitEmail = mEmails.Split(new string[] {";"}, StringSplitOptions.RemoveEmptyEntries);

                        //เตรียมข้อมูล
                        foreach (string email in splitEmail)
                        {
                            ActionInfo info = new ActionInfo();
                            info.To = email.ToLower().Trim();
                            info.ClientCode = monitoring.ClientCode;
                            info.LevelCode = clientMessageAction.LevelCode;
                            info.Message = monitoring.Message;
                            info.LocationCode = clientMessageAction.LocationCode;
                            info.LocationName = clientMessageAction.LocationName;
                            info.MessageGroupName = monitoring.MessageGroupName;
                            info.MessageDateTime = monitoring.MessageDateTime;
                            info.DeviceCode = monitoring.DeviceCode;
                            info.DeviceDescription = monitoring.DeviceDescription;
                            if (!string.IsNullOrEmpty(monitoring.MessageRemark))
                            {
                                if (!string.IsNullOrEmpty(info.DeviceDescription))
                                {
                                    info.DeviceDescription += " - ";
                                }
                                info.DeviceDescription += monitoring.MessageRemark;
                            }
                            info.SummaryMonitoringReportID = monitoring.Id;

                            lEmails.Add(info);

                            useEmail = true;

                            if (!distinctEmail.ContainsKey(info.To))
                                distinctEmail.Add(info.To, info.To);
                        }

                        #endregion

                        #region //SMS SECTION

                        string mSMSs = string.Empty;
                        if (clientMessageAction.OverwritenAction == true)
                        {
                            mSMSs = clientMessageAction.SMS2;
                        }
                        else
                        {
                            mSMSs = clientMessageAction.SMS + ";" + clientMessageAction.SMS2;
                        }

                        mSMSs = mSMSs.Replace(',', ';');

                        var splitSMS = mSMSs.Split(new string[] {";"}, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string SMS in splitSMS)
                        {
                            ActionInfo info = new ActionInfo();
                            info.To = SMS.Trim();
                            info.ClientCode = monitoring.ClientCode;
                            info.LevelCode = clientMessageAction.LevelCode;
                            info.Message = monitoring.Message;
                            info.LocationCode = clientMessageAction.LocationCode;
                            info.LocationName = clientMessageAction.LocationName;
                            info.MessageGroupName = monitoring.MessageGroupName;
                            info.MessageDateTime = monitoring.MessageDateTime;
                            info.DeviceCode = monitoring.DeviceCode;
                            info.DeviceDescription = monitoring.DeviceDescription;
                            if (string.IsNullOrEmpty(info.DeviceDescription)) info.DeviceDescription = monitoring.MessageRemark;
                            if (string.IsNullOrEmpty(info.DeviceDescription)) info.DeviceDescription = monitoring.Message;

                            //if (!string.IsNullOrEmpty(monitoring.MessageRemark))
                            //{
                            //    if (!string.IsNullOrEmpty(info.DeviceDescription))
                            //    {
                            //        info.DeviceDescription += " - ";
                            //    }
                            //    info.DeviceDescription += monitoring.MessageRemark;
                            //}

                            info.SummaryMonitoringReportID = monitoring.Id;

                            lSMSs.Add(info);

                            useSMS = true;

                            if (!distinctSMS.ContainsKey(info.To))
                                distinctSMS.Add(info.To, info.To);
                        }



                        #endregion

                        #region //Update ค่า Last Action

                        if (useEmail || useSMS)
                        {
                            string s = "";
                            if (useEmail)
                                s = ", Email";
                            if (useSMS)
                                s += ", SMS";
                            s = s.Substring(2);

                            var reportSummaryMonitoring = db.RmsReportSummaryMonitorings.Find(monitoring.Id);
                            reportSummaryMonitoring.LastActionType = s;
                            reportSummaryMonitoring.LastActionDateTime = DateTime.Now;
                            db.SaveChanges();
                        }

                        #endregion
                    }

                    #region Action Process

                    var rmsSystemConfigs = db.RmsSystemConfigs;
                    var config = rmsSystemConfigs.First(c => c.Name == "ActionGateway");
                    string ActionGatewayName = "";
                    if (config != null)
                    {
                        ActionGatewayName = config.Value ?? config.DefaultValue;
                    }

                    GatewayName gateway;
                    Enum.TryParse(ActionGatewayName, true, out gateway);


                    var rmsMessageMasters = db.RmsMessageMasters;


                    #region // Prepare Email Body

                    config = rmsSystemConfigs.First(c => c.Name == "EmailSubject");
                    string emailSubject = "Monitoring : พบข้อผิดพลาด ::clientcode::";
                    if (config != null)
                    {
                        emailSubject = config.Value ?? config.DefaultValue;
                    }

                    config = rmsSystemConfigs.First(c => c.Name == "EmailBody");
                    string emailBody = "ถึงผู้ดูแล|มีข้อผิดพลาดดังนี้||::summaryerror::||ขอบคุณครับ";
                    if (config != null)
                    {
                        emailBody = config.Value ?? config.DefaultValue;
                    }

                    config = rmsSystemConfigs.First(c => c.Name == "EmailFrom");
                    string emailFrom = "";
                    if (config != null)
                    {
                        emailFrom = config.Value ?? config.DefaultValue;
                    }


                    foreach (KeyValuePair<string, string> keyValuePair in distinctEmail)
                    {
                        string to = keyValuePair.Key;

                        string tSubject = emailSubject.Replace("::clientcode::", lRmsReportSummaryMonitorings[0].ClientCode);
                        var actionInfos = lEmails.Where(l => l.To == to).ToList();

                        string tableOfErrors = Environment.NewLine;
                        bool writeHeader = false;

                        foreach (var actionInfo in actionInfos)
                        {
                            if (!writeHeader)
                            {
                                tableOfErrors += actionInfo.ToPlainTextHeader() + Environment.NewLine;
                                writeHeader = true;
                            }

                            tableOfErrors += actionInfo.ToPlainText() + Environment.NewLine;
                        }

                        string tBody =
                            emailBody.Replace("|", Environment.NewLine)
                                .Replace("::clientcode::", lRmsReportSummaryMonitorings[0].ClientCode)
                                .Replace("::summaryerror::", tableOfErrors);

                        // Call Send Mail Here

                        if (!Convert.ToBoolean(ConfigurationManager.AppSettings["RMS.ActionModeTest"]))
                        {

                            var actionGatewayService = new ActionGateway();
                            try
                            {
                                var actionResult = actionGatewayService.SendEmail(gateway, emailFrom, new List<string> {to}, tSubject, tBody);
                                AddLogActionSend("Email", emailFrom, to, tBody, true, null);
                            }
                            catch (Exception ex)
                            {
                                AddLogActionSend("Email", emailFrom, to, tBody, false, ex.Message);
                            }
                        }
                        else
                        {
                            // Testing
                            string fileName = DateTime.Now.ToString("yyyyMMddTHHmmssff", new CultureInfo("en-AU")) + "." + to + ".txt";
                            if (!File.Exists(destinationPath + fileName))
                                File.Create(destinationPath + fileName).Close();

                            using (StreamWriter sw = File.AppendText(destinationPath + fileName))
                            {
                                sw.WriteLine(tBody);
                            }
                            System.Threading.Thread.Sleep(100);
                        }

                    }

                    #endregion

                    #region // Prepare SMS Body

                    config = rmsSystemConfigs.First(c => c.Name == "SMSSender");
                    string smsSender = "SKS Monitoring";
                    if (config != null)
                    {
                        smsSender = config.Value ?? config.DefaultValue;
                    }

                    foreach (var actionInfo in lSMSs)
                    {
                        var masterMessage = rmsMessageMasters.FirstOrDefault(f => f.Message.ToLower() == actionInfo.Message.ToLower());
                        if (masterMessage != null)
                        {

                            string tBody = masterMessage.SmsBody;
                            if (string.IsNullOrEmpty(tBody))
                            {
                                tBody = "พบข้อผิดพลาดของ ::devicedescription:: ที่ตู้ ::clientcode:: กรุณาตรวจสอบ";
                            }

                            tBody = tBody.Replace("|", Environment.NewLine)
                                .Replace("::clientcode::", lRmsReportSummaryMonitorings[0].ClientCode)
                                .Replace("::devicedescription::", string.IsNullOrEmpty(actionInfo.DeviceDescription)? 
                                    actionInfo.DeviceCode : actionInfo.DeviceDescription)
                                .Replace("::locationname::", actionInfo.LocationName)
                                .Replace("::locationcode::", actionInfo.LocationCode)
                                .Replace("::devicecode::", actionInfo.DeviceCode)
                                .Replace("::messagedatetime::", actionInfo.MessageDateTime == null ? 
                                    DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("en-AU")) :
                                    actionInfo.MessageDateTime.Value.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("en-AU")));

                            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["RMS.ActionModeTest"]))
                            {
                                var actionGatewayService = new ActionGateway();
                                try
                                {
                                    var actionResult = actionGatewayService.SendSMS(gateway, actionInfo.To, smsSender, tBody);
                                    AddLogActionSend("SMS", smsSender, actionInfo.To, tBody, true, null);

                                }
                                catch (Exception ex)
                                {
                                    AddLogActionSend("SMS", smsSender, actionInfo.To, tBody, false, ex.Message);
                                }
                            }
                            else
                            {
                                // Testing
                                string fileName = DateTime.Now.ToString("yyyyMMddTHHmmssff", new CultureInfo("en-AU")) + "." + actionInfo.To + ".txt";
                                if (!File.Exists(destinationPath + fileName))
                                    File.Create(destinationPath + fileName).Close();

                                using (StreamWriter sw = File.AppendText(destinationPath + fileName))
                                {
                                    sw.WriteLine(tBody);
                                }
                                System.Threading.Thread.Sleep(100);
                            }



                        }
                    }

                    #region Mode : SMS Summary (1 SMS contains multi error records)

                    //config = rmsSystemConfigs.First(c => c.Name == "SMSBody");
                    //string smsBody = "RMS พบข้อผิดพลาด ::clientcode:: > ::summaryerror::";
                    //if (config != null)
                    //{
                    //    smsBody = config.Value ?? config.DefaultValue;
                    //}

                    //// Prepare SMS Body
                    //foreach (KeyValuePair<string, string> keyValuePair in distinctSMS)
                    //{
                    //    string to = keyValuePair.Key;
                    //    var actionInfos = lSMSs.Where(l => l.To == to).ToList();

                    //    string tableOfErrors = "";
                    //    foreach (var actionInfo in actionInfos)
                    //    {
                    //        tableOfErrors += actionInfo.ToSMSText() + " ";
                    //    }

                    //    string tBody =
                    //        smsBody.Replace("|", Environment.NewLine)
                    //            .Replace("::clientcode::", lRmsReportSummaryMonitorings[0].ClientCode)
                    //            .Replace("::summaryerror::", tableOfErrors);

                    //    tBody = tBody.Trim();

                    //    // Call Send SMS Here

                    //    if (!Convert.ToBoolean(ConfigurationManager.AppSettings["RMS.ActionModeTest"]))
                    //    {
                    //        var actionGatewayService = new ActionGateway();
                    //        var actionResult = actionGatewayService.SendSMS(gateway, to, smsSender, tBody);
                    //    }
                    //    else
                    //    {
                    //        // Testing
                    //        string fileName = DateTime.Now.ToString("yyyyMMddTHHmmss", new CultureInfo("en-AU")) + "." + to + ".txt";
                    //        if (!File.Exists(destinationPath + fileName))
                    //            File.Create(destinationPath + fileName).Close();

                    //        using (StreamWriter sw = File.AppendText(destinationPath + fileName))
                    //        {
                    //            sw.WriteLine(tBody);
                    //        }
                    //    }

                    //}

                    #endregion


                    #endregion


                    #endregion
                }
            }
            catch (Exception ex)
            {
                string exLicense = "License is invalid";
                if (ex.Message.ToLower().IndexOf(exLicense.ToLower()) > -1)
                {
                    // Log into RMS_Log_Monitoring
                    AddLogActionSend(null, null, null, null, false, ex.Message);
                }

                throw new Exception("ManualSending errors. " + ex.Message, ex);
            }
        }

        private void TechnicalSending()
        {
            int activeClients;
            try
            {
                using (var db = new MyDbContext())
                {
                    #region Check License Validity by Calling ListClientWithIPAddress

                    var ms = new Centralize.BSL.MonitoringEngine.MonitoringService();
                    ms.ListClientWithIPAddress(licenseInfo, out activeClients);

                    #endregion


                    var dbRmsReportSummaryMonitorings = db.RmsReportSummaryMonitorings.Where(rsm => rsm.Status == 1);

                    List<RmsReportSummaryMonitoring> lRmsReportSummaryMonitorings =
                        new List<RmsReportSummaryMonitoring>(dbRmsReportSummaryMonitorings.ToList());

                    if (lRmsReportSummaryMonitorings.Count > 0) ManualSending(lRmsReportSummaryMonitorings);

                }
            }
            catch (Exception ex)
            {
                string exLicense = "License is invalid";
                if (ex.Message.ToLower().IndexOf(exLicense.ToLower()) > -1)
                {
                    // Log into RMS_Log_Monitoring
                    AddLogActionSend(null, null, null, null, false, ex.Message);
                }


                throw new Exception("TechnicalSending errors. " + ex.Message, ex);
            }
            
        }

        public void AddLogActionSend(string actionType, string from, string to, string body, bool? isSuccess, string errorMessage)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    var log = db.RmsLogActionSends.Create();
                    log.ActionDateTime = DateTime.Now;
                    log.ActionType = actionType;
                    log.From = from;
                    log.To = to;
                    if (!string.IsNullOrEmpty(body))
                        log.Body = body.Length > 1000 ? body.Substring(0, 1000) : body;
                    log.BodyFull = body;
                    log.IsSucess = isSuccess;
                    log.ErrorMessage = errorMessage;
                    log.IsSucess = isSuccess;
                    log.ErrorMessage = errorMessage;
                    db.RmsLogActionSends.Add(log);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("AddLogActionSend failed. " + ex.Message, ex);
            }
        }

    }
}