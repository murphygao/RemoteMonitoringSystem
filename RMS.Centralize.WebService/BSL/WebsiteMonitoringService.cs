using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;
using RMS.Centralize.DAL;
using RMS.Centralize.WebService.Model;
using RMS.Common.Exception;

namespace RMS.Centralize.WebService.BSL
{
    public class WebsiteMonitoringService : BaseService
    {

        public List<WebsiteMonitoringInfo> ListWebsiteMonitoringsByClient(int clientID)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var listOfType = db.RmsWebsiteMonitorings.Include("RmsClient").Where(w => w.ClientId == clientID);

                    var lWebsitemotorings = new List<RmsWebsiteMonitoring>(listOfType.ToList());
                    List<WebsiteMonitoringInfo> lWebsiteMonitoringInfos = new List<WebsiteMonitoringInfo>();

                    List<RmsListOfValue> lListOfValues = new List<RmsListOfValue>();
                    if (lWebsitemotorings != null && lWebsitemotorings.Count > 0)
                    {
                        var dbSetLOV = db.RmsListOfValues;
                        lListOfValues = new List<RmsListOfValue>(dbSetLOV.ToList());
                    }

                    foreach (var rmsWebsiteMonitoring in lWebsitemotorings)
                    {
                        WebsiteMonitoringInfo _temp = new WebsiteMonitoringInfo();

                        _temp.InitInhertedProperties(rmsWebsiteMonitoring);

                        _temp.ClientCode = rmsWebsiteMonitoring.RmsClient.ClientCode;
                        _temp.ClientIPAddress = rmsWebsiteMonitoring.RmsClient.IpAddress;

                        var rmsListOfValue = lListOfValues.FirstOrDefault(f => f.ListName == "WebsiteMonitoringType" && f.ItemId == rmsWebsiteMonitoring.WebsiteMonitoringTypeId);
                        _temp.WebsiteMonitoringTypeValue = rmsListOfValue != null ? rmsListOfValue.ItemValue : null;

                        rmsListOfValue = lListOfValues.FirstOrDefault(f => f.ListName == "WebsiteMonitoringProtocol" && f.ItemId == rmsWebsiteMonitoring.WebsiteMonitoringProtocolId);
                        _temp.WebsiteMonitoringProtocolValue = rmsListOfValue != null ? rmsListOfValue.ItemValue : null;

                        lWebsiteMonitoringInfos.Add(_temp);
                    }


