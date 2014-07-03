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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMasterTableService" in both code and config file together.
    [ServiceContract]
    public interface IMasterTableService
    {
        [OperationContract]
        void TestConnection();

        [OperationContract]
        MasterTableResult ListColorLabels();
    }

    public class MasterTableResult : Result
    {
        [DataMember]
        public List<RmsColorLabel> ListColorLabels { get; set; }

        [DataMember]
        public RmsColorLabel ColorLabel { get; set; }

    }

}
