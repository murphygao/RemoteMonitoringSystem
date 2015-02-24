using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Common.Exception;

namespace RMS.Agent.Helper
{
    public class Common
    {
        public static void ExtractLog(string query, string dirName, string fileName)
        {
            try
            {
                //query = "*[System[TimeCreated[@SystemTime >= '" + DateTime.Now.AddHours(-1).ToUniversalTime().ToString("o") + "']]]";

                //    "(Provider/@Name=\"AD FS 2.0 Auditing\") and " +
                //    "(TimeCreated/@SystemTime <= \"" + toDate.ToString("yyyy-MM-ddTHH:mm:ss") + "\") and " +
                //"(TimeCreated/@SystemTime >= " + DateTime.Now.AddHours(-10).ToString("o") + ")" +
                //"]]";
                //" and (TimeCreated/@SystemTime <= " + toDate.Ticks + ")]]";
                //" and TimeCreated[timediff(@SystemTime) <= 86400000]]]";  

                string fileNameFullPath = (dirName + @"\" + fileName).Replace(@"\\", @"\");
                //query = "*";

                if (File.Exists(fileNameFullPath))
                    File.Delete(fileNameFullPath);

                using (var logSession = new EventLogSession())
                {
                    logSession.ExportLogAndMessages("System", PathType.LogName, query, fileNameFullPath, true, CultureInfo.CurrentCulture);
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
