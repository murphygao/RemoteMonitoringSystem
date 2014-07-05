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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMasterTableService" in both code and config file together.
    [ServiceContract]
    public interface IMasterTableService
    {
        [OperationContract]
        void TestConnection();

        #region Color Label

        [OperationContract]
        MasterTableResult ListColorLabels();

        #endregion

        #region Message Master

        [OperationContract]
        MasterTableResult SearchMessageMaster(JQueryDataTableParamModel param, string message);

        [OperationContract]
        MasterTableResult ListMessageMaster();

        [OperationContract]
        MasterTableResult GetMessageMaster(string message);

        [OperationContract]
        Result UpdateMessageMaster(string id, string m, string message, string description, string emailBody, string smsBody, string emailBodySolved,
            string smsBodySolved, string updatedBy);

        [OperationContract]
        Result DeleteMessageMaster(string message, string updatedBy);

        #endregion

        #region System Config

        [OperationContract]
        MasterTableResult ListSystemConfig();

        [OperationContract]
        MasterTableResult GetSystemConfig(string name);

        [OperationContract]
        Result UpdateSystemConfig(string name, string value, string defaultValue, string description, string updatedBy);

        #endregion

    }

    public class MasterTableResult : Result
    {
        [DataMember]
        public int TotalRecords { get; set; }

        #region Color Label

        [DataMember]
        public List<RmsColorLabel> ListColorLabels { get; set; }

        [DataMember]
        public RmsColorLabel ColorLabel { get; set; }

        #endregion


        #region Message Master

        [DataMember]
        public List<MessageMasterInfo> ListMessageMasterInfos { get; set; }

        [DataMember]
        public List<RmsMessageMaster> ListMessageMasters { get; set; }

        [DataMember]
        public RmsMessageMaster MessageMaster { get; set; }

        #endregion

        #region System Config

        [DataMember]
        public List<RmsSystemConfig> ListSystemConfigs { get; set; }

        [DataMember]
        public RmsSystemConfig SystemConfig { get; set; }

        #endregion


    }

}
