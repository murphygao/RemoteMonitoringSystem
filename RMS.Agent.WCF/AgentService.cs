using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.ServiceModel;
using System.Text;
using RMS.Agent.BSL.AutoUpate;
using RMS.Agent.BSL.Monitoring;
using RMS.Agent.BSL.Monitoring.Models;
using RMS.Agent.BSL.RemoteCommand;
using RMS.Agent.Entity;
using RMS.Agent.Helper;
using RMS.Agent.Model;
using RMS.Agent.WCF.Model;
using RMS.Common.Exception;
using RMS.Monitoring.API;
using EventLog = RMS.Agent.Model.EventLog;

namespace RMS.Agent.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AgentService" in both code and config file together.
    public class AgentService : IAgentService
    {
        private BSL.AutoUpate.AutoUpdateService autoUpdateService;
        private BSL.RemoteCommand.RemoteCommandService commandService;
        private BSL.Monitoring.MonitoringService monitoringService;
        private string tempEventFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\logs\TempEvent.txt";
        public static object _lock = new object();

        public void TestConnection()
        {
            try
            {
                AddLog("Testing", "Testing Agent Service", "");
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "TestConnection failed. " + ex.Message, ex, true);
            }
        }

        public void AutoUpdate(string type)
        {
            try
            {
                AddLog("Auto Update", "Update Agent", "");
                autoUpdateService = new AutoUpdateService();
                autoUpdateService.Command(type);
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "AutoUpdate failed. " + ex.Message, ex, true);
            }
        }

        public void RemoteCommand(RemoteCommand remoteCommand)
        {
            try
            {
                commandService = new RemoteCommandService();
                commandService.Command(remoteCommand);
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "RemoteCommand failed. " + ex.Message, ex, true);
            }
        }

        public Result Monitoring(string clientCode)
        {
            try
            {
                AddLog("Monitoring", "Monitoring Performances & Peripherals", "Client Code: " + clientCode);

                monitoringService = new MonitoringService();
                monitoringService.Command(clientCode);

                return new Result {IsSuccess = true};
            }
            catch (Exception ex)
            {
                AddLog("Monitoring", "Monitoring Failed", ex.Message + (ex.InnerException == null? string.Empty : " " + ex.InnerException.Message));
                throw new RMSAppException(this, "0500", "Monitoring failed. " + ex.Message, ex, true);
            }
        }

        public List<DeviceStatus> SelfMonitoring(string clientCode)
        {
            try
            {
                AddLog("Self Monitoring", "Monitoring Performances & Peripherals", (string.IsNullOrEmpty(clientCode)? string.Empty : "Client Code: " + clientCode));

                monitoringService = new MonitoringService();
                return monitoringService.SelfMonitoring(clientCode);

            }
            catch (Exception ex)
            {
                AddLog("Self Monitoring", "SelfMonitoring Failed", ex.Message + (ex.InnerException == null ? string.Empty : " " + ex.InnerException.Message));
                throw new RMSAppException(this, "0500", "SelfMonitoring failed. " + ex.Message, ex, true);
            }
        }

        public PaperStatusResult CheckPaperStatus(string brand, string model, string vid, string pid, bool enableTextFileStatus)
        {
            try
            {
                if (string.IsNullOrEmpty(brand))
                {
                    return new PaperStatusResult { IsSuccess = false, ErrorMessage = "brand cannot be null or empty." };
                }

                if (string.IsNullOrEmpty(vid))
                {
                    return new PaperStatusResult { IsSuccess = false, ErrorMessage = "vid cannot be null or empty." };
                }

                if (string.IsNullOrEmpty(pid))
                {
                    return new PaperStatusResult { IsSuccess = false, ErrorMessage = "pid cannot be null or empty." };
                }

                string[] listPaperStatus;

                if (brand.ToLower().Trim() == "brother")
                {
                    Brother service = new Brother();
                    listPaperStatus = service.CheckPaperStatus(model, vid, pid);
                }
                else if (brand.ToLower().Trim() == "custom")
                {
                    Custom service = new Custom();
                    listPaperStatus = service.CheckPaperStatus(model, vid, pid);
                }
                else
                {
                    return new PaperStatusResult { IsSuccess = false, ErrorMessage = "brand not match." };
                }

                if (enableTextFileStatus)
                {
                    string MessageStorageFolder = ConfigurationManager.AppSettings["RMS.MessageStorageFolder"];
                    if (!string.IsNullOrEmpty(MessageStorageFolder))
                    {
                        Directory.CreateDirectory(MessageStorageFolder);

                        // ทำการลบไฟล์ status ออกจาก folder
                        // โดยการดึงรายชื่อไฟล์ทั้งหมดของอุปกรณ์นั้น ไปเทียบกับใน result list
                        // หากชื่อไฟล์ ไม่อยู่ใน list ให้ลบทิ้ง (ทั้งนี้ list ต้องมีข้อมูลอยู่ หากไม่มีเลย แสดงว่า ดึง status ไม่ได้ ให้ข้ามการลบไป)

                        // ถ้ามีข้อมูล status ใน list ให้ทำการลบไฟล์ ที่ไม่เกียวข้อง
                        if (listPaperStatus.Length > 0)
                        {
                            string[] listFileStatus = Directory.GetFiles(MessageStorageFolder, "*_" + brand + ".txt");
                            if (listPaperStatus[0] != null && listPaperStatus[0].ToLower() == "ok")
                            {
                                //แสดงว่า status ปกติ ให้ลบไฟล์ทั้งหมด
                                foreach (var filestatus in listFileStatus)
                                {
                                    try
                                    {
                                        File.Delete(filestatus);
                                    }
                                    catch (Exception ex)
                                    {
                                        new RMSAppException(this, "0500", "CheckPaperStatus failed. " + filestatus + " cannot be deleted. " + ex.Message, ex, true);
                                    }
                                }
                            }
                            else
                            {
                                //ให้เลือกลบที่ไม่เกี่ยวข้อง
                                foreach (var filestatus in listFileStatus)
                                {
                                    // คัดกรองรูปแบบ text file
                                    // [STATUS]_[BRAND].txt
                                    string statusFromFile = Path.GetFileNameWithoutExtension(filestatus).Replace("_" + brand, "");
                                    if (listPaperStatus.All(a => a.ToLower() != statusFromFile.ToLower()))
                                    {
                                        try
                                        {
                                            File.Delete(filestatus);
                                        }
                                        catch (Exception ex)
                                        {
                                            new RMSAppException(this, "0500", "CheckPaperStatus failed. " + filestatus + " cannot be deleted. " + ex.Message, ex, true);
                                        }
                                    }
                                }
                            }
                        }

                        // ถ้าไม่ใช่ OK status ให้เริ่มเขียน text file
                        foreach (var paperStatus in listPaperStatus)
                        {
                            if (paperStatus.ToLower() != "ok")
                            {
                                try
                                {
                                    using (File.Create(MessageStorageFolder + paperStatus.ToUpper() + "_" + brand.ToUpper() + ".txt"))
                                    {
                                    }
                                }
                                catch (Exception ex)
                                {
                                    new RMSAppException(this, "0500", "CheckPaperStatus failed. " + MessageStorageFolder + paperStatus.ToUpper() + "_" + brand.ToUpper() + ".txt" + " cannot be created. " + ex.Message, ex, true);
                                }
                            }
                        }
                    }
                }
                return new PaperStatusResult { IsSuccess = true, ListStatus = listPaperStatus };
            }
            catch (Exception ex)
            {
                return new PaperStatusResult { IsSuccess = false, ErrorMessage = ex.Message };
            }
        }


        private void AddLog(string eventType, string message, string detail)
        {
            try
            {
                lock (_lock)
                {
                    ListEventLogs logs = new ListEventLogs();

                    string strResultList = string.Empty;

                    if (!Directory.Exists(System.IO.Path.GetDirectoryName(tempEventFile)))
                        Directory.CreateDirectory(System.IO.Path.GetDirectoryName(tempEventFile));

                    if (File.Exists(tempEventFile))
                    {

                        using (FileStream fs = File.OpenRead(tempEventFile))
                        {
                            using (TextReader tr = new StreamReader(fs))
                            {
                                strResultList = tr.ReadToEnd();
                                tr.Close();
                            }
                            fs.Close();

                        }
                        if (!string.IsNullOrEmpty(strResultList))
                        {
                            logs = Serializer.XML.DeserializeObject<ListEventLogs>(strResultList);
                        }

                    }

                    logs.Add(new EventLog {EventDateTime = DateTime.Now, EventType = eventType, Message = message, Detail = detail});
                    strResultList = Serializer.XML.SerializeObject(logs);
                    using (TextWriter tw = new StreamWriter(tempEventFile, false)) // Create & open the file
                    {
                        tw.Write(strResultList);
                        tw.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //string eventType, string message, string detail
                throw new RMSAppException(this, "0500", "AddLog failed. eventType=" + eventType + ", message=" + message + ", detail=" + detail + ", " + ex.Message, ex, false);
            }
        }
    }
}
