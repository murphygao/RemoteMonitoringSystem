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
        private int countDown = 1;
        private string keyInValue = "";
        private string password = "7319";
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

        public string downloadURL = ConfigurationManager.AppSettings["RMS.AutoUpdate.DownloadURL"];
        public string versionFileNameOnServer = ConfigurationManager.AppSettings["RMS.AutoUpdate.VersionFileNameOnServer"];

        public static List<string> info = new List<string>();

        public MainWindow()
        {
            try
            {
                InitializeComponent();

                string keyFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\key.txt";

                if (File.Exists(keyFilePath))
                {
                    password = File.ReadAllText(keyFilePath).Trim();
                }

                keyInValue = keyInValue.PadLeft(password.Length, '0');

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



        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            //lblCount.Content = countDown;
            if (countDown-- <= 0)
            {
                OOSService service = new OOSService();
                if (service.PrepareForClosing())
                    Application.Current.Shutdown();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.btnNumber1.Click += new System.Windows.RoutedEventHandler(this.btnNumber_Click);
            this.btnNumber2.Click += new System.Windows.RoutedEventHandler(this.btnNumber_Click);
            this.btnNumber3.Click += new System.Windows.RoutedEventHandler(this.btnNumber_Click);
            this.btnNumber4.Click += new System.Windows.RoutedEventHandler(this.btnNumber_Click);
            this.btnNumber5.Click += new System.Windows.RoutedEventHandler(this.btnNumber_Click);
            this.btnNumber6.Click += new System.Windows.RoutedEventHandler(this.btnNumber_Click);
            this.btnNumber7.Click += new System.Windows.RoutedEventHandler(this.btnNumber_Click);
            this.btnNumber8.Click += new System.Windows.RoutedEventHandler(this.btnNumber_Click);
            this.btnNumber9.Click += new System.Windows.RoutedEventHandler(this.btnNumber_Click);

        }

        public void btnNumber_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                keyInValue += (sender as Button).Tag.ToString();
                keyInValue = keyInValue.Substring(1);
                if (keyInValue == password)
                {
                    OOSService service = new OOSService();
                    if (service.PrepareForClosing())
                        Application.Current.Shutdown();
                }
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "btnNumber_Click failed. " + ex.Message, ex, true);
            }
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
                }

                if (tempStr.IndexOf("skipupdate") > -1)
                {
                    skipUpdate = true;
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

                    if (URLExists(info[3] + info[4]))
                    {
                        UpdateNow();
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
