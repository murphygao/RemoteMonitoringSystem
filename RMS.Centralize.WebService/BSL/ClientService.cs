﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Model;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService.BSL
{
    public class ClientService : BaseService
    {

        public List<ClientInfo> SearchClient(JQueryDataTableParamModel param, DateTime? asOfDate, int? clientTypeID, string clientCode, bool? clientStatus,
            string ipAddress, string location, out int totalRecord)
        {
            List<ClientInfo> lClientInfos = new List<ClientInfo>();
            SqlParameter[] parameters = new SqlParameter[11];

            try
            {
                using (var db = new MyDbContext())
                {
                    SqlParameter p1 = new SqlParameter("PageNbr", DBNull.Value);
                    SqlParameter p2 = new SqlParameter("PageSize", param.iDisplayLength);
                    SqlParameter p3 = new SqlParameter("FirstRec", param.iDisplayStart);

                    SqlParameter p4;
                    if (String.IsNullOrEmpty(param.iSortColumn))
                    {
                        p4 = new SqlParameter("SortCol", DBNull.Value);
                    }
                    else
                    {
                        p4 = new SqlParameter("SortCol", param.iSortColumn);
                    }

                    SqlParameter p5 = new SqlParameter("TotalRecords", SqlDbType.Int);
                    p5.Direction = ParameterDirection.Output;

                    SqlParameter p6;
                    SqlParameter p7;
                    SqlParameter p8;
                    SqlParameter p9;
                    SqlParameter p10;
                    SqlParameter p11;

                    if (asOfDate == null)
                    {
                        p6 = new SqlParameter("AsOfDate", DBNull.Value);
                        p6.DbType = DbType.DateTime;
                    }
                    else
                    {
                        p6 = new SqlParameter("AsOfDate", asOfDate);
                    }


                    if (clientTypeID == null)
                    {
                        p7 = new SqlParameter("ClientTypeID", DBNull.Value);
                        p7.SqlDbType = SqlDbType.Int;
                    }
                    else
                    {
                        p7 = new SqlParameter("ClientTypeID", clientTypeID);
                    }

                    if (String.IsNullOrEmpty(clientCode))
                    {
                        p8 = new SqlParameter("ClientCode", DBNull.Value);
                    }
                    else
                    {
                        p8 = new SqlParameter("ClientCode", clientCode);
                    }


                    if (clientStatus == null)
                    {
                        p9 = new SqlParameter("Enable", DBNull.Value);
                    }
                    else
                    {
                        p9 = new SqlParameter("Enable", clientStatus);
                    }

                    if (String.IsNullOrEmpty(ipAddress))
                    {
                        p10 = new SqlParameter("IPAddress", DBNull.Value);
                    }
                    else
                    {
                        p10 = new SqlParameter("IPAddress", ipAddress);
                    }

                    if (String.IsNullOrEmpty(location))
                    {
                        p11 = new SqlParameter("Location", DBNull.Value);
                    }
                    else
                    {
                        p11 = new SqlParameter("Location", location);
                    }



                    parameters[0] = p1;
                    parameters[1] = p2;
                    parameters[2] = p3;
                    parameters[3] = p4;
                    parameters[4] = p5;
                    parameters[5] = p6;
                    parameters[6] = p7;
                    parameters[7] = p8;
                    parameters[8] = p9;
                    parameters[9] = p10;
                    parameters[10] = p11;

                    db.Configuration.ProxyCreationEnabled = false;
                    //db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.Database.SqlQuery<ClientInfo>("RMS_ListClientMonitoring " +
                                                                            "@AsOfDate, @ClientTypeID, @ClientCode, @Enable" +
                                                                            ", @IPAddress, @Location" +
                                                                            ", @PageNbr, @PageSize, @FirstRec, @SortCol, @TotalRecords OUTPUT"
                        , parameters);

                    lClientInfos = new List<ClientInfo>(listOfType.ToList());
                    totalRecord = (int)parameters[4].Value;

                    return lClientInfos;
                }

            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "SearchClient failed. " + ex.Message, ex, false);
            }
        }

        public int Updateclient(int? id, string m, string clientCode, int? clientTypeID, bool? useLocalInfo, int? referenceClientID, string ipAddress, int? locationID, bool? hasMonitoringAgent, bool? activeList, bool? status, DateTime? effectiveDate, DateTime? expiredDate,
            int? state, string updatedBy)
        {
            try
            {
                if (string.IsNullOrEmpty(m))
                {
                    return Add(clientCode, clientTypeID, useLocalInfo, referenceClientID, ipAddress, locationID, hasMonitoringAgent, activeList, status, effectiveDate, expiredDate, state, updatedBy);
                }
                else if (m == "e" && id != null)
                {
                    return Update(id, clientCode, clientTypeID, useLocalInfo, referenceClientID, ipAddress, locationID, hasMonitoringAgent, activeList, status, effectiveDate, expiredDate, state, updatedBy);
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Updateclient failed. " + ex.Message, ex, false);
            }
        }


        public RmsClient GetClient(GetClientBy getClientBy, int? clientID, string clientCode, string ipAddress, bool activeClient)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    RmsClient client = new RmsClient();

                    if (getClientBy == GetClientBy.ClientID)
                    {
                        var ret = db.RmsClients.Where(c => c.ClientId == clientID && (!activeClient || (c.Enable == true && c.EffectiveDate <= DateTime.Today && (c.ExpiredDate == null || c.ExpiredDate >= DateTime.Today))));
                        var lClients = new List<RmsClient>(ret.ToList());

                        if (lClients.Count == 0)
                            throw new Exception("Client not found by ClientID: " + clientID + ". Please check Monitoring Database");

                        client = lClients[0];

                    }
                    else if (getClientBy == GetClientBy.ClientCode)
                    {
                        var ret = db.RmsClients.Where(c => c.ClientCode == clientCode && (!activeClient || (c.Enable == true && c.EffectiveDate <= DateTime.Today && (c.ExpiredDate == null || c.ExpiredDate >= DateTime.Today))));
                        var lClients = new List<RmsClient>(ret.ToList());

                        if (lClients.Count > 1)
                            throw new Exception("Found more thand one client by ClientCode: " + clientCode + ". Please check Monitoring Database");
                        if (lClients.Count == 0)
                            throw new Exception("Client not found by ClientCode: " + clientCode + ". Please check Monitoring Database");

                        client = lClients[0];

                    }
                    else if (getClientBy == GetClientBy.IPAddress)
                    {

                        var ret = db.RmsClients.Where(c => c.IpAddress == ipAddress && c.ClientTypeId == 1 && (!activeClient || (c.Enable == true && c.EffectiveDate <= DateTime.Today && (c.ExpiredDate == null || c.ExpiredDate >= DateTime.Today))));
                        var lClients = new List<RmsClient>(ret.ToList());

                        if (lClients.Count > 1)
                            throw new Exception("Found more thand one client by IPAddress: " + ipAddress +
                                                ". Please check Monitoring Database");
                        if (lClients.Count == 0)
                            throw new Exception("Client not found by IPAddress: " + ipAddress + ". Please check Monitoring Database");

                        client = lClients[0];

                    }
                    return client;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "GetClient failed. " + ex.Message, ex, false);
            }
        }

        public List<RmsClient> CheckExistingClientCode(string clientCode)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var ret = db.RmsClients.Where(c => c.ClientCode == clientCode);
                    var lClients = new List<RmsClient>(ret.ToList());

                    return lClients;

                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "CheckExistingClientCode failed. " + ex.Message, ex, false);
            }
        }

        private int Add(string clientCode, int? clientTypeID, bool? useLocalInfo, int? referenceClientID, string ipAddress, int? locationID, bool? hasMonitoringAgent, bool? activeList, bool? status
            , DateTime? effectiveDate, DateTime? expiredDate, int? state, string updatedBy)
        {
            int activeClients;

            try
            {

                if (status == true)
                {
                    var ms = new Centralize.BSL.MonitoringEngine.MonitoringService();
                    ms.ListClientWithIPAddress(licenseInfo, out activeClients, hasMonitoringAgent);
                    int? quota;
                    if (hasMonitoringAgent == null)
                        quota = licenseInfo.quota1 + licenseInfo.quota2;
                    else if (hasMonitoringAgent.Value)
                    {
                        quota = licenseInfo.quota1;
                    }
                    else
                    {
                        quota = licenseInfo.quota2;
                    }

                    if (activeClients >= quota)
                    {
                        throw new Exception("Out of active client's quota or expired. Please contact product owner.");
                    }
                }
                using (var db = new MyDbContext())
                {
                    using (var ts = db.Database.BeginTransaction())
                    {


                        var newClient = db.RmsClients.Create();
                        newClient.ClientCode = clientCode;
                        newClient.ClientTypeId = clientTypeID;
                        if (useLocalInfo == null || useLocalInfo.Value)
                        {
                            newClient.UseLocalInfo = true;
                            newClient.ReferenceClientId = null;
                            newClient.IpAddress = ipAddress;
                            newClient.LocationId = locationID;
                        }
                        else
                        {
                            newClient.UseLocalInfo = false;
                            newClient.ReferenceClientId = referenceClientID;
                            newClient.IpAddress = null;
                            newClient.LocationId = null;
                        }

                        newClient.HasMonitoringAgent = hasMonitoringAgent;
                        newClient.ActiveList = activeList;
                        newClient.Enable = status;
                        newClient.EffectiveDate = effectiveDate;
                        newClient.ExpiredDate = expiredDate;
                        newClient.State = 1;
                        newClient.CreatedBy = updatedBy;
                        newClient.UpdatedBy = updatedBy;


                        if (CheckExistingClientCode(clientCode).Count == 0)
                        {
                            db.RmsClients.Add(newClient);
                            db.SaveChanges();

                            var rmsSeverityLevels = db.RmsSeverityLevels;
                            foreach (var level in rmsSeverityLevels)
                            {
                                var rmsClientSeverityAction = db.RmsClientSeverityActions.Create();
                                rmsClientSeverityAction.ClientId = newClient.ClientId;
                                rmsClientSeverityAction.SeverityLevelId = level.SeverityLevelId;
                                rmsClientSeverityAction.CreatedBy = updatedBy;
                                rmsClientSeverityAction.UpdatedBy = updatedBy;

                                db.RmsClientSeverityActions.Add(rmsClientSeverityAction);

                            }

                            db.SaveChanges();
                            ts.Commit();
                            return 1;
                        }
                        else
                        {
                            return 2;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Add failed. " + ex.Message, ex, false);
            }
        }

        private int Update(int? id, string clientCode, int? clientTypeID, bool? useLocalInfo, int? referenceClientID, string ipAddress, int? locationID, bool? hasMonitoringAgent, bool? activeList, bool? status
            , DateTime? effectiveDate, DateTime? expiredDate, int? state, string updatedBy)
        {
            int activeClients;

            try
            {
                using (var db = new MyDbContext())
                {
                    var client = db.RmsClients.Find(id);
                    if (client == null) throw new Exception("Client ID not found in database.");
                    client.ClientCode = clientCode;
                    client.ClientTypeId = clientTypeID;
                    client.UseLocalInfo = useLocalInfo;

                    if (useLocalInfo == null || useLocalInfo.Value)
                    {
                        client.ReferenceClientId = null;
                        client.IpAddress = ipAddress;
                        client.LocationId = locationID;
                    }
                    else
                    {
                        client.ReferenceClientId = referenceClientID;
                        client.IpAddress = null;
                        client.LocationId = null;
                    }

                    client.HasMonitoringAgent = hasMonitoringAgent;
                    client.ActiveList = activeList;

                    if (client.Enable == false && status == true) // เปลี่ยนสถานะเป็น Active
                    {
                        var ms = new Centralize.BSL.MonitoringEngine.MonitoringService();
                        ms.ListClientWithIPAddress(licenseInfo, out activeClients, hasMonitoringAgent);
                        int? quota;
                        if (hasMonitoringAgent == null)
                            quota = licenseInfo.quota1 + licenseInfo.quota2;
                        else if (hasMonitoringAgent.Value)
                        {
                            quota = licenseInfo.quota1;
                        }
                        else
                        {
                            quota = licenseInfo.quota2;
                        }

                        if (activeClients >= quota)
                        {
                            throw new Exception("Out of active client's quota or expired. Please contact product owner.");
                        }
                    }

                    client.Enable = status;
                    client.EffectiveDate = effectiveDate;
                    client.ExpiredDate = expiredDate;
                    client.UpdatedBy = updatedBy;
                    if (state != null)
                        client.State = state;

                    var lClients = CheckExistingClientCode(clientCode);

                    if (lClients.Count == 0 || (lClients.Count == 1 && lClients[0].ClientId == id))
                    {
                        db.SaveChanges();
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Update failed. " + ex.Message, ex, false);
            }

        }

        public bool Delete(int id, string updatedBy)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    using (var ts = db.Database.BeginTransaction())
                    {

                        //RMS_ClientSeverityAction
                        //RMS_ClientMonitoring
                        //RMS_Client
                        db.Database.ExecuteSqlCommand("DELETE FROM RMS_ClientSeverityAction WHERE ClientID = {0}; " +
                                                      "DELETE FROM RMS_ClientMonitoring WHERE ClientID = {0}; " +
                                                      "DELETE FROM RMS_WebsiteMonitoring WHERE ClientID = {0}; " +
                                                      "DELETE FROM RMS_Client Where ClientID = {0};", id);

                        db.SaveChanges();

                        ts.Commit();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "DeleteClient (" + id + ") failed. " + ex.Message, ex, true);

            }
        }

        public List<ClientSeverityActionInfo> ListClientSeverityActions(int clientID)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.RmsClientSeverityActions.Include("RmsSeverityLevel").Where(w => w.ClientId == clientID);

                    List<RmsClientSeverityAction> lists = new List<RmsClientSeverityAction>(listOfType.ToList());

                    List<ClientSeverityActionInfo> listResult = new List<ClientSeverityActionInfo>();

                    foreach (var clientSeverityAction in lists)
                    {
                        ClientSeverityActionInfo info = new ClientSeverityActionInfo(clientSeverityAction);
                        listResult.Add(info);
                    }

                    return listResult;

                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "ListClientSeverityActions failed. " + ex.Message, ex, false);
            }

        }

        public ClientSeverityActionInfo GetClientSeverityAction(int clientID, int severityLevelID)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var clientAction = db.RmsClientSeverityActions.Include("RmsSeverityLevel").FirstOrDefault(w => w.ClientId == clientID && w.SeverityLevelId == severityLevelID);

                    if (clientAction == null) throw new Exception("ClientSeverityAction (clientID: " + clientID + ", severityLevelID: " + severityLevelID + ") Not Found.");

                    ClientSeverityActionInfo ret = new ClientSeverityActionInfo(clientAction);
                    return ret;

                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "GetClientSeverityActions failed. " + ex.Message, ex, false);
            }
        }

        public RmsClientSeverityAction UpdateClientSeverityAction(int clientID, int severityLevelID, bool overwritenAction, string email, string sms, int? commandLineID, string updatedBy)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var rms = db.RmsClientSeverityActions.Find(clientID, severityLevelID);
                    if (rms == null) throw new Exception("ClientSeverityAction (clientID: " + clientID + ", severityLevelID: " + severityLevelID + ") Not Found");

                    rms.OverwritenAction = overwritenAction;
                    rms.Email = string.IsNullOrEmpty(email)? null : email.Trim();
                    rms.Sms = string.IsNullOrEmpty(sms) ? null : sms.Trim();
                    rms.CommandLindId = commandLineID;
                    rms.UpdatedBy = updatedBy;
                    db.SaveChanges();

                    return rms;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "UpdateClientSeverityAction failed. " + ex.Message, ex, true);
            }
        }

    }
}