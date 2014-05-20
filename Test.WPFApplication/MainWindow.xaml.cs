using System;
using System.Collections.Generic;
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
using Test.WPFApplication.MonitoringService;

namespace Test.WPFApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCallAddMessage_Click(object sender, RoutedEventArgs e)
        {
            MonitoringServiceClient msc = new MonitoringServiceClient();

            var rawMessage = new RmsReportMonitoringRaw();
            rawMessage.ClientCode = txtClientCode.Text;
            rawMessage.DeviceCode = txtDeviceCode.Text;
            rawMessage.MessageGroupCode = txtMessageGroupCode.Text;
            rawMessage.Message = txtMessage.Text;
            rawMessage.MessageDateTime = DateTime.Now;

            if (!string.IsNullOrEmpty(txtMonitoringProfileDeviceID.Text.Trim()))
                rawMessage.MonitoringProfileDeviceId = Convert.ToInt32(txtMonitoringProfileDeviceID.Text);

            rawMessage.MessageRemark = txtMessageRemark.Text;

            msc.AddBusinessMessage(rawMessage);

        }
    }
}
