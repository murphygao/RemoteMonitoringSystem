using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Management;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Model;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ClientService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ClientService.svc or ClientService.svc.cs at the Solution Explorer and start debugging.
    public class ClientService : IClientService
    {
        public void TestConnection()
        {
        }

        public ClientResult GetClient(GetClientBy getClientBy, int? clientID, string clientCode, string ipAddress, bool withDetail, bool activeClient)
        {
            try
            {
                BSL.ClientService cs = new BSL.ClientService();
                var client = cs.GetClient(getClientBy, clientID, clientCode, ipAddress, activeClient);

                var sr = new ClientResult
                {
                    IsSuccess = true,
                    Client = client,

                };

                if (client != null && withDetail)
                {

                    using (var db = new MyDbContext())
                    {
                        db.Configuration.ProxyCreationEnabled = false;
                        db.Configuration.LazyLoadingEnabled = false;

                        RmsMonitoringProfile monitorngProfile = new RmsMonitoringProfile();
                        List<RmsMonitoringProfileDevice> monitoringProfileDevices = new List<RmsMonitoringProfileDevice>();
                        List<RmsDevice> devices = new List<RmsDevice>();


                        var _client = db.RmsClients.Where(c => c.ClientId == client.ClientId && (!activeClient || (c.Enable == true && c.EffectiveDate <= DateTime.Today && (c.ExpiredDate == null || c.ExpiredDate >= DateTime.Today))))
                            .Include(
                                i =>
                                    i.RmsClientMonitorings.Select(cm => cm.RmsMonitoringProfile)
                                        .Select(mp => mp.RmsMonitoringProfileDevices.Select(mpd => mpd.RmsDevice))).FirstOrDefault();

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

                }
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "GetClient failed. " + ex.Message, ex, true);

                var sr = new ClientResult
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
                return sr;
            }
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
                new RMSWebException(this, "0500", "SetClientState failed. " + ex.Message, ex, true);

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
                new RMSWebException(this, "0500", "Search failed. " + ex.Message, ex, true);

                var sr = new ClientInfoResult
                {
                    IsSuccess = false,
                    ErrorMessage = "Search errors. " +  ex.Message
                };
                return sr;
            }
        }

        public Result Update(int? id, string m, string clientCode, int? clientTypeID, bool? useLocalInfo, int? referenceClientID, string ipAddress, int? locationID, bool? hasMonitoringAgent, bool? activeList, bool? status, DateTime? effectiveDate, DateTime? expiredDate, int? state)
        {
            try
            {
                var cs = new BSL.ClientService();
                int result = cs.Updateclient(id, m, clientCode, clientTypeID, useLocalInfo, referenceClientID, ipAddress, locationID, hasMonitoringAgent, activeList, status, effectiveDate, expiredDate, state);

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
                new RMSWebException(this, "0500", "Update failed. " + ex.Message, ex, true);

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
                new RMSWebException(this, "0500", "ExistingClientCode failed. " + ex.Message, ex, true);

                var sr = new ClientResult
                {
                    IsSuccess = false,
                    ErrorMessage = "ExistingClientCode errors. " + ex.Message
                };
                return sr;
            }

        }

        public MainAppClientResult ListMainAppClient()
        {
            try
            {
                BSL.ClientService cs = new BSL.ClientService();
                var listMainAppClient = cs.ListMainAppClient();

                var sr = new MainAppClientResult
                {
                    IsSuccess = true,
                    ListMainAppClients = listMainAppClient,
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "ListMainAppClient failed. " + ex.Message, ex, true);

                var sr = new MainAppClientResult
                {
                    IsSuccess = false,
                    ErrorMessage = "ListMainAppClient errors. " + ex.Message
                };
                return sr;
            }
        }

        public LocationResult ListLocation()
        {
            try
            {
                BSL.ClientService cs = new BSL.ClientService();
                var listLocation = cs.ListLocation();

                var sr = new LocationResult
                {
                    IsSuccess = true,
                    ListLocations = listLocation,
                };
                return sr;
            }
            catch (Exception ex)
            {
                new RMSWebException(this, "0500", "ListLocation failed. " + ex.Message, ex, true);

                var sr = new LocationResult
                {
                    IsSuccess = false,
                    ErrorMessage = "ListLocation errors. " + ex.Message
                };
                return sr;
            }
        }

    }
}
