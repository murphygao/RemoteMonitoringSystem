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
            InitializeComponent();

            string keyFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\password.txt";

            if (File.Exists(keyFilePath))
            {
                password = File.ReadAllText(keyFilePath).Trim();
            }

            txtPassword.Focus();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password == password)
            {
                Application.Current.Shutdown();
            }
            else
            {
                txtPassword.Password = "";
            }
        }
    }
}
