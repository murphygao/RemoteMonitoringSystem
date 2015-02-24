using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using ESN.Common.Service;

namespace RMS.Adapter.KTB
{
    public class VTMAdapter : IDisposable
    {
        private bool disposed = false;
        public Result SendEmail(MailAddress from, List<MailAddress> lTo, string subject, string body, IEnumerable<Attachment> lAttachFiles, bool isHtml = true)
        {
            try
            {

                if (from == null || string.IsNullOrEmpty(from.Address))
                    from = new MailAddress(ConfigurationManager.AppSettings["RMS.KTB.SMTP.From"]);
                if (string.IsNullOrEmpty(from.Address)) throw new ArgumentNullException("from", "Please check database and Web.config > appSettings > RMS.KTB.SMTP.From ");

                if (isHtml) body = body.Replace(Environment.NewLine, "<br/>");
                var emailService = new SendEmailService();
                var result = emailService.SendEmail(@from, lTo, subject, body, lAttachFiles, isHtml);
                if (result.IsSuccess)
                {
                    return new Result {IsSuccess = true};
                }
                else
                {
                    return new Result
                    {
                        IsSuccess = false,
                        ErrorMessage = result.ErrorMessage
                    };
                }

            }
            catch (Exception ex)
            {
                return new Result
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from">This is the phone number which appears as sender of the SMS. If it is not given, default value will be KTB short code number.</param>
        /// <param name="lTo">This is the list of the mobile phone numbers which are the destination of the SMS.</param>
        /// <param name="body">This is content of the SMS message. It must be Unicode for Thai language.</param>
        /// <param name="sender">Sender name that appears as from number in mobile phone. Sender name can be text. If it is specified, it’s value will replace value of msg_from at mobile phone.</param>
        /// <param name="lang">This flag define language for SMS. Default value is ‘E’ for English when it is not given. E for English, T for Thai</param>
        /// <returns>Result</returns>
        public Result SendSMS(string from, List<string> lTo, string body, string sender, string referenceNo, string lang = "T")
        {
            try
            {

                if (lTo == null || lTo.Count == 0) throw new ArgumentNullException("lTo");
                if (string.IsNullOrEmpty(body)) throw new ArgumentNullException("body");

                string channelID = ConfigurationManager.AppSettings["RMS.KTB.SMS.ChannelID"];
                if (string.IsNullOrEmpty(channelID)) throw new ArgumentNullException("channelID", "Please check Web.config > appSettings > RMS.KTB.SMS.ChannelID ");

                if (string.IsNullOrEmpty(referenceNo)) throw new ArgumentNullException("referenceNo");

                if (string.IsNullOrEmpty(from))
                    from = ConfigurationManager.AppSettings["RMS.KTB.SMS.From"];

                if (string.IsNullOrEmpty(sender))
                    sender = ConfigurationManager.AppSettings["RMS.KTB.SMS.Sender"];
                if (string.IsNullOrEmpty(sender)) throw new ArgumentNullException("sender", "Please check Web.config > appSettings > RMS.KTB.SMS.Sender ");

                if (lang != "T" && lang != "E")
                {
                    lang = "T";
                }
                
                sendSMS_Req smsReq = new sendSMS_Req();

                smsReq.channel_id = channelID;
                smsReq.reference_no = referenceNo;

                List<SmsList_Type> lsmSmsListTypes = new List<SmsList_Type>();
                foreach (var to in lTo)
                {
                    SmsList_Type smsListType = new SmsList_Type();
                    smsListType.msg_from = from;
                    smsListType.msg_to = to;
                    smsListType.content = body;
                    smsListType.lang = lang;
                    smsListType.sender_name = sender;

                    lsmSmsListTypes.Add(smsListType);
                }

                smsReq.smslist = lsmSmsListTypes.ToArray();


                MobileMKTAdaptorSoapClient MKTservice = new KTBMobileMKTAdaptorService().Service;
                sendSMS_Resp smsResp = MKTservice.sendSMS(smsReq);

                /*
                respCode	respMsg	                                                respDetail
                MMSV500	    Success	                                                transaction_id
                MMSV501	    Invalid Parameter, type mismatch	                    parameter id
                MMSV502	    Invalid Parameter, missing variable	                    parameter id
                MMSV503	    Invalid Parameter, length exceed	                    parameter id
                MMSV504 	Invalid Parameter, unknow parameter value	            parameter id
                MMSV505	    Cannot call SMS Management System	                    exception string
                MMSV506	    Invalid channel	
                MMSV507	    Undefined exception occurs while service is processing	exception string
                MMSV508	    Failure at SMS Management System	
                MMSV509	    Cannot verify channel id	                            exception string
                MMSV510	    Not enough quota for sending SMS	
                MMSV511	    Duplicate reference no.	
                */

                if (smsResp != null)
                {
                    if (smsResp.respCode == "MMSV500")
                    {
                        return new Result
                        {
                            IsSuccess = true,
                            ErrorMessage = referenceNo + ", " + smsResp.respDetail
                        };
                    }
                    else
                    {
                        return new Result
                        {
                            IsSuccess = false,
                            ErrorCode = smsResp.respCode,
                            ErrorMessage = referenceNo + ", " + smsResp.respDetail,
                        };
                    }
                }
                else
                {
                    return new Result
                    {
                        IsSuccess = false,
                        ErrorMessage = "sendSMS_Resp is null."
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                }
                // There are no unmanaged resources to release, but
                // if we add them, they need to be released here.
            }
            disposed = true;

            // If it is available, make the call to the
            // base class's Dispose(Boolean) method
            //base.Dispose(disposing);
        }
    }

    public class Result
    {
        public bool IsSuccess { get; set; }

        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public string Detail { get; set; }

    }
}
