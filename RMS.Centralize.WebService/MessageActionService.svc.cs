using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Model;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MessageActionService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MessageActionService.svc or MessageActionService.svc.cs at the Solution Explorer and start debugging.
    public class MessageActionService : IMessageActionService
    {

        public void TestConnection()
        {
        }

        #region Page: MessageAction

        public InitMessageActionResult InitDataForMeesageAction() 
        {
            var mg = new List<RmsMessageGroup>();

            try
            {
                using (var db = new MyDbContext())
                {
                    var messageGroups =
                        db.RmsMessageGroups.Where(s => s.ActiveList == true).OrderBy(o => o.MessageType).ThenBy(o => o.MessageGroupName);
                    mg = messageGroups.ToList();
                }
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "InitDataForMeesageAction failed. " + ex.Message, ex, true);

                return new InitMessageActionResult
                {
                    IsSuccess = false,
                    ErrorMessage =  ex.Message
                };
            }

            string ddlMessageGroup = "<option value=\"\">Message Group</option>";
            string optgroup = "";
            foreach (var rmsMessageGroup in mg)
            {
                if (optgroup != rmsMessageGroup.MessageType)
                {
                    ddlMessageGroup += "<optgroup label=\"" + rmsMessageGroup.MessageType + " Message\">";
                    optgroup = rmsMessageGroup.MessageType;
                }
                ddlMessageGroup += "<option value=\"" + rmsMessageGroup.MessageGroupId + "\">" + rmsMessageGroup.MessageGroupName + "</option>";
            }

            return new InitMessageActionResult
            {
                IsSuccess = true,
                MessageGroup = ddlMessageGroup
            };

        }

        public MessageActionResult Search(JQueryDataTableParamModel param, int? messageGroupID, string message, bool? activeStatus)
        {
            List<RmsMessageAction> listRmsMessages = new List<RmsMessageAction>();
            SqlParameter[] parameters = new SqlParameter[8];

            try
            {
                using (var db = new MyDbContext())
                {
                    SqlParameter p1;
                    SqlParameter p2;
                    SqlParameter p3;

                    if (messageGroupID == null)
                    {
                        p1 = new SqlParameter("MessageGroupID", DBNull.Value);
                    }
                    else
                    {
                        p1 = new SqlParameter("MessageGroupID", messageGroupID);
                    }

                    if (string.IsNullOrEmpty(message))
                    {
                        p2 = new SqlParameter("Message", DBNull.Value);
                    }
                    else
                    {
                        p2 = new SqlParameter("Message", message);
                    }

                    if (activeStatus == null)
                    {
                        p3 = new SqlParameter("ActiveStatus", DBNull.Value);
                    }
                    else
                    {
                        p3 = new SqlParameter("ActiveStatus", activeStatus);
                    }
                    SqlParameter p4 = new SqlParameter("PageNbr", DBNull.Value);
                    SqlParameter p5 = new SqlParameter("PageSize", param.iDisplayLength);
                    SqlParameter p6 = new SqlParameter("FirstRec", param.iDisplayStart);
                    SqlParameter p7 = new SqlParameter("SortCol", param.iSortColumn);
                    SqlParameter p8 = new SqlParameter("TotalRecords", SqlDbType.Int);
                    p8.Direction = ParameterDirection.Output;

                    parameters[0] = p1;
                    parameters[1] = p2;
                    parameters[2] = p3;
                    parameters[3] = p4;
                    parameters[4] = p5;
                    parameters[5] = p6;
                    parameters[6] = p7;
                    parameters[7] = p8;

                    db.Configuration.ProxyCreationEnabled = false;
                    //db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.Database.SqlQuery<RmsMessageAction>("RMS_ListMessageAction " +
                                                                            "@MessageGroupID, @Message, @ActiveStatus, @PageNbr, @PageSize, @FirstRec, @SortCol, @TotalRecords OUTPUT"
                        , parameters);

                    listRmsMessages = new List<RmsMessageAction>(listOfType.ToList());

                    var sr = new MessageActionResult
                    {
                        IsSuccess =  true,
                        ListMessageActions = listRmsMessages,
                        TotalRecords = (int)parameters[7].Value
                    };
                    return sr;
                }

            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "MessageAction-Search failed. " + ex.Message, ex, true);

                return new MessageActionResult
                {
                    IsSuccess = false
                };
            }
        }

        public Result Delete(int? id)
        {
            if (id == null) throw new ArgumentNullException("MessageID");

            try
            {
                using (var db = new MyDbContext())
                {
                    var messsage = db.RmsMessages.Create();
                    messsage.MessageId = id.Value;
                    db.RmsMessages.Attach(messsage);
                    db.RmsMessages.Remove(messsage);
                    db.SaveChanges();

                    return new Result { IsSuccess = true };
                }
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "MessageAction-Delete failed. " + ex.Message, ex, true);

                return new Result { IsSuccess = false, ErrorMessage = ex.Message};
            }
        }

        #endregion


        #region Page : MessageEdit

        public InitMessageActionResult InitDataForMeesageEdit()
        {
            #region Message Group

            var mg = new List<RmsMessageGroup>();

            try
            {
                using (var db = new MyDbContext())
                {
                    var messageGroups =
                        db.RmsMessageGroups.Where(s => s.ActiveList == true).OrderBy(o => o.MessageType).ThenBy(o => o.MessageGroupName);
                    mg = messageGroups.ToList();
                }
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "InitDataForMeesageEdit failed. " + ex.Message, ex, true);
                
                return new InitMessageActionResult
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }

            string ddlMessageGroup = "<option value=\"\">Message Group</option>";
            string optgroup = "";
            foreach (var rmsMessageGroup in mg)
            {
                if (optgroup != rmsMessageGroup.MessageType)
                {
                    ddlMessageGroup += "<optgroup label=\"" + rmsMessageGroup.MessageType + " Message\">";
                    optgroup = rmsMessageGroup.MessageType;
                }
                ddlMessageGroup += "<option value=\"" + rmsMessageGroup.MessageGroupId + "\">" + rmsMessageGroup.MessageGroupName + "</option>";
            }

            #endregion

            #region Severity Level

            var sl = new List<RmsSeverityLevel>();

            try
            {
                using (var db = new MyDbContext())
                {
                    var severity = db.RmsSeverityLevels.Where(s => s.ActiveList == true).OrderBy(o => o.OrderList);
                    sl = severity.ToList();
                }
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "InitDataForMeesageEdit failed. " + ex.Message, ex, true);

                return new InitMessageActionResult
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }

            string ddlSeverityLevel = "<option value=\"\">Please Select</option>";
            foreach (var rmsSeverity in sl)
            {
                ddlSeverityLevel += "<option value=\"" + rmsSeverity.SeverityLevelId + "\">" + rmsSeverity.LevelName + "</option>";
            }

            #endregion

            var ret = new InitMessageActionResult
            {
                IsSuccess = true,
                MessageGroup = ddlMessageGroup,
                SeverityLevel = ddlSeverityLevel
            };

            return ret;
        }

        public MessageActionResult Get(int? id)
        {
            try
            {
                if (id == null) throw new ArgumentNullException("MessageID");
            }
            catch (ArgumentNullException ex)
            {
                new RMSWebException(this, "0500", "MessageEdit-Get failed. " + ex.Message, ex, true);
                throw;
            }
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var rmsMessage = db.RmsMessages.Find(id);

                    return new MessageActionResult
                    {
                        IsSuccess = true,
                        Message = rmsMessage
                    };
                }
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "MessageEdit-Get failed. " + ex.Message, ex, true);

                return new MessageActionResult
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public Result UpdateMessage(int? id, string m, int? messageGroupID, string message, int? severityLevelID, bool activeList, bool activeStatus)
        {
            try
            {
                if (messageGroupID == null) throw new ArgumentNullException("messageGroupID");
                if (string.IsNullOrEmpty(message)) throw new ArgumentNullException("message");

                if (m == "e" && id == null) throw new ArgumentNullException("id");

            }
            catch (ArgumentNullException ex)
            {
                new RMSWebException(this, "0500", "MessageEdit-UpdateMessage failed. " + ex.Message, ex, true);
                throw;
            }            
            
            // Add New Mode
            if (string.IsNullOrEmpty(m))
            {
                try
                {
                    using (var db = new MyDbContext())
                    {
                        var rmsMessage = db.RmsMessages.Create();
                        rmsMessage.MessageGroupId = messageGroupID;
                        rmsMessage.Message = message;
                        rmsMessage.SeverityLevelId = severityLevelID;
                        rmsMessage.ReadOnly = false;
                        rmsMessage.ActiveList = activeList;
                        rmsMessage.ActiveStatus = activeStatus;
                        db.RmsMessages.Add(rmsMessage);

                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    new RMSWebException(this, "0500", "MessageEdit-UpdateMessage failed. " + ex.Message, ex, true);

                    return new Result
                    {
                        IsSuccess = false,
                        ErrorMessage = ex.Message
                    };
                }
            }
            else if (m == "e")
            {
                try
                {
                    using (var db = new MyDbContext())
                    {
                        var rmsMessage = db.RmsMessages.Find(id);
                        rmsMessage.MessageGroupId = messageGroupID;
                        rmsMessage.Message = message;
                        rmsMessage.SeverityLevelId = severityLevelID;
                        rmsMessage.ActiveList = activeList;
                        rmsMessage.ActiveStatus = activeStatus;

                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    new RMSWebException(this, "0500", "MessageEdit-UpdateMessage failed. " + ex.Message, ex, true);
                    
                    return new Result
                    {
                        IsSuccess = false,
                        ErrorMessage = ex.Message
                    };
                }
            }
            else
            {
                new RMSWebException(this, "0500", "MessageEdit-UpdateMessage Missing Parameter. m=" + m, true);
                return new Result
                {
                    IsSuccess = false,
                    ErrorMessage = "Missing Parameter"
                };
            }

            return new Result
            {
                IsSuccess = true
            };
        }

        #endregion

    }
}
