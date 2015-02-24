using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ESN.AutoUpdate.API;
using RMS.Agent.BSL.AutoUpate;
using RMS.Agent.OutOfServiceApp.BSL;
using RMS.Common.Exception;
using Path = System.IO.Path;

namespace RMS.Agent.OutOfServiceApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        bool cursorVisible = Convert.ToBoolean(ConfigurationManager.AppSettings["RMS.CursorVisible"] ?? "false");

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

        private delegate void btnTransparentDelegate(object sender);

        private btnTransparentDelegate doBtnTransparent;

        private double btnOpticalOnPressed = 0.1;


        public MainWindow()
        {
            try
            {
                InitializeComponent();

                SetProcessPriority();

                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                if (cursorVisible)
                {
                    Mouse.OverrideCursor = Cursors.Arrow;
                }
                else
                {
                    Mouse.OverrideCursor = Cursors.None;
                }

                #region Auto Update Process

                _updateService = new UpdateService();
                _updateService.EventAutoUpdateResult += AddAutoUpdateLog;
                //_updateService.EventAutoUpdateProcess += AddAutoUpdateProcess;

                try
                {
                    txtCurrentVersion.Content = _updateService.StartAutoUpdateProcess();
                }
                catch (Exception exception)
                {
                    new RMSAppException(this, "0500", "Window_Loaded - StartAutoUpdateProcess failed. " + exception.Message, exception, true);
                    try
                    {
                        var versionFilePathOnClient = applicationStartupPath + @"\version.txt";

                        if (File.Exists(versionFilePathOnClient))
                        {
                            txtCurrentVersion.Content = System.IO.File.ReadAllText(versionFilePathOnClient);
                        }
                    }
                    catch
                    {
                    }
                }
                #endregion

                this.Activate();
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "MainWindow failed. " + ex.Message, ex, true);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainFrame.MinHeight = this.Height;
            mainFrame.MinWidth = this.Width;

            mainFrame.MaxHeight = this.Height;
            mainFrame.MaxWidth = this.Width;


            mainFrame.Navigate(new HomePage());
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

                var service = new Agent.BSL.AutoUpate.AutoUpdateService();
                service.UpdateResult("", appName, _updateService.currentVerionOnClient.ToString(), args.updateVersion, args.isComplete, args.errorMessage);

            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "AddAutoUpdateLog failed. " + ex.Message, ex, true);
            }
        }

        #endregion

    }

}
