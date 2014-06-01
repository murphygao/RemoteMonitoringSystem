using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RMS.Common.Exception;
using SKAdapter;

namespace RMS.Centralize.WebService.Gateway
{
    public class ActionGateway
    {
        public ActionResult SendEmail(GatewayName gatewayName, string from, List<string> toList, string subject, string body)
        {
            try
            {
                if (toList == null || toList.Count == 0) return new ActionResult { IsSuccess = false, ErrorMessage = "Recipient cannot be null." };

                switch (gatewayName)
                {
                    case GatewayName.AIS_SKS:
                        return AIS_SKS_Email(from, toList, subject, body);
                        break;
                    case GatewayName.KTB_VTM:
                        break;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "SendEmail failed. " + ex.Message, ex, false);
            }
        }

        public ActionResult SendSMS(GatewayName gatewayName, string mobileNumber, string sender, string body)
        {
            try
            {
                if (string.IsNullOrEmpty(mobileNumber)) return new ActionResult { IsSuccess = false, ErrorMessage = "Mobile Number cannot be null." };

                switch (gatewayName)
                {
                    case GatewayName.AIS_SKS:
                        return AIS_SKS_SMS(mobileNumber, sender, body);
                        break;
                    case GatewayName.KTB_VTM:
                        break;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "SendSMS failed. " + ex.Message, ex, false);
            }
        }

        #region AIS Gateway

        private ActionResult AIS_SKS_Email(string from, List<string> toList, string subject, string body)
        {
            try
            {
                using (AisServiceAdapter sksAdapter = new AisServiceAdapter("-", "-"))
                {
                    List<MailAddress> lMailAddresses = new List<MailAddress>();
                    foreach (var toEmail in toList)
                    {
                        lMailAddresses.Add(new MailAddress(toEmail));
                    }

                    ServiceAdapterResult result = sksAdapter.SendEmail(new MailAddress(from), lMailAddresses.ToArray(), subject, body);

                    return new ActionResult
                    {
                        IsSuccess = result.Success,
                        ErrorCode = result.ErrorCode,
                        ErrorMessage = result.ErrorCode
                    };
                }
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "AIS_SKS_Email failed. " + ex.Message, ex, true);

                return new ActionResult
                {
                    IsSuccess = false,
                    ErrorCode = "",
                    ErrorMessage = ex.Message,
                    InnerException = ex
                };
            }
        }

        private ActionResult AIS_SKS_SMS(string mobileNumber, string sender, string body)
        {
            try
            {
                using (AisServiceAdapter sksAdapter = new AisServiceAdapter("-", "-"))
                {

                    List<MailAddress> lMailAddresses = new List<MailAddress>();

                    ServiceAdapterResult result = sksAdapter.SmsGatewaySend(sender, mobileNumber, body);

                    return new ActionResult
                    {
                        IsSuccess = result.Success,
                        ErrorCode = result.ErrorCode,
                        ErrorMessage = result.ErrorCode
                    };
                }
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "AIS_SKS_SMS failed. " + ex.Message, ex, true);

                return new ActionResult
                {
                    IsSuccess = false,
                    ErrorCode = "",
                    ErrorMessage = ex.Message,
                    InnerException = ex
                };
            }
        }


        #endregion
    }

    public class ActionResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public Exception InnerException { get; set; }

    }

    public enum GatewayName
    {
        AIS_SKS,
        KTB_VTM
    }
}
