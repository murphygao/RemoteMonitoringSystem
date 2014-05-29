using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Management;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Model;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ClientService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ClientService.svc or ClientService.svc.cs at the Solution Explorer and start debugging.
    public class ClientService : IClientService
    {
        public void TestConnection()
        {
        }

        public ClientResult GetClient(GetClientBy getClientBy, int? clientID, string clientCode, string ipAddress, bool withDetail)
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
                        var ret = db.RmsClients.Where(c => c.ClientId == clientID && c.EffectiveDate <= DateTime.Today);
                        var lClients = new List<RmsClient>(ret.ToList());

                        if (lClients.Count == 0)
                            throw new Exception("Active Client not found by ClientID:" + clientID + ". Please check Monitoring Database");

                        client = lClients[0];

                    }
                    else if (getClientBy == GetClientBy.ClientCode)
                    {
                        var ret = db.RmsClients.Where(c => c.ClientCode == clientCode && c.EffectiveDate <= DateTime.Today);
                        var lClients = new List<RmsClient>(ret.ToList());

                        if (lClients.Count > 1)
                            throw new Exception("Found more thand one client by ClientCode:" + clientCode + ". Please check Monitoring Database");
                        if (lClients.Count == 0)
                            throw new Exception("Active Client not found by ClientCode:" + clientCode + ". Please check Monitoring Database");

                        client = lClients[0];
                        clientID = client.ClientId;

                    }
                    else if (getClientBy == GetClientBy.IPAddress)
                    {

                        // Finding Main App Client by IP Address
                        var ret = db.Database.SqlQuery<MainAppClient>("RMS_GetMainAppClientByIPAddress " +
                                                                      "@IPAddress");

                        var lMainAppClients = new List<MainAppClient>(ret.ToList());

                        if (lMainAppClients.Count > 1)
                            throw new Exception("Found more thand one client by IPAddress:" + clientCode + ". Please check MainApp Database");
                        if (lMainAppClients.Count == 0)
                            throw new Exception("Client not found by IPAddress:" + clientCode + ". Please check MainApp Database");

                        // Got Main App Client
                        var mainAppClient = lMainAppClients[0];

                        // Finding Client by Reference Client ID
                        var ret2 = db.RmsClients.Where(c => c.ReferenceClientId == mainAppClient.ClientID && c.EffectiveDate <= DateTime.Today);
                        var lClients = new List<RmsClient>(ret2.ToList());
                        if (lClients.Count > 1)
                            throw new Exception("Found more thand one client by ReferenceClientId:" + clientCode +
                                                ". Please check Monitoring Database");
                        if (lClients.Count == 0)
                            throw new Exception("Active Client not found by ReferenceClientId:" + clientCode + ". Please check Monitoring Database");

                        client = lClients[0];
                        clientID = client.ClientId;

                    }

                    var sr = new ClientResult
                    {
                        IsSuccess = true,
                        Client = client,

                    };

                    if (client != null && withDetail)
                    {
                        RmsMonitoringProfile monitorngProfile = new RmsMonitoringProfile();
                        List<RmsMonitoringProfileDevice> monitoringProfileDevices = new List<RmsMonitoringProfileDevice>();
                        List<RmsDevice> devices = new List<RmsDevice>();

                        db.Configuration.ProxyCreationEnabled = false;
                        db.Configuration.LazyLoadingEnabled = false;

                        var _client = db.RmsClients.Where(c => c.ClientId == clientID && c.Enable == true && c.EffectiveDate <= DateTime.Today)
                            .Include(i => i.RmsClientMonitorings.Select(cm => cm.RmsMonitoringProfile).Select(mp => mp.RmsMonitoringProfileDevices.Select(mpd => mpd.RmsDevice))).FirstOrDefault();

                        if (_client != null)
                        {
                            var rmsClientMonitoring = _client.RmsClientMonitorings.Where(cm => cm.EffectiveDate <= DateTime.Today)
                                .OrderByDescending(od => od.EffectiveDate)
                                .FirstOrDefault();
                            if (rmsClientMonitoring != null)
                            {
                                monitorngProfile = rmsClientMonitoring.RmsMonitoringProfile;
                                sr.MonitoringProfile = monitorngProfile;

                                monitoringProfileDevices = new List<RmsMonitoringProfileDevice>(monitorngProfile.RmsMonitoringProfileDevices);
                                sr.ListMonitoringProfileDevices = monitoringProfileDevices;

                                foreach (var mpd in monitoringProfileDevices)
                                {
                                    if (devices.All(d => d.DeviceId != mpd.RmsDevice.DeviceId))
                                        devices.Add(mpd.RmsDevice);
                                }
                                sr.ListDevices = new List<RmsDevice>(devices);
                            }
                        }

                        sr.ListDeviceType = db.RmsDeviceTypes.ToList();
                    }


                    return sr;

                }
            }
            catch (Exception ex)
            {
                var sr = new ClientResult
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
                return sr;
            }
            return null;
        }

        public Result SetClientState(int clientID, ClientState state)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    var client = db.RmsClients.Find(clientID);
                    client.State = (int) state;

                    db.SaveChanges();
                }

                var sr = new Result
                {
                    IsSuccess = true
                };
                return sr;
            }
            catch (Exception ex)
            {
                var sr = new Result
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
                return sr;
            }
        }

        public ClientInfoResult Search(JQueryDataTableParamModel param, DateTime? asOfDate, int? clientTypeID, string clientCode, bool? clientStatus, string ipAddress)
        {
            try
            {
                int totalRecord;
                BSL.ClientService cs = new BSL.ClientService();
                List<ClientInfo> clientInfos = cs.SearchClient(param, asOfDate, clientTypeID, clientCode, clientStatus, ipAddress, out totalRecord);

                var sr = new ClientInfoResult
                {
                    IsSuccess = true,
                    ListClients = clientInfos,
                    TotalRecords = totalRecord
                };

                return sr;

            }
            catch (Exception ex)
            {
                var sr = new ClientInfoResult
                {
                    IsSuccess = false,
                    ErrorMessage = "Search errors. " +  ex.Message
                };
                return sr;
            }
        }

        public Result Update(int? id, string m, string clientCode, int? clientTypeID, int? referenceClientID, bool? activeList, bool? status, DateTime? effectiveDate, DateTime? expiredDate, int? state)
        {
            try
            {
                var cs = new BSL.ClientService();
                int result = cs.Updateclient(id, m, clientCode, clientTypeID, referenceClientID, activeList, status, effectiveDate, expiredDate, state);

                if (result == 1) // Complete
                {
                    var sr = new Result
                    {
                        IsSuccess = true
                    };
                    return sr;
                }
                else if (result == 2)
                {
                    var sr = new Result
                    {
                        IsSuccess = false,
                        ErrorMessage = "Client code is taken already."
                    };
                    return sr;
                }
                else
                {
                    var sr = new Result
                    {
                        IsSuccess = false,
                        ErrorMessage = "Unknown error code: " + result
                    };
                    return sr;
                }

            }
            catch (Exception ex)
            {
                var sr = new Result
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
                return sr;
            }
            
        }

        public ClientResult ExistingClientCode(string clientCode)
        {
            try
            {
                var cs = new BSL.ClientService();
                var clients = cs.CheckExistingClientCode(clientCode);

                var sr = new ClientResult
                {
                    IsSuccess = true,
                    ListClients = clients,
                    TotalRecords = clients.Count
                };
                return sr;
            }
            catch (Exception ex)
            {
                var sr = new ClientResult
                {
                    IsSuccess = false,
                    ErrorMessage = "Search errors. " + ex.Message
                };
                return sr;
            }

        }
    }
}
