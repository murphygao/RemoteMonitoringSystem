using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.BSL;
using RMS.Centralize.WebService.Model;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISummaryReportService" in both code and config file together.
    [ServiceContract]
    public interface ISummaryReportService
    {
        [OperationContract]
        SummaryMonitoringResult SearchSummaryMonitoring(JQueryDataTableParamModel param, DateTime? txtStartEventDate, DateTime? txtEndEventDate
            , string txtClientCode, string txtLocation, string ddlMessageGroup, string txtMessage, bool? ddlMessageStatus, int? clientID);


        [OperationContract]
        ClientInfoResult GetClientInfo(int clientID);

        [OperationContract]
        Result ActionRequest(ActionService.ActionSendType actionSendType, long reportID);


    }
    
    [DataContract]
    public class SummaryMonitoringResult : Result
    {
        [DataMember]
        public List<ReportSummaryMonitoring> ListSummaryMonitorings { get; set; }

        [DataMember]
        public ReportSummaryMonitoring SummaryMonitoring { get; set; }

        [DataMember]
        public int TotalRecords { get; set; }
    }


    [DataContract]
    public class ClientInfoResult : Result
    {

        [DataMember]
        public ClientInfo Client { get; set; }

    }


}
