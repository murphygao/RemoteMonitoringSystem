using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using RMS.Centralize.DAL;
using Timer = System.Timers.Timer;

namespace RMS.Centralize.Engine.MonitoringEngine
{
    class Program
    {
        private static string WebMonitoringEngineURL;
        private static string appGuid;
        private static Timer timer;
        private static int interval;

        static void Main(string[] args)
        {
            try
            {
                SetAppGUID();

                using (Mutex mutex = new Mutex(false, "Global\\" + appGuid))
                {
                    if (!mutex.WaitOne(0, false))
                    {
                        Environment.Exit(1);
                    }

                    WebMonitoringEngineURL = ConfigurationManager.AppSettings["WEB_MONITORING_ENGINE_URL"];

                    timer = new Timer();
                    SetInterval();  //set interval of checking here
                    timer.Elapsed += timer_Elapsed;
                    timer.Start();

                    Console.Read();
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                //Environment.Exit(1);
            }
        }

        private static void SetInterval()
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

                if (interval != tempInterval)
                {
                    interval = tempInterval;
                    timer.Interval = interval*1000;
                }

            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static void CallEngine()
        {
            //WebRequest webRequest = WebRequest.Create(WebMonitoringEngineURL);
            //WebResponse webResp = webRequest.GetResponse();

            var webpage = new WebClient();
            string s = webpage.DownloadString(new Uri(WebMonitoringEngineURL));
            s = s.Substring(0,s.Length-9).Substring(68);
            Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " : Finished -> " + s);
        }

        private static void SetAppGUID()
        {
            var assembly = typeof(Program).Assembly;
            var attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
            appGuid = attribute.Value;
        }
        static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine( DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " : Started");
            CallEngine();
            SetInterval();
        }
    }
}
