using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using RMS.Common.Exception;

namespace RMS.Agent.Helper
{
    public class Common
    {
        public static void ExtractLog(string query, string dirName, string fileName)
        {
            string fileNameFullPath = string.Empty;

            try
            {
                //query = "*[System[TimeCreated[@SystemTime >= '" + DateTime.Now.AddHours(-1).ToUniversalTime().ToString("o") + "']]]";

                //    "(Provider/@Name=\"AD FS 2.0 Auditing\") and " +
                //    "(TimeCreated/@SystemTime <= \"" + toDate.ToString("yyyy-MM-ddTHH:mm:ss") + "\") and " +
                //"(TimeCreated/@SystemTime >= " + DateTime.Now.AddHours(-10).ToString("o") + ")" +
                //"]]";
                //" and (TimeCreated/@SystemTime <= " + toDate.Ticks + ")]]";
                //" and TimeCreated[timediff(@SystemTime) <= 86400000]]]";  

                fileNameFullPath = (dirName + @"\" + fileName).Replace(@"\\", @"\");
                //query = "*";

                if (File.Exists(fileNameFullPath))
                    File.Delete(fileNameFullPath);

                using (var logSession = new EventLogSession())
                {
                    logSession.ExportLogAndMessages("Application", PathType.LogName, query, fileNameFullPath, true, CultureInfo.CurrentCulture);
                    logSession.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new RMSAppException("ExtractLog failed. " + ex.Message, ex, false);
            }
        }

        public static void ExtractLog(string query, string dirName, string fileName, string serverName, string domainName, string userName, string password)
        {
            string fileNameFullPath = string.Empty;

            try
            {

                fileNameFullPath = (dirName + @"\" + fileName).Replace(@"\\", @"\");
                //query = "*";

                if (File.Exists(fileNameFullPath))
                    File.Delete(fileNameFullPath);

                var secureString = new SecureString();
                password.ToCharArray().ToList().ForEach(p => secureString.AppendChar(p));

                using (var logSession = new EventLogSession(serverName, domainName, userName, secureString, SessionAuthentication.Default))
                {
                    logSession.ExportLogAndMessages("Application", PathType.LogName, query, fileNameFullPath, true, CultureInfo.CurrentCulture);
                    logSession.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new RMSAppException("ExtractLog failed. " + ex.Message, ex, false);
            }
        }

    }
}
