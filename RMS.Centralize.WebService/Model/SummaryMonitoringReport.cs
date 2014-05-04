using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.Centralize.WebService.Model
{
    public class ReportSummaryMonitoring : DAL.RmsReportSummaryMonitoring
    {
        public string Location { get; set; }
        public string LevelName { get; set; }
        public string ColorCode { get; set; }
        public string ColorTagStart { get; set; }
        public string ColorTagEnd { get; set; }

        public long? RowNum { get; set; } // RowNum

    }
}