using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMS.Centralize.DAL;

namespace RMS.Centralize.WebService.Model
{
    public class ClientSeverityActionInfo : RmsClientSeverityAction
    {

        public int? RowNum { get; set; } // RowNum
        public string LevelCode { get; set; }
        public string LevelName { get; set; }

        public ClientSeverityActionInfo()
        {

        }

        public ClientSeverityActionInfo(RmsClientSeverityAction rms)
        {
            if (rms != null)
            {
                this.ClientId = rms.ClientId;
                this.SeverityLevelId = rms.SeverityLevelId;
                this.OverwritenAction = rms.OverwritenAction;
                this.Email = rms.Email;
                this.Sms = rms.Sms;
                this.CommandLindId = rms.CommandLindId;
                this.CreatedBy = rms.CreatedBy;
                this.CreatedDate = rms.CreatedDate;
                this.UpdatedBy = rms.UpdatedBy;
                this.UpdatedDate = rms.UpdatedDate;

                if (rms.RmsSeverityLevel != null)
                {
                    this.LevelCode = rms.RmsSeverityLevel.LevelCode;
                    this.LevelName = rms.RmsSeverityLevel.LevelName;
                }
            }
        }
    }
}