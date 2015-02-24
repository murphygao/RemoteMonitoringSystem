using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.BSL;
using RMS.Centralize.WebService.Model;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService.Email
{
    public partial class SummaryStatusAllClients : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
                            info.iRMSStatus = 1;
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
                            info.iRMSStatus = 2;
                        }

                    }

                    // ถ้าไม่เจอ ให้จบการทำงาน

                    Repeater1.DataSource = lstatusList;
                    Repeater1.DataBind();
                }

            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Page_Load failed. " + ex.Message, ex, true);
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