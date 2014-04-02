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
        SearchResult Search(JQueryDataTableParamModel param, string txtActionProfile, string txtEmail, string txtSms);

        [OperationContract]
        void Delete();

        [OperationContract]
        void Get();

        [OperationContract]
        void Update();
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class SearchResult
    {

        [DataMember]
        public List<RmsActionProfile> ListActionProfile { get; set; }

        [DataMember]
        public int TotalRecords { get; set; }

    }

}
