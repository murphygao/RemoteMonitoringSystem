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

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SummaryReportService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SummaryReportService.svc or SummaryReportService.svc.cs at the Solution Explorer and start debugging.
    public class SummaryReportService : ISummaryReportService
    {
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

                return new ClientInfoResult
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public Result ActionRequest(ActionService.ActionSendType actionSendType, long reportID)
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
                        var action = new ActionService();
                        action.ActionSend(ActionService.ActionSendType.ManualSending, lReports);
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
                return new Result
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }

        }
    }
}
