using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.BSL;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MonitoringService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MonitoringService.svc or MonitoringService.svc.cs at the Solution Explorer and start debugging.
    public class MonitoringService : IMonitoringService
    {
        public void TestConnection()
        {
              
        }

        public void AddMessage(RmsReportMonitoringRaw rawMessage)
        {
            try
            {
                var lRawMessages = new List<RmsReportMonitoringRaw>();
                lRawMessages.Add(rawMessage);
                AddMessages(lRawMessages);
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "AddMessage failed. " + ex.Message, ex, true);
            }
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
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "AddMessages failed. " + ex.Message, ex, true);
            }
        }

        public void AddBusinessMessage(RmsReportMonitoringRaw rawMessage)
        {
            try
            {
                var ip = GetIP();
                rawMessage.ClientIpAddress = ip;

                using (var db = new MyDbContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;

                    db.RmsReportMonitoringRaws.Add(rawMessage);

                    db.SaveChanges();
                }

                if (string.IsNullOrEmpty(rawMessage.ClientCode)) return;
                if (string.IsNullOrEmpty(rawMessage.MessageGroupCode)) return;
                if (string.IsNullOrEmpty(rawMessage.Message)) return;

                var sv = new SummaryService();
                var caller = new SummaryService.DoSummaryMonitoringForBusinessAsync(sv.DoSummaryMonitoringForBusiness);
                caller.BeginInvoke(rawMessage, null, null);
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "AddBusinessMessage failed. " + ex.Message, ex, true);
            }

        }

        public void StartMonitoringEngine()
        {
            try
            {
                BSL.MonitoringEngineService mes = new MonitoringEngineService();
                mes.Start();
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "StartMonitoringEngine failed. " + ex.Message, ex, true);
            }
        }

        private string GetIP()
        {
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties prop = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint =
                    prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                string ip = endpoint.Address;

                return ip;
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "GetIP failed. " + ex.Message, ex, false);
            }
        }
    }
}
