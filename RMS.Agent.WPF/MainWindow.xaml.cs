using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
using ESN.AutoUpdate.API;
using RMS.Agent.BSL.AutoUpate;
using RMS.Agent.Helper;
using RMS.Agent.Model;
using RMS.Agent.Proxy;
using RMS.Agent.Proxy.AutoUpdateProxy;
using RMS.Agent.Proxy.ClientProxy;
using RMS.Agent.WCF;
using RMS.Common.Exception;
using EventLog = RMS.Agent.Model.EventLog;
using MessageBox = System.Windows.MessageBox;
using MonitoringService = RMS.Agent.BSL.Monitoring.MonitoringService;

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

        public static string applicationStartupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        private string historyFile = applicationStartupPath + @"\logs\history.txt";
        private string tempEventFile = applicationStartupPath + @"\logs\TempEvent.txt";
        private bool processingTempEvent = false;
        private string currentStateOnServer = string.Empty;

        private string maFilePath = ConfigurationManager.AppSettings["RMS.MA_FILE_PATH"];
        private string clientCode;
        public static System.Configuration.KeyValueConfigurationCollection LocalConfigurationSettings;

        //private static string processToEnd;
        //private static string postProcess;
        //public const string updaterPrefix = "M1234_";
        //public static string updater = applicationStartupPath + @"\ESN.AutoUpdate.exe";
        //private bool skipUpdate = false;

        //public const string updateSuccess = "UpdateMe has been successfully updated";
        //public const string updateCurrent = "No updates available for UpdateMe";
        //public const string updateInfoError = "Error in retrieving UpdateMe information";

        //public decimal currentVerionOnClient = 0;
        //public decimal updateToVersion = 0;

        //public string downloadURL = ConfigurationManager.AppSettings["RMS.AutoUpdate.DownloadURL"];
        //public string versionFileNameOnServer = ConfigurationManager.AppSettings["RMS.AutoUpdate.VersionFileNameOnServer"];

        //public static List<string> info = new List<string>();

        private ESN.AutoUpdate.API.UpdateService _updateService;

        private bool watchdogWakeUp = Convert.ToBoolean(ConfigurationManager.AppSettings["RMS.WatchdogWakeUp"] ?? "true");


        public MainWindow()
        {
            try
            {
                InitializeComponent();
                
                SetProcessPriority();

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

                InitResultHistory();

                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                // MA State?
                lblState.Content = File.Exists(maFilePath) ? "Maintenance" : "Normal";
                currentStateOnServer = "N/A";

                dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 3);

                #region Local Configuration

                // Initial Local Configuration
                ReloadLocalConfiguration();

                logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Info", Message = "Client Code: " + clientCode, Detail = "" });
                #endregion

            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "MainWindow failed. " + ex.Message, ex, true);
                logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Error", Message = "MainWindow errors", Detail = ex.Message });

                System.Windows.Application.Current.Shutdown();
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

                #region Auto Update Process

                _updateService = new UpdateService();
                _updateService.EventAutoUpdateResult += AddAutoUpdateLog;
                _updateService.EventAutoUpdateProcess += AddAutoUpdateProcess;

                try
                {
                    txtCurrentVersion.Content = _updateService.StartAutoUpdateProcess();
                }
                catch (Exception exception)
                {
                    new RMSAppException(this, "0500", "Window_Loaded - StartAutoUpdateProcess failed. " + exception.Message, exception, true);
                }
                #endregion

                //txtCurrentVersion.Content = currentVerionOnClient;

                btnStart_Click(null, null);

            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "Window_Loaded failed. " + ex.Message, ex, true);
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
                btnStart.IsEnabled = true;
                btnStop.IsEnabled = false;

                new RMSAppException(this, "0500", "btnStart_Click failed. " + ex.Message, ex, true);

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
                btnStart.IsEnabled = false;
                btnStop.IsEnabled = true;

                new RMSAppException(this, "0500", "btnStop_Click failed. " + ex.Message, ex, true);

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

                NetTcpBinding myBinding = new NetTcpBinding();
                myBinding.Security.Mode = SecurityMode.None;
                myBinding.Security.Transport.ClientCredentialType = TcpClientCredentialType.None;

                host.AddServiceEndpoint(typeof(RMS.Agent.WCF.IAgentService), myBinding, netTCP);

                host.Open();
                lblStatus.Content = "Started";
            }
            catch (Exception ex)
            {
                try
                {
                    host.Abort();
                    lblStatus.Content = "Stopped";
                    logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Error", Message = "StartService errors", Detail = ex.Message });
                    btnStart.IsEnabled = true;
                    btnStop.IsEnabled = false;
                }
                catch (Exception ex2)
                {
                    throw new RMSAppException(this, "0500", "host.Abort() failed. " + ex2.Message, ex2, false);
                }

                throw new RMSAppException(this, "0500", "StartService failed. " + ex.Message, ex, false);
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

                throw new RMSAppException(this, "0500", "StopService failed. " + ex.Message, ex, false);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Exit Monitoring Client", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {

                    logs.Add(new EventLog {EventDateTime = DateTime.Now, EventType = "Agent", Message = "Application Closing", Detail = ""});
                    dgLogs.ItemsSource = null;
                    dgLogs.ItemsSource = logs;

                    ni.Visible = false;

                    if (host != null && host.State == CommunicationState.Opened)
                        host.Close();

                    string strResultList = Serializer.XML.SerializeObject(logs);
                    using (TextWriter tw = new StreamWriter(historyFile, false)) // Create & open the file
                    {
                        tw.Write(strResultList);
                        tw.Close();
                    }

                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "Window_Closing failed. " + ex.Message, ex, true);
                App.Current.Shutdown();

            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {

                #region Try to Check Watchdog Process

                if (watchdogWakeUp)
                {
                    try
                    {
                        string watchdogProcessName = ConfigurationManager.AppSettings["RMS.WatchdogProcessName"] ?? "RMS.Agent.Watchdog";
                        if (!IsProcessRunning(watchdogProcessName))
                        {
                            Process.Start(ConfigurationManager.AppSettings["RMS.WatchdogFilePath"] ??
                                          @"D:\App\RMS.Agent.Watchdog\RMS.Agent.Watchdog.exe");
                        }
                    }
                    catch (Exception ex)
                    {
                        new RMSAppException(this, "500", "Check Watchdog Process failed. " + ex.Message, ex, true);
                    }
                }

                #endregion
                
                if (File.Exists(maFilePath))
                {
                    if (currentStateOnServer != "Maintenance")
                    {
                        ReloadLocalConfiguration();
                        MonitoringService ms = new MonitoringService();
                        if (ms.SetMonitoringState(clientCode, ClientState.Maintenance))
                        {
                            currentStateOnServer = "Maintenance";
                        }
                        else
                        {
                            currentStateOnServer = "N/A";
                        }
                    }

                    lblState.Content = "Maintenance";

                }
                else
                {
                    if (currentStateOnServer != "Normal")
                    {
                        ReloadLocalConfiguration();
                        MonitoringService ms = new MonitoringService();
                        if (ms.SetMonitoringState(clientCode, ClientState.Normal))
                        {
                            currentStateOnServer = "Normal";
                        }
                        else
                        {
                            currentStateOnServer = "N/A";
                        }
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
                                try
                                {
                                    logs.AddRange(Serializer.XML.DeserializeObject<ListEventLogs>(strResultList));
                                }
                                catch
                                {
                                    
                                }
                                finally
                                {
                                    File.Delete(tempEventFile);
                                    dgLogs.ItemsSource = null;
                                    dgLogs.ItemsSource = logs;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new RMSAppException(this, "0500", "Import tempEventFile failed. " + ex.Message, ex, false);
                }
                finally
                {
                    processingTempEvent = false;
                }
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "dispatcherTimer_Tick failed. " + ex.Message, ex, true);
            }
            finally
            {
                try
                {
                    string strResultList = Serializer.XML.SerializeObject(logs);
                    using (TextWriter tw = new StreamWriter(historyFile, false)) // Create & open the file
                    {
                        tw.Write(strResultList);
                        tw.Close();
                    }
                }
                catch (Exception ex)
                {
                    new RMSAppException(this, "0500", "Saving History Log failed. " + ex.Message, ex, true);
                }
            }
        }

        private IEnumerable<EventLog> Generate()
        {
            try
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
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "Generate failed. " + ex.Message, ex, false);
            }
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
                new RMSAppException(this, "0500", "InitResultHistory failed. " + ex.Message, ex, true);

                logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Error", Message = "InitResultHistory errors", Detail = ex.Message });
            }
        }

        private void ReloadLocalConfiguration()
        {
            Configuration config;

            ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
            configFile.ExeConfigFilename = applicationStartupPath + @"\Local.config";
            config = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
            LocalConfigurationSettings = config.AppSettings.Settings;

            if (LocalConfigurationSettings["RMS.CLIENT_CODE"] != null)
                clientCode = LocalConfigurationSettings["RMS.CLIENT_CODE"].Value;

        }

        #region Auto Update Methods

        private void AddAutoUpdateLog(object sender, EventArgs e)
        {
            try
            {
                UpdateService.AutoUpdateResultEventArgs args = (UpdateService.AutoUpdateResultEventArgs)e;

                string strUpdateVersion = string.IsNullOrEmpty(args.updateVersion) ? "" : " (" + args.updateVersion + ")";

                var path = System.Reflection.Assembly.GetEntryAssembly().Location;
                var appName = System.IO.Path.GetFileNameWithoutExtension(path);

                var service = new BSL.AutoUpate.AutoUpdateService();
                service.UpdateResult(clientCode, appName, _updateService.currentVerionOnClient.ToString(), args.updateVersion, args.isComplete, args.errorMessage);
                
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "AddAutoUpdateLog failed. " + ex.Message, ex, true);
            }
        }

        private void AddAutoUpdateProcess(object sender, EventArgs e)
        {
            try
            {
                UpdateService.EventLog args = (UpdateService.EventLog) e;
                logs.Add(new EventLog { EventDateTime =  args.EventDateTime, EventType = args.EventType, Message = args.Message, Detail = args.Detail });
                string strResultList = Serializer.XML.SerializeObject(logs);
                using (TextWriter tw = new StreamWriter(historyFile, false)) // Create & open the file
                {
                    tw.Write(strResultList);
                    tw.Close();
                }
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "AddAutoUpdateProcess failed. " + ex.Message, ex, true);
            }
        }

        #endregion


        private bool IsProcessRunning(string sProcessName)
        {
            try
            {
                Process[] proc = Process.GetProcessesByName(sProcessName);
                if (proc.Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "IsProcessRunning failed. " + ex.Message, ex, false);
            }
        }

        private void SetProcessPriority()
        {
            try
            {
                Process myProcess = Process.GetCurrentProcess();
                var priority = Convert.ToString(ConfigurationManager.AppSettings["RMS.ProcessPriority"] ?? "high");
                switch (priority.ToLower())
                {
                    case "realtime":
                        myProcess.PriorityClass = ProcessPriorityClass.RealTime;
                        break;
                    case "normal":
                        myProcess.PriorityClass = ProcessPriorityClass.Normal;
                        break;
                    default:
                        myProcess.PriorityClass = ProcessPriorityClass.High;
                        break;

                }
                myProcess.PriorityClass = ProcessPriorityClass.High;
            }
            catch { }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Manual Monitoring",
                    System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {

                }
            }
            catch
                (Exception ex)
            {

            }
        }

    }
}
