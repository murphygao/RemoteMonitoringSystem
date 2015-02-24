using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.BSL;
using RMS.Centralize.WebService.Model;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SummaryReportService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SummaryReportService.svc or SummaryReportService.svc.cs at the Solution Explorer and start debugging.
    public class SummaryReportService : ISummaryReportService
    {
        public void TestConnection()
        {

        }

        public SummaryMonitoringResult SearchSummaryMonitoring(JQueryDataTableParamModel param, DateTime? txtStartEventDate, DateTime? txtEndEventDate
            , string txtClientCode, string txtLocation, string ddlMessageGroup, string txtMessage, string ddlMessageStatus, int? clientID)
        {
            List<ReportSummaryMonitoring> lReportSummaryMonitorings = new List<ReportSummaryMonitoring>();
            SqlParameter[] parameters = new SqlParameter[19];

            try
            {
                using (var db = new MyDbContext())
                {
                    SqlParameter p1 = new SqlParameter("PageNbr", DBNull.Value);
                    SqlParameter p2 = new SqlParameter("PageSize", param.iDisplayLength);
                    SqlParameter p3 = new SqlParameter("FirstRec", param.iDisplayStart);
                    
                    SqlParameter p4;
                    if (String.IsNullOrEmpty(param.iSortColumn))
                    {
                        p4 = new SqlParameter("SortCol", DBNull.Value);
                    }
                    else
                    {
                        p4 = new SqlParameter("SortCol", param.iSortColumn);
                    }

                    SqlParameter p5 = new SqlParameter("TotalRecords", SqlDbType.Int);
                    p5.Direction = ParameterDirection.Output;

                    SqlParameter p6;
                    SqlParameter p7;
                    SqlParameter p8;
                    SqlParameter p9;
                    SqlParameter p10;
                    SqlParameter p11;
                    SqlParameter p12;
                    SqlParameter p13;
                    SqlParameter p14;
                    SqlParameter p15;
                    SqlParameter p16;
                    SqlParameter p17;
                    SqlParameter p18;
                    SqlParameter p19;


                    if (String.IsNullOrEmpty(txtClientCode))
                    {
                        p6 = new SqlParameter("ClientCode", DBNull.Value);
                    }
                    else
                    {
                        p6 = new SqlParameter("ClientCode", txtClientCode);
                    }

                    p7 = new SqlParameter("ClientIPAddress", DBNull.Value);

                    if (String.IsNullOrEmpty(txtLocation))
                    {
                        p8 = new SqlParameter("Location", DBNull.Value);
                    }
                    else
                    {
                        p8 = new SqlParameter("Location", txtLocation);
                    }

                    p9 = new SqlParameter("DeviceID", DBNull.Value);
                    p9.SqlDbType = SqlDbType.Int;


                    if (String.IsNullOrEmpty(ddlMessageGroup))
                    {
                        p10 = new SqlParameter("MessageGroupID", DBNull.Value);
                    }
                    else
                    {
                        p10 = new SqlParameter("MessageGroupID", ddlMessageGroup);
                    }

                    if (String.IsNullOrEmpty(txtMessage))
                    {
                        p11 = new SqlParameter("Message", DBNull.Value);
                    }
                    else
                    {
                        p11 = new SqlParameter("Message", txtMessage);
                    }

                    if (String.IsNullOrEmpty(ddlMessageStatus))
                    {
                        p12 = new SqlParameter("Status", DBNull.Value);
                    }
                    else
                    {
                        p12 = new SqlParameter("Status", ddlMessageStatus);
                    }

                    if (txtStartEventDate == null)
                    {
                        p13 = new SqlParameter("EventDateTimeStart", DBNull.Value);
                        p13.DbType = DbType.DateTime;
                    }
                    else
                    {
                        p13 = new SqlParameter("EventDateTimeStart", txtStartEventDate);
                    }

                    if (txtEndEventDate == null)
                    {
                        p14 = new SqlParameter("EventDateTimeEnd", DBNull.Value);
                        p14.DbType = DbType.DateTime;
                    }
                    else
                    {
                        p14 = new SqlParameter("EventDateTimeEnd", txtEndEventDate);
                    }

                    p15 = new SqlParameter("EventDateTime", DBNull.Value);
                    p15.SqlDbType = SqlDbType.DateTime;

                    p16 = new SqlParameter("CreatedDate", DBNull.Value);
                    p16.SqlDbType = SqlDbType.DateTime;

                    p17 = new SqlParameter("UpdatedDate", DBNull.Value);
                    p17.SqlDbType = SqlDbType.DateTime;

                    p18 = new SqlParameter("IncludeOK", false);

                    if (clientID == null)
                    {
                        p19 = new SqlParameter("ClientID", DBNull.Value);
                        p19.DbType = DbType.Int32;
                    }
                    else
                    {
                        p19 = new SqlParameter("ClientID", clientID);
                    }


                    parameters[0] = p1;
                    parameters[1] = p2;
                    parameters[2] = p3;
                    parameters[3] = p4;
                    parameters[4] = p5;
                    parameters[5] = p6;
                    parameters[6] = p7;
                    parameters[7] = p8;
                    parameters[8] = p9;
                    parameters[9] = p10;
                    parameters[10] = p11;
                    parameters[11] = p12;
                    parameters[12] = p13;
                    parameters[13] = p14;
                    parameters[14] = p15;
                    parameters[15] = p16;
                    parameters[16] = p17;
                    parameters[17] = p18;
                    parameters[18] = p19;

                    db.Configuration.ProxyCreationEnabled = false;
                    //db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.Database.SqlQuery<ReportSummaryMonitoring>("RMS_Report_ListSummaryMonitoring " +
                                                                            "@ClientCode, @ClientIPAddress, @Location, @DeviceID" +
                                                                            ", @MessageGroupID, @Message, @Status" +
                                                                            ", @EventDateTimeStart, @EventDateTimeEnd" +
                                                                            ", @EventDateTime, @CreatedDate, @UpdatedDate, @IncludeOK, @ClientID" +
                                                                            ", @PageNbr, @PageSize, @FirstRec, @SortCol, @TotalRecords OUTPUT"
                        , parameters);

                    lReportSummaryMonitorings = new List<ReportSummaryMonitoring>(listOfType.ToList());

                    SummaryMonitoringResult sr = new SummaryMonitoringResult
                    {
                        IsSuccess = true,
                        ListSummaryMonitorings = lReportSummaryMonitorings,
                        TotalRecords = (int)parameters[4].Value
                    };
                    return sr;
                }

            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "SearchSummaryMonitoring failed. " + ex.Message, ex, true);

                return new SummaryMonitoringResult
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }

        }

        public ClientInfoResult GetClientInfo(int clientID)
        {
            try
            {
                using (var db = new MyDbContext())
                {

                    SqlParameter p1 = new SqlParameter("ClientID", clientID);

                    var listOfType = db.Database.SqlQuery<ClientInfo>("RMS_GetClientInfoforReport " +
                                                                                   "@ClientID", p1);

                    List<ClientInfo> lClientIfnInfos = new List<ClientInfo>(listOfType.ToList());

                    if (lClientIfnInfos.Count > 0)
                    {
                        ClientInfoResult cr = new ClientInfoResult
                        {
                            IsSuccess = true,
                            Client = lClientIfnInfos[0]
                        };
                        return cr;
                    }
                    else
                    {
                        ClientInfoResult cr = new ClientInfoResult
                        {
                            IsSuccess = false,
                            ErrorMessage = "Client Not Found."
                        };
                        return cr;
                    }
                }
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "GetClientInfo failed. " + ex.Message, ex, true);

                return new ClientInfoResult
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public Result ActionRequest(ActionSendService.ActionSendType actionSendType, long reportID)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    var report = db.RmsReportSummaryMonitorings.First(smg => smg.Id == reportID && smg.Status == 1);
                    if (report != null)
                    {
                        List<RmsReportSummaryMonitoring> lReports = new List<RmsReportSummaryMonitoring>();
                        lReports.Add(report);
                        var action = new ActionSendService();
                        action.ActionSend(ActionSendService.ActionSendType.ManualSending, lReports);
                        return new Result
                        {
                            IsSuccess = true
                        };
                    }
                    return new Result
                    {
                        IsSuccess = false,
                        ErrorMessage = "Message Not Found."
                    };
                }
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "ActionRequest failed. " + ex.Message, ex, true);

                return new Result
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }

        }

        public SummaryStatusAllClientsInfoResult SearchSummaryStatusAllClients()
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

                    var rmsLocations = db.RmsLocations;

                    foreach (var info in lstatusList)
                    {
                        if (info.AgentNotAlive == 0)
                        {
                            info.sRMSStatus = "RMS is up";
                            info.iRMSStatus = 10;
                        }
                        else
                        {
                            info.sRMSStatus = "RMS is down";
                            info.iRMSStatus = 0;
                        }

                        //Alive:Yes และ Issue = 0 แสดงว่า Agent Not Alive
                        if (info.AgentNotAlive == 0 && info.CounterNotOK == 0)
                        {
                            if (!info.HasMonitoringAgent)
                            {
                                info.sRMSStatus = "No RMS Agent";
                                info.iRMSStatus = 11;

                                if (info.ClientTypeID == 3)
                                {
                                    info.sRMSStatus = "Server Type - " + info.sRMSStatus;
                                    info.iRMSStatus = 12;
                                }
                            }
                            else if (info.CounterOK == 0)
                            {
                                info.sRMSStatus = "RMS is up, but cannot monitoring.";
                                info.iRMSStatus = 31;
                            }
                        }

                        var location = rmsLocations.First(c => c.LocationId == info.LocationID);
                        if (!CanBroadCast(location))
                        {
                            info.sRMSStatus = "Off Duty";
                            info.iRMSStatus = 20;
                        }

                    }
                }
                SummaryStatusAllClientsInfoResult cr = new SummaryStatusAllClientsInfoResult
                {
                    IsSuccess = true,
                    ListSummaryStatusAllClientsInfos = lstatusList,
                    TotalRecords = lstatusList.Count
                };
                return cr;

            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "SearchSummaryStatusAllClientsInfo failed. " + ex.Message, ex, true);
                return new SummaryStatusAllClientsInfoResult
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };

            }
        }

        public bool CanBroadCast(RmsLocation location)
        {
            try
            {
                //ครวจสอบว่า วันและเวลาตอนนี้ ต้อง broadcast หรือไม่
                DateTime myToday = DateTime.Now;

                if ((int)myToday.DayOfWeek == 1) // Monday
                {
                    if (location.MondayEnable == null || location.MondayEnable == false) return false;
                    if (location.MondayWholeDay == true) return true;
                    if (location.MondayStart == null || location.MondayEnd == null) return true;

                    int sHH = location.MondayStart.Value.Hour;
                    int smm = location.MondayStart.Value.Minute;
                    DateTime startTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, sHH, smm, 0);
                    int eHH = location.MondayEnd.Value.Hour;
                    int emm = location.MondayEnd.Value.Minute;
                    DateTime endTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, eHH, emm, 0);
                    if (startTime <= myToday && myToday <= endTime) return true;
                }

                else if ((int)myToday.DayOfWeek == 2) // Tuesday
                {
                    if (location.TuesdayEnable == null || location.TuesdayEnable == false) return false;
                    if (location.TuesdayWholeDay == true) return true;
                    if (location.TuesdayStart == null || location.TuesdayEnd == null) return true;

                    int sHH = location.TuesdayStart.Value.Hour;
                    int smm = location.TuesdayStart.Value.Minute;
                    DateTime startTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, sHH, smm, 0);
                    int eHH = location.TuesdayEnd.Value.Hour;
                    int emm = location.TuesdayEnd.Value.Minute;
                    DateTime endTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, eHH, emm, 0);
                    if (startTime <= myToday && myToday <= endTime) return true;
                }

                else if ((int)myToday.DayOfWeek == 3) // Wednesday
                {
                    if (location.WednesdayEnable == null || location.WednesdayEnable == false) return false;
                    if (location.WednesdayWholeDay == true) return true;
                    if (location.WednesdayStart == null || location.WednesdayEnd == null) return true;

                    int sHH = location.WednesdayStart.Value.Hour;
                    int smm = location.WednesdayStart.Value.Minute;
                    DateTime startTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, sHH, smm, 0);
                    int eHH = location.WednesdayEnd.Value.Hour;
                    int emm = location.WednesdayEnd.Value.Minute;
                    DateTime endTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, eHH, emm, 0);
                    if (startTime <= myToday && myToday <= endTime) return true;
                }

                else if ((int)myToday.DayOfWeek == 4) // Thursday
                {
                    if (location.ThursdayEnable == null || location.ThursdayEnable == false) return false;
                    if (location.ThursdayWholeDay == true) return true;
                    if (location.ThursdayStart == null || location.ThursdayEnd == null) return true;

                    int sHH = location.ThursdayStart.Value.Hour;
                    int smm = location.ThursdayStart.Value.Minute;
                    DateTime startTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, sHH, smm, 0);
                    int eHH = location.ThursdayEnd.Value.Hour;
                    int emm = location.ThursdayEnd.Value.Minute;
                    DateTime endTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, eHH, emm, 0);
                    if (startTime <= myToday && myToday <= endTime) return true;
                }

                else if ((int)myToday.DayOfWeek == 5) // Friday
                {
                    if (location.FridayEnable == null || location.FridayEnable == false) return false;
                    if (location.FridayWholeDay == true) return true;
                    if (location.FridayStart == null || location.FridayEnd == null) return true;

                    int sHH = location.FridayStart.Value.Hour;
                    int smm = location.FridayStart.Value.Minute;
                    DateTime startTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, sHH, smm, 0);
                    int eHH = location.FridayEnd.Value.Hour;
                    int emm = location.FridayEnd.Value.Minute;
                    DateTime endTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, eHH, emm, 0);
                    if (startTime <= myToday && myToday <= endTime) return true;
                }

                else if ((int)myToday.DayOfWeek == 6) // Saturday
                {
                    if (location.SaturdayEnable == null || location.SaturdayEnable == false) return false;
                    if (location.SaturdayWholeDay == true) return true;
                    if (location.SaturdayStart == null || location.SaturdayEnd == null) return true;

                    int sHH = location.SaturdayStart.Value.Hour;
                    int smm = location.SaturdayStart.Value.Minute;
                    DateTime startTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, sHH, smm, 0);
                    int eHH = location.SaturdayEnd.Value.Hour;
                    int emm = location.SaturdayEnd.Value.Minute;
                    DateTime endTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, eHH, emm, 0);
                    if (startTime <= myToday && myToday <= endTime) return true;
                }

                else if ((int)myToday.DayOfWeek == 0) // Sunday
                {
                    if (location.SundayEnable == null || location.SundayEnable == false) return false;
                    if (location.SundayWholeDay == true) return true;
                    if (location.SundayStart == null || location.SundayEnd == null) return true;

                    int sHH = location.SundayStart.Value.Hour;
                    int smm = location.SundayStart.Value.Minute;
                    DateTime startTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, sHH, smm, 0);
                    int eHH = location.SundayEnd.Value.Hour;
                    int emm = location.SundayEnd.Value.Minute;
                    DateTime endTime = new DateTime(myToday.Year, myToday.Month, myToday.Day, eHH, emm, 0);
                    if (startTime <= myToday && myToday <= endTime) return true;
                }


                return false;
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "CanBroadCast failed. " + ex.Message, ex, false);
            }

        }

    }
}
