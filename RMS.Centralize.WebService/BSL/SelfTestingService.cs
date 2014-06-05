using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RMS.Centralize.WebService.BSL
{
    public class SelfTestingService
    {
        public DataTable TestSQL(string sql)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDbContext"].ConnectionString;
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("result");
                da.Fill(dt);

                return dt;

            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable("result");
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(new object[] { ex.Message });
                return dt;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

        }
    }
}