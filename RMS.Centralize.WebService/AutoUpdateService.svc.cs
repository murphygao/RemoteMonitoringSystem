using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RMS.Centralize.WebService.Model;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AutoUpdateService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AutoUpdateService.svc or AutoUpdateService.svc.cs at the Solution Explorer and start debugging.
    public class AutoUpdateService : IAutoUpdateService
    {
        public void TestConnection()
        {

        }

        public Result AddAutoUpdateLog(string clientCode, string appName, string currentVersion, string updateVersion, bool isComplete, string errorMessage)
        {
            try
            {
                var ipAdress = MonitoringService.GetIP();
                try
                {
                    BSL.ClientService cs = new BSL.ClientService();
                    var rmsClient = cs.GetClient(GetClientBy.IPAddress, null, null, ipAdress, true);
                    if (rmsClient != null)
                    {
                        clientCode = rmsClient.ClientCode;
                    }
                }
                catch
                {
                }

                if (string.IsNullOrEmpty(clientCode) && string.IsNullOrEmpty(ipAdress)) throw new ArgumentNullException("clientCode && ipAddress");
                if (string.IsNullOrEmpty(appName)) throw new ArgumentNullException("appName");

                BSL.AutoUpdateService service = new BSL.AutoUpdateService();
                var location = service.AddAutoUpdateLog(clientCode, ipAdress, appName, currentVersion, updateVersion, isComplete, errorMessage);

                var sr = new Result
                {
                    IsSuccess = true,
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "AddAutoUpdateLog failed. " + ex.Message, ex, true);

                var sr = new Result
                {
                    IsSuccess = false,
                    ErrorMessage = "AddAutoUpdateLog errors. " + ex.Message
                };
                return sr;
            }
        }
    }
}
