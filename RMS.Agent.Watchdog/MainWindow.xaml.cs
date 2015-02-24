using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ESN.AutoUpdate.API;
using RMS.Agent.BSL.AutoUpate;
using RMS.Agent.Watchdog.BSL;
using RMS.Common.Exception;
using TaskScheduler;

namespace RMS.Agent.Watchdog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.NotifyIcon ni;
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;
        private WatchdogService ws;

        public static string applicationStartupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        //private static string processToEnd;// = ConfigurationManager.AppSettings["RMS.AGENT_PROCESS_NAME"] ?? "RMS.Agent.WPF";
        //private static string postProcess;// = applicationStartupPath + @"\" + processToEnd + ".exe";
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

        public MainWindow()
        {
            try
            {
                InitializeComponent();

                SetProcessPriority();

                ni = new System.Windows.Forms.NotifyIcon();

                ni.Icon = RMS.Agent.Watchdog.Properties.Resources.icon_16;

                ni.Visible = true;
                ni.DoubleClick +=
                    delegate(object sender, EventArgs args)
                    {
                        this.Show();
                        this.WindowState = WindowState.Normal;
                    };

                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                lblAppStatus.Content = "Stopped";
                btnStart.IsEnabled = true;
                btnStop.IsEnabled = false;

                lblExecPath.Content = Assembly.GetExecutingAssembly().Location;

                ws = new WatchdogService();
                
                dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 3);

                #region Auto Update Process

                _updateService = new UpdateService();
                _updateService.EventAutoUpdateResult += AddAutoUpdateLog;

                try
                {
                    txtCurrentVersion.Content = _updateService.StartAutoUpdateProcess();
                }
                catch (Exception exception)
                {
                    new RMSAppException(this, "0500", "MainWindow - StartAutoUpdateProcess failed. " + exception.Message, exception, true);
                }

                #endregion

                FixAppConfig();
                System.Threading.Thread.Sleep(10000);
                btnStart_Click(null, null);

                try
                {
                    CreateTaskScheduler();
                }
                catch {}
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "MainWindow failed. " + ex.Message, ex, true);

                System.Diagnostics.Process.Start(System.Windows.Application.ResourceAssembly.Location);
                System.Windows.Application.Current.Shutdown();
            }

        }
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblAppStatus.Content = "Started";
                btnStart.IsEnabled = false;
                btnStop.IsEnabled = true;

                dispatcherTimer.Start();
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "btnStart_Click failed. " + ex.Message, ex, true);
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblAppStatus.Content = "Stopped";
                btnStart.IsEnabled = true;
                btnStop.IsEnabled = false;

                dispatcherTimer.Stop();
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "btnStop_Click failed. " + ex.Message, ex, true);
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                ws.Start();
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "dispatcherTimer_Tick failed. " + ex.Message, ex, true);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Exit Watchdog", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                ni.Visible = false;
                try
                {
                    RemoveTaskScheduler();
                }
                catch
                {
                    App.Current.Shutdown();
                }
            }
            else
            {
                e.Cancel = true;
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


        #region Auto Update Methods

        private void AddAutoUpdateLog(object sender, EventArgs e)
        {
            try
            {
                UpdateService.AutoUpdateResultEventArgs args = (UpdateService.AutoUpdateResultEventArgs)e;

                string strUpdateVersion = string.IsNullOrEmpty(args.updateVersion) ? "" : " (" + args.updateVersion + ")";

                var path = System.Reflection.Assembly.GetEntryAssembly().Location;
                var appName = System.IO.Path.GetFileNameWithoutExtension(path);

                var service = new RMS.Agent.BSL.AutoUpate.AutoUpdateService();
                service.UpdateResult("", appName, _updateService.currentVerionOnClient.ToString(), args.updateVersion, args.isComplete, args.errorMessage);

            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "AddAutoUpdateLog failed. " + ex.Message, ex, true);
            }
        }

        #endregion

        #region TaskScheduler

        private void CreateTaskScheduler()
        {
            string AutoCreateTaskScheduler = ConfigurationManager.AppSettings["RMS.AutoCreateTaskScheduler"] ?? "false";
            if (!Convert.ToBoolean(AutoCreateTaskScheduler)) return;

            ITaskService taskService = null;
            ITaskDefinition taskDefinition = null;
            ITriggerCollection _iTriggerCollection = null;
            ITrigger _trigger = null;
            IActionCollection actions = null;
            IAction action = null;
            IExecAction execAction = null;
            ITaskFolder rootFolder = null;

            try
            {
                //create task service instance
                taskService = new TaskScheduler.TaskScheduler();
                taskService.Connect();

                taskDefinition = taskService.NewTask(0);
                taskDefinition.Settings.Enabled = true;
                taskDefinition.Settings.Compatibility = _TASK_COMPATIBILITY.TASK_COMPATIBILITY_V2_1;
                taskDefinition.RegistrationInfo.Description = "Testing the creation of a scheduled task via VB .NET";

                //create trigger for task creation.
                _iTriggerCollection = taskDefinition.Triggers;
                _trigger = _iTriggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_DAILY);
                _trigger.StartBoundary = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                //_trigger.EndBoundary = DateTime.Now.AddMinutes(1).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                _trigger.Repetition.Interval = "PT5M";
                _trigger.Repetition.Duration = "P1D";
                _trigger.Enabled = true;

                actions = taskDefinition.Actions;
                _TASK_ACTION_TYPE actionType = _TASK_ACTION_TYPE.TASK_ACTION_EXEC;

                //create new action
                action = actions.Create(actionType);
                execAction = action as IExecAction;
                execAction.Path = Assembly.GetExecutingAssembly().Location;
                rootFolder = taskService.GetFolder(@"\");

                //register task.
                rootFolder.RegisterTaskDefinition(System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location), taskDefinition, (int)_TASK_CREATION.TASK_CREATE_OR_UPDATE, null, null,
                    _TASK_LOGON_TYPE.TASK_LOGON_NONE, null);

            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "CreateTaskScheduler failed. " + ex.Message, ex, true);
            }
            finally
            {
                if (rootFolder != null)
                    Marshal.ReleaseComObject(rootFolder);
                if (_iTriggerCollection != null)
                    Marshal.ReleaseComObject(_iTriggerCollection);
                if (_trigger != null)
                    Marshal.ReleaseComObject(_trigger);
                if (actions != null)
                    Marshal.ReleaseComObject(actions);
                if (action != null)
                    Marshal.ReleaseComObject(action);
                if (taskDefinition != null)
                    Marshal.ReleaseComObject(taskDefinition);
                if (taskService != null)
                    Marshal.ReleaseComObject(taskService);

                taskService = null;
                taskDefinition = null;
                _iTriggerCollection = null;
                _trigger = null;
                actions = null;
                action = null;
                execAction = null;
                rootFolder = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

            }
        }

        private void RemoveTaskScheduler()
        {
            string AutoCreateTaskScheduler = ConfigurationManager.AppSettings["RMS.AutoCreateTaskScheduler"] ?? "false";
            if (!Convert.ToBoolean(AutoCreateTaskScheduler)) return;

            ITaskService taskService = null;
            ITaskFolder rootFolder = null;

            try
            {
                taskService = new TaskScheduler.TaskScheduler();
                taskService.Connect();

                rootFolder = taskService.GetFolder(@"\");

                rootFolder.DeleteTask(System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location), 0);

            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "RemoveTaskScheduler failed. " + ex.Message, ex, true);
            }
            finally
            {
                if (rootFolder != null)
                    Marshal.ReleaseComObject(rootFolder);
                if (taskService != null)
                    Marshal.ReleaseComObject(taskService);

                taskService = null;
                rootFolder = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

        }


        #endregion

        private void FixAppConfig()
        {
            try
            {
                string filePath = @"d:\app\RMS.Agent.WPF\RMS.Agent.WPF.exe.config";
                string text = File.ReadAllText(filePath);
                if (text.IndexOf("ESN.AutoUpdate.DownloadURL") < 0)
                    text = text.Replace("RMS.AutoUpdate.DownloadURL", "ESN.AutoUpdate.DownloadURL");

                if (text.IndexOf("ESN.AutoUpdate.VersionFileNameOnServer") < 0)
                    text = text.Replace("RMS.AutoUpdate.VersionFileNameOnServer", "ESN.AutoUpdate.VersionFileNameOnServer");
                File.WriteAllText(@"d:\app\RMS.Agent.WPF\RMS.Agent.WPF.exe.config", text);
            }
            catch
            {
            }
        }

    }
}
