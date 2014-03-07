using System;
using System.IO;
using System.Threading;

namespace RMS.Monitoring.Device.PerformanceCounter
{
    public class PerformanceService
    {
        public void Monitoring()
        {
        }

        private decimal GetProcessorUsage()
        {
            System.Diagnostics.PerformanceCounter cpuCounter = null;
            try
            {
                cpuCounter = new System.Diagnostics.PerformanceCounter {CategoryName = "Processor", CounterName = "% Processor Time", InstanceName = "_Total"};

                cpuCounter.NextValue();
                Thread.Sleep(500);
                return Convert.ToDecimal(Math.Round(cpuCounter.NextValue(), 4));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cpuCounter != null)
                    cpuCounter.Dispose();
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
                throw ex;
            }
            finally
            {
                if (ramCounter != null)
                    ramCounter.Dispose();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driveName">C: or D: or E: or ...</param>
        /// <returns>Total free space in GB</returns>
        private decimal GetDiskFreeSpace(string driveName)
        {
            try
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    if (drive.DriveType == DriveType.CDRom || !drive.Name.ToLower().StartsWith(driveName.ToLower())) continue;

                    //There are more attributes you can use.
                    //Check the MSDN link for a complete example.
                    //Console.WriteLine(drive.Name);
                    if (drive.IsReady)
                        return Convert.ToDecimal(Math.Round(drive.TotalFreeSpace / Math.Pow(2, 30), 2));
                    break;
                }
                return -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
