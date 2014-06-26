using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RMS.Centralize.WebService.Model;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LocationService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select LocationService.svc or LocationService.svc.cs at the Solution Explorer and start debugging.
    public class LocationService : ILocationService
    {
        public void TestConnection()
        {

        }

        public LocationResult List()
        {
            try
            {
                BSL.LocationService ls = new BSL.LocationService();
                var listLocation = ls.List();

                var sr = new LocationResult
                {
                    IsSuccess = true,
                    ListLocations = listLocation,
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "List failed. " + ex.Message, ex, true);

                var sr = new LocationResult
                {
                    IsSuccess = false,
                    ErrorMessage = "List errors. " + ex.Message
                };
                return sr;
            }
        }

        public LocationResult Get(int locationID)
        {
            try
            {
                BSL.LocationService ls = new BSL.LocationService();
                var location = ls.Get(locationID);

                var sr = new LocationResult
                {
                    IsSuccess = true,
                    Location = location,
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "Get failed. " + ex.Message, ex, true);

                var sr = new LocationResult
                {
                    IsSuccess = false,
                    ErrorMessage = "Get errors. " + ex.Message
                };
                return sr;
            }
        }

        public LocationResult Search(JQueryDataTableParamModel param, string location, bool? activeList)
        {
            try
            {
                int totalRecord;

                BSL.LocationService ls = new BSL.LocationService();
                var listLocationInfos = ls.Search(param, location, activeList, out totalRecord);

                var sr = new LocationResult
                {
                    IsSuccess = true,
                    ListLocationInfos = listLocationInfos,
                    TotalRecords = totalRecord
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "Search failed. " + ex.Message, ex, true);

                var sr = new LocationResult
                {
                    IsSuccess = false,
                    ErrorMessage = "Search errors. " + ex.Message
                };
                return sr;
            }
        }

        public Result Update(int? id, string m, string locationCode, string locationName, bool mondayEnable, bool mondayWholeDay, DateTime? mondayStart,
            DateTime? mondayEnd, bool tuesdayEnable, bool tuesdayWholeDay, DateTime? tuesdayStart, DateTime? tuesdayEnd, bool wednesdayEnable,
            bool wednesdayWholeDay, DateTime? wednesdayStart, DateTime? wednesdayEnd, bool thursdayEnable, bool thursdayWholeDay, DateTime? thursdayStart,
            DateTime? thursdayEnd, bool fridayEnable, bool fridayWholeDay, DateTime? fridayStart, DateTime? fridayEnd, bool saturdayEnable,
            bool saturdayWholeDay, DateTime? saturdayStart, DateTime? saturdayEnd, bool sundayEnable, bool sundayWholeDay, DateTime? sundayStart,
            DateTime? sundayEnd, bool activeList, string updatedBy)
        {
            try
            {
                if (!(string.IsNullOrEmpty(m) || m == "e")) throw new ArgumentException("m parameter (" + m + ") is incorrect format.", "m");

                if (string.IsNullOrEmpty(m))
                {
                    BSL.LocationService ls = new BSL.LocationService();
                    var newLocation = ls.Add(locationCode, locationName
                        , mondayEnable, mondayWholeDay, mondayStart, mondayEnd
                        , tuesdayEnable, tuesdayWholeDay, tuesdayStart, tuesdayEnd
                        , wednesdayEnable, wednesdayWholeDay, wednesdayStart, wednesdayEnd
                        , thursdayEnable, thursdayWholeDay, thursdayStart, thursdayEnd
                        , fridayEnable, fridayWholeDay, fridayStart, fridayEnd
                        , saturdayEnable, saturdayWholeDay, saturdayStart, saturdayEnd
                        , sundayEnable, sundayWholeDay, sundayStart, sundayEnd
                        , activeList, updatedBy);

                    var sr = new Result
                    {
                        IsSuccess = true,
                    };
                    return sr;
                } else if (m == "e")
                {
                    if (id == null) throw new ArgumentNullException("LocationID");

                    BSL.LocationService ls = new BSL.LocationService();
                    var editLocation = ls.Update(id.Value, locationCode, locationName
                        , mondayEnable, mondayWholeDay, mondayStart, mondayEnd
                        , tuesdayEnable, tuesdayWholeDay, tuesdayStart, tuesdayEnd
                        , wednesdayEnable, wednesdayWholeDay, wednesdayStart, wednesdayEnd
                        , thursdayEnable, thursdayWholeDay, thursdayStart, thursdayEnd
                        , fridayEnable, fridayWholeDay, fridayStart, fridayEnd
                        , saturdayEnable, saturdayWholeDay, saturdayStart, saturdayEnd
                        , sundayEnable, sundayWholeDay, sundayStart, sundayEnd
                        , activeList, updatedBy);

                    var sr = new Result
                    {
                        IsSuccess = true,
                    };
                    return sr;
                }
                var wrongParam = new Result
                {
                    IsSuccess = false,
                    ErrorMessage = "Update errors. Please check m parameter (" + m + ")"
                };
                return wrongParam;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "Update failed. " + ex.Message, ex, true);

                var sr = new Result
                {
                    IsSuccess = false,
                    ErrorMessage = "Update errors. " + ex.Message
                };
                return sr;
            }
        }

        public Result Delete(int locationID, string updatedBy)
        {
            try
            {
                BSL.LocationService ls = new BSL.LocationService();
                var ret = ls.Delete(locationID, updatedBy);

                var sr = new Result
                {
                    IsSuccess = ret
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "Delete failed. " + ex.Message, ex, true);

                var sr = new Result
                {
                    IsSuccess = false,
                    ErrorMessage = "Delete errors. " + ex.Message
                };
                return sr;
            }
        }
    }
}
