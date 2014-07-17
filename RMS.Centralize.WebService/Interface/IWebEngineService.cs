using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWebEngineService" in both code and config file together.
    [ServiceContract]
    public interface IWebEngineService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/Test/")]
        void TestConnection();

        [OperationContract]
        [WebGet(UriTemplate = "/StartMonitoringEngine/")]
        string StartMonitoringEngine();

        [OperationContract]
        [WebGet(UriTemplate = "/StartSummaryStatusAllClients/")]
        string StartSummaryStatusAllClients();


    }
}
