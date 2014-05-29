using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.AccessControl;
using System.ServiceModel.Channels;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Model;

namespace RMS.Centralize.WebService.BSL
{
    public class SummaryService : BaseService
    {
        public delegate void DoSummaryMonitoringAsync(List<RmsReportMonitoringRaw> lRaw);

        public delegate void DoSummaryMonitoringForBusinessAsync(RmsReportMonitoringRaw raw);

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
            string deviceDescription = string.Empty;
            int? messageGroupID = null;
            string messageGroupName = string.Empty;
            int? messageID = null;
            string messageGroupCode = string.Empty;

            
            try
            {
                var oClientCode = lRaw[0].ClientCode;
                var oDeviceCode = lRaw[0].DeviceCode;
                var oMessageGroupCode = lRaw[0].MessageGroupCode;
                var oMessage = lRaw[0].Message;

                List<RmsReportSummaryMonitoring> lPrepareForActions = new List<RmsReportSummaryMonitoring>();

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

                        SqlParameter[] parameters = new SqlParameter[1];
                        SqlParameter p1 = new SqlParameter("ClientID", clientID);
                        parameters[0] = p1;

                        var clientLocation = db.Database.SqlQuery<Location>("RMS_GetLocationByClientID " +
                                                                                "@ClientID", parameters).First();

                        if (clientLocation != null)
                            locationID = clientLocation.LocationID;
                    }

                    //var device = db.RmsDevices.FirstOrDefault(d => d.DeviceCode.ToLower() == oDeviceCode.ToLower());
                    //if (device != null)
                    //{
                    //    deviceID = device.DeviceId;
                    //}

                    //var messageGroup = db.RmsMessageGroups.FirstOrDefault(m => m.MessageGroupCode.ToLower() == oMessageGroupCode);
                    //if (messageGroup != null)
                    //{
                    //    messageGroupID = messageGroup.MessageGroupId;
                    //    messageGroupName = messageGroup.MessageGroupName;
                    //}

                    //var message = db.RmsMessages.FirstOrDefault(m => m.Message.ToLower() == oMessage);
                    //if (message != null)
                    //{
                    //    messageID = message.MessageId;
                    //}

                    #endregion

                    int? lastMonitoringProfileDeviceId = null;
                    int counterResetToZero = 1;

