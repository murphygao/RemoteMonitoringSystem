using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
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
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.BSL;
using RMS.Centralize.WebService.Gateway;
using TaskScheduler;
using Test.WPFApplication.MonitoringService;
using RmsReportMonitoringRaw = Test.WPFApplication.MonitoringService.RmsReportMonitoringRaw;

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
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

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

            rawMessage.MessageRemark = string.IsNullOrEmpty(txtMessageRemark.Text) ? null : txtMessageRemark.Text;

            msc.AddBusinessMessage(rawMessage);
        }

        private void btnCallAddWebsiteMessage_Click(object sender, RoutedEventArgs e)
        {
            MonitoringServiceClient msc = new MonitoringServiceClient();

            var rawMessage = new RmsReportMonitoringRaw();
            rawMessage.ClientCode = txtClientCode.Text;
            rawMessage.MessageGroupCode = txtMessageGroupCode.Text;
            rawMessage.Message = txtMessage.Text;
            rawMessage.MessageDateTime = DateTime.Now;

            if (!string.IsNullOrEmpty(txtWebsiteMonitoringID.Text.Trim()))
                rawMessage.WebsiteMonitoringId = Convert.ToInt32(txtWebsiteMonitoringID.Text);

            rawMessage.MessageRemark = string.IsNullOrEmpty(txtMessageRemark.Text) ? null : txtMessageRemark.Text;
            List<RmsReportMonitoringRaw> list = new List<RmsReportMonitoringRaw>();
            list.Add(rawMessage);
            msc.AddWebsiteMessage(list);

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

        private void btnCreateTaskScheduler_Click(object sender, RoutedEventArgs e)
        {
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
                execAction.Path = @"C:\Windows\System32\notepad.exe";
                rootFolder = taskService.GetFolder(@"\");

                //register task.
                rootFolder.RegisterTaskDefinition(System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location), taskDefinition, (int)_TASK_CREATION.TASK_CREATE_OR_UPDATE, null, null,
                    _TASK_LOGON_TYPE.TASK_LOGON_NONE, null);

            }
            catch (Exception ex)
            {
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

        private void btnRemoveTaskScheduler_Click(object sender, RoutedEventArgs e)
        {
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
                Console.WriteLine(ex);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txtWMResult.Text = "";
            if (!string.IsNullOrEmpty(txtWMHostName.Text))
            {
                MyPing(txtWMHostName.Text);
            }
            else
            {
                txtWMResult.Text = "Host / IP Address cannot be null.";
            }
        }

        private void MyPing(string hostName)
        {
            try
            {
                string _replyAddress = string.Empty;
                int _sent = 0;
                int _received = 0;
                int _lost = 0;
                long? _min = null;
                long? _max = null;
                long? _avg = null;


                using (var ping = new Ping())
                {
                    // Create a buffer of 32 bytes of data to be transmitted. 
                    string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                    byte[] buffer = Encoding.ASCII.GetBytes(data);

                    // Wait 10 seconds for a reply. 
                    int timeout = 5000;

                    // Set options for transmission: 
                    // The data can go through 64 gateways or routers 
                    // before it is destroyed, and the data packet 
                    // cannot be fragmented.
                    PingOptions options = new PingOptions(64, true);

                    int echoNum = 3;
                    long totalTime = 0;

                    for (int i = 0; i < echoNum; i++)
                    {
                        var reply = ping.Send(hostName, timeout, buffer, options);

                        _sent++;

                        if (reply != null && reply.Status == IPStatus.Success)
                        {
                            _received++;
                            if (_max == null || reply.RoundtripTime > _max) _max = reply.RoundtripTime;
                            if (_min == null || reply.RoundtripTime < _min) _min = reply.RoundtripTime;

                            totalTime += reply.RoundtripTime;
                        }
                        else
                        {
                            _lost++;
                        }

                        if (reply != null && reply.Address != null)
                            _replyAddress = reply.Address.ToString();
                    }

                    if (totalTime != 0)
                        _avg = totalTime / _sent;

                }

                /*
                 * 
Ping statistics for 113.21.241.55:
    Packets: Sent = 4, Received = 4, Lost = 0 (0% loss),
Approximate round trip times in milli-seconds:
    Minimum = 32ms, Maximum = 46ms, Average = 41ms
                 * */
                txtWMResult.Text += "Ping statistics for " + _replyAddress + ":" + Environment.NewLine;
                txtWMResult.Text += "    Packets: Sent = " + _sent + ", Received = " + _received + ", Lost = " + _lost + " (" + (100 * _lost / _sent) + "% loss)," + Environment.NewLine;

                if (_sent != _lost)
                {
                    txtWMResult.Text += "Approximate round trip times in milli-seconds:" + Environment.NewLine;
                    txtWMResult.Text += "    Minimum = " + _min + "ms, Maximum = " + _max + "ms, Average = " + _avg + "ms";
                }

            }
            catch (Exception ex)
            {
                txtWMResult.Text = "Error: " + ex.Message + (ex.InnerException != null ? ex.InnerException.Message : "");
            }
        }

        private void btnWMList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WebsiteMonitoringService wmService = new WebsiteMonitoringService();
                var listWebsiteMonitorings = wmService.ListWebsiteMonitoringsByClient(Convert.ToInt32(txtWMClientID.Text));
                dtgWMResultListWebsiteMonitoring.ItemsSource = listWebsiteMonitorings;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



    }
}
