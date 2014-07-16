using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Common.Exception;

namespace RMS.Agent.BSL.AutoUpate
{
    public class AutoUpdateService
    {

        public void Command(string type)
        {
            switch (type)
            {
                case "all":
                    UpdateAgentAll();
                    break;
                case "dll":
                    UpdateAgentDLLOnly();
                    break;
                case "others":
                    UpdateOthers();
                    break;
            }
        }


        private void UpdateAgentAll()
        {
            

        }

        private void UpdateAgentDLLOnly()
        {
            

        }

        private void UpdateOthers()
        {
            

        }

        public void UpdateResult(string clientCode, string appName, string currentVersion, string updateVersion, bool isComplete, string errorMessage)
        {
            try
            {
                var service = new RMS.Agent.Proxy.AutoUpdateService().autoUpdateService;
                service.AddAutoUpdateLogAsync(clientCode, appName, currentVersion, updateVersion, isComplete, errorMessage);

            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "AddAutoUpdateLog failed. " + ex.Message, ex, false);
            }

        }
    }

}
