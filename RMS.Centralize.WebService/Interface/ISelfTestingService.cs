using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISelfTestingService" in both code and config file together.
    [ServiceContract]
    public interface ISelfTestingService
    {
        [OperationContract]
        void TestConnection();

        [OperationContract]
        string TestWebConfig();

        [OperationContract]
        string TestDatabaseConnection();

        [OperationContract]
        string TestAgentConnection(string ipAddress);

        [OperationContract]
        string TestEmailSmsConnection(string email, string sms);

        [OperationContract]
        DataTable TestQuery(string sql);

    }
}
