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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWebsiteMonitoringService" in both code and config file together.
    [ServiceContract]
    public interface IWebsiteMonitoringService
    {

        [OperationContract]
        void TestConnection();

        [OperationContract]
        WebsiteMonitoringResult ListWebsiteMonitoringsByClient(JQueryDataTableParamModel param, int clientID);

        [OperationContract]
        WebsiteMonitoringResult GetWebsiteMonitoring(int websiteMonitoringID);

        [OperationContract]
        Result UpdateWebsiteMonitoring(int? id, string m, int clientID, string websiteMonitoringTypeID, string websiteMonitoringProtocolID
            , int? portNumber, string domainName, bool enable, string updatedBy);

        [OperationContract]
        Result DeleteWebsiteMonitoring(int websiteMonitoringID, string updatedBy);


    }

    public class WebsiteMonitoringResult : Result
    {
        [DataMember]
        public int TotalRecords { get; set; }

        #region WebsiteMonitoring

        [DataMember]
        public List<WebsiteMonitoringInfo> ListWebsiteMonitoringInfos { get; set; }

        [DataMember]
        public WebsiteMonitoringInfo WebsiteMonitoringInfo { get; set; }

        #endregion
    }

}
