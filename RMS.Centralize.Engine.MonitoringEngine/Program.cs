using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mime;
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

namespace RMS.Centralize.Engine.MonitoringEngine
{
    class Program
    {
        private static string appGuid;

        private static string monitoringEngineURL;
        private static Timer timerME;
        private static int intervalME = 60;

        private static string websiteMonitoringEngineURL;
        private static Timer timerWME;
        private static int intervalWME = 60;

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

                    monitoringEngineURL = ConfigurationManager.AppSettings["RMS.MonitoringWebEngineURL"];
                    websiteMonitoringEngineURL = ConfigurationManager.AppSettings["RMS.WebsiteMonitoringWebEngineURL"];

                    timerME = new Timer();
                    timerWME = new Timer();
                    refreshConfigTimer = new Timer();


                    

                    #region First Start

                    Task task1 = Task.Factory.StartNew(() => CallMonitoringEngine());

                    Task task2 = Task.Factory.StartNew(() => CallWebsiteMonitoringEngine());
                    
                    Task.WaitAll(task1, task2);

                    #endregion

                    #region Monitoring Engine Initialize

                    timerME.Interval = intervalME * 1000; // Default
                    SetMonitoringInterval(); //set intervalME of checking here
                    timerME.Elapsed += TimerME_Elapsed;
                    timerME.Start();

                    #endregion

                    #region Website Monitoring Engine Initialize

                    timerWME.Interval = intervalWME * 1000; // Default
                    SetWebsiteMonitoringInterval(); //set intervalWME of checking here
                    timerWME.Elapsed += TimerWME_Elapsed;
                    timerWME.Start();

                    #endregion

                    #region Configuration Timer Initialize

                    refreshConfigTimer.Interval = refreshInterval*1000;
                    refreshConfigTimer.Elapsed += refreshConfigTimer_Elapsed;
                    refreshConfigTimer.Start();

                    #endregion

                    SetMonitoringInterval();

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

        private static void SetMonitoringInterval()
        {
            try
            {
                int tempInterval = 30;

                using (var db = new MyDbContext())
                {
                    var config = db.RmsSystemConfigs.Find("MonitoringEngineInterval");
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

                if (tempInterval < 15) tempInterval = 15;

                if (intervalME != tempInterval)
                {
                    intervalME = tempInterval;
                    timerME.Interval = intervalME * 1000;
                }

            }
            catch (Exception ex)
            {
                throw new RMSAppException("SetMonitoringInterval failed. " + ex.Message, ex, false);
            }
        }
        private static void SetWebsiteMonitoringInterval()
        {
            try
            {
                int tempInterval = 60;

                using (var db = new MyDbContext())
                {
                    var config = db.RmsSystemConfigs.Find("WebsiteMonitoringEngine.Interval");
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

                if (intervalWME != tempInterval)
                {
                    intervalWME = tempInterval;
                    timerWME.Interval = intervalWME * 1000;
                }

            }
            catch (Exception ex)
            {
                throw new RMSAppException("SetWebsiteMonitoringInterval failed. " + ex.Message, ex, false);
            }
        }

        private static void CallMonitoringEngine()
        {
            try
            {
                //WebRequest webRequest = WebRequest.Create(monitoringEngineURL);
                //WebResponse webResp = webRequest.GetResponse();
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " : Started Monitoring");

                var webpage = new WebDownload();
                
                string s = webpage.DownloadString(new Uri(monitoringEngineURL));
                s = s.Substring(0,s.Length-9).Substring(68);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " : Finished Monitoring -> " + s);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " : Error Monitoring -> " + ex.Message);
                new RMSAppException("CallMonitoringEngine failed. monitoringEngineURL = " + monitoringEngineURL + ", " + ex.Message, ex, true);
            }
        }
        private static void CallWebsiteMonitoringEngine()
        {
            try
            {
                if (ConfigurationManager.AppSettings["RMS.WebsiteMonitoringEnable"] == null ||
                    !Convert.ToBoolean(ConfigurationManager.AppSettings["RMS.WebsiteMonitoringEnable"])) return;

                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " : Started Website Monitoring");

                var webpage = new WebDownload();

                string s = webpage.DownloadString(new Uri(websiteMonitoringEngineURL));
                s = s.Substring(0, s.Length - 9).Substring(68);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " : Finished Website Monitoring -> " + s);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " : Error Website Monitoring -> " + ex.Message);
                new RMSAppException("CallWebsiteMonitoringEngine failed. websiteMonitoringEngineURL = " + websiteMonitoringEngineURL + ", " + ex.Message, ex, true);
            }
        }
   
        public class WebDownload : WebClient
        {
            /// <summary>
            /// Time in milliseconds
            /// </summary>
            public int Timeout { get; set; }

            public WebDownload() : this(10) { }

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

        static void TimerME_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                CallMonitoringEngine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " : TimerME_Elapsed failed. " + ex.Message);
                throw new RMSAppException("TimerME_Elapsed failed. " + ex.Message, ex, true);
            }
        }
        static void TimerWME_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                CallWebsiteMonitoringEngine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " : TimerWME_Elapsed failed. " + ex.Message);
                throw new RMSAppException("TimerWME_Elapsed failed. " + ex.Message, ex, true);
            }
        }
        
        static void refreshConfigTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                SetMonitoringInterval();
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " : refreshConfigTimer_Elapsed failed. " + ex.Message);
                throw new RMSAppException("refreshConfigTimer_Elapsed failed. " + ex.Message, ex, true);
            }
        }
    }
}
