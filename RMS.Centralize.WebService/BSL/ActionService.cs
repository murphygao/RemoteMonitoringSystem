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
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Gateway;
using RMS.Centralize.WebService.Model;
using RMS.Common.Exception;

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
            SummaryHighLevelSending,
        }

        public void ActionSend(ActionSendType actionSendType, List<RmsReportSummaryMonitoring> lRmsReportSummaryMonitorings, List<RmsReportSummaryMonitoring> lSolvedMonitorings = null)
        {
            try
            {
                switch (actionSendType)
                {
                    case ActionSendType.ManualSending:
                        ManualSending(lRmsReportSummaryMonitorings, lSolvedMonitorings);
                        break;

                    case ActionSendType.SummaryTechnicalSending:
                        TechnicalSending();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "ActionSend failed. " + ex.Message, ex, false);
            }
        }

        private void ManualSending(List<RmsReportSummaryMonitoring> lRmsReportSummaryMonitorings, List<RmsReportSummaryMonitoring> lSolvedMonitorings = null)
        {

            new RMSDebugLog("Start ManualSending ", true);

            if (lRmsReportSummaryMonitorings == null) lRmsReportSummaryMonitorings = new List<RmsReportSummaryMonitoring>();
            if (lSolvedMonitorings == null) lSolvedMonitorings = new List<RmsReportSummaryMonitoring>();

            if (lRmsReportSummaryMonitorings.Count == 0 && lSolvedMonitorings.Count == 0)
            {
                new RMSWebException("Both lRmsReportSummaryMonitorings and lSolvedMonitorings cannot be zero. ", true);
                return;
            }

            new RMSDebugLog("Start ManualSending: lRmsReportSummaryMonitorings.Count = " + lRmsReportSummaryMonitorings.Count, true);
            new RMSDebugLog("Start ManualSending: lSolvedMonitorings.Count = " + lSolvedMonitorings.Count, true);


            int activeClients;
            try
            {
                List<ActionInfo> lEmails = new List<ActionInfo>();
                List<ActionInfo> lSMSs = new List<ActionInfo>();
                Dictionary<string, string> distinctEmail = new Dictionary<string, string>();
                Dictionary<string, string> distinctSMS = new Dictionary<string, string>();

                using (var db = new MyDbContext())
                {

                    #region Check License Validity by Calling ListClientWithIPAddresss

                    var ms = new Centralize.BSL.MonitoringEngine.MonitoringService();
                    ms.ListClientWithIPAddress(licenseInfo, out activeClients, null);

                    #endregion

                    int? _clientID;
                    string _clientCode;

                    if (lRmsReportSummaryMonitorings.Count > 0)
                    {
                        _clientID = lRmsReportSummaryMonitorings[0].ClientId;
                        _clientCode = lRmsReportSummaryMonitorings[0].ClientCode;
                    }
                    else
                    {
                        _clientID = lSolvedMonitorings[0].ClientId;
                        _clientCode = lSolvedMonitorings[0].ClientCode;
                    }

                    SqlParameter[] parameters = new SqlParameter[1];
                    SqlParameter p1 = new SqlParameter("ClientID", _clientID);
                    parameters[0] = p1;

                    var details = db.Database.SqlQuery<ClientMessageAction>("RMS_ListClientMessageAction " +
                                                                            "@ClientID", parameters);
                    List<ClientMessageAction> lClientMessageActions = new List<ClientMessageAction>(details.ToList());

                    // lClientMessageActions ถ้าเป็น 0 แสดงว่า table: RMS_ClientSeverityAction ไม่มีข้อมูลของ Client ID นี้
                    if (lClientMessageActions.Count == 0) throw new Exception("Defined action not found. Please check table: RMS_ClientSeverityAction for Client ID: " + _clientCode);


                    #region Message Type : Error Message

                    foreach (var monitoring in lRmsReportSummaryMonitorings)
                    {
                        RmsReportSummaryMonitoring monitoring1 = monitoring;
                        bool useEmail = false;
                        bool useSMS = false;

                        var clientMessageAction = lClientMessageActions.First(cma => cma.MessageID == monitoring1.MessageId);

                        if (clientMessageAction == null) continue;

                        #region //EMAIL SECTION

                        new RMSDebugLog("Start ManualSending - Prepare Email ", true);

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
                            info.MessageRemark = monitoring.MessageRemark;

                            info.SummaryMonitoringReportID = monitoring.Id;

                            info.MessageType = MessageType.ErrorMessage;

                            lEmails.Add(info);

                            useEmail = true;

                            if (!distinctEmail.ContainsKey(info.To))
                                distinctEmail.Add(info.To, info.To);
                        }

                        #endregion

                        #region //SMS SECTION

                        new RMSDebugLog("Start ManualSending - Prepare SMS ", true);

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
                            info.MessageRemark = monitoring.MessageRemark;
                            info.SummaryMonitoringReportID = monitoring.Id;
                            info.MessageType = MessageType.ErrorMessage;

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

                    #endregion

                    #region Message Type : Solved Message

                    foreach (var monitoring in lSolvedMonitorings)
                    {
                        RmsReportSummaryMonitoring monitoring1 = monitoring;
                        bool useEmail = false;
                        bool useSMS = false;

                        var clientMessageAction = lClientMessageActions.First(cma => cma.MessageID == monitoring1.MessageId);

                        if (clientMessageAction == null) continue;

                        #region //EMAIL SECTION

                        new RMSDebugLog("Start ManualSending - Prepare Email ", true);

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

                        var splitEmail = mEmails.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

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
                            info.MessageRemark = monitoring.MessageRemark;

                            info.SummaryMonitoringReportID = monitoring.Id;

                            info.MessageType = MessageType.SolvedMessage;

                            lEmails.Add(info);

                            useEmail = true;

                            if (!distinctEmail.ContainsKey(info.To))
                                distinctEmail.Add(info.To, info.To);
                        }

                        #endregion

                        #region //SMS SECTION

                        new RMSDebugLog("Start ManualSending - Prepare SMS ", true);

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

                        var splitSMS = mSMSs.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

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
                            info.MessageRemark = monitoring.MessageRemark;
                            info.SummaryMonitoringReportID = monitoring.Id;
                            info.MessageType = MessageType.SolvedMessage;

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

                    #endregion


                    #region Action Process

                    new RMSDebugLog("Start ManualSending - Action Process ", true);

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

                    new RMSDebugLog("Start ManualSending - Prepare Email Body ", true);

                    config = rmsSystemConfigs.First(c => c.Name == "EmailSubject");
                    string emailSubject = "RMS Monitoring ของตู้ที่ ::clientcode::";
                    if (config != null)
                    {
                        emailSubject = config.Value ?? config.DefaultValue;
                    }

                    config = rmsSystemConfigs.First(c => c.Name == "EmailBody");
                    string emailBody = "ถึงผู้ดูแลของตู้ที่ ::clientcode::||รายงานผลการเปลี่ยนสถานะของอุปกรณ์ของตู้ที่ ::clientcode::||::summaryreport::||ขอบคุณครับ";
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

                        string tSubject = emailSubject.Replace("::clientcode::", _clientCode);
                        var actionInfos = lEmails.Where(l => l.To == to).ToList();

                        string tableOfSummaryReport = Environment.NewLine;
                        bool writeHeader = false;

                        foreach (var actionInfo in actionInfos)
                        {
                            //if (!writeHeader)
                            //{
                            //    tableOfErrors += actionInfo.ToPlainTextHeader() + Environment.NewLine;
                            //    writeHeader = true;
                            //}
                            
                            var masterMessage = rmsMessageMasters.FirstOrDefault(f => f.Message.ToLower() == actionInfo.Message.ToLower());
                            string tEmail;

                            if (actionInfo.MessageType == MessageType.ErrorMessage)
                            {
                                tEmail = "กรุณาตรวจสอบ ::devicedescription:: ของตู้ที่ ::clientcode:: - ::messageremark::";
                            }
                            else
                            {
                                tEmail = "::devicedescription:: ของตู้ที่ ::clientcode:: กลับสู่สภาวะปกติ ::messageremark::";
                            }

                            if (masterMessage != null)
                            {
                                if (actionInfo.MessageType == MessageType.ErrorMessage && !string.IsNullOrEmpty(masterMessage.EmailBody))
                                {
                                    tEmail = masterMessage.EmailBody;
                                }
                                else if (actionInfo.MessageType == MessageType.SolvedMessage && !string.IsNullOrEmpty(masterMessage.EmailBodySolved))
                                {
                                    tEmail = masterMessage.EmailBodySolved;
                                }
                            }

                            tEmail = tEmail.Replace("|", Environment.NewLine)
                                .Replace("::clientcode::", _clientCode)
                                .Replace("::devicedescription::", string.IsNullOrEmpty(actionInfo.DeviceDescription)
                                    ? actionInfo.DeviceCode
                                    : actionInfo.DeviceDescription)
                                .Replace("::locationname::", actionInfo.LocationName)
                                .Replace("::messageremark::", actionInfo.MessageRemark)
                                .Replace("::locationcode::", actionInfo.LocationCode)
                                .Replace("::devicecode::", actionInfo.DeviceCode)
                                .Replace("::messagedatetime::", actionInfo.MessageDateTime == null
                                    ? DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("en-AU"))
                                    : actionInfo.MessageDateTime.Value.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("en-AU")));

                            tableOfSummaryReport += tEmail + Environment.NewLine;
                        }

                        string tBody =
                            emailBody.Replace("|", Environment.NewLine)
                                .Replace("::clientcode::", _clientCode)
                                .Replace("::summaryreport::", tableOfSummaryReport);

                        // Call Send Mail Here
                        new RMSDebugLog("Start ManualSending - Call Send Mail ", true);

                        if (!Convert.ToBoolean(ConfigurationManager.AppSettings["RMS.ActionModeTest"]))
                        {

                            var actionGatewayService = new ActionGateway();
                            try
                            {
                                var actionResult = actionGatewayService.SendEmail(gateway, emailFrom, new List<string> {to}, tSubject, tBody);
                                if (actionResult.IsSuccess)
                                {
                                    AddLogActionSend("Email", emailFrom, to, tBody, true, null);
                                
                                }
                                else
                                {
                                    AddLogActionSend("Email", emailFrom, to, tBody, false, actionResult.ErrorMessage);
                                }
                            }
                            catch (Exception ex)
                            {
                                new RMSWebException(this, "0500", "GatewayService.SendEmail failed. " + ex.Message, ex, true);
                                AddLogActionSend("Email", emailFrom, to, tBody, false, ex.Message);
                            }
                        }
                        else
                        {
                            // Testing
                            try
                            {
                                new RMSDebugLog("Start ManualSending Testing Mode - Email ", true);

                                string fileName = DateTime.Now.ToString("yyyyMMddTHHmmssff", new CultureInfo("en-AU")) + "." + to + ".txt";
                                if (!File.Exists(destinationPath + fileName))
                                    File.Create(destinationPath + fileName).Close();

                                using (StreamWriter sw = File.AppendText(destinationPath + fileName))
                                {
                                    sw.WriteLine(tBody);
                                }

                                AddLogActionSend("Email", emailFrom, to, tBody, true, "Testing Mode");
                            }
                            catch (Exception ex)
                            {
                                new RMSDebugLog("Start ManualSending Testing Mode - Email - Error " + ex.Message, true);
                            }
                            System.Threading.Thread.Sleep(100);
                        }

                    }

                    #endregion

                    #region // Prepare SMS Body
                    new RMSDebugLog("Start ManualSending - Prepare SMS Body ", true);

                    config = rmsSystemConfigs.First(c => c.Name == "SMSSender");
                    string smsSender = "RMS Monitoring";
                    if (config != null)
                    {
                        smsSender = config.Value ?? config.DefaultValue;
                    }


                    // Send SMS per Message
                    foreach (var actionInfo in lSMSs)
                    {
                        var masterMessage = rmsMessageMasters.FirstOrDefault(f => f.Message.ToLower() == actionInfo.Message.ToLower());

                        string tBody;

                        if (actionInfo.MessageType == MessageType.ErrorMessage)
                        {
                            tBody = "กรุณาตรวจสอบ ::devicedescription:: ของตู้ที่ ::clientcode:: - ::messageremark::";
                        }
                        else
                        {
                            tBody = "::devicedescription:: ของตู้ที่ ::clientcode:: กลับสู่สภาวะปกติ ::messageremark::";
                        }

                        if (masterMessage != null)
                        {
                            if (actionInfo.MessageType == MessageType.ErrorMessage && !string.IsNullOrEmpty(masterMessage.SmsBody))
                            {
                                tBody = masterMessage.SmsBody;
                            }
                            else if (actionInfo.MessageType == MessageType.SolvedMessage && !string.IsNullOrEmpty(masterMessage.SmsBodySolved))
                            {
                                tBody = masterMessage.SmsBodySolved;
                            }
                        }

                        tBody = tBody.Replace("|", Environment.NewLine)
                            .Replace("::clientcode::", _clientCode)
                            .Replace("::devicedescription::", string.IsNullOrEmpty(actionInfo.DeviceDescription)
                                ? actionInfo.DeviceCode
                                : actionInfo.DeviceDescription)
                            .Replace("::locationname::", actionInfo.LocationName)
                            .Replace("::messageremark::", actionInfo.MessageRemark)
                            .Replace("::locationcode::", actionInfo.LocationCode)
                            .Replace("::devicecode::", actionInfo.DeviceCode)
                            .Replace("::messagedatetime::", actionInfo.MessageDateTime == null
                                ? DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("en-AU"))
                                : actionInfo.MessageDateTime.Value.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("en-AU")));

                        if (!string.IsNullOrEmpty(actionInfo.To))
                            actionInfo.To = actionInfo.To.Replace("-", "").Replace(" ", "");

                        new RMSDebugLog("Start ManualSending - Call Send SMS ", true);

                        if (!Convert.ToBoolean(ConfigurationManager.AppSettings["RMS.ActionModeTest"]))
                        {
                            var actionGatewayService = new ActionGateway();
                            try
                            {
                                var actionResult = actionGatewayService.SendSMS(gateway, actionInfo.To, smsSender, tBody);
                                if (actionResult.IsSuccess)
                                {
                                    AddLogActionSend("SMS", smsSender, actionInfo.To, tBody, true, null);

                                }
                                else
                                {
                                    AddLogActionSend("SMS", smsSender, actionInfo.To, tBody, false, actionResult.ErrorMessage);
                                }

                            }
                            catch (Exception ex)
                            {
                                new RMSWebException(this, "0500", "GatewayService.SendSMS failed. " + ex.Message, ex, true);
                                AddLogActionSend("SMS", smsSender, actionInfo.To, tBody, false, ex.Message);
                            }
                        }
                        else
                        {
                            // Testing
                            try
                            {
                                new RMSDebugLog("Start ManualSending Testing Mode - SMS ", true);

                                string fileName = DateTime.Now.ToString("yyyyMMddTHHmmssff", new CultureInfo("en-AU")) + "." + actionInfo.To + ".txt";
                                if (!File.Exists(destinationPath + fileName))
                                    File.Create(destinationPath + fileName).Close();

                                using (StreamWriter sw = File.AppendText(destinationPath + fileName))
                                {
                                    sw.WriteLine(tBody);
                                }

                                AddLogActionSend("SMS", smsSender, actionInfo.To, tBody, true, "Testing Mode");
                            }
                            catch (Exception ex)
                            {
                                new RMSDebugLog("Start ManualSending Testing Mode - SMS - Error " + ex.Message, true);
                            }
                            System.Threading.Thread.Sleep(100);
                        }

                    }

                    #region Mode : SMS Summary (1 SMS contains multi error records)

                    //config = rmsSystemConfigs.First(c => c.Name == "SMSBody");
                    //string smsBody = "RMS พบข้อผิดพลาด ::clientcode:: > ::summaryreport::";
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
                    //            .Replace("::summaryreport::", tableOfErrors);

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
                    try
                    {
                        AddLogActionSend(null, null, null, null, false, ex.Message);
                    }
                    catch (Exception ex2)
                    {
                        new RMSWebException(this, "0500", "AddLogActionSend failed. " + ex2.Message + ". " + ex.Message, ex2, true);
                    }
                }
                throw new RMSWebException(this, "0500", "ManualSending failed. " + ex.Message, ex, false);
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
                    ms.ListClientWithIPAddress(licenseInfo, out activeClients, null);

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
                    try
                    {
                        AddLogActionSend(null, null, null, null, false, ex.Message);
                    }
                    catch (Exception ex2)
                    {
                        new RMSWebException(this, "0500", "AddLogActionSend failed. " + ex2.Message + ". " + ex.Message, ex2, true);
                    }
                }

                throw new RMSWebException(this, "0500", "TechnicalSending failed. " + ex.Message, ex, false);
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
                throw new RMSWebException(this, "0500", "AddLogActionSend failed. " + ex.Message, ex, false);
            }
        }

    }
}