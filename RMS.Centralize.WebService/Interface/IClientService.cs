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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IClientService" in both code and config file together.
    [ServiceContract]
    public interface IClientService
    {
        [OperationContract]
        ClientResult GetClient(GetClientBy searchBy, int? clientID, string clientCode, string ipAddress, bool withDetail);

        [OperationContract]
        Result SetClientState(int clientID, ClientState state);
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
}
