using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Model;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SummaryReportService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SummaryReportService.svc or SummaryReportService.svc.cs at the Solution Explorer and start debugging.
    public class SummaryReportService : ISummaryReportService
    {
        public SummaryMonitoringResult SearchSummaryMonitoring(JQueryDataTableParamModel param, DateTime? txtStartMessageDate, DateTime? txtEndMessageDate
            , string txtClientCode, string txtLocation, string ddlMessageGroup, string txtMessage, bool? ddlMessageStatus)
        {

            List<ReportSummaryMonitoring> lReportSummaryMonitorings = new List<ReportSummaryMonitoring>();
            SqlParameter[] parameters = new SqlParameter[18];

            try
            {
                using (var db = new MyDbContext())
                {
                    SqlParameter p1;
                    SqlParameter p2;
                    SqlParameter p3;
                    SqlParameter p4;
                    SqlParameter p5;
                    SqlParameter p6;
                    SqlParameter p7;
                    SqlParameter p8;
                    SqlParameter p9;
                    SqlParameter p10;
                    SqlParameter p11;
                    SqlParameter p12;
                    SqlParameter p13;

                    if (String.IsNullOrEmpty(txtClientCode))
                    {
                        p1 = new SqlParameter("ClientCode", DBNull.Value);
                    }
                    else
                    {
                        p1 = new SqlParameter("ClientCode", txtClientCode);
                    }

                    p2 = new SqlParameter("ClientIPAddress", DBNull.Value);

                    if (String.IsNullOrEmpty(txtLocation))
                    {
                        p3 = new SqlParameter("Location", DBNull.Value);
                    }
                    else
                    {
                        p3 = new SqlParameter("Location", txtLocation);
                    }

                    p4 = new SqlParameter("DeviceID", DBNull.Value);
                    p4.SqlDbType = SqlDbType.Int;


                    if (String.IsNullOrEmpty(ddlMessageGroup))
                    {
                        p5 = new SqlParameter("MessageGroupID", DBNull.Value);
                    }
                    else
                    {
                        p5 = new SqlParameter("MessageGroupID", ddlMessageGroup);
                    }

                    if (String.IsNullOrEmpty(txtMessage))
                    {
                        p6 = new SqlParameter("Message", DBNull.Value);
                    }
                    else
                    {
                        p6 = new SqlParameter("Message", txtMessage);
                    }

                    if (ddlMessageStatus == null)
                    {
                        p7 = new SqlParameter("Status", DBNull.Value);
                        p7.DbType = DbType.Int32;
                    }
                    else
                    {
                        p7 = new SqlParameter("Status", ddlMessageStatus);
                    }

                    if (txtStartMessageDate == null)
                    {
                        p8 = new SqlParameter("MessageDateTimeStart", DBNull.Value);
                        p8.DbType = DbType.DateTime;
                    }
                    else
                    {
                        p8 = new SqlParameter("MessageDateTimeStart", txtStartMessageDate);
                    }

                    if (txtEndMessageDate == null)
                    {
                        p9 = new SqlParameter("MessageDateTimeEnd", DBNull.Value);
                        p9.DbType = DbType.DateTime;
                    }
                    else
                    {
                        p9 = new SqlParameter("MessageDateTimeEnd", txtEndMessageDate);
                    }

                    p10 = new SqlParameter("EventDateTime", DBNull.Value);
                    p10.SqlDbType = SqlDbType.DateTime;

                    p11 = new SqlParameter("CreatedDate", DBNull.Value);
                    p11.SqlDbType = SqlDbType.DateTime;

                    p12 = new SqlParameter("UpdatedDate", DBNull.Value);
                    p12.SqlDbType = SqlDbType.DateTime;

                    p13 = new SqlParameter("IncludeOK", false);

                    SqlParameter p14 = new SqlParameter("PageNbr", DBNull.Value);
                    SqlParameter p15 = new SqlParameter("PageSize", param.iDisplayLength);
                    SqlParameter p16 = new SqlParameter("FirstRec", param.iDisplayStart);
                    SqlParameter p17 = new SqlParameter("SortCol", param.iSortColumn);
                    SqlParameter p18 = new SqlParameter("TotalRecords", SqlDbType.Int);
                    p18.Direction = ParameterDirection.Output;

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

                    db.Configuration.ProxyCreationEnabled = false;
                    //db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.Database.SqlQuery<ReportSummaryMonitoring>("RMS_Report_ListSummaryMonitoring " +
                                                                            "@ClientCode, @ClientIPAddress, @Location, @DeviceID" +
                                                                            ", @MessageGroupID, @Message, @Status" +
                                                                            ", @MessageDateTimeStart, @MessageDateTimeEnd" +
                                                                            ", @EventDateTime, @CreatedDate, @UpdatedDate, @IncludeOK" +
                                                                            ", @PageNbr, @PageSize, @FirstRec, @SortCol, @TotalRecords OUTPUT"
                        , parameters);

                    lReportSummaryMonitorings = new List<ReportSummaryMonitoring>(listOfType.ToList());

                    SummaryMonitoringResult sr = new SummaryMonitoringResult
                    {
                        IsSuccess = true,
                        ListSummaryMonitorings = lReportSummaryMonitorings,
                        TotalRecords = (int)parameters[17].Value
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
    }
}
