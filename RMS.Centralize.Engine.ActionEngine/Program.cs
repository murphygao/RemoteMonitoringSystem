using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using RMS.Centralize.DAL;
using RMS.Common.Exception;
using Timer = System.Timers.Timer;

namespace RMS.Centralize.Engine.ActionEngine
{
    class Program
    {
        private static string WebActionEngineURL;
        private static string appGuid;
        private static Timer timer;
        private static int interval = 3600;
        private static Timer refreshConfigTimer;
        private static int refreshInterval = 60;

        static void Main(string[] args)
        {
            try
            {
                SetAppGUID();
                var sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                var mutexsecurity = new MutexSecurity();
                mutexsecurity.AddAccessRule(new MutexAccessRule(sid, MutexRights.FullControl, AccessControlType.Allow));
                mutexsecurity.AddAccessRule(new MutexAccessRule(sid, MutexRights.ChangePermissions, AccessControlType.Deny));
                mutexsecurity.AddAccessRule(new MutexAccessRule(sid, MutexRights.Delete, AccessControlType.Deny));
                bool created;
                using (Mutex mutex = new Mutex(false, "Global\\" + appGuid, out created, mutexsecurity))
                {
                    if (!created)
                    {
                        Environment.Exit(1);
                    }

                    WebActionEngineURL = ConfigurationManager.AppSettings["RMS.WEB_ACTION_ENGINE_URL"];

                    #region First Start

                    Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " : Started");
                    CallEngine();

                    #endregion

                    timer = new Timer();
                    timer.Interval = interval * 1000; // Default
                    SetInterval();  //set interval of checking here
                    timer.Elapsed += timer_Elapsed;
                    timer.Start();

                    refreshConfigTimer = new Timer();
                    refreshConfigTimer.Interval = refreshInterval * 1000;
                    refreshConfigTimer.Elapsed += refreshConfigTimer_Elapsed;
                    refreshConfigTimer.Start();

                    SetInterval();

                    while (Console.ReadLine() != "exit")
                    {
                        Console.WriteLine("Exit this application, type: exit");
                    };

                }

            }
            catch (Exception ex)
            {
                new RMSAppException("Main failed. " + ex.Message, ex, true);
                Console.WriteLine("Engine stopped. Main Application failed. " + ex.Message);
                Console.WriteLine("");
                Console.WriteLine("Please contact administrator. This application will be automatically closed within 60 seconds.");
                System.Threading.Thread.Sleep(1000 * 60);
            }
            finally
            {
                Environment.Exit(1);
            }
        }

        private static void SetInterval()
        {
            try
            {
                int tempInterval = 3600;

                using (var db = new MyDbContext())
                {
                    var config = db.RmsSystemConfigs.Find("ActionEngine.Interval");
                    if (config != null)
                    {
                        if (!string.IsNullOrEmpty(config.Value))
                        {
                            tempInterval = Convert.ToInt32(config.Value);
                        }
                        else
                        {
                            tempInterval = Convert.ToInt32(config.DefaultValue);
                        }
                    }
                }

                if (tempInterval < 60) tempInterval = 60;

                if (interval != tempInterval)
                {
                    interval = tempInterval;
                    timer.Interval = interval * 1000;
                }

                if (interval < refreshInterval)
                {
                    refreshInterval = interval;
                    refreshConfigTimer.Interval = refreshInterval * 1000;
                }
                else if (interval > refreshInterval)
                {
                    refreshInterval = interval;
                    if (refreshInterval > 60) refreshInterval = 60;

                    if (refreshConfigTimer.Interval != (refreshInterval * 1000))
                        refreshConfigTimer.Interval = refreshInterval * 1000;
                }


            }
            catch (Exception ex)
            {
                throw new RMSAppException("SetInterval failed. " + ex.Message, ex, false);
            }
        }

        private static void CallEngine()
        {
            try
            {
                //WebRequest webRequest = WebRequest.Create(WebMonitoringEngineURL);
                //WebResponse webResp = webRequest.GetResponse();

                var webpage = new WebDownload();

                string s = webpage.DownloadString(new Uri(WebActionEngineURL));
                s = s.Substring(0, s.Length - 9).Substring(68);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " : Finished -> " + System.Net.WebUtility.HtmlDecode(s));
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " : Error -> " + ex.Message);
                new RMSAppException("CallEngine failed. WebActionEngineURL = " + WebActionEngineURL + ", " + ex.Message, ex, true);
            }
        }

        public class WebDownload : WebClient
        {
            /// <summary>
            /// Time in milliseconds
            /// </summary>
            public int Timeout { get; set; }

            public WebDownload() : this(5) { }

            public WebDownload(int timeoutMinute)
            {
                this.Timeout = timeoutMinute * 1000 * 60;
            }

            protected override WebRequest GetWebRequest(Uri address)
            {
                var request = base.GetWebRequest(address);
                if (request != null)
                {
                    request.Timeout = this.Timeout;
                }
                return request;
            }
        }

        private static void SetAppGUID()
        {
            try
            {
                var assembly = typeof(Program).Assembly;
                var attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
                appGuid = attribute.Value;
            }
            catch (Exception ex)
            {
                throw new RMSAppException("SetAppGUID failed. " + ex.Message, ex, false);
            }
        }

        static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " : Started");
                CallEngine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " : timer_Elapsed failed. " + ex.Message);
                throw new RMSAppException("timer_Elapsed failed. " + ex.Message, ex, true);
            }
        }
        static void refreshConfigTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                SetInterval();
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " : refreshConfigTimer_Elapsed failed. " + ex.Message);
                throw new RMSAppException("refreshConfigTimer_Elapsed failed. " + ex.Message, ex, true);
            }
        }
    }
}
