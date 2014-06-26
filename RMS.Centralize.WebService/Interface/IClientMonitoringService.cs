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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IClientMonitoringService" in both code and config file together.
    [ServiceContract]
    public interface IClientMonitoringService
    {
        [OperationContract]
        void TestConnection();

        [OperationContract]
        ClientMonitoringResult ListByClient(int clientID);

        [OperationContract]
        ClientMonitoringResult Get(int clientID, int monitoringProfileID);

        [OperationContract]
        Result Update(int clientID, int monitoringProfileID, string m, DateTime effectiveDate, string updatedBy);

        [OperationContract]
        Result Delete(int clientID, int monitoringProfileID, string updatedBy);
    }


    public class ClientMonitoringResult : Result
    {
        [DataMember]
        public List<RmsClientMonitoring> ListMonitoringProfiles { get; set; }

        [DataMember]
        public List<ClientMonitoringInfo> ListMonitoringProfileInfos { get; set; }

        [DataMember]
        public int TotalRecords { get; set; }

        [DataMember]
        public RmsClientMonitoring MonitoringProfile { get; set; }

        [DataMember]
        public ClientMonitoringInfo MonitoringProfileInfo { get; set; }

    } 
}
