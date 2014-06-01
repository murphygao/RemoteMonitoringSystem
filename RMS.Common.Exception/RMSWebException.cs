using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ESN.Common.Exception;

namespace RMS.Common.Exception
{
    public class RMSWebException : BaseException
    {
        public RMSWebException()
        {
        }

        public RMSWebException(string sMessage, bool bLog) : base(sMessage, bLog)
        {
        }

        public RMSWebException(string sMessage, System.Exception oInnerException, bool bLog) : base(sMessage, oInnerException, bLog)
        {
        }

        public RMSWebException(object oSource, string sCode, string sMessage, bool bLog) : base(oSource, sCode, sMessage, bLog)
        {
        }

        public RMSWebException(object oSource, string sCode, string sMessage, System.Exception oInnerException, bool bLog) : base(oSource, sCode, sMessage, oInnerException, bLog)
        {
        }

        public RMSWebException(object oSource, string sCode, string sMessage, System.Exception oInnerException, bool bLog, bool bThrowSoap) : base(oSource, sCode, sMessage, oInnerException, bLog, bThrowSoap)
        {
        }

        protected RMSWebException(SerializationInfo oInfo, StreamingContext oContext) : base(oInfo, oContext)
        {
        }

        public override void SetLogFileName()
        {
            try
            {
                fileName = (string)ConfigurationManager.AppSettings["RMS.ErrorLogFile"] ?? @"D:\App\RMS\Logs\ErrorLog.txt";
            }
            catch
            {
                fileName = @"D:\App\RMS\Logs\ErrorLog.txt";
            }
            //fileName = HttpContext.Current.Server.MapPath(fileName);
        }
    }
}
