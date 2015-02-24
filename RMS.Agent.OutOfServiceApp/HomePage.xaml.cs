using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using RMS.Common.Exception;
using Path = System.IO.Path;

namespace RMS.Agent.OutOfServiceApp
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private int countDown = 1;
        private string keyInValue = "";
        private string password = "7319";

        
        private delegate void btnTransparentDelegate(object sender);

        private btnTransparentDelegate doBtnTransparent;

        private double btnOpticalOnPressed = 0.3;


        public HomePage()
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

                doBtnTransparent = new btnTransparentDelegate(MakebtnTranspalent);

                if (this.NavigationService.CanGoBack)
                {
                    this.NavigationService.RemoveBackEntry();
                }
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "HomePage() failed. " + ex.Message, ex, true);
            }
        }


        public void btnNumber_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                keyInValue += (sender as Button).Tag.ToString();
                keyInValue = keyInValue.Substring(1);
                if (keyInValue == password)
                {
                    string mainAppFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\mainapplication.txt";

                    int validateMainAppFile = 0;
                    if (File.Exists(mainAppFilePath))
                    {
                        string line;
                        int counter = 0;

                        // Read the file and display it line by line.
                        using (var file = new System.IO.StreamReader(mainAppFilePath))
                        {
                            while ((line = file.ReadLine()) != null)
                            {
                                counter++;
                                if (counter == 1)
                                {
                                    validateMainAppFile++;
                                }
                                else if (counter == 2)
                                {
                                    validateMainAppFile++;
                                }
                            }
                        }
                    }

                    if (validateMainAppFile == 2)
                    {
                        this.NavigationService.Navigate(new ChoicePage());
                    }
                    else
                    {
                        this.NavigationService.Navigate(new ExitPage());
                    }
                    //OOSService service = new OOSService();
                    //if (service.PrepareForClosing())
                    //    Application.Current.Shutdown();
                }
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "btnNumber_Click failed. " + ex.Message, ex, false);
            }
        }

        private void MakebtnTranspalent(object sender)
        {
            try
            {
                Thread.Sleep(200);
                Dispatcher.Invoke(() =>
                {
                    ((Button)sender).Opacity = 0;
                });
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "MakebtnTranspalent failed. " + ex.Message, ex, false);
            }
        }

        private void btnNumber1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnNumber_Click(sender, e);
                ((Button)sender).Opacity = btnOpticalOnPressed;
                doBtnTransparent.BeginInvoke(sender, null, null);
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "btnNumber1_Click failed. " + ex.Message, ex, true);
            }
        }


        private void btnNumber2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnNumber_Click(sender, e);
                ((Button)sender).Opacity = btnOpticalOnPressed;
                doBtnTransparent.BeginInvoke(sender, null, null);
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "btnNumber2_Click failed. " + ex.Message, ex, true);
            }
        }

        private void btnNumber3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnNumber_Click(sender, e);
                ((Button)sender).Opacity = btnOpticalOnPressed;
                doBtnTransparent.BeginInvoke(sender, null, null);
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "btnNumber3_Click failed. " + ex.Message, ex, true);
            }
        }

        private void btnNumber4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnNumber_Click(sender, e);
                ((Button)sender).Opacity = btnOpticalOnPressed;
                doBtnTransparent.BeginInvoke(sender, null, null);
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "btnNumber4_Click failed. " + ex.Message, ex, true);
            }
        }

        private void btnNumber5_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnNumber_Click(sender, e);
                ((Button)sender).Opacity = btnOpticalOnPressed;
                doBtnTransparent.BeginInvoke(sender, null, null);
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "btnNumber5_Click failed. " + ex.Message, ex, true);
            }
        }

        private void btnNumber6_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnNumber_Click(sender, e);
                ((Button)sender).Opacity = btnOpticalOnPressed;
                doBtnTransparent.BeginInvoke(sender, null, null);
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "btnNumber6_Click failed. " + ex.Message, ex, true);
            }
        }

        private void btnNumber7_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnNumber_Click(sender, e);
                ((Button)sender).Opacity = btnOpticalOnPressed;
                doBtnTransparent.BeginInvoke(sender, null, null);
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "btnNumber7_Click failed. " + ex.Message, ex, true);
            }
        }

        private void btnNumber8_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnNumber_Click(sender, e);
                ((Button)sender).Opacity = btnOpticalOnPressed;
                doBtnTransparent.BeginInvoke(sender, null, null);
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "btnNumber8_Click failed. " + ex.Message, ex, true);
            }
        }

        private void btnNumber9_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnNumber_Click(sender, e);
                ((Button)sender).Opacity = btnOpticalOnPressed;
                doBtnTransparent.BeginInvoke(sender, null, null);
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "btnNumber9_Click failed. " + ex.Message, ex, true);
            }
        }


    }
}
