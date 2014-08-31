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
        private string currentState = string.Empty;

        private string maFilePath = ConfigurationManager.AppSettings["RMS.MA_FILE_PATH"];
        private string clientCode;
        public static System.Configuration.KeyValueConfigurationCollection LocalConfigurationSettings;

        private static string processToEnd;
        private static string postProcess;
        public const string updaterPrefix = "M1234_";
        public static string updater = applicationStartupPath + @"\ESN.AutoUpdate.exe";
        private bool skipUpdate = false;

        public const string updateSuccess = "UpdateMe has been successfully updated";
        public const string updateCurrent = "No updates available for UpdateMe";
        public const string updateInfoError = "Error in retrieving UpdateMe information";

        public decimal currentVerionOnClient = 0;
        public decimal updateToVersion = 0;

        public string downloadURL = ConfigurationManager.AppSettings["RMS.AutoUpdate.DownloadURL"];
        public string versionFileNameOnServer = ConfigurationManager.AppSettings["RMS.AutoUpdate.VersionFileNameOnServer"];

        public static List<string> info = new List<string>();

        private bool watchdogWakeUp = Convert.ToBoolean(ConfigurationManager.AppSettings["RMS.WatchdogWakeUp"] ?? "true");


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

                InitResultHistory();


                // MA State?
                lblState.Content = File.Exists(maFilePath) ? "Maintenance" : "Normal";
                currentState = lblState.Content.ToString();

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

                StartAutoUpdateProcess();

                #endregion

                txtCurrentVersion.Content = currentVerionOnClient;

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

                host.AddServiceEndpoint(typeof(RMS.Agent.WCF.IAgentService),
                    new NetTcpBinding(SecurityMode.Transport), netTCP);
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
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Exit Application", System.Windows.MessageBoxButton.YesNo);
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
                    if (currentState == lblState.Content.ToString())
                    {
                        ReloadLocalConfiguration();
                        MonitoringService ms = new MonitoringService();
                        ms.SetMonitoringState(clientCode, ClientState.Maintenance);
                    }

                    lblState.Content = "Maintenance";
                }
                else
                {
                    if (currentState == lblState.Content.ToString())
                    {
                        ReloadLocalConfiguration();
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

        private void StartAutoUpdateProcess()
        {
            try
            {
                LoadLocalAutoUpdateInfo();
                Update.updateMe(updaterPrefix, applicationStartupPath + @"\");
                UnpackCommandline();

                if (!skipUpdate)
                    CheckforUpdate();
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "StartAutoUpdateProcess failed. " + ex.Message, ex, true);
            }
        }

        private void UnpackCommandline()
        {
            try
            {
                bool commandPresent = false;
                string tempStr = "";

                string updateVersion = string.Empty;
                bool? isComplete = null;
                string errorMessage = string.Empty;

                List<string> listArg = new List<string>(Environment.GetCommandLineArgs());

                foreach (string arg in Environment.GetCommandLineArgs())
                {
                    if (!commandPresent)
                    {
                        commandPresent = arg.Trim().StartsWith("/");
                    }

                    if (commandPresent)
                    {
                        tempStr += arg;
                    }



                }
                for (int i = 0; i < listArg.Count; i++)
                {

                    if (listArg[i].ToLower().Trim() == "/skipupdate") skipUpdate = true;
                    if (listArg[i].ToLower().Trim() == "/updated" && string.IsNullOrEmpty(errorMessage)) isComplete = true;
                    if (listArg[i].ToLower().Trim() == "/updateversion")
                    {
                        updateVersion = listArg[i + 1];
                        decimal.TryParse(updateVersion, out updateToVersion);

                    }
                    if (listArg[i].ToLower().Trim() == "/error")
                    {
                        errorMessage = listArg[i + 1];
                        isComplete = false;
                    }

                }

                string strUpdateVersion = string.IsNullOrEmpty(updateVersion) ? "" : " (" + updateVersion + ")";

                if (isComplete == true)
                {

                    if (currentVerionOnClient == updateToVersion)
                    {
                        logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Auto Update", Message = "Auto Update" + strUpdateVersion + " is complete."
                            , Detail = "" });

                        AddAutoUpdateLog(updateVersion, isComplete.Value, errorMessage);
                    }
                    else
                    {
                        string err = "Update Version ("+updateVersion+") and Current Version ("+currentVerionOnClient+") must be the same. version.txt in Update File is not correct. Please contact administrator.";
                        logs.Add(new EventLog
                        {
                            EventDateTime = DateTime.Now,
                            EventType = "Auto Update",
                            Message = "Auto Update" + strUpdateVersion + " is complete but version control is incorrent."
                            ,
                            Detail = err
                        });

                        skipUpdate = true;

                        AddAutoUpdateLog(updateVersion, false, err);

                    }

                }
                else if (isComplete == false)
                {
                    logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Auto Update", Message = "Auto Update" + strUpdateVersion + " failed."
                        , Detail = errorMessage });

                    AddAutoUpdateLog(updateVersion, isComplete.Value, errorMessage);
                }

                if (skipUpdate)
                    logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Auto Update", Message = "Skip Auto Update.", Detail = ""});
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "CheckforUpdate failed. " + ex.Message, ex, true);
            }
        }

        private void LoadLocalAutoUpdateInfo()
        {
            var path = System.Reflection.Assembly.GetEntryAssembly().Location;
            processToEnd = System.IO.Path.GetFileNameWithoutExtension(path);
            postProcess = applicationStartupPath + @"\" + processToEnd + ".exe";

            var versionFilePathOnClient = applicationStartupPath + @"\version.txt";
            string strVersion = "0";

            if (File.Exists(versionFilePathOnClient))
            {
                strVersion = System.IO.File.ReadAllText(versionFilePathOnClient);
            }

            decimal.TryParse(strVersion, out currentVerionOnClient);

        }

        private void CheckforUpdate()
        {

            try
            {
                info = Update.getUpdateInfo(downloadURL, versionFileNameOnServer, applicationStartupPath + @"\", 1);

                if (info == null)
                {

                    //Update_bttn.Visible = false;
                    //updateResult.Text = updateInfoError;
                    //updateResult.Visible = true;

                }
                else
                {
                    //ตรวจสอบ version ถ้าบน server ใหม่กว่า แสดงว่าสามารถ update ได้
                    if (decimal.Parse(info[1]) > currentVerionOnClient)
                    {
                        logs.Add(new EventLog {EventDateTime = DateTime.Now, EventType = "Auto Update", Message = "Found New Version", Detail = ""});

                        if (URLExists(info[3] + info[4]))
                        {
                            logs.Add(new EventLog
                            {
                                EventDateTime = DateTime.Now,
                                EventType = "Auto Update",
                                Message = "Started Auto Update Process",
                                Detail = info[3] + info[4]
                            });
                            logs.Add(new EventLog
                            {
                                EventDateTime = DateTime.Now,
                                EventType = "Agent",
                                Message = "Application Closing for Auto Update",
                                Detail = ""
                            });
                            string strResultList = Serializer.XML.SerializeObject(logs);
                            using (TextWriter tw = new StreamWriter(historyFile, false)) // Create & open the file
                            {
                                tw.Write(strResultList);
                                tw.Close();
                            }
                            UpdateNow(info[1]);
                        }
                        else
                        {
                            logs.Add(new EventLog
                            {
                                EventDateTime = DateTime.Now,
                                EventType = "Auto Update",
                                Message = "File Not Found.",
                                Detail = info[3] + info[4]
                            });
                        }
                        //Update_bttn.Visible = true;
                        //updateResult.Visible = false;

                    }


                }
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "CheckforUpdate failed. " + ex.Message, ex, true);
            }

        }

        private void UpdateNow(string updateVersion = "")
        {
            Update.installUpdateRestart(info[3], info[4], "\"" + applicationStartupPath + "\\", processToEnd, postProcess, "updated", updater, updateVersion);
            Close();
        }

        public bool URLExists(string url)
        {
            bool result = true;

            try
            {
                WebRequest webRequest = WebRequest.Create(url);
                webRequest.Timeout = 5000; // miliseconds
                webRequest.Method = "HEAD";

                webRequest.GetResponse();

            }
            catch
            {
                result = false;
            }

            return result;
        }

        private void AddAutoUpdateLog(string updateVersion, bool isComplete, string errorMessage)
        {
            try
            {
                var path = System.Reflection.Assembly.GetEntryAssembly().Location;
                var appName = System.IO.Path.GetFileNameWithoutExtension(path);

                var service = new BSL.AutoUpate.AutoUpdateService();
                service.UpdateResult(clientCode, appName, currentVerionOnClient.ToString(), updateVersion, isComplete, errorMessage);
                
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "AddAutoUpdateLog failed. " + ex.Message, ex, true);
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

    }
}
