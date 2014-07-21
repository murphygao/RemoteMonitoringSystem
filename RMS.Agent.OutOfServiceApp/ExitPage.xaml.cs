using System;
using System.Collections.Generic;
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
using RMS.Agent.OutOfServiceApp.BSL;
using RMS.Common.Exception;
using Path = System.IO.Path;

namespace RMS.Agent.OutOfServiceApp
{
    /// <summary>
    /// Interaction logic for ExitPage.xaml
    /// </summary>
    public partial class ExitPage : Page
    {

        private string password = "rms";

        public ExitPage()
        {
            try
            {
                InitializeComponent();

                string keyFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\password.txt";

                if (File.Exists(keyFilePath))
                {
                    password = File.ReadAllText(keyFilePath).Trim();
                }

                txtPassword.Focus();
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
                if (txtPassword.Password == password)
                {
                    OOSService service = new OOSService();
                    if (service.PrepareForClosing())
                        Application.Current.Shutdown();
                }
                else
                {
                    txtPassword.Password = "";
                }
            }
            catch (Exception ex)
            {
                new RMSAppException(this, "0500", "btnOk_Click failed. " + ex.Message, ex, true);
            }
        }
    }
}
