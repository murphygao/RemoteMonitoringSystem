using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.Centralize.WebService.Model
{
    public class ClientMessageAction
    {
        /*
        c.[ClientID]
        ,c.[ClientTypeID]
        ,c.[ClientCode]
        ,csa.[SeverityLevelID]
        ,csa.[OverwritenAction]
        ,csa.[Email] AS Email2
        ,csa.[SMS] AS SMS2
        ,csa.[CommandLindID] AS CommandlindID2
        ,sl.[LevelCode]
        ,ap.[ActionProfileID]
        ,ap.[Email]
        ,ap.[SMS]
        ,ap.[CommandLineID]
        ,m.[MessageID]
        ,m.[Message]
        ,m.[MessageGroupID]
       
         */

        public int ClientID { get; set; } 
        public int? ClientTypeID { get; set; }
        public string ClientCode { get; set; } 
        public int? SeverityLevelID { get; set; }
        public bool? OverwritenAction { get; set; } 
        public string Email2 { get; set; } 
        public string SMS2 { get; set; } 
        public int? CommandlindID2 { get; set; }
        public string LevelCode { get; set; }
        public int? ActionProfileID { get; set; }
        public string Email { get; set; }
        public string SMS { get; set; }
        public int? CommandlindID { get; set; }
        public int? MessageID { get; set; }
        public string Message { get; set; }
        public int? MessageGroupID { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string MessageGroupName { get; set; }

    }
}