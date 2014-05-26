using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Model;

namespace RMS.Centralize.WebService.BSL
{
    public class ClientService : BaseService
    {

        public List<ClientInfo> SearchClient(JQueryDataTableParamModel param, DateTime? asOfDate, int? clientTypeID, string clientCode, bool? clientStatus,
            string ipAddress, out int totalRecord)
        {
            List<ClientInfo> lClientInfos = new List<ClientInfo>();
            SqlParameter[] parameters = new SqlParameter[10];

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

                    if (asOfDate == null)
                    {
                        p6 = new SqlParameter("AsOfDate", DBNull.Value);
                        p6.DbType = DbType.DateTime;
                    }
                    else
                    {
                        p6 = new SqlParameter("AsOfDate", asOfDate);
                    }


                    if (clientTypeID == null)
                    {
                        p7 = new SqlParameter("ClientTypeID", DBNull.Value);
                        p7.SqlDbType = SqlDbType.Int;
                    }
                    else
                    {
                        p7 = new SqlParameter("ClientTypeID", clientTypeID);
                    }

                    if (String.IsNullOrEmpty(clientCode))
                    {
                        p8 = new SqlParameter("ClientCode", DBNull.Value);
                    }
                    else
                    {
                        p8 = new SqlParameter("ClientCode", clientCode);
                    }


                    if (clientStatus == null)
                    {
                        p9 = new SqlParameter("Enable", DBNull.Value);
                    }
                    else
                    {
                        p9 = new SqlParameter("Enable", clientStatus);
                    }

                    if (String.IsNullOrEmpty(ipAddress))
                    {
                        p10 = new SqlParameter("IPAddress", DBNull.Value);
                    }
                    else
                    {
                        p10 = new SqlParameter("IPAddress", ipAddress);
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

                    db.Configuration.ProxyCreationEnabled = false;
                    //db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.Database.SqlQuery<ClientInfo>("RMS_ListClientMonitoring " +
                                                                            "@AsOfDate, @ClientTypeID, @ClientCode, @Enable" +
                                                                            ", @IPAddress" +
                                                                            ", @PageNbr, @PageSize, @FirstRec, @SortCol, @TotalRecords OUTPUT"
                        , parameters);

                    lClientInfos = new List<ClientInfo>(listOfType.ToList());
                    totalRecord = (int)parameters[4].Value;

                    return lClientInfos;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("SearchClient errors. " + ex.Message, ex);
            }


        }
    }
}