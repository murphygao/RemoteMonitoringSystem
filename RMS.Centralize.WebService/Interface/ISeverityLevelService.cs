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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISeverityLevelService" in both code and config file together.
    [ServiceContract]
    public interface ISeverityLevelService
    {
        [OperationContract]
        void TestConnection();

        [OperationContract]
        SeverityLevelResult Search(JQueryDataTableParamModel param, string severityLevel, bool? activeList);
        
        [OperationContract]
        SeverityLevelResult List(bool? activeList);

        [OperationContract]
        SeverityLevelResult Get(int severityLevelID);

        [OperationContract]
        Result Update(int? id, string m, string levelCode, string levelName, int orderList, bool activeList, int defaultActionProfileID
            , string colorCode, bool actionRepeatable, int actionRepeatInterval, string updatedBy);

        [OperationContract]
        Result Delete(int id, string updatedBy);
    }

    public class SeverityLevelResult : Result
    {
        [DataMember]
        public List<RmsSeverityLevel> ListSeverityLevels { get; set; }

        [DataMember]
        public int TotalRecords { get; set; }

        [DataMember]
        public RmsSeverityLevel SeverityLevel { get; set; }

        [DataMember]
        public List<SeverityLevelInfo> ListSeverityLevelInfos { get; set; }

    }

}
