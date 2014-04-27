using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.BSL;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MonitoringService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MonitoringService.svc or MonitoringService.svc.cs at the Solution Explorer and start debugging.
    public class MonitoringService : IMonitoringService
    {
        public void AddMessage(RmsReportMonitoringRaw rawMessage)
        {
            try
            {
                var lRawMessages = new List<RmsReportMonitoringRaw>();
                lRawMessages.Add(rawMessage);
                AddMessages(lRawMessages);
            }
            catch {}
        }

        public void AddMessages(List<RmsReportMonitoringRaw> lRawMessages)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;

                    foreach (var rawMessage in lRawMessages)
                    {
                        db.RmsReportMonitoringRaws.Add(rawMessage);
                    }

                    db.SaveChanges();
                }

                var sv = new SummaryService();
                var caller = new SummaryService.DoSummaryMonitoringAsync(sv.DoSummaryMonitoring);
                caller.BeginInvoke(lRawMessages, null, null);
            }
            catch { }
        }

        public void StartMonitoringEngine()
        {
            RMS.Centralize.BSL.MonitoringEngine.MonitoringService ms = new Centralize.BSL.MonitoringEngine.MonitoringService();
            ms.Start();
        }
    }
}
