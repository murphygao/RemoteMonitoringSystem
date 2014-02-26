using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace RMS.Agent.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceHost host = null; 


        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnStart_Click(null, null);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;
            StartService();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
            StopService();
        }

        private void StartService()
        {
            try
            {
                using (host =
                        new ServiceHost(typeof(RMS.Agent.WCF.AgentService)))
                {
                    host.AddServiceEndpoint(typeof(RMS.Agent.WCF.IAgentService),
                        new BasicHttpBinding(), "http://localhost:8080/agent/basic");

                    host.AddServiceEndpoint(typeof(RMS.Agent.WCF.IAgentService),
                        new WSHttpBinding(), "http://localhost:8080/agent/wsAddress");

                    host.AddServiceEndpoint(typeof(RMS.Agent.WCF.IAgentService),
                        new NetTcpBinding(), "net.tcp://localhost:8081/AgentNetTcp");

                    host.Open();
                    lblStatus.Content = "Start";
                }
            }
            catch (Exception ex)
            {
                host.Abort();
                lblStatus.Content = "Stop";
                MessageBox.Show("Error = " + ex.Message);
                btnStart.IsEnabled = true;
                btnStop.IsEnabled = false;
            } 
        }

        private void StopService()
        {
            if (host.State == CommunicationState.Opened)
                host.Close();
            lblStatus.Content = "Stop";
        }
    }
}
