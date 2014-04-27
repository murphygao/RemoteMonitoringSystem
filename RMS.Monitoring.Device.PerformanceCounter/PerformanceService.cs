using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using RMS.Agent.Proxy.ClientProxy;
using RMS.Agent.Proxy.MonitoringProxy;
using RMS.Monitoring.Helper;

namespace RMS.Monitoring.Device.PerformanceCounter
{
    public class PerformanceService
    {
        public List<RmsReportMonitoringRaw> Monitoring(ClientResult clientResult)
        {
            List<RmsReportMonitoringRaw> lRmsReportMonitoringRaws = new List<RmsReportMonitoringRaw>();

            /*
             * 1. Check CPU
             * 2. Check RAM
             * 3. Check Disk
             * 
             */

            #region 1. Check CPU

            var list = Common.GetRmsMonitoringProfileDevicebyDeviceCode(clientResult, "CPU", Models.DeviceCode.Performance);

            if (list.Count > 0)
            {
                decimal? highThreshold = list[0].HighThreshold;
                var processorUsage = GetProcessorUsage();

                RmsReportMonitoringRaw raw = new RmsReportMonitoringRaw();
                raw.ClientCode = clientResult.Client.ClientCode;
                raw.DeviceCode = "CPU";

                if (processorUsage > highThreshold)
                {
                    raw.Message = "OVER_CPU_USAGE";
                }
                else
                {
                    raw.Message = "OK";
                }
                raw.MessageDateTime = DateTime.Now;
                raw.MonitoringProfileDeviceId = list[0].MonitoringProfileDeviceId;

                lRmsReportMonitoringRaws.Add(raw);
            }

            #endregion

            #region 2. Check RAM

            list = Common.GetRmsMonitoringProfileDevicebyDeviceCode(clientResult, "MEMORY", Models.DeviceCode.Performance);

            if (list.Count > 0)
            {
                decimal? lowThreshold = list[0].LowThreshold;
                var availableMemory = GetAvailableMemory();

                RmsReportMonitoringRaw raw = new RmsReportMonitoringRaw();
                raw.ClientCode = clientResult.Client.ClientCode;
                raw.DeviceCode = "MEMORY";

                if (availableMemory < lowThreshold)
                {
                    raw.Message = "LOW_MEMORY";
                }
                else
                {
                    raw.Message = "OK";
                }
                raw.MessageDateTime = DateTime.Now;
                raw.MonitoringProfileDeviceId = list[0].MonitoringProfileDeviceId;

                lRmsReportMonitoringRaws.Add(raw);
            }

            #endregion

            #region 3. Check Disk

            list = Common.GetRmsMonitoringProfileDevicebyDeviceCode(clientResult, "DISK", Models.DeviceCode.Performance);
            foreach (var rmsMonitoringProfileDevice in list)
            {
                decimal? lowThreshold = rmsMonitoringProfileDevice.LowThreshold;
                var diskFreeSpace = GetDiskFreeSpace(rmsMonitoringProfileDevice.StringValue);

                RmsReportMonitoringRaw raw = new RmsReportMonitoringRaw();
                raw.ClientCode = clientResult.Client.ClientCode;
                raw.DeviceCode = "DISK";

                if (diskFreeSpace < lowThreshold)
                {
                    raw.Message = "LOW_DISK_SPACE";
                }
                else
                {
                    raw.Message = "OK";
                }
                raw.MessageDateTime = DateTime.Now;
                raw.MonitoringProfileDeviceId = rmsMonitoringProfileDevice.MonitoringProfileDeviceId;

                lRmsReportMonitoringRaws.Add(raw);
            }

            #endregion


            return lRmsReportMonitoringRaws;
        }

        #region Private Methods

        private decimal GetProcessorUsage()
        {
            System.Diagnostics.PerformanceCounter cpuCounter = null;
            try
            {
                cpuCounter = new System.Diagnostics.PerformanceCounter
                {
                    CategoryName = "Processor",
                    CounterName = "% Processor Time",
                    InstanceName = "_Total"
                };

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
                        return Convert.ToDecimal(Math.Round(drive.TotalFreeSpace/Math.Pow(2, 30), 2));
                    break;
                }
                return -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
