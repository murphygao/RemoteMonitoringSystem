using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Model;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILocationService" in both code and config file together.
    [ServiceContract]
    public interface ILocationService
    {
        [OperationContract]
        void TestConnection();

        [OperationContract]
        LocationResult List();

        [OperationContract]
        LocationResult Get(int locationID);

        [OperationContract]
        LocationResult Search(JQueryDataTableParamModel param, string location, bool? activeList);

        [OperationContract]
        Result Update(int? id, string m, string locationCode, string locationName
            , bool mondayEnable, bool mondayWholeDay, DateTime? mondayStart, DateTime? mondayEnd
            , bool tuesdayEnable, bool tuesdayWholeDay, DateTime? tuesdayStart, DateTime? tuesdayEnd
            , bool wednesdayEnable, bool wednesdayWholeDay, DateTime? wednesdayStart, DateTime? wednesdayEnd
            , bool thursdayEnable, bool thursdayWholeDay, DateTime? thursdayStart, DateTime? thursdayEnd
            , bool fridayEnable, bool fridayWholeDay, DateTime? fridayStart, DateTime? fridayEnd
            , bool saturdayEnable, bool saturdayWholeDay, DateTime? saturdayStart, DateTime? saturdayEnd
            , bool sundayEnable, bool sundayWholeDay, DateTime? sundayStart, DateTime? sundayEnd
            , bool activeList, string updatedBy);

        [OperationContract]
        Result Delete(int locationID, string updatedBy);

    }


    public class LocationResult : Result
    {
        [DataMember]
        public List<RmsLocation> ListLocations { get; set; }

        [DataMember]
        public int TotalRecords { get; set; }

        [DataMember]
        public RmsLocation Location { get; set; }

        [DataMember]
        public List<LocationInfo> ListLocationInfos { get; set; }

    }

}
