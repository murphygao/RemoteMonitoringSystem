using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RMS.Centralize.WebService.Model
{
    [DataContract]
    public class JQueryDataTableParamModel
    {

        /// <summary>
        /// Request sequence number sent by DataTable, same value must be returned in response
        /// </summary>       
        [DataMember]
        public string sEcho { get; set; }

        /// <summary>
        /// Text used for filtering
        /// </summary>
        [DataMember]
        public string sSearch { get; set; }

        /// <summary>
        /// Number of records that should be shown in table
        /// </summary>
        [DataMember]
        public int iDisplayLength { get; set; }

        /// <summary>
        /// First record that should be shown(used for paging)
        /// </summary>
        [DataMember]
        public int iDisplayStart { get; set; }

        /// <summary>
        /// Number of columns in table
        /// </summary>
        [DataMember]
        public int iColumns { get; set; }

        /// <summary>
        /// Number of columns that are used in sorting
        /// </summary>
        [DataMember]
        public int iSortingCols { get; set; }

        /// <summary>
        /// Comma separated list of column names
        /// </summary>
        [DataMember]
        public string sColumns { get; set; }


        [DataMember]
        public string iSortColumn { get; set; }

    }
}