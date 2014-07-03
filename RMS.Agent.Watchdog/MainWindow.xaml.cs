﻿using System;
using System.Collections.Generic;
using System.Configuration;
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
using RMS.Agent.BSL.AutoUpate;
using RMS.Agent.Watchdog.BSL;
using RMS.Common.Exception;

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
                this.SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();

                ni = new System.Windows.Forms.NotifyIcon();

                ni.Icon = RMS.Agent.Watchdog.Properties.Resources.icon_16;

                ni.Visible = true;
                ni.DoubleClick +=
                    delegate(object sender, EventArgs args)
                    {
                        this.Show();
                        this.WindowState = WindowState.Normal;
                    };

                lblAppStatus.Content = "Stopped";
                btnStart.IsEnabled = true;
                btnStop.IsEnabled = false;

                lblExecPath.Content = Assembly.GetExecutingAssembly().Location;

                ws = new WatchdogService();
                
                dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 3);

                StartAutoUpdateProcess();

                txtCurrentVersion.Content = currentVerionOnClient;

                System.Threading.Thread.Sleep(10000);
                btnStart_Click(null, null);
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
            ni.Visible = false;
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

    internal static class WindowExtensions
    {
        // from winuser.h
        private const int GWL_STYLE = -16,
                          WS_MAXIMIZEBOX = 0x10000,
                          WS_MINIMIZEBOX = 0x20000;

        [DllImport("user32.dll")]
        extern private static int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        extern private static int SetWindowLong(IntPtr hwnd, int index, int value);

        internal static void HideMinimizeAndMaximizeButtons(this Window window)
        {
            try
            {
                IntPtr hwnd = new WindowInteropHelper(window).Handle;
                var currentStyle = GetWindowLong(hwnd, GWL_STYLE);

                SetWindowLong(hwnd, GWL_STYLE, (currentStyle & ~WS_MAXIMIZEBOX));
            }
            catch
            {
            }
        }
    }

}
