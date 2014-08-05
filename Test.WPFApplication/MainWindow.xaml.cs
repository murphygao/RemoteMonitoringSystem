using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Mail;
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
using RMS.Agent.BSL.Monitoring.Models;
using RMS.Centralize.WebService.Gateway;
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

            msc.AddMessage(rawMessage);

        }

        private void btnCallAddBizMessage_Click(object sender, RoutedEventArgs e)
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

        private void btnSendEmail_Click(object sender, RoutedEventArgs e)
        {
            //var actionGatewayService = new ActionGateway();

            //var actionResult = actionGatewayService.SendEmail(GatewayName.AIS_SKS, "sks@corp.ais900dev.org", new List<string>() { "setht471@corp.ais900dev.org" }, "HTML Email", txtEmailBody.Text);
            //if (actionResult.IsSuccess)
            //{
            //}
            //else
            //{
            //    MessageBox.Show(actionResult.ErrorMessage);
            //}

            MessageBox.Show(SendMail(txtEmailBody.Text));


        }

        private string SendMail(string content)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("10.252.160.41");

                mail.From = new MailAddress("sks@corp.ais900dev.org");
                mail.To.Add("setht471@corp.ais900dev.org");
                mail.Subject = "HTML Email";

                mail.IsBodyHtml = true;

                mail.Body = content;

                SmtpServer.Port = 25;
                //SmtpServer.Credentials = new System.Net.NetworkCredential("username", "password");
                SmtpServer.EnableSsl = false;

                SmtpServer.Send(mail);

                return "Send Complete";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private decimal GetAvailableMemory()
        {
            System.Diagnostics.PerformanceCounter ramCounter = null;

            try
            {
                ramCounter = new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes");

                return Convert.ToDecimal(Math.Round(ramCounter.NextValue(), 0));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                if (ramCounter != null)
                    ramCounter.Dispose();
            }

        }

        private void btnCheckMemory_Click(object sender, RoutedEventArgs e)
        {
            var availableMemory = GetAvailableMemory();
            lblAvailableMemory.Content = availableMemory + " MBytes";
        }

        private void btnSendEmail1_Click(object sender, RoutedEventArgs e)
        {
            ActionGateway gateway = new ActionGateway();
            var actionResult = gateway.SendEmail(GatewayName.KTB_VTM, txtEmailFrom.Text, new List<string>() {txtEmailTo.Text}, txtEmailSubject.Text, txtEmailBody1.Text);
            if (actionResult.IsSuccess)
            {
                MessageBox.Show("Done");
            }
            else
            {
                MessageBox.Show("Error: " + actionResult.ErrorMessage);
            }
        
        }

        private void btnSendSMS_Click(object sender, RoutedEventArgs e)
        {
            ActionGateway gateway = new ActionGateway();
            var actionResult = gateway.SendSMS(GatewayName.KTB_VTM, txtSMSTo.Text, txtSMSSender.Text, txtSMSBody.Text);
            if (actionResult.IsSuccess)
            {
                MessageBox.Show("Done");
            }
            else
            {
                MessageBox.Show("Error: " + actionResult.ErrorMessage);
            }
        }

        private void btnSelfTesting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RMS.Agent.BSL.Monitoring.MonitoringService ms = new RMS.Agent.BSL.Monitoring.MonitoringService();
                var deviceStatuses = ms.SelfMonitoring(txtSelfTesting_ClientCode.Text);

                dtgSelfTestingResult.ItemsSource = deviceStatuses;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


    }
}
