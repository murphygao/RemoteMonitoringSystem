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
    public interface IMessageActionService
    {
        [OperationContract]
        void TestConnection();

        #region Page: MessageAction

        [OperationContract]
        InitMessageActionResult InitDataForMeesageAction();

        [OperationContract]
        MessageActionResult Search(JQueryDataTableParamModel param, int? messageGroupID, string message, bool? activeStatus);

        [OperationContract]
        Result Delete(int? id);

        #endregion


        #region Page : MessageEdit


        [OperationContract]
        InitMessageActionResult InitDataForMeesageEdit();

        [OperationContract]
        MessageActionResult Get(int? id);

        [OperationContract]
        Result UpdateMessage(int? id, string m, int? messageGroupID, string message, int? severityLevelID, bool activeList, bool activeStatus);

        #endregion


    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class MessageActionResult : Result
    {
        [DataMember]
        public List<RmsMessageAction> ListMessageActions { get; set; }

        [DataMember]
        public RmsMessage Message { get; set; }

        [DataMember]
        public int TotalRecords { get; set; }
    }


    [DataContract]
    public class InitMessageActionResult : Result
    {
        [DataMember]
        public string MessageGroup { get; set; }

        [DataMember]
        public string SeverityLevel { get; set; }

    }
}
