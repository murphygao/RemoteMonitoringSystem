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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IActionProfileService" in both code and config file together.
    [ServiceContract]
    public interface IActionProfileService
    {
        [OperationContract]
        void TestConnection();

        [OperationContract]
        ActionProfileResult List(bool? activeList);

        [OperationContract]
        ActionProfileResult Search(JQueryDataTableParamModel param, string txtActionProfile, string txtEmail, string txtSms);

        [OperationContract]
        Result Delete(int? actionProfileID);

        [OperationContract]
        ActionProfileResult Get(int? id);

        [OperationContract]
        Result Update(int? id, string m, string ActionProfileName, string Email, string SMS, bool ActiveList);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class ActionProfileResult : Result
    {

        [DataMember]
        public List<RmsActionProfile> ListActionProfiles { get; set; }

        [DataMember]
        public RmsActionProfile ActionProfile { get; set; }

        [DataMember]
        public int TotalRecords { get; set; }

    }

}