                    return lWebsiteMonitoringInfos;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "ListWebsiteMonitoringsByClient failed. " + ex.Message, ex, false);
            }

        }

        public WebsiteMonitoringInfo GetWebsiteMonitoring(int websiteMonitoringID)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;

                    var rmsWebsiteMonitoring = db.RmsWebsiteMonitorings.Include("RmsClient").First(w => w.WebsiteMonitoringId == websiteMonitoringID);

                    if (rmsWebsiteMonitoring == null) return null;

                    List<RmsListOfValue> lListOfValues = new List<RmsListOfValue>();
                    var dbSetLOV = db.RmsListOfValues;
                    lListOfValues = new List<RmsListOfValue>(dbSetLOV.ToList());

                    WebsiteMonitoringInfo _temp = new WebsiteMonitoringInfo();

                    _temp.InitInhertedProperties(rmsWebsiteMonitoring);

                    _temp.ClientCode = rmsWebsiteMonitoring.RmsClient.ClientCode;
                    _temp.ClientIPAddress = rmsWebsiteMonitoring.RmsClient.IpAddress;

                    var rmsListOfValue = lListOfValues.FirstOrDefault(f => f.ListName == "WebsiteMonitoringType" && f.ItemId == rmsWebsiteMonitoring.WebsiteMonitoringTypeId);
                    _temp.WebsiteMonitoringTypeValue = rmsListOfValue != null ? rmsListOfValue.ItemValue : null;

                    rmsListOfValue = lListOfValues.FirstOrDefault(f => f.ListName == "WebsiteMonitoringProtocol" && f.ItemId == rmsWebsiteMonitoring.WebsiteMonitoringProtocolId);
                    _temp.WebsiteMonitoringProtocolValue = rmsListOfValue != null ? rmsListOfValue.ItemValue : null;

                    return _temp;

                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "GetWebsiteMonitoring failed. " + ex.Message, ex, false);
            }

        }

        public RmsWebsiteMonitoring AddWebsiteMonitoring(int clientID, string websiteMonitoringTypeID, string websiteMonitoringProtocolID
            , int? portNumber, string domainName, bool enable, string updatedBy)
        {

            try
            {
                using (var db = new MyDbContext())
                {
                    var count = db.RmsWebsiteMonitorings.Count(w => w.ClientId == clientID);
                    if (count >= licenseInfo.iValue1) throw new Exception("Out of quota or expired. Please contact product owner.");


                    if (string.IsNullOrEmpty(websiteMonitoringTypeID))
                    {
                        var rmsListOfValue = db.RmsListOfValues.FirstOrDefault(w => w.ListName == "WebsiteMonitoringProtocol" && w.ItemId == websiteMonitoringProtocolID);
                        if (rmsListOfValue != null)
                        {
                            websiteMonitoringTypeID = rmsListOfValue.PItemId;
                        }
                    }


                    //RMS_MessageMaster
                    var rms = db.RmsWebsiteMonitorings.Create();
                    rms.ClientId = clientID;
                    rms.WebsiteMonitoringTypeId = websiteMonitoringTypeID;
                    rms.WebsiteMonitoringProtocolId = websiteMonitoringProtocolID;
                    rms.PortNumber = portNumber;
                    rms.DomainName = string.IsNullOrEmpty(domainName) ? null : domainName.Trim();
                    rms.Enable = enable;
                    rms.CreatedBy = updatedBy;
                    rms.UpdatedBy = updatedBy;


                    db.RmsWebsiteMonitorings.Add(rms);
                    db.SaveChanges();
                    return rms;
                }

            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "AddWebsiteMonitoring failed. " + ex.Message, ex, true);
            }

        }

        public RmsWebsiteMonitoring UpdateWebsiteMonitoring(int websiteMonitoringID, int clientID, string websiteMonitoringTypeID, string websiteMonitoringProtocolID
            , int? portNumber, string domainName, bool enable, string updatedBy)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    var rms = db.RmsWebsiteMonitorings.FirstOrDefault(w => w.WebsiteMonitoringId == websiteMonitoringID);
                    if (rms == null) throw new Exception("RmsWebsiteMonitorings (websiteMonitoringID=" + websiteMonitoringID + ") Not Found");

                    var count = db.RmsWebsiteMonitorings.Count(w => w.ClientId == clientID);
                    if (count > licenseInfo.iValue1) throw new Exception("Out of quota or expired. Please contact product owner.");

                    if (string.IsNullOrEmpty(websiteMonitoringTypeID))
                    {
                        var rmsListOfValue = db.RmsListOfValues.FirstOrDefault(w => w.ListName == "WebsiteMonitoringProtocol" && w.ItemId == websiteMonitoringProtocolID);
                        if (rmsListOfValue != null)
                        {
                            websiteMonitoringTypeID = rmsListOfValue.PItemId;
                        }
                    }

                    //RMS_WebsiteMonitoring

                    rms.ClientId = clientID;
                    rms.WebsiteMonitoringTypeId = websiteMonitoringTypeID;
                    rms.WebsiteMonitoringProtocolId = websiteMonitoringProtocolID;
                    rms.PortNumber = portNumber;
                    rms.DomainName = string.IsNullOrEmpty(domainName) ? null : domainName.Trim();
                    rms.Enable = enable;
                    rms.UpdatedBy = updatedBy;

                    db.SaveChanges();
                    return rms;
                }

            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "UpdateWebsiteMonitoring failed. " + ex.Message, ex, true);
            }

        }

        public bool DeleteWebsiteMonitoring(int websiteMonitoringID, string updatedBy)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    var rms = db.RmsWebsiteMonitorings.Create();
                    rms.WebsiteMonitoringId = websiteMonitoringID;
                    db.RmsWebsiteMonitorings.Attach(rms);
                    db.RmsWebsiteMonitorings.Remove(rms);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "DeleteWebsiteMonitoring (websiteMonitoringID=" + websiteMonitoringID + ") failed. " + ex.Message, ex, true);

            }
        }
    }

}