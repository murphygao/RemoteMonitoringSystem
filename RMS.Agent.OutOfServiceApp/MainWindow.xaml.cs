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
using RMS.Agent.OutOfServiceApp.BSL;

namespace RMS.Agent.OutOfServiceApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int countDown = 1;

        public MainWindow()
        {
            InitializeComponent();
            //lblCount.Content = countDown;
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

        private void btnMenu_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            countDown = 1;
            //lblCount.Content = countDown;
        }

    }

}
