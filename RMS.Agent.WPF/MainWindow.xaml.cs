using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using RMS.Agent.BSL.Monitoring;
using RMS.Agent.Helper;
using RMS.Agent.Model;
using RMS.Agent.Proxy.ClientProxy;
using RMS.Agent.WCF;
using MessageBox = System.Windows.MessageBox;

namespace RMS.Agent.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceHost host = null;
        private System.Windows.Forms.NotifyIcon ni;
        public static ListEventLogs logs = new ListEventLogs();
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;

        private string historyFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\logs\history.txt";
        private string tempEventFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\logs\TempEvent.txt";
        private bool processingTempEvent = false;
        private string currentState = string.Empty;

        private string maFilePath = ConfigurationManager.AppSettings["RMS.MA_FILE_PATH"];
        private string clientCode = ConfigurationManager.AppSettings["CLIENT_CODE"];


        public MainWindow()
        {
            try
            {
                InitializeComponent();

                ni = new NotifyIcon();

                ni.Icon = Properties.Resources.monitoring;

                ni.Visible = true;
                ni.DoubleClick +=
                    delegate(object sender, EventArgs args)
                    {
                        this.Show();
                        this.WindowState = WindowState.Normal;
                    };

                lblExecPath.Content = Assembly.GetExecutingAssembly().Location;

                var logs = Generate();

                InitResultHistory();


                // MA State?
                lblState.Content = File.Exists(maFilePath) ? "Maintenance" : "Normal";
                currentState = lblState.Content.ToString();

                dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
            }
            catch (Exception ex)
            {
                logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Error", Message = "MainWindow errors", Detail = ex.Message });
            }
        }
        
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                logs.Add(new EventLog{EventDateTime = DateTime.Now, EventType = "Agent", Message = "Application Startup", Detail = ""});
                dgLogs.ItemsSource = null;
                dgLogs.ItemsSource = logs;
                btnStart_Click(null, null);
            }
            catch (Exception ex)
            {
                logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Error", Message = "Window_Loaded errors", Detail = ex.Message });
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnStart.IsEnabled = false;
                btnStop.IsEnabled = true;
                logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Agent", Message = "Application Started", Detail = "" });
                dgLogs.ItemsSource = null;
                dgLogs.ItemsSource = logs;
                StartService();
                dispatcherTimer.Start();
            }
            catch (Exception ex)
            {
                logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Error", Message = "btnStart_Click errors", Detail = ex.Message });
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnStart.IsEnabled = true;
                btnStop.IsEnabled = false;
                logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Agent", Message = "Application Stopped", Detail = "" });
                dgLogs.ItemsSource = null;
                dgLogs.ItemsSource = logs;
                StopService();
                dispatcherTimer.Stop();
            }
            catch (Exception ex)
            {
                logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Error", Message = "btnStop_Click errors", Detail = ex.Message });
            }
        }

        private void StartService()
        {
            try
            {
                string ip_port = ConfigurationManager.AppSettings["RMS.LOCAL_IP"];
                string netTCP = "net.tcp://" + ip_port + "/AgentNetTcp";


                host = new ServiceHost(typeof(RMS.Agent.WCF.AgentService));

                //host.AddServiceEndpoint(typeof(RMS.Agent.WCF.IAgentService),
                //    new BasicHttpBinding(), "http://localhost:8080/agent/basic");

                //host.AddServiceEndpoint(typeof(RMS.Agent.WCF.IAgentService),
                //    new WSHttpBinding(), "http://localhost:8080/agent/wsAddress");

                host.AddServiceEndpoint(typeof(RMS.Agent.WCF.IAgentService),
                    new NetTcpBinding(), netTCP);

                host.Open();
                lblStatus.Content = "Started";
            }
            catch (Exception ex)
            {
                host.Abort();
                lblStatus.Content = "Stopped";
                logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Error", Message = "StartService errors", Detail = ex.Message });
                btnStart.IsEnabled = true;
                btnStop.IsEnabled = false;
            }
        }

        private void StopService()
        {
            try
            {
                if (host.State == CommunicationState.Opened)
                    host.Close();
                lblStatus.Content = "Stopped";
            }
            catch (Exception ex)
            {
                logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Error", Message = "StopService errors", Detail = ex.Message });
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Agent", Message = "Application Closing", Detail = "" });
                dgLogs.ItemsSource = null;
                dgLogs.ItemsSource = logs;

                ni.Visible = false;

                if (host.State == CommunicationState.Opened)
                    host.Close();

                string strResultList = Serializer.XML.SerializeObject(logs);
                using (TextWriter tw = new StreamWriter(historyFile, false)) // Create & open the file
                {
                    tw.Write(strResultList);
                    tw.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (File.Exists(maFilePath))
            {
                if (currentState == lblState.Content.ToString())
                {
                    MonitoringService ms = new MonitoringService();
                    ms.SetMonitoringState(clientCode, ClientState.Maintenance);
                }

                lblState.Content = "Maintenance";
            }
            else
            {
                if (currentState == lblState.Content.ToString())
                {
                    MonitoringService ms = new MonitoringService();
                    ms.SetMonitoringState(clientCode, ClientState.Normal);
                }
                lblState.Content = "Normal";
            }

            if (processingTempEvent) return;

            try
            {
                processingTempEvent = true;

                lock (AgentService._lock)
                {
                    if (File.Exists(tempEventFile))
                    {

                        string strResultList = string.Empty;

                        using (FileStream fs = File.OpenRead(tempEventFile))
                        {
                            using (TextReader tr = new StreamReader(fs))
                            {
                                strResultList = tr.ReadToEnd();
                                tr.Close();
                            }
                            fs.Close();

                        }
                        if (!string.IsNullOrEmpty(strResultList))
                        {
                            logs.AddRange(Serializer.XML.DeserializeObject<ListEventLogs>(strResultList));
                            File.Delete(tempEventFile);
                            dgLogs.ItemsSource = null;
                            dgLogs.ItemsSource = logs;
                        }
                    }
                }
            }
            finally
            {
                processingTempEvent = false;
            }
        }


        private IEnumerable<EventLog> Generate()
        {
            for (int i = -1000; i < 0; i=i+50)
            {
                DateTime d = DateTime.Now.AddMinutes(i);

                EventLog log = new EventLog
                {
                    EventDateTime = d,
                    EventType = "General",
                    Message = "Testing Message",
                    Detail = "Description"
                };

                logs.Add(log);
            }
            return logs;
        }

        private void InitResultHistory()
        {
            try
            {
                string fileName = historyFile;

                if (!Directory.Exists(System.IO.Path.GetDirectoryName(fileName)))
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(fileName));

                if (File.Exists(fileName))
                {
                    string strResultList = string.Empty;

                    using (FileStream fs = File.OpenRead(fileName))
                    {
                        using (TextReader tr = new StreamReader(fs))
                        {
                            strResultList = tr.ReadToEnd();
                            tr.Close();
                        }
                        fs.Close();

                    }
                    if (!string.IsNullOrEmpty(strResultList))
                    {
                        logs = Serializer.XML.DeserializeObject<ListEventLogs>(strResultList);
                    }

                    dgLogs.ItemsSource = null;
                    dgLogs.ItemsSource = logs;

                }
            }
            catch (Exception ex)
            {
                logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Error", Message = "InitResultHistory errors", Detail = ex.Message });
            }
        }

    }
}
