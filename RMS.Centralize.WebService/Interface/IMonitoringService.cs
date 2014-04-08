using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RMS.Centralize.DAL;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMonitoringService" in both code and config file together.
    [ServiceContract]
    public interface IMonitoringService
    {
        [OperationContract]
        void AddMessage(RmsReportMonitoringRaw rawMessage);

        [OperationContract]
        void AddMessages(List<RmsReportMonitoringRaw> lRawMessages);
    }
}
