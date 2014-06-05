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
    public class RMSDebugLog : BaseException
    {
        public RMSDebugLog()
        {
        }

        public RMSDebugLog(string sMessage, bool bLog) : base(sMessage, bLog)
        {
        }

        public RMSDebugLog(string sMessage, System.Exception oInnerException, bool bLog) : base(sMessage, oInnerException, bLog)
        {
        }

        public RMSDebugLog(object oSource, string sCode, string sMessage, bool bLog) : base(oSource, sCode, sMessage, bLog)
        {
        }

        public RMSDebugLog(object oSource, string sCode, string sMessage, System.Exception oInnerException, bool bLog) : base(oSource, sCode, sMessage, oInnerException, bLog)
        {
        }

        public RMSDebugLog(object oSource, string sCode, string sMessage, System.Exception oInnerException, bool bLog, bool bThrowSoap) : base(oSource, sCode, sMessage, oInnerException, bLog, bThrowSoap)
        {
        }

        protected RMSDebugLog(SerializationInfo oInfo, StreamingContext oContext)
            : base(oInfo, oContext)
        {
        }

        protected override void Dump(string sMessage)
        {
            bool debugEnable = false;
            try
            {
                debugEnable = Convert.ToBoolean(ConfigurationManager.AppSettings["RMS.DebugLogEnable"] ?? "false");
            }
            catch
            {
                debugEnable = false;
            }
            if (debugEnable)
                base.Dump(sMessage);
        }

        public override void SetLogFileName()
        {
            try
            {
                fileName = (string)ConfigurationManager.AppSettings["RMS.DebugLogFile"] ?? @"D:\App\RMS\Logs\DebugLog.txt";
            }
            catch
            {
                fileName = @"D:\App\RMS\Logs\DebugLog.txt";
            }
            //fileName = HttpContext.Current.Server.MapPath(fileName);
        }
    }
}
