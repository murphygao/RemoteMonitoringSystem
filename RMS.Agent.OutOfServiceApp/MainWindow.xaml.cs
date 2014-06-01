using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
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



    }

}
