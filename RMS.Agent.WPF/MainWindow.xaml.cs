using System;
using System.Collections.Generic;
using System.Configuration;
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
using RMS.Agent.BSL.Monitoring;
using RMS.Agent.Helper;
using RMS.Agent.Model;
using RMS.Agent.Proxy.ClientProxy;
using RMS.Agent.WCF;
using RMS.Common.Exception;
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

        public string downloadURL = ConfigurationManager.AppSettings["RMS.AutoUpdate.DownloadURL"];
        public string versionFileNameOnServer = ConfigurationManager.AppSettings["RMS.AutoUpdate.VersionFileNameOnServer"];

        public static List<string> info = new List<string>();


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
                    new NetTcpBinding(), netTCP);
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
                logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Agent", Message = "Application Closing", Detail = "" });
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
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "Window_Closing failed. " + ex.Message, ex, true);
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
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
            LoadLocalAutoUpdateInfo();
            Update.updateMe(updaterPrefix, applicationStartupPath + @"\");
            UnpackCommandline();

            if (!skipUpdate)
                CheckforUpdate();

        }

        private void UnpackCommandline()
        {
            bool commandPresent = false;
            string tempStr = "";

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

            if (commandPresent)
            {
                if (tempStr.Remove(0, 2) == "updated")
                {
                    //ถ้าเข้ามาในนี้ได้แสดงว่า update สำเร็จ?
                    logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Auto Update", Message = "Finish Auto Update.", Detail = "" });
                }

                if (tempStr.IndexOf("skipupdate") > -1)
                {
                    skipUpdate = true;
                    logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Auto Update", Message = "Skip Auto Update.", Detail = tempStr.Substring(tempStr.IndexOf("/error")+1) });
                }

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
                    logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Auto Update", Message = "Found New Version", Detail = "" });

                    if (URLExists(info[3] + info[4]))
                    {
                        logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Auto Update", Message = "Started Auto Update Process", Detail = info[3] + info[4] });
                        logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Agent", Message = "Application Closing for Auto Update", Detail = "" });
                        string strResultList = Serializer.XML.SerializeObject(logs);
                        using (TextWriter tw = new StreamWriter(historyFile, false)) // Create & open the file
                        {
                            tw.Write(strResultList);
                            tw.Close();
                        }
                        UpdateNow();
                    }
                    else
                    {
                        logs.Add(new EventLog { EventDateTime = DateTime.Now, EventType = "Auto Update", Message = "File Not Found.", Detail = info[3] + info[4] });
                    }
                    //Update_bttn.Visible = true;
                    //updateResult.Visible = false;

                }
                else //ตรวตสอบพบว่า version บน server ไม่ได้ใหม่กว่า ไม่ต้อง update แต่อย่างใด
                {

                    //Update_bttn.Visible = false;
                    //updateResult.Visible = true;
                    //updateResult.Text = updateCurrent;

                }

            }

        }

        private void UpdateNow()
        {
            Update.installUpdateRestart(info[3], info[4], "\"" + applicationStartupPath + "\\", processToEnd, postProcess, "updated", updater);
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

        #endregion

    }
}
