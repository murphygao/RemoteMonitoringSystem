using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.ServiceModel.Channels;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Model;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService.BSL
{
    public class SummaryService : BaseService
    {
        public delegate void DoSummaryMonitoringAsync(List<RmsReportMonitoringRaw> lRaw, List<RMSAttachment> lAttachments);

        public delegate void DoSummaryMonitoringForBusinessAsync(RmsReportMonitoringRaw raw);

        public delegate void DoSummaryWebsiteMonitoringAsync(List<RmsReportMonitoringRaw> lRaw);

        public void DoSummaryMonitoring(List<RmsReportMonitoringRaw> lRaw, List<RMSAttachment> lAttachments)
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

                // List สำหรับเก็บ Message ที่ถูก Solved แล้ว (BAD -> GOOD)
                List<RmsReportSummaryMonitoring> lSolvedStatus = new List<RmsReportSummaryMonitoring>();

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

                        var clientLocation = db.Database.SqlQuery<LocationInfo>("RMS_GetLocationByClientID " +
                                                                                "@ClientID", parameters).First();

                        if (clientLocation != null)
                            locationID = clientLocation.LocationId;
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

                    var listLevels = db.RmsSeverityLevels.Include("RmsColorLabel");
                    List<RmsSeverityLevel> lSeverityLevels = new List<RmsSeverityLevel>(listLevels.ToList());

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
                            else
                            {
                                continue;
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
                                        lSolvedStatus.Add(summaryMonitoring);
                                    }

                                    #region // เพิ่ม OK Message

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

                                    #endregion

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
                                        // ตรวจสอบดูว่า Message ที่ส่งมาชุดนี้ มี DEVICE_NOT_FOUND อยู่หรือไม่ 
                                        // ถ้าไม่มีอยู่เลย ก้อให้ปรับ Message ใน DB ที่เป็นข้อความ DEVICE_NOT_FOUND ให้ Status เป็น 0
                                        if (!lRaw.Any(r => r.MonitoringProfileDeviceId == raw.MonitoringProfileDeviceId && r.Message == "DEVICE_NOT_FOUND"))
                                        {
                                            report.Status = 0;

                                            if (!lSolvedStatus.Exists(e => e.Id == report.Id))
                                                lSolvedStatus.Add(report);
                                        }

                                    }

                                    if (!lRaw.Any(r => r.MonitoringProfileDeviceId == raw.MonitoringProfileDeviceId &&
                                                (r.Message == report.Message || r.Message == "DEVICE_NOT_FOUND" || r.Message == "DEVICE_NOT_READY")))
                                    {
                                        report.Status = 0;

                                        if (!lSolvedStatus.Exists(e => e.Id == report.Id) && report.Message.ToUpper() != "OK")
                                            lSolvedStatus.Add(report);
                                    }
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

                                    // ตรวจสอบว่า ต้องส่ง message ซ้ำหรือไม่
                                    // โดยตรวจจาก severity level ว่า repeatable หรือไม่
                                    var level = lSeverityLevels.Find(s => s.SeverityLevelId == summaryMonitoring.SeverityLevelId);
                                    if (level != null && level.ActionRepeatable == true)
                                    {
                                        if (summaryMonitoring.LastActionDateTime == null)
                                        {
                                            //case นี้ปกติเป็นไปไม่ได้ ทำเผื่อไว้เท่านั้น
                                            lPrepareForActions.Add(summaryMonitoring);
                                        }
                                        // ถ้าเวลาปัจจุบัน มากกว่า last action send + interval แสดงว่า ให้ส่ง ซ้ำได้
                                        else if (DateTime.Now > summaryMonitoring.LastActionDateTime.Value.AddMinutes(level.ActionRepeatInterval ?? 0))
                                        {
                                            lPrepareForActions.Add(summaryMonitoring);
                                        }
                                    }
                                }
                                db.SaveChanges();
                            }
                        }

                        #endregion
                    }

                }

                if (lPrepareForActions.Count > 0 || lSolvedStatus.Count > 0)
                {
                    var action = new ActionSendService();
                    action.ActionSend(ActionSendService.ActionSendType.ManualSending, lPrepareForActions, lSolvedStatus, lAttachments);
                }

            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "DoSummaryMonitoring failed. " + ex.Message, ex, true);
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

                var action = new ActionSendService();
                action.ActionSend(ActionSendService.ActionSendType.ManualSending, lPrepareForActions);

            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "DoSummaryMonitoringForBusiness failed. " + ex.Message, ex, true);
            }
        }

        public string DoSummaryStatusAllClients()
        {
            DateTime startDateTime = DateTime.Now;
            string s = string.Empty;
            try
            {
                string SummaryStatusAllClientsTemplateURL = ConfigurationManager.AppSettings["RMS.SummaryStatusAllClientsTemplateURL"];

                var webpage = new WebDownload();

                s = webpage.DownloadString(new Uri(SummaryStatusAllClientsTemplateURL));

                #region Remove Not Neccessary Objects

                if (s.IndexOf("<!--BEGIN-->") >= 0)
                    s = s.Substring(s.IndexOf("<!--BEGIN-->"));

                if (s.IndexOf("<!--END-->") >= 0)
                    s = s.Substring(0, s.IndexOf("<!--END-->"));

                #endregion

                var action = new ActionSendService();
                action.ActionSend(ActionSendService.ActionSendType.SummaryTechnicalSending, s);


                using (var db = new MyDbContext())
                {
                    var logActionEngine = db.RmsLogActionEngines.Create();
                    logActionEngine.ActionDateTime = startDateTime;
                    logActionEngine.BodyFull = s;
                    logActionEngine.FinishDateTime = DateTime.Now;
                    logActionEngine.IsSuccess = true;

                    db.RmsLogActionEngines.Add(logActionEngine);
                    db.SaveChanges();
                }

                return s;
            }
            catch (Exception ex)
            {

                try
                {
                    using (var db = new MyDbContext())
                    {
                        var logActionEngine = db.RmsLogActionEngines.Create();
                        logActionEngine.ActionDateTime = startDateTime;
                        logActionEngine.BodyFull = (s == string.Empty)? null : s;
                        logActionEngine.FinishDateTime = DateTime.Now;
                        logActionEngine.IsSuccess = false;
                        logActionEngine.ErrorMessage = ex.Message;

                        db.RmsLogActionEngines.Add(logActionEngine);
                        db.SaveChanges();
                    }
                }
                catch
                {
                }
                throw new RMSWebException(this, "0500", "DoSummaryStatusAllClients failed. " + ex.Message, ex, true);
            }
        }

        public void DoSummaryWebsiteMonitoring(List<RmsReportMonitoringRaw> lRaw)
        {
            #region DB Info

            /*
	            [ID] [bigint] IDENTITY(1,1) NOT NULL,
	            [ClientCode] [nvarchar](50) NULL,
	            [ClientIPAddress] [nvarchar](50) NULL,
	            [DeviceCode] [nvarchar](20) NULL,
	            [MessageGroupCode] [nvarchar](10) NULL,
	            [Message] [nvarchar](50) NULL,
	            [MessageDateTime] [datetime] NULL,
	            [MessageRemark] [nvarchar](500) NULL,
	            [MonitoringProfileDeviceID] [int] NULL,
	            [CreatedDate] [datetime] NULL,
	            [WebsiteMonitoringID] [int] NULL,  
             */

            /*
	            [ID] [bigint] IDENTITY(1,1) NOT NULL,
	            [RawID] [bigint] NULL,
	            [ClientID] [int] NULL,
	            [ClientCode] [nvarchar](50) NULL,
	            [ClientIPAddress] [nvarchar](50) NULL,
	            [LocationID] [int] NULL,
	            [DeviceID] [int] NULL,
	            [DeviceCode] [nvarchar](20) NULL,
	            [DeviceDescription] [nvarchar](250) NULL,
	            [MessageGroupID] [int] NULL,
	            [MessageGroupCode] [nvarchar](10) NULL,
	            [MessageGroupName] [nvarchar](100) NULL,
	            [MessageID] [int] NULL,
	            [Message] [nvarchar](50) NULL,
	            [Status] [int] NULL,
	            [MessageDateTime] [datetime] NULL,
	            [MessageRemark] [nvarchar](500) NULL,
	            [MonitoringProfileDeviceID] [int] NULL,
	            [SeverityLevelID] [int] NULL,
	            [LastActionDateTime] [datetime] NULL,
	            [LastActionType] [nvarchar](50) NULL,
	            [EventDateTime] [datetime] NULL,
	            [CreatedDate] [datetime] NULL,
	            [CreatedBy] [nvarchar](50) NULL,
	            [UpdatedDate] [datetime] NULL,
	            [UpdatedBy] [nvarchar](50) NULL,
	            [WebsiteMonitoringID] [int] NULL,  
             */

            #endregion

            // Find More Data
            // ClientID, LocationID, DeviceID, MessageGroupID, MessageGroupName, MessageID

            int? clientID = null;
            int? locationID = null;
            int? deviceID = null;
            string deviceDescription = null;
            int? messageGroupID = null;
            string messageGroupName = string.Empty;
            int? messageID = null;
            string messageGroupCode = string.Empty;

            string protocolName = null;
            string hostName = null;
            int? portNumber = null;

            string messageRemark = null;

            try
            {
                var oClientCode = lRaw[0].ClientCode;
                var oDeviceCode = lRaw[0].DeviceCode;
                var oMessageGroupCode = lRaw[0].MessageGroupCode;
                var oMessage = lRaw[0].Message;

                List<RmsReportSummaryMonitoring> lPrepareForActions = new List<RmsReportSummaryMonitoring>();

                // List สำหรับเก็บ Message ที่ถูก Solved แล้ว (BAD -> GOOD)
                List<RmsReportSummaryMonitoring> lSolvedStatus = new List<RmsReportSummaryMonitoring>();

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

                        var clientLocation = db.Database.SqlQuery<LocationInfo>("RMS_GetLocationByClientID " +
                                                                                "@ClientID", parameters).First();

                        if (clientLocation != null)
                            locationID = clientLocation.LocationId;
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

                    int? lastWebsiteMonitoringId = null;
                    int counterResetToZero = 1;

                    var listLevels = db.RmsSeverityLevels.Include("RmsColorLabel");
                    List<RmsSeverityLevel> lSeverityLevels = new List<RmsSeverityLevel>(listLevels.ToList());

                    var dbSetlistOfValues = db.RmsListOfValues;
                    List<RmsListOfValue> lListOfValues = new List<RmsListOfValue>(dbSetlistOfValues);

                    foreach (RmsReportMonitoringRaw reportMonitoringRaw in lRaw.OrderBy(o => o.WebsiteMonitoringId))
                    {

                        if (string.IsNullOrEmpty(reportMonitoringRaw.Message)) continue;
                        if (string.IsNullOrEmpty(reportMonitoringRaw.MessageGroupCode)) continue;
                        if (reportMonitoringRaw.WebsiteMonitoringId == null) continue;


                        if (lastWebsiteMonitoringId != reportMonitoringRaw.WebsiteMonitoringId)
                        {
                            counterResetToZero = 1;
                            lastWebsiteMonitoringId = reportMonitoringRaw.WebsiteMonitoringId;
                        }

                        var rmsMessageGroup = db.RmsMessageGroups.FirstOrDefault(w => w.MessageGroupCode.ToLower() == reportMonitoringRaw.MessageGroupCode.ToLower());

                        if (rmsMessageGroup == null) continue;

                        if (reportMonitoringRaw.Message.ToLower() == "ok")
                        {
                            messageGroupID = rmsMessageGroup.MessageGroupId;
                            messageGroupName = rmsMessageGroup.MessageGroupName;
                            messageGroupCode = rmsMessageGroup.MessageGroupCode;
                        }
                        else
                        {
                            var rmsMessage = db.RmsMessages.FirstOrDefault(w => w.Message.ToLower() == reportMonitoringRaw.Message.ToLower());

                            if (rmsMessage != null)
                            {
                                messageGroupID = rmsMessageGroup.MessageGroupId;
                                messageGroupName = rmsMessageGroup.MessageGroupName;
                                messageGroupCode = rmsMessageGroup.MessageGroupCode;
                                messageID = rmsMessage.MessageId;
                            }
                            else
                            {
                                continue;
                            }
                        }

                        var rmsWebsiteMonitoring = db.RmsWebsiteMonitorings.FirstOrDefault(w => w.WebsiteMonitoringId == reportMonitoringRaw.WebsiteMonitoringId);
                        if (rmsWebsiteMonitoring == null) continue;

                        var websitMonitoringType = lListOfValues.FirstOrDefault(w => w.ListName == "WebsitMonitoringType" && w.ItemId == rmsWebsiteMonitoring.WebsiteMonitoringTypeId);
                        var websitMonitoringProtocol = lListOfValues.FirstOrDefault(w => w.ListName == "WebsitMonitoringProtocol" && w.ItemId == rmsWebsiteMonitoring.WebsiteMonitoringProtocolId);

                        if (websitMonitoringType != null && websitMonitoringProtocol != null)
                        {
                            protocolName = websitMonitoringType.ItemValue + "/" + websitMonitoringProtocol.ItemValue;
                            messageRemark = websitMonitoringProtocol.ItemValue;
                        }

                        // 1 = Ping, 2 = Telnet
                        if (rmsWebsiteMonitoring.WebsiteMonitoringProtocolId == "1" || rmsWebsiteMonitoring.WebsiteMonitoringProtocolId == "2")
                        {
                            hostName = lClients[0].IpAddress;
                        }
                        else // Http or Https
                        {
                            hostName = rmsWebsiteMonitoring.DomainName;
                        }

                        portNumber = rmsWebsiteMonitoring.PortNumber;

                        messageRemark = (messageRemark + " " + hostName + " " + portNumber).Trim();

                        #region ถ้า Message เป็น OK

                        if (reportMonitoringRaw.Message.ToLower() == "ok")
                        {

                            var okMessage = db.RmsReportSummaryMonitorings.FirstOrDefault(sm => sm.ClientId == clientID
                                                                                                && sm.MessageGroupId == messageGroupID
                                                                                                && sm.WebsiteMonitoringId == reportMonitoringRaw.WebsiteMonitoringId
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
                                                                                                 && sm.MessageGroupId == messageGroupID
                                                                                                 &&
                                                                                                 sm.WebsiteMonitoringId ==
                                                                                                 raw.WebsiteMonitoringId
                                                                                                 && sm.Status == 1);
                                    foreach (var summaryMonitoring in monitorings)
                                    {
                                        summaryMonitoring.Status = 0;
                                        lSolvedStatus.Add(summaryMonitoring);
                                    }

                                    #region // เพิ่ม OK Message

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
                                    monitoring.MessageRemark = reportMonitoringRaw.MessageRemark ?? messageRemark;
                                    monitoring.MonitoringProfileDeviceId = reportMonitoringRaw.MonitoringProfileDeviceId;
                                    monitoring.SeverityLevelId = null;
                                    monitoring.EventDateTime = DateTime.Now;

                                    monitoring.WebsiteMonitoringId = reportMonitoringRaw.WebsiteMonitoringId;
                                    monitoring.ProtocolName = protocolName;
                                    monitoring.HostName = hostName;
                                    monitoring.PortNumber = portNumber;

                                    
                                    #endregion

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
                                                                                                     && sm.MessageGroupId == messageGroupID
                                                                                                     && sm.WebsiteMonitoringId ==
                                                                                                     raw.WebsiteMonitoringId
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
                                monitoring.MessageRemark = reportMonitoringRaw.MessageRemark ?? messageRemark;
                                monitoring.MonitoringProfileDeviceId = reportMonitoringRaw.MonitoringProfileDeviceId;
                                if (rmsMessage != null) monitoring.SeverityLevelId = rmsMessage.SeverityLevelId;
                                monitoring.EventDateTime = DateTime.Now;

                                monitoring.WebsiteMonitoringId = reportMonitoringRaw.WebsiteMonitoringId;
                                monitoring.ProtocolName = protocolName;
                                monitoring.HostName = hostName;
                                monitoring.PortNumber = portNumber;

                                db.RmsReportSummaryMonitorings.Add(monitoring);
                                db.SaveChanges();

                                lPrepareForActions.Add(monitoring);

                            }
                            else // ไม่เท่ากับ null หมายถึง พบ message ค้างอยู่ใน table ไม่จำเป็นต้องใส่ข้อมูลเพิ่มแต่อย่างใด
                            {
                                foreach (var summaryMonitoring in reportSummaryMonitoring.Where(sm => sm.Message != "OK"))
                                {
                                    summaryMonitoring.EventDateTime = DateTime.Now;

                                    // ตรวจสอบว่า ต้องส่ง message ซ้ำหรือไม่
                                    // โดยตรวจจาก severity level ว่า repeatable หรือไม่
                                    var level = lSeverityLevels.Find(s => s.SeverityLevelId == summaryMonitoring.SeverityLevelId);
                                    if (level != null && level.ActionRepeatable == true)
                                    {
                                        if (summaryMonitoring.LastActionDateTime == null)
                                        {
                                            //case นี้ปกติเป็นไปไม่ได้ ทำเผื่อไว้เท่านั้น
                                            lPrepareForActions.Add(summaryMonitoring);
                                        }
                                        // ถ้าเวลาปัจจุบัน มากกว่า last action send + interval แสดงว่า ให้ส่ง ซ้ำได้
                                        else if (DateTime.Now > summaryMonitoring.LastActionDateTime.Value.AddMinutes(level.ActionRepeatInterval ?? 0))
                                        {
                                            lPrepareForActions.Add(summaryMonitoring);
                                        }
                                    }
                                }
                                db.SaveChanges();
                            }
                        }

                        #endregion
                    }

                }

                if (lPrepareForActions.Count > 0 || lSolvedStatus.Count > 0)
                {
                    var action = new ActionSendService();
                    action.ActionSend(ActionSendService.ActionSendType.ManualSending, lPrepareForActions, lSolvedStatus);
                }

            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "DoSummaryWebsiteMonitoring failed. " + ex.Message, ex, true);
            }


        }
    }


}