﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Model;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IClientService" in both code and config file together.
    [ServiceContract]
    public interface IClientService
    {
        [OperationContract]
        void TestConnection();

        [OperationContract]
        ClientResult GetClient(GetClientBy searchBy, int? clientID, string clientCode, string ipAddress, bool withDetail, bool activeClient);

        [OperationContract]
        Result SetClientState(int clientID, ClientState state);

        [OperationContract]
        ClientInfoResult Search(JQueryDataTableParamModel param, DateTime? asOfDate, int? clientTypeID, string clientCode, bool? clientStatus, string ipAddress, string location);


        [OperationContract]
        Result Update(int? id, string m, string clientCode, int? clientTypeID, bool? useLocalInfo, int? referenceClientID, string ipAddress, int? locationID, bool? hasMonitoringAgent, bool? activeList, bool? status, DateTime? effectiveDate, DateTime? expiredDate, int? state, string updatedBy);

        [OperationContract]
        Result Delete(int id, string updatedBy);

        [OperationContract]
        ClientResult ExistingClientCode(string clientCode);

        [OperationContract]
        LocationResult ListLocation();

        [OperationContract]
        ClientSeverityActionResult ListClientSeverityActions(int clientID);

        [OperationContract]
        ClientSeverityActionResult GetClientSeverityAction(int clientID, int severityLevelID);

        [OperationContract]
        ClientSeverityActionResult UpdateClientSeverityAction(int clientID, int severityLevelID, bool overwritenAction, string email, string sms,
            int? commandLineID, string updatedBy);






    }


    [DataContract]
    public enum GetClientBy
    {
        [EnumMember]
        ClientID,

        [EnumMember]
        ClientCode,

        [EnumMember]
        IPAddress

    }

    [DataContract]
    public enum ClientState
    {
        [EnumMember]
        Normal = 1,

        [EnumMember]
        Maintenance = 2

    }

    [DataContract]
    public class ClientResult : Result
    {

        [DataMember]
        public List<RmsClient> ListClients { get; set; }

        [DataMember]
        public RmsClient Client { get; set; }

        [DataMember]
        public int TotalRecords { get; set; }


        #region Detail

        [DataMember]
        public RmsMonitoringProfile MonitoringProfile { get; set; }
        
        [DataMember]
        public List<RmsMonitoringProfileDevice> ListMonitoringProfileDevices { get; set; }

        [DataMember]
        public List<RmsDevice> ListDevices { get; set; }

        [DataMember]
        public List<RmsDeviceType> ListDeviceType { get; set; }

        #endregion


    }

    public partial class ClientInfoResult : Result
    {

        [DataMember]
        public List<ClientInfo> ListClients { get; set; }

        [DataMember]
        public int TotalRecords { get; set; }

    }

    public class ClientSeverityActionResult : Result
    {
        [DataMember]
        public int TotalRecords { get; set; }

        [DataMember]
        public List<ClientSeverityActionInfo> ListClientSeverityActionInfos { get; set; }

        [DataMember]
        public ClientSeverityActionInfo Info { get; set; }

        [DataMember]
        public RmsClientSeverityAction ClientSeverityAction { get; set; }
        
    }

    
}
