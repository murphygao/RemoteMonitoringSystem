using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
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
using RMS.Agent.OutOfServiceApp.BSL;
using RMS.Common.Exception;
using Path = System.IO.Path;

namespace RMS.Agent.OutOfServiceApp
{
    /// <summary>
    /// Interaction logic for ChoicePage.xaml
    /// </summary>
    public partial class ChoicePage : Page
    {

        private string choiceStartApp = string.Empty;
        private string mainAppExecPath = string.Empty;
        private bool checkChoiceAutoStart = false;
        public ChoicePage()
        {
            try
            {
                InitializeComponent();

                string mainAppFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\mainapplication.txt";

                if (File.Exists(mainAppFilePath))
                {
                    int counter = 0;
                    string line;

                    // Read the file and display it line by line.
                    using (var file = new System.IO.StreamReader(mainAppFilePath))
                    {
                        while ((line = file.ReadLine()) != null)
                        {
                            counter++;
                            if (counter == 1)
                            {
                                choiceStartApp = line.Trim();
                            }
                            else if (counter == 2)
                            {
                                mainAppExecPath = line.Trim();
                            }
                        }
                    }
                    if (counter == 2 && !string.IsNullOrEmpty(choiceStartApp) &&
                        !string.IsNullOrEmpty(mainAppExecPath))
                    {
                        btnOk.Content = choiceStartApp;
                        checkChoiceAutoStart = true;
                    }
                    else
                    {
                        this.NavigationService.Navigate(new Uri("ExitPage.xaml", UriKind.Relative));
                    }
                }
                else
                {
                    this.NavigationService.Navigate(new ExitPage());
                }
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "ExitPage() failed. " + ex.Message, ex, true);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "btnCancel_Click() failed. " + ex.Message, ex, true);
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int delayAfterStartMainApp = Convert.ToInt32(ConfigurationManager.AppSettings["RMS.DelayAfterStartMainApp"] ?? "1500");

                try
                {
                    var windowsIdentity = WindowsIdentity.GetCurrent();
                    if (windowsIdentity != null)
                    {
                        string[] temp = Convert.ToString(windowsIdentity.Name).Split('\\');
                        if (temp.Length > 1)
                        {
                            string userName = temp[1];
                            string tmpMainAppExecPath = mainAppExecPath.Replace("::userlogin::", userName);
                            Process.Start(tmpMainAppExecPath);
                        }
                    }

                    System.Threading.Thread.Sleep(delayAfterStartMainApp);
                    Application.Current.Shutdown();
                }
                catch (Exception exception)
                {
                    new RMSAppException(this, "0500", "StartApp failed. " + exception.Message, exception, true);
                    //if (exception.Message == "The system cannot find the file specified")
                    //{
                    //    OOSService service = new OOSService();
                    //    service.PrepareForClosing();
                    //}
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Cannot " + choiceStartApp + ". " + exception.Message + ". Please select Exit App and " + choiceStartApp.ToLower() + " manually", "Warning",
                        System.Windows.MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "btnOk_Click failed. " + ex.Message, ex, true);
            }
        }

        private void btnExitApp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(new ExitPage());
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "btnExitApp_Click failed. " + ex.Message, ex, true);
            }
        }
    }
}
