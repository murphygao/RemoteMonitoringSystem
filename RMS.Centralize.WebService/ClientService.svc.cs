using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Model;

namespace RMS.Centralize.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ClientService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ClientService.svc or ClientService.svc.cs at the Solution Explorer and start debugging.
    public class ClientService : IClientService
    {
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

                        var _client = db.RmsClients.Where(c => c.ClientId == clientID && c.EffectiveDate <= DateTime.Today)
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
    }
}
