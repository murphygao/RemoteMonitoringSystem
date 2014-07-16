using System;
using System.Collections.Generic;
using System.Configuration;
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
        private static string processToEnd;// = ConfigurationManager.AppSettings["RMS.AGENT_PROCESS_NAME"] ?? "RMS.Agent.WPF";
        private static string postProcess;// = applicationStartupPath + @"\" + processToEnd + ".exe";
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

        private delegate void btnTransparentDelegate(object sender);

        private btnTransparentDelegate doBtnTransparent;

        private double btnOpticalOnPressed = 0.1;


        public MainWindow()
        {
            try
            {
                InitializeComponent();


                if (cursorVisible)
                {
                    Mouse.OverrideCursor = Cursors.Arrow;
                }
                else
                {
                    Mouse.OverrideCursor = Cursors.None;
                }

                StartAutoUpdateProcess();
                txtCurrentVersion.Content = currentVerionOnClient;

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

                Dispatcher.Invoke(() =>
                {
                    txtCurrentVersion.Content = currentVerionOnClient;
                });

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
                        AddAutoUpdateLog(updateVersion, isComplete.Value, errorMessage);
                    }
                    else
                    {
                        string err = "Update Version (" + updateVersion + ") and Current Version (" + currentVerionOnClient + ") must be the same. version.txt in Update File is not correct. Please contact administrator.";

                        skipUpdate = true;

                        AddAutoUpdateLog(updateVersion, false, err);

                    }

                }
                else if (isComplete == false)
                {
                    AddAutoUpdateLog(updateVersion, isComplete.Value, errorMessage);
                }

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

                        if (URLExists(info[3] + info[4]))
                        {
                            UpdateNow(info[1]);
                        }
                        else
                        {
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

                var service = new RMS.Agent.BSL.AutoUpate.AutoUpdateService();
                service.UpdateResult("", appName, currentVerionOnClient.ToString(), updateVersion, isComplete, errorMessage);

            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "AddAutoUpdateLog failed. " + ex.Message, ex, true);
            }
        }

        #endregion

    }

}
