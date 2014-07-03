using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMS.Centralize.DAL;

namespace RMS.Centralize.WebService.Model
{
    public class SeverityLevelInfo : RmsSeverityLevel
    {

        /*
         * 
ROWNUM,
sl.[SeverityLevelID]
,sl.[LevelCode]
,sl.[LevelName]
,sl.[OrderList]
,sl.[ActiveList]
,sl.[DefaultActionProfileID]
,sl.[ColorCode]
,sl.[ActionRepeatable]
,sl.[ActionRepeatInterval]
,sl.[CreatedDate]
,sl.[CreatedBy]
,sl.[UpdatedDate]
,sl.[UpdatedBy]
,ap.[ActionProfileName]
,cl.[ColorName]
,cl.[ColorTagStart]
,cl.[ColorTagEnd]          
         * 
         */

        public string ActionProfileName { get; set; }
        public string ColorName { get; set; }
        public string ColorTagStart { get; set; }
        public string ColorTagEnd { get; set; }
        public long? RowNum { get; set; } // RowNum

    }
}