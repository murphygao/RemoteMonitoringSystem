using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ESN.Common.Exception;

namespace RMS.Common.Exception
{
    public class RMSLicenseException : BaseException
    {
        public RMSLicenseException()
        {
        }

        public RMSLicenseException(string sMessage, bool bLog) : base(sMessage, bLog)
        {
        }

        public RMSLicenseException(string sMessage, System.Exception oInnerException, bool bLog) : base(sMessage, oInnerException, bLog)
        {
        }

        public RMSLicenseException(object oSource, string sCode, string sMessage, bool bLog) : base(oSource, sCode, sMessage, bLog)
        {
        }

        public RMSLicenseException(object oSource, string sCode, string sMessage, System.Exception oInnerException, bool bLog) : base(oSource, sCode, sMessage, oInnerException, bLog)
        {
        }

        public RMSLicenseException(object oSource, string sCode, string sMessage, System.Exception oInnerException, bool bLog, bool bThrowSoap) : base(oSource, sCode, sMessage, oInnerException, bLog, bThrowSoap)
        {
        }

        protected RMSLicenseException(SerializationInfo oInfo, StreamingContext oContext)
            : base(oInfo, oContext)
        {
        }

        public override void SetLogFileName()
        {
            try
            {
                fileName = (string)ConfigurationManager.AppSettings["RMS.LicenseLogFile"] ?? @"D:\App\RMS\Logs\LicenseLog.txt";
            }
            catch
            {
                fileName = @"D:\App\RMS\Logs\LicenseLog.txt";
            }
            //fileName = HttpContext.Current.Server.MapPath(fileName);
        }
    }
}
