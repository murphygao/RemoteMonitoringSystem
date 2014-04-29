using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Web;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Model;

namespace RMS.Centralize.WebService.BSL
{
    public class ActionService
    {
        public void ActionRequest(List<RmsReportSummaryMonitoring> lRmsReportSummaryMonitorings)
        {

        
        }

        public void ActionSend(List<RmsReportSummaryMonitoring> lRmsReportSummaryMonitorings)
        {
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

                        var clientMessageAction = lClientMessageActions.First(cma => cma.MessageID == monitoring1.MessageId);

                        //EMAIL SECTION
                        string mEmails = string.Empty;
                        if (clientMessageAction.OverwritenAction == true)
                        {
                            mEmails = clientMessageAction.Email2;
                        }
                        else
                        {
                            mEmails = clientMessageAction.Email + ";" + clientMessageAction.Email2;
                        }

                        var splitEmail = mEmails.Split(new string[]{";"}, StringSplitOptions.RemoveEmptyEntries);

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

                            lEmails.Add(info);

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

                            lSMSs.Add(info);

                            if (!distinctSMS.ContainsKey(info.To))
                                distinctSMS.Add(info.To, info.To);
                        }
                    }

                    // Prepare Email
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


                    }


                    // Prepare SMS
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
    }
}