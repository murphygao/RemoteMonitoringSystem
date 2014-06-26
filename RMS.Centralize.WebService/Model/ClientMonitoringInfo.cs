using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMS.Centralize.DAL;

namespace RMS.Centralize.WebService.Model
{
    public class ClientMonitoringInfo : RmsClientMonitoring
    {
        public string ProfileName { get; set; }
        public int? RowNum { get; set; } // RowNum

        public ClientMonitoringInfo()
        {
            
        }
        public ClientMonitoringInfo(RmsClientMonitoring rmsClientMonitoring)
        {
            this.MonitoringProfileId = rmsClientMonitoring.MonitoringProfileId;
            this.ClientId = rmsClientMonitoring.ClientId;
            this.EffectiveDate = rmsClientMonitoring.EffectiveDate;
            this.ProfileName = rmsClientMonitoring.RmsMonitoringProfile.ProfileName;
            this.CreatedBy = rmsClientMonitoring.CreatedBy;
            this.CreatedDate = rmsClientMonitoring.CreatedDate;
            this.UpdatedBy = rmsClientMonitoring.UpdatedBy;
            this.UpdatedDate = rmsClientMonitoring.UpdatedDate;
        }
    }
}