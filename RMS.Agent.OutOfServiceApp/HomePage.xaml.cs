using System;
using System.Collections.Generic;
using System.Configuration;
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
            InitializeComponent();

            string keyFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\key.txt";

            if (File.Exists(keyFilePath))
            {
                password = File.ReadAllText(keyFilePath).Trim();
            }

            keyInValue = keyInValue.PadLeft(password.Length, '0');

            doBtnTransparent = new btnTransparentDelegate(MakebtnTranspalent);

        }


        public void btnNumber_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                keyInValue += (sender as Button).Tag.ToString();
                keyInValue = keyInValue.Substring(1);
                if (keyInValue == password)
                {
                    this.NavigationService.Navigate(new ExitPage());
                    //OOSService service = new OOSService();
                    //if (service.PrepareForClosing())
                    //    Application.Current.Shutdown();
                }
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "btnNumber_Click failed. " + ex.Message, ex, true);
            }
        }

        private void MakebtnTranspalent(object sender)
        {
            System.Threading.Thread.Sleep(200);
            Dispatcher.Invoke(() =>
            {
                ((Button)sender).Opacity = 0;
            });
        }

        private void btnNumber1_Click(object sender, RoutedEventArgs e)
        {
            btnNumber_Click(sender, e);
            ((Button)sender).Opacity = btnOpticalOnPressed;
            doBtnTransparent.BeginInvoke(sender, null, null);
        }


        private void btnNumber2_Click(object sender, RoutedEventArgs e)
        {
            btnNumber_Click(sender, e);
            ((Button)sender).Opacity = btnOpticalOnPressed;
            doBtnTransparent.BeginInvoke(sender, null, null);
        }

        private void btnNumber3_Click(object sender, RoutedEventArgs e)
        {
            btnNumber_Click(sender, e);
            ((Button)sender).Opacity = btnOpticalOnPressed;
            doBtnTransparent.BeginInvoke(sender, null, null);
        }

        private void btnNumber4_Click(object sender, RoutedEventArgs e)
        {
            btnNumber_Click(sender, e);
            ((Button)sender).Opacity = btnOpticalOnPressed;
            doBtnTransparent.BeginInvoke(sender, null, null);
        }

        private void btnNumber5_Click(object sender, RoutedEventArgs e)
        {
            btnNumber_Click(sender, e);
            ((Button)sender).Opacity = btnOpticalOnPressed;
            doBtnTransparent.BeginInvoke(sender, null, null);
        }

        private void btnNumber6_Click(object sender, RoutedEventArgs e)
        {
            btnNumber_Click(sender, e);
            ((Button)sender).Opacity = btnOpticalOnPressed;
            doBtnTransparent.BeginInvoke(sender, null, null);
        }

        private void btnNumber7_Click(object sender, RoutedEventArgs e)
        {
            btnNumber_Click(sender, e);
            ((Button)sender).Opacity = btnOpticalOnPressed;
            doBtnTransparent.BeginInvoke(sender, null, null);
        }

        private void btnNumber8_Click(object sender, RoutedEventArgs e)
        {
            btnNumber_Click(sender, e);
            ((Button)sender).Opacity = btnOpticalOnPressed;
            doBtnTransparent.BeginInvoke(sender, null, null);
        }

        private void btnNumber9_Click(object sender, RoutedEventArgs e)
        {
            btnNumber_Click(sender, e);
            ((Button)sender).Opacity = btnOpticalOnPressed;
            doBtnTransparent.BeginInvoke(sender, null, null);
        }


    }
}
