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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMonitoringProfileService" in both code and config file together.
    [ServiceContract]
    public interface IMonitoringProfileService
    {
        [OperationContract]
        void TestConnection();

        [OperationContract]
        MonitoringProfileResult List(bool? activeList);

        [OperationContract]
        MonitoringProfileResult Get(int monitoringProfileID);

        [OperationContract]
        MonitoringProfileResult Search(JQueryDataTableParamModel param, string name, bool? activeList);

        [OperationContract]
        Result Update(int? id, string m, string profileName, bool activeList, string updatedBy);

        [OperationContract]
        Result Delete(int monitoringProfileID, string updatedBy);

    }


    public class MonitoringProfileResult : Result
    {
        [DataMember]
        public List<RmsMonitoringProfile> ListMonitoringProfiles { get; set; }

        [DataMember]
        public int TotalRecords { get; set; }

        [DataMember]
        public RmsMonitoringProfile MonitoringProfile { get; set; }

        public List<MonitoringProfileInfo> ListMonitoringProfileInfo { get; set; }

        

    }    
}
