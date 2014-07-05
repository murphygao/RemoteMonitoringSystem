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
            if (rmsClientMonitoring != null)
            {
                this.MonitoringProfileId = rmsClientMonitoring.MonitoringProfileId;
                this.ClientId = rmsClientMonitoring.ClientId;
                this.EffectiveDate = rmsClientMonitoring.EffectiveDate;
                this.CreatedBy = rmsClientMonitoring.CreatedBy;
                this.CreatedDate = rmsClientMonitoring.CreatedDate;
                this.UpdatedBy = rmsClientMonitoring.UpdatedBy;
                this.UpdatedDate = rmsClientMonitoring.UpdatedDate;

                if (rmsClientMonitoring.RmsMonitoringProfile != null)
                {
                    this.ProfileName = rmsClientMonitoring.RmsMonitoringProfile.ProfileName;
                }
            }
        }
    }
}