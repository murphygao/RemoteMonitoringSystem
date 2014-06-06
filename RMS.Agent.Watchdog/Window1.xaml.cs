using System;
using System.Linq;
using System.Windows;
using System.Diagnostics;


namespace RMS.Agent.Watchdog
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void Main() 
        {
            System.Threading.Thread.Sleep(100);
            Process currentProcess = Process.GetCurrentProcess();
            var runningProcess = (from process in Process.GetProcesses()
                                  where
                                    process.Id != currentProcess.Id &&
                                    process.ProcessName.Equals(
                                      currentProcess.ProcessName,
                                      StringComparison.Ordinal)
                                  select process).FirstOrDefault();
            if (runningProcess != null)
            {
                return;
            }

            RMS.Agent.Watchdog.App app = new RMS.Agent.Watchdog.App();
            app.InitializeComponent();
            app.Run();
        }

        public Window1()
        {
            InitializeComponent();
        }
        

    }
}