                    foreach (RmsReportMonitoringRaw reportMonitoringRaw in lRaw.OrderBy(o => o.MonitoringProfileDeviceId))
                    {

                        if (lastMonitoringProfileDeviceId != reportMonitoringRaw.MonitoringProfileDeviceId)
                        {
                            counterResetToZero = 1;
                            lastMonitoringProfileDeviceId = reportMonitoringRaw.MonitoringProfileDeviceId;
                        }


                        SqlParameter[] parameters = new SqlParameter[1];
                        SqlParameter p1 = new SqlParameter("MonitoringProfileDeviceID", reportMonitoringRaw.MonitoringProfileDeviceId);
                        parameters[0] = p1;

                        var details = db.Database.SqlQuery<MessageDetail>("RMS_ListMessageGroupByMonitoringProfileDeviceID " +
                                                                                "@MonitoringProfileDeviceID", parameters);
                        List<MessageDetail> lMessageDetails = new List<MessageDetail>(details.ToList());

                        if (lMessageDetails.Count == 0) continue;

                        if (reportMonitoringRaw.Message.ToLower() == "ok")
                        {
                            if (lMessageDetails.Count > 0)
                            {
                                deviceID = lMessageDetails[0].DeviceID;
                                deviceDescription = lMessageDetails[0].DeviceDescription;
                                messageGroupID = lMessageDetails[0].MessageGroupID;
                                messageGroupName = lMessageDetails[0].MessageGroupName;
                                messageGroupCode = lMessageDetails[0].MessageGroupCode;
                            }
                        }
                        else
                        {
                            var messageDetail = lMessageDetails.FirstOrDefault(md => md.Message == reportMonitoringRaw.Message);
                            if (messageDetail != null)
                            {
                                deviceID = messageDetail.DeviceID;
                                deviceDescription = messageDetail.DeviceDescription;
                                messageGroupID = messageDetail.MessageGroupID;
                                messageGroupName = messageDetail.MessageGroupName;
                                messageID = messageDetail.MessageID;
                                messageGroupCode = messageDetail.MessageGroupCode;
                            }
                        }

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
                                    monitoring.DeviceDescription = deviceDescription;
                                    monitoring.MessageGroupId = messageGroupID;
                                    monitoring.MessageGroupCode = messageGroupCode;
                                    monitoring.MessageGroupName = messageGroupName;
                                    monitoring.MessageId = null;
                                    monitoring.Message = reportMonitoringRaw.Message;
                                    monitoring.Status = 1;
                                    monitoring.MessageDateTime = reportMonitoringRaw.MessageDateTime;
                                    monitoring.MessageRemark = reportMonitoringRaw.MessageRemark;
                                    monitoring.MonitoringProfileDeviceId = reportMonitoringRaw.MonitoringProfileDeviceId;
                                    monitoring.SeverityLevelId = null;
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
                           
                            //ค้นหาดูว่ามี OK Message ที่ active หรือไม่

                            if (reportSummaryMonitoring.Any() && counterResetToZero > 0)
                            {
                                counterResetToZero--;

                                foreach (var report in reportSummaryMonitoring)
                                {

                                    // ปรับ message OK ใน Summary Report ให้เป็น 0
                                    if (report.Message.ToUpper() == "OK")
                                    {
                                        report.Status = 0;
                                    }
                                    // ปรับ Message DEVICE_NOT_FOUND ใน Summary Report ให้เป็น 0
                                    else if (report.Message.ToUpper() == "DEVICE_NOT_FOUND")
                                    {
                                        if (!lRaw.Any(r => r.MonitoringProfileDeviceId == raw.MonitoringProfileDeviceId && r.Message == "DEVICE_NOT_FOUND"))
                                            report.Status = 0;
                                    }

                                    if (!lRaw.Any(r => r.MonitoringProfileDeviceId == raw.MonitoringProfileDeviceId && (r.Message == report.Message || r.Message == "DEVICE_NOT_FOUND" || r.Message == "DEVICE_NOT_READY")))
                                        report.Status = 0;
                                }
                            }

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
                                SeverityLevelID 	int	Checked
                                EventDateTime	datetime	Checked
                                CreatedDate	datetime	Checked
                                CreatedBy	nvarchar(50)	Checked
                                UpdatedDate	datetime	Checked
                                UpdatedBy	nvarchar(50)	Checked         
                             */
                                var rmsMessage = db.RmsMessages.FirstOrDefault(m => m.MessageId == messageID);

                                var monitoring = db.RmsReportSummaryMonitorings.Create();
                                monitoring.RawId = reportMonitoringRaw.Id;
                                monitoring.ClientId = clientID;
                                monitoring.ClientCode = reportMonitoringRaw.ClientCode;
                                monitoring.ClientIpAddress = reportMonitoringRaw.ClientIpAddress;
                                monitoring.LocationId = locationID;
                                monitoring.DeviceId = deviceID;
                                monitoring.DeviceCode = reportMonitoringRaw.DeviceCode;
                                monitoring.DeviceDescription = deviceDescription;
                                monitoring.MessageGroupId = messageGroupID;
                                monitoring.MessageGroupCode = messageGroupCode;
                                monitoring.MessageGroupName = messageGroupName;
                                monitoring.MessageId = messageID;
                                monitoring.Message = reportMonitoringRaw.Message;
                                monitoring.Status = 1;
                                monitoring.MessageDateTime = reportMonitoringRaw.MessageDateTime;
                                monitoring.MessageRemark = reportMonitoringRaw.MessageRemark;
                                monitoring.MonitoringProfileDeviceId = reportMonitoringRaw.MonitoringProfileDeviceId;
                                if (rmsMessage != null) monitoring.SeverityLevelId = rmsMessage.SeverityLevelId;
                                monitoring.EventDateTime = DateTime.Now;

                                db.RmsReportSummaryMonitorings.Add(monitoring);
                                db.SaveChanges();

                                lPrepareForActions.Add(monitoring);

                            }
                            else // ไม่เท่ากับ null หมายถึง พบ message ค้างอยู่ใน table ไม่จำเป็นต้องใส่ข้อมูลเพิ่มแต่อย่างใด
                            {

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

                if (lPrepareForActions.Count > 0)
                {
                    var action = new ActionService();
                    action.ActionSend(ActionService.ActionSendType.ManualSending, lPrepareForActions);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public void DoSummaryMonitoringForBusiness(RmsReportMonitoringRaw raw)
        {

            #region DB Reference

            /*
      ,[RawID]
      ,[ClientID]
      ,[ClientCode]
      ,[ClientIPAddress]
      ,[LocationID]
      ,[DeviceID]
      ,[DeviceCode]
      ,[DeviceDescription]
      ,[MessageGroupID]
      ,[MessageGroupCode]
      ,[MessageGroupName]
      ,[MessageID]
      ,[Message]
      ,[Status]
      ,[MessageDateTime]
      ,[MessageRemark]
      ,[MonitoringProfileDeviceID]
      ,[SeverityLevelID]
      ,[LastActionDateTime]
      ,[LastActionType]
      ,[EventDateTime]
      ,[CreatedDate]
      ,[CreatedBy]
      ,[UpdatedDate]
      ,[UpdatedBy]

             */

            #endregion


            var oClientCode = raw.ClientCode;
            var oMessageGroupCode = raw.MessageGroupCode;
            var oMessage = raw.Message;

            List<RmsReportSummaryMonitoring> lPrepareForActions = new List<RmsReportSummaryMonitoring>();

            try
            {
                using (var db = new MyDbContext())
                {
                    #region Prepare Parameters

                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;


                    /*
                 * 	
                    @Message			nvarchar(50) = NULL,
	                @ClientCode			nvarchar(50) = NULL,
	                @ClientIPAddress	nvarchar(50) = NULL,
	                @MessageGroupCode	nvarchar(50) = NULL
                 * 
                 */

                    SqlParameter[] parameters = new SqlParameter[4];
                    SqlParameter p1 = new SqlParameter("Message", oMessage);
                    SqlParameter p2 = new SqlParameter("ClientCode", oClientCode);

                    SqlParameter p3;

                    if (string.IsNullOrEmpty(raw.ClientIpAddress))
                    {
                        p3 = new SqlParameter("ClientIPAddress", DBNull.Value);
                    }
                    else
                    {
                        p3 = new SqlParameter("ClientIPAddress", raw.ClientIpAddress);
                    }

                    SqlParameter p4 = new SqlParameter("MessageGroupCode", oMessageGroupCode);

                    parameters[0] = p1;
                    parameters[1] = p2;
                    parameters[2] = p3;
                    parameters[3] = p4;

                    var listOfType = db.Database.SqlQuery<ClientMessageInfo>("RMS_GetClientMessageInfoByMessageClientCodeMessageGroupCode " +
                                                                             "@Message, @ClientCode, @ClientIPAddress, @MessageGroupCode", parameters);

                    List<ClientMessageInfo> lClientMessageInfos = new List<ClientMessageInfo>(listOfType.ToList());

                    // ถ้าไม่เจอ ให้จบการทำงาน
                    if (lClientMessageInfos.Count == 0) return;



                    #endregion

                    #region Insert into DB

                    var monitoring = db.RmsReportSummaryMonitorings.Create();
                    monitoring.RawId = raw.Id;
                    monitoring.ClientId = lClientMessageInfos[0].ClientID;
                    monitoring.ClientCode = raw.ClientCode;
                    monitoring.ClientIpAddress = raw.ClientIpAddress;
                    monitoring.LocationId = lClientMessageInfos[0].LocationID;
                    //monitoring.DeviceId = deviceID;
                    //monitoring.DeviceCode = raw.DeviceCode;
                    //monitoring.DeviceDescription = deviceDescription;
                    monitoring.MessageGroupId = lClientMessageInfos[0].MessageGroupID;
                    monitoring.MessageGroupCode = raw.MessageGroupCode;
                    monitoring.MessageGroupName = lClientMessageInfos[0].MessageGroupName;
                    monitoring.MessageId = lClientMessageInfos[0].MessageID;
                    monitoring.Message = raw.Message;
                    monitoring.Status = 2;
                    monitoring.MessageDateTime = raw.MessageDateTime;

                    if (!string.IsNullOrEmpty(raw.MessageRemark))
                        monitoring.MessageRemark = raw.MessageRemark;

                    //monitoring.MonitoringProfileDeviceId = raw.MonitoringProfileDeviceId;
                    monitoring.SeverityLevelId = lClientMessageInfos[0].SeverityLevelID;
                    monitoring.EventDateTime = DateTime.Now;

                    db.RmsReportSummaryMonitorings.Add(monitoring);
                    db.SaveChanges();

                    lPrepareForActions.Add(monitoring);

                    #endregion

                }

                var action = new ActionService();
                action.ActionSend(ActionService.ActionSendType.ManualSending, lPrepareForActions);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}