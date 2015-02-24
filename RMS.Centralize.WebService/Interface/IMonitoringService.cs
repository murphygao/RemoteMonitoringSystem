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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMonitoringService" in both code and config file together.
    [ServiceContract]
    public interface IMonitoringService
    {
        [OperationContract]
        void TestConnection();

        [OperationContract]
        void AddMessage(RmsReportMonitoringRaw rawMessage);

        [OperationContract]
        void AddMessages(List<RmsReportMonitoringRaw> lRawMessages);

        [OperationContract]
        void AddBusinessMessage(RmsReportMonitoringRaw rawMessage);

        [OperationContract]
        void AddMessageWithAttachFiles(RmsReportMonitoringRaw rawMessage, List<RMSAttachment> lAttachments);

        [OperationContract]
        void AddMessagesWithAttachFiles(List<RmsReportMonitoringRaw> lRawMessages, List<RMSAttachment> lAttachments);

        [OperationContract]
        void AddBusinessMessageWithAttachFiles(RmsReportMonitoringRaw rawMessage, List<RMSAttachment> lAttachments);

        [OperationContract]
        void AddWebsiteMessage(List<RmsReportMonitoringRaw> lRawMessages);

        [OperationContract]
        void StartMonitoringEngine();

        [OperationContract]
        void StartWebsiteMonitoringEngine();

    }
}
