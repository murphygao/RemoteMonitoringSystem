using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Model;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService.BSL
{
    public class LocationService
    {

        public List<RmsLocation> List()
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.RmsLocations;

                    List<RmsLocation> listLocations = new List<RmsLocation>(listOfType.ToList());

                    return listLocations;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "ListLocation failed. " + ex.Message, ex, false);
            }
        }

        public RmsLocation Get(int locationID)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var location = db.RmsLocations.Find(locationID);

                    if (location == null) throw new Exception("Location (" + locationID + ") Not Found.");

                    return location;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "GetLocation failed. " + ex.Message, ex, false);
            }
        }

        public List<LocationInfo> Search(JQueryDataTableParamModel param, string location, bool? activeList, out int totalRecord)
        {
            List<LocationInfo> lLocations = new List<LocationInfo>();
            SqlParameter[] parameters = new SqlParameter[7];

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

                    if (String.IsNullOrEmpty(location))
                    {
                        p6 = new SqlParameter("Location", DBNull.Value);
                    }
                    else
                    {
                        p6 = new SqlParameter("Location", location);
                    }

                    if (activeList == null)
                    {
                        p7 = new SqlParameter("ActiveList", DBNull.Value);
                    }
                    else
                    {
                        p7 = new SqlParameter("ActiveList", activeList);
                    }

                    parameters[0] = p1;
                    parameters[1] = p2;
                    parameters[2] = p3;
                    parameters[3] = p4;
                    parameters[4] = p5;
                    parameters[5] = p6;
                    parameters[6] = p7;

                    db.Configuration.ProxyCreationEnabled = false;
                    //db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.Database.SqlQuery<LocationInfo>("RMS_SearchLocation " +
                                                                            "@Location, @ActiveList" +
                                                                            ", @PageNbr, @PageSize, @FirstRec, @SortCol, @TotalRecords OUTPUT"
                        , parameters);

                    lLocations = new List<LocationInfo>(listOfType.ToList());
                    totalRecord = (int)parameters[4].Value;

                    return lLocations;
                }

            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Search failed. " + ex.Message, ex, false);
            }
        }

        public bool Delete(int locaitonID, string updatedBy)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    if (db.RmsClients.Any(c => c.UseLocalInfo == true && c.LocationId == locaitonID)) 
                        throw new Exception("This location is already used. Cannot delete it.");

                    var location = db.RmsLocations.Create();
                    location.LocationId = locaitonID;
                    db.RmsLocations.Attach(location);
                    db.RmsLocations.Remove(location);
                    db.SaveChanges();
                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Delete failed. " + ex.Message, ex, true);

            }

        }

        public RmsLocation Update(int id, string locationCode, string locationName, bool mondayEnable, bool mondayWholeDay, DateTime? mondayStart,
            DateTime? mondayEnd, bool tuesdayEnable, bool tuesdayWholeDay, DateTime? tuesdayStart, DateTime? tuesdayEnd, bool wednesdayEnable,
            bool wednesdayWholeDay, DateTime? wednesdayStart, DateTime? wednesdayEnd, bool thursdayEnable, bool thursdayWholeDay, DateTime? thursdayStart,
            DateTime? thursdayEnd, bool fridayEnable, bool fridayWholeDay, DateTime? fridayStart, DateTime? fridayEnd, bool saturdayEnable,
            bool saturdayWholeDay, DateTime? saturdayStart, DateTime? saturdayEnd, bool sundayEnable, bool sundayWholeDay, DateTime? sundayStart,
            DateTime? sundayEnd, bool activeList, string updatedBy)
        {

            try
            {
                using (var db = new MyDbContext())
                {
                    var location = db.RmsLocations.Find(id);
                    if (location == null) throw new Exception("Location (" + id + ") Not Found");

                    location.LocationCode = locationCode;
                    location.LocationName = locationName;

                    location.MondayEnable = mondayEnable;
                    location.MondayWholeDay = mondayWholeDay;
                    location.MondayStart = mondayStart;
                    location.MondayEnd = mondayEnd;

                    location.TuesdayEnable = tuesdayEnable;
                    location.TuesdayWholeDay = tuesdayWholeDay;
                    location.TuesdayStart = tuesdayStart;
                    location.TuesdayEnd = tuesdayEnd;

                    location.WednesdayEnable = wednesdayEnable;
                    location.WednesdayWholeDay = wednesdayWholeDay;
                    location.WednesdayStart = wednesdayStart;
                    location.WednesdayEnd = wednesdayEnd;

                    location.ThursdayEnable = thursdayEnable;
                    location.ThursdayWholeDay = thursdayWholeDay;
                    location.ThursdayStart = thursdayStart;
                    location.ThursdayEnd = thursdayEnd;

                    location.FridayEnable = fridayEnable;
                    location.FridayWholeDay = fridayWholeDay;
                    location.FridayStart = fridayStart;
                    location.FridayEnd = fridayEnd;

                    location.SaturdayEnable = saturdayEnable;
                    location.SaturdayWholeDay = saturdayWholeDay;
                    location.SaturdayStart = saturdayStart;
                    location.SaturdayEnd = saturdayEnd;

                    location.SundayEnable = sundayEnable;
                    location.SundayWholeDay = sundayWholeDay;
                    location.SundayStart = sundayStart;
                    location.SundayEnd = sundayEnd;

                    location.ActiveList = activeList;
                    location.UpdatedBy = updatedBy;

                    db.SaveChanges();

                    return location;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Update failed. " + ex.Message, ex, true);
            }

        }

        public RmsLocation Add(string locationCode, string locationName, bool mondayEnable, bool mondayWholeDay, DateTime? mondayStart,
            DateTime? mondayEnd, bool tuesdayEnable, bool tuesdayWholeDay, DateTime? tuesdayStart, DateTime? tuesdayEnd, bool wednesdayEnable,
            bool wednesdayWholeDay, DateTime? wednesdayStart, DateTime? wednesdayEnd, bool thursdayEnable, bool thursdayWholeDay, DateTime? thursdayStart,
            DateTime? thursdayEnd, bool fridayEnable, bool fridayWholeDay, DateTime? fridayStart, DateTime? fridayEnd, bool saturdayEnable,
            bool saturdayWholeDay, DateTime? saturdayStart, DateTime? saturdayEnd, bool sundayEnable, bool sundayWholeDay, DateTime? sundayStart,
            DateTime? sundayEnd, bool activeList, string updatedBy)
        {

            try
            {
                using (var db = new MyDbContext())
                {
                    var location = db.RmsLocations.Create();

                    location.LocationCode = locationCode;
                    location.LocationName = locationName;

                    location.MondayEnable = mondayEnable;
                    location.MondayWholeDay = mondayWholeDay;
                    location.MondayStart = mondayStart;
                    location.MondayEnd = mondayEnd;

                    location.TuesdayEnable = tuesdayEnable;
                    location.TuesdayWholeDay = tuesdayWholeDay;
                    location.TuesdayStart = tuesdayStart;
                    location.TuesdayEnd = tuesdayEnd;

                    location.WednesdayEnable = wednesdayEnable;
                    location.WednesdayWholeDay = wednesdayWholeDay;
                    location.WednesdayStart = wednesdayStart;
                    location.WednesdayEnd = wednesdayEnd;

                    location.ThursdayEnable = thursdayEnable;
                    location.ThursdayWholeDay = thursdayWholeDay;
                    location.ThursdayStart = thursdayStart;
                    location.ThursdayEnd = thursdayEnd;

                    location.FridayEnable = fridayEnable;
                    location.FridayWholeDay = fridayWholeDay;
                    location.FridayStart = fridayStart;
                    location.FridayEnd = fridayEnd;

                    location.SaturdayEnable = saturdayEnable;
                    location.SaturdayWholeDay = saturdayWholeDay;
                    location.SaturdayStart = saturdayStart;
                    location.SaturdayEnd = saturdayEnd;

                    location.SundayEnable = sundayEnable;
                    location.SundayWholeDay = sundayWholeDay;
                    location.SundayStart = sundayStart;
                    location.SundayEnd = sundayEnd;

                    location.ActiveList = activeList;
                    location.CreatedBy = updatedBy;
                    location.UpdatedBy = updatedBy;

                    db.RmsLocations.Add(location);
                    db.SaveChanges();

                    return location;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Add failed. " + ex.Message, ex, true);
            }

        }

    
    }
}