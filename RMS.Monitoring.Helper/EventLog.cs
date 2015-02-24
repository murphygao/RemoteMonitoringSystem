using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Monitoring.Helper
{
    public class EventLog
    {
        public static string ExtractLogByPeriod(string directoryFullPath, string clientCode, int hourFromNow)
        {
            string query = "*[System[TimeCreated[@SystemTime >= '" + DateTime.Now.AddHours(-1 * hourFromNow).ToUniversalTime().ToString("o") + "']]]";
            //    "(Provider/@Name=\"AD FS 2.0 Auditing\") and " +
            //    "(TimeCreated/@SystemTime <= \"" + toDate.ToString("yyyy-MM-ddTHH:mm:ss") + "\") and " +
            //"(TimeCreated/@SystemTime >= " + DateTime.Now.AddHours(-10).ToString("o") + ")" +
            //"]]";
            //" and (TimeCreated/@SystemTime <= " + toDate.Ticks + ")]]";
            //" and TimeCreated[timediff(@SystemTime) <= 86400000]]]";  

            string eventLogFile = clientCode + ".EventLog." + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss", new CultureInfo("en-AU"));

            if (!Directory.Exists(directoryFullPath))
            {
                Directory.CreateDirectory(directoryFullPath);
            }

            string fileNameWithFullPath = (directoryFullPath + @"\" + eventLogFile + ".evtx").Replace(@"\\", @"\");
            using (var logSession = new EventLogSession())
            {
                if (File.Exists(fileNameWithFullPath))
                    File.Delete(fileNameWithFullPath);
                logSession.ExportLogAndMessages("System", PathType.LogName, query, fileNameWithFullPath, true, CultureInfo.CurrentCulture);
            }

            return fileNameWithFullPath;
        }

    }
}
