using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
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
using RMS.Agent.Watchdog.BSL;

namespace RMS.Agent.Watchdog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;
        private WatchdogService ws;
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();

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

                lblAgentFilePath.Content = ConfigurationManager.AppSettings["AGENT_FILE_PATH"];

                ws = new WatchdogService();

                dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
                
                btnStart_Click(null, null);
            }
            catch (Exception ex)
            {

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
            lblAppStatus.Content = "Started";
            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;

            dispatcherTimer.Start();

        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            lblAppStatus.Content = "Stopped";
            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;

            dispatcherTimer.Stop();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            ws.Start();
        }
    }

}
