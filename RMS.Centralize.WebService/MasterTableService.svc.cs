using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RMS.Centralize.DAL;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MasterTableService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MasterTableService.svc or MasterTableService.svc.cs at the Solution Explorer and start debugging.
    public class MasterTableService : IMasterTableService
    {
        public void TestConnection()
        {

        }

        public MasterTableResult ListColorLabels()
        {
            try
            {
                BSL.MasterTableService service = new BSL.MasterTableService();
                var lists = service.ListColorLabels();

                var sr = new MasterTableResult
                {
                    IsSuccess = true,
                    ListColorLabels = lists,
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "List failed. " + ex.Message, ex, true);

                var sr = new MasterTableResult
                {
                    IsSuccess = false,
                    ErrorMessage = "List errors. " + ex.Message
                };
                return sr;
            }
        }
    }
}
