using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Model;

namespace RMS.Centralize.WebService.BSL
{
    public class ActionService
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
                throw ex;
            }
        }

        private void ManualSending(List<RmsReportSummaryMonitoring> lRmsReportSummaryMonitorings, bool updateLastAction = false)
        {
            if (lRmsReportSummaryMonitorings == null || lRmsReportSummaryMonitorings.Count == 0) return;

            try
            {
                List<ActionInfo> lEmails = new List<ActionInfo>();
                List<ActionInfo> lSMSs = new List<ActionInfo>();
                Dictionary<string, string> distinctEmail = new Dictionary<string, string>();
                Dictionary<string, string> distinctSMS = new Dictionary<string, string>();

                using (var db = new MyDbContext())
                {
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

                        //EMAIL SECTION

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
                            info.SummaryMonitoringReportID = monitoring.Id;

                            lEmails.Add(info);

                            useEmail = true;

                            if (!distinctEmail.ContainsKey(info.To))
                                distinctEmail.Add(info.To, info.To);
                        }

                        //SMS SECTION
                        string mSMSs = string.Empty;
                        if (clientMessageAction.OverwritenAction == true)
                        {
                            mSMSs = clientMessageAction.SMS2;
                        }
                        else
                        {
                            mSMSs = clientMessageAction.SMS + ";" + clientMessageAction.SMS2;
                        }

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
                            info.SummaryMonitoringReportID = monitoring.Id;

                            lSMSs.Add(info);

                            useSMS = true;

                            if (!distinctSMS.ContainsKey(info.To))
                                distinctSMS.Add(info.To, info.To);
                        }


                        //Update ค่า Last Action
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
                    }

                    // Prepare Email Body
                    foreach (KeyValuePair<string, string> keyValuePair in distinctEmail)
                    {
                        string to = keyValuePair.Key;
                        string subject = "Monitoring : พบข้อผิดพลาด " + lRmsReportSummaryMonitorings[0].ClientCode;
                        var actionInfos = lEmails.Where(l => l.To == to).ToList();

                        string body = Environment.NewLine;
                        foreach (var actionInfo in actionInfos)
                        {
                            body += actionInfo.ToPlainText() + Environment.NewLine;
                        }


                        // Call Send Mail Here
                        /*
                         * 
                         * 
                         * 
                         */

                        string fileName = DateTime.Now.ToString("yyyyMMddTHHmmss", new CultureInfo("en-AU")) + "." + to + ".txt";
                        // Testing
                        if (!File.Exists(destinationPath + fileName))
                            File.Create(destinationPath + fileName).Close();

                        using (StreamWriter sw = File.AppendText(destinationPath + fileName))
                        {
                            sw.WriteLine(body);
                        }

                    }


                    // Prepare SMS Body
                    foreach (KeyValuePair<string, string> keyValuePair in distinctSMS)
                    {
                        string to = keyValuePair.Key;
                        string body = "พบข้อผิดพลาด " + lRmsReportSummaryMonitorings[0].ClientCode + " > ";
                        var actionInfos = lEmails.Where(l => l.To == to).ToList();

                        foreach (var actionInfo in actionInfos)
                        {
                            body += actionInfo.ToSMSText() + " ";
                        }
                        body = body.Trim();

                        // Call Send SMS Here
                        /*
                         * 
                         * 
                         * 
                         */


                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void TechnicalSending()
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    var dbRmsReportSummaryMonitorings = db.RmsReportSummaryMonitorings.Where(rsm => rsm.Status == 1);

                    List<RmsReportSummaryMonitoring> lRmsReportSummaryMonitorings =
                        new List<RmsReportSummaryMonitoring>(dbRmsReportSummaryMonitorings.ToList());

                    if (lRmsReportSummaryMonitorings.Count > 0) ManualSending(lRmsReportSummaryMonitorings);

                }
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
    }
}