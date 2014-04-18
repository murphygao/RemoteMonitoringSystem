using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using RMS.Centralize.DAL;

namespace RMS.Centralize.WebService.BSL
{
    public class SummaryService
    {
        public delegate void DoSummaryMonitoringAsync(List<RmsReportMonitoringRaw> lRaw);

        public void DoSummaryMonitoring(List<RmsReportMonitoringRaw> lRaw)
        {
            #region DB Info

            /*
                ID	bigint	Unchecked
                ClientCode	nvarchar(50)	Checked
                ClientIPAddress	nvarchar(50)	Checked
                DeviceCode	nvarchar(20)	Checked
                MessageGroupCode	nvarchar(10)	Checked
                Message	nvarchar(50)	Checked
                MessageDateTime	datetime	Checked
                MessageRemark	nvarchar(500)	Checked
                MonitoringProfileDeviceID	int	Checked
                CreatedDate	datetime	Checked         
             */

            /*
                ID	bigint	Unchecked
                RawID	int	Checked
                ClientID	int	Checked
                ClientCode	nvarchar(50)	Checked
                ClientIPAddress	nvarchar(50)	Checked
                LocationID	int	Checked
                DeviceID	int	Checked
                DeviceCode	nvarchar(20)	Checked
                MessageGroupID	int	Checked
                MessageGroupCode	nvarchar(10)	Checked
                MessageGroupName	nvarchar(100)	Checked
                MessageID	int	Checked
                Message	nvarchar(50)	Checked
                Status	int	Checked
                MessageDateTime	datetime	Checked
                MessageRemark	nvarchar(500)	Checked
                MonitoringProfileDeviceID	int	Checked
                EventDateTime	datetime	Checked
                CreatedDate	datetime	Checked
                CreatedBy	nvarchar(50)	Checked
                UpdatedDate	datetime	Checked
                UpdatedBy	nvarchar(50)	Checked         
             */

            #endregion

            // Find More Data
            // ClientID, LocationID, DeviceID, MessageGroupID, MessageGroupName, MessageID

            int? clientID = null;
            int? locationID = null;
            int? deviceID = null;
            int? messageGroupID = null;
            string messageGroupName = string.Empty;
            int? messageID = null;

            
            try
            {
                var oClientCode = lRaw[0].ClientCode;
                var oDeviceCode = lRaw[0].DeviceCode;
                var oMessageGroupCode = lRaw[0].MessageGroupCode;
                var oMessage = lRaw[0].Message;

                
                using (var db = new MyDbContext())
                {
                    #region Prepare Parameters

                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var ret = db.RmsClients.Where(c => c.ClientCode.ToLower() == oClientCode.ToLower() && c.Enable == true && c.EffectiveDate <= DateTime.Today && (c.ExpiredDate == null || c.ExpiredDate >= DateTime.Today));
                    var lClients = new List<RmsClient>(ret.ToList());

                    if (lClients.Count > 0)
                    {
                        clientID = lClients[0].ClientId;
                        locationID = lClients[0].LocationId;
                    }

                    var device = db.RmsDevices.FirstOrDefault(d => d.DeviceCode.ToLower() == oDeviceCode.ToLower());
                    if (device != null)
                    {
                        deviceID = device.DeviceId;
                    }

                    var messageGroup = db.RmsMessageGroups.FirstOrDefault(m => m.MessageGroupCode.ToLower() == oMessageGroupCode);
                    if (messageGroup != null)
                    {
                        messageGroupID = messageGroup.MessageGroupId;
                        messageGroupName = messageGroup.MessageGroupName;
                    }

                    var message = db.RmsMessages.FirstOrDefault(m => m.Message.ToLower() == oMessage);
                    if (message != null)
                    {
                        messageID = message.MessageId;
                    }

                    #endregion

                    foreach (RmsReportMonitoringRaw reportMonitoringRaw in lRaw)
                    {
                        #region ถ้า Message เป็น OK

                        if (reportMonitoringRaw.Message.ToLower() == "ok")
                        {

                            var okMessage = db.RmsReportSummaryMonitorings.FirstOrDefault(sm => sm.ClientId == clientID
                                                                                                && sm.DeviceId == deviceID
                                                                                                && sm.MonitoringProfileDeviceId == reportMonitoringRaw.MonitoringProfileDeviceId
                                                                                                && sm.Message == "OK"
                                                                                                && sm.Status == 1);

                            //ถ้าพบ OK แสดงว่า ไม่จำเป็นต้องเพิ่ม OK Message และไม่ต้องปรับค่าใดๆ กับ Message อื่นๆ
                            //แต่ถ้าไม่พบ แสดงว่า มี message error ก่อนหน้านี้ ให้ทำการปรับ status ค่าอื่นๆ ให้เป็น 0 และเพิ่ม OK Message
                            if (okMessage == null)
                            {
                                using (var ts = new TransactionScope(TransactionScopeOption.RequiresNew))
                                {
                                    RmsReportMonitoringRaw raw = reportMonitoringRaw;

                                    var monitorings = db.RmsReportSummaryMonitorings.Where(sm => sm.ClientId == clientID
                                                                                                 && sm.DeviceId == deviceID
                                                                                                 &&
                                                                                                 sm.MonitoringProfileDeviceId ==
                                                                                                 raw.MonitoringProfileDeviceId
                                                                                                 && sm.Status == 1);
                                    foreach (var summaryMonitoring in monitorings)
                                    {
                                        summaryMonitoring.Status = 0;
                                    }

                                    // เพิ่ม OK Message
                                    var monitoring = db.RmsReportSummaryMonitorings.Create();
                                    monitoring.RawId = reportMonitoringRaw.Id;
                                    monitoring.ClientId = clientID;
                                    monitoring.ClientCode = reportMonitoringRaw.ClientCode;
                                    monitoring.ClientIpAddress = reportMonitoringRaw.ClientIpAddress;
                                    monitoring.LocationId = locationID;
                                    monitoring.DeviceId = deviceID;
                                    monitoring.DeviceCode = reportMonitoringRaw.DeviceCode;
                                    monitoring.MessageGroupId = messageGroupID;
                                    monitoring.MessageGroupCode = reportMonitoringRaw.MessageGroupCode;
                                    monitoring.MessageGroupName = messageGroupName;
                                    monitoring.MessageId = messageID;
                                    monitoring.Message = reportMonitoringRaw.Message;
                                    monitoring.Status = 1;
                                    monitoring.MessageDateTime = reportMonitoringRaw.MessageDateTime;
                                    monitoring.MessageRemark = reportMonitoringRaw.MessageRemark;
                                    monitoring.MonitoringProfileDeviceId = reportMonitoringRaw.MonitoringProfileDeviceId;
                                    monitoring.EventDateTime = DateTime.Now;

                                    db.RmsReportSummaryMonitorings.Add(monitoring);


                                    db.SaveChanges();

                                    ts.Complete();

                                }
                            }
                            else
                            {
                                okMessage.EventDateTime = DateTime.Now;
                                db.SaveChanges();
                            }
                        }
                        
                        #endregion

                        #region ถ้า Message ไม่ใช่ OK

                        else // Message ที่ไม่ OK
                        {

                            // ถึง Message ที่ Active
                            RmsReportMonitoringRaw raw = reportMonitoringRaw;
                            var reportSummaryMonitoring = db.RmsReportSummaryMonitorings.Where(sm => sm.ClientId == clientID
                                                                                                     && sm.DeviceId == deviceID
                                                                                                     && sm.MonitoringProfileDeviceId ==
                                                                                                     raw.MonitoringProfileDeviceId
                                                                                                     && sm.Status == 1);


                            // เท่ากับ null หมายถึง สามารถเพิ่มลง table ได้
                            if (!reportSummaryMonitoring.Any(sm => sm.Message == reportMonitoringRaw.Message))
                            {
                                /*
                                ID	bigint	Unchecked
                                RawID	int	Checked
                                ClientID	int	Checked
                                ClientCode	nvarchar(50)	Checked
                                ClientIPAddress	nvarchar(50)	Checked
                                LocationID	int	Checked
                                DeviceID	int	Checked
                                DeviceCode	nvarchar(20)	Checked

                                MessageGroupID	int	Checked
                                MessageGroupCode	nvarchar(10)	Checked
                                MessageGroupName	nvarchar(100)	Checked
                                MessageID	int	Checked
                                Message	nvarchar(50)	Checked
                                Status	int	Checked
                                MessageDateTime	datetime	Checked
                                MessageRemark	nvarchar(500)	Checked
                                MonitoringProfileDeviceID	int	Checked
                                EventDateTime	datetime	Checked
                                CreatedDate	datetime	Checked
                                CreatedBy	nvarchar(50)	Checked
                                UpdatedDate	datetime	Checked
                                UpdatedBy	nvarchar(50)	Checked         
                             */
                                var monitoring = db.RmsReportSummaryMonitorings.Create();
                                monitoring.RawId = reportMonitoringRaw.Id;
                                monitoring.ClientId = clientID;
                                monitoring.ClientCode = reportMonitoringRaw.ClientCode;
                                monitoring.ClientIpAddress = reportMonitoringRaw.ClientIpAddress;
                                monitoring.LocationId = locationID;
                                monitoring.DeviceId = deviceID;
                                monitoring.DeviceCode = reportMonitoringRaw.DeviceCode;
                                monitoring.MessageGroupId = messageGroupID;
                                monitoring.MessageGroupCode = reportMonitoringRaw.MessageGroupCode;
                                monitoring.MessageGroupName = messageGroupName;
                                monitoring.MessageId = messageID;
                                monitoring.Message = reportMonitoringRaw.Message;
                                monitoring.Status = 1;
                                monitoring.MessageDateTime = reportMonitoringRaw.MessageDateTime;
                                monitoring.MessageRemark = reportMonitoringRaw.MessageRemark;
                                monitoring.MonitoringProfileDeviceId = reportMonitoringRaw.MonitoringProfileDeviceId;
                                monitoring.EventDateTime = DateTime.Now;

                                db.RmsReportSummaryMonitorings.Add(monitoring);
                                db.SaveChanges();

                            }
                            else // ไม่เท่ากับ null หมายถึง พบ message ค้างอยู่ใน table ไม่จำเป็นต้องใส่ข้อมูลเพิ่มแต่อย่างใด
                            {
                                //ค้นหาดูว่ามี OK Message ที่ active หรือไม่

                                var okMessages = reportSummaryMonitoring.Where(sm => sm.Message == "OK");
                                if (okMessages.Any())
                                {
                                    foreach (var okMessage in okMessages)
                                    {
                                        okMessage.Status = 0;
                                    }
                                }

                                foreach (var summaryMonitoring in reportSummaryMonitoring.Where(sm => sm.Message != "OK"))
                                {
                                    summaryMonitoring.EventDateTime = DateTime.Now;
                                }
                                db.SaveChanges();
                            }
                        }

                        #endregion
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}