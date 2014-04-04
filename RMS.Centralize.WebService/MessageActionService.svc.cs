using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RMS.Centralize.WebService.Model;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MessageActionService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MessageActionService.svc or MessageActionService.svc.cs at the Solution Explorer and start debugging.
    public class MessageActionService : IMessageActionService
    {

        #region Page: MessageAction

        public InitialResult InitDataForMeesageAction()
        {
            throw new NotImplementedException();
        }

        public MessageActionResult Search(JQueryDataTableParamModel param, int? messageGroupID, string message, bool activeStatus)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int? actionProfileID)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Page : MessageEdit

        public InitialResult InitDataForMeesageEdit()
        {
            throw new NotImplementedException();
        }

        public MessageActionResult Get(int? id)
        {
            throw new NotImplementedException();
        }

        public Result UpdateMessage(int? id, string m, int? messageGroupID, string message, int? severityLevelID, bool activeList, bool activeStatus)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
