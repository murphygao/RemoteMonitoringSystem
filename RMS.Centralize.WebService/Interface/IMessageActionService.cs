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
        #region Page: MessageAction

        [OperationContract]
        InitialResult InitDataForMeesageAction();

        [OperationContract]
        MessageActionResult Search(JQueryDataTableParamModel param, int? messageGroupID, string message, bool activeStatus);

        [OperationContract]
        Result Delete(int? actionProfileID);

        #endregion


        #region Page : MessageEdit


        [OperationContract]
        InitialResult InitDataForMeesageEdit();

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
        public RmsMessageAction MessageAction { get; set; }

        [DataMember]
        public int TotalRecords { get; set; }
    }


    [DataContract]
    public class InitialResult : Result
    {
        [DataMember]
        public string MessageGroup { get; set; }

        [DataMember]
        public string SeverityLevel { get; set; }

    }
}
