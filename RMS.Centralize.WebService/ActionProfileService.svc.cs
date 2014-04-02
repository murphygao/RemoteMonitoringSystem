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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ActionProfileService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ActionProfileService.svc or ActionProfileService.svc.cs at the Solution Explorer and start debugging.
    public class ActionProfileService : IActionProfileService
    {

        public SearchResult Search(JQueryDataTableParamModel param, string txtActionProfile, string txtEmail, string txtSms)
        {

            List<RmsActionProfile> listRmsActionProfile = new List<RmsActionProfile>();
            SqlParameter[] parameters = new SqlParameter[8];

            try
            {
                using (var db = new MyDbContext())
                {
                    SqlParameter p1;
                    SqlParameter p2;
                    SqlParameter p3;

                    if (String.IsNullOrEmpty(txtActionProfile))
                    {
                        p1 = new SqlParameter("ActionProfileName", DBNull.Value);
                    }
                    else
                    {
                        p1 = new SqlParameter("ActionProfileName", txtActionProfile);
                    }

                    if (String.IsNullOrEmpty(txtEmail))
                    {
                        p2 = new SqlParameter("Email", DBNull.Value);
                    }
                    else
                    {
                        p2 = new SqlParameter("Email", txtEmail);
                    }

                    if (String.IsNullOrEmpty(txtSms))
                    {
                        p3 = new SqlParameter("Sms", DBNull.Value);
                    }
                    else
                    {
                        p3 = new SqlParameter("Sms", txtSms);
                    }
                    SqlParameter p4 = new SqlParameter("PageNbr", DBNull.Value);
                    SqlParameter p5 = new SqlParameter("PageSize", param.iDisplayLength);
                    SqlParameter p6 = new SqlParameter("FirstRec", param.iDisplayStart);
                    SqlParameter p7 = new SqlParameter("SortCol", param.iSortColumn);
                    SqlParameter p8 = new SqlParameter("TotalRecords", SqlDbType.Int);
                    p8.Direction = ParameterDirection.Output;

                    parameters[0] = p1;
                    parameters[1] = p2;
                    parameters[2] = p3;
                    parameters[3] = p4;
                    parameters[4] = p5;
                    parameters[5] = p6;
                    parameters[6] = p7;
                    parameters[7] = p8;

                    db.Configuration.ProxyCreationEnabled = false;
                    //db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.Database.SqlQuery<RmsActionProfile>("RMS_ListActionProfile " +
                                                                            "@ActionProfileName, @Email, @Sms, @PageNbr, @PageSize, @FirstRec, @SortCol, @TotalRecords OUTPUT"
                        , parameters);

                    listRmsActionProfile = new List<RmsActionProfile>(listOfType.ToList());

                    SearchResult sr = new SearchResult
                    {
                        ListActionProfile = listRmsActionProfile, 
                        TotalRecords = (int) parameters[7].Value
                    };
                    return sr;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void Delete()
        {
        }

        public void Get()
        {
        }

        public void Update()
        {
        }
    }
}
