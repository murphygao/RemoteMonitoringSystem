﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using RMS.Centralize.WebSite.Proxy.LocationProxy;
using RMS.Common.Exception;

namespace RMS.Centralize.WebSite.Proxy
{
    public class LocationService
    {
        #region Private members section

        private string webServicURL;
        private string webServiceUserName;
        private string webServicePassword;
        private int? proxyEnable;
        private string proxyAddress;
        private int? proxyPort;
        private string proxyUserName;
        private string proxyPassword;
        private int? timeOut;
        private int? maxServicePointIdleTime1;
        private int? maxServicePointIdleTime2;


        #endregion

        #region *** Initial section ***

        LocationServiceClient _LocationService;

        #region Constructor

        public LocationService()
            : this(null)
        {
        }

        public LocationService(string urlWebService)
        {

            try
            {
                _LocationService = new LocationServiceClient();
                Initialize(urlWebService);
            }
            catch (Exception ex)
            {
                _LocationService = null;
                throw new RMSWebException(this, "0500", "LocationService ctor failed. " + ex.Message, ex, false);
            }
        }


        #endregion

        public LocationServiceClient locationService
        {
            get { return _LocationService; }
            set { _LocationService = value; }
        }

        private void Initialize(string urlWebService)
        {
            try
            {
                try
                {
                    if (urlWebService != null)
                        webServicURL = urlWebService;
                    else
                        webServicURL = ConfigurationManager.AppSettings["RMS.WebServicURL_LocationService"];


                    webServiceUserName = ConfigurationManager.AppSettings["WebServiceUserName"];
                    webServicePassword = ConfigurationManager.AppSettings["WebServicePassword"];

                    if (ConfigurationManager.AppSettings["TimeOut"] != null)
                        timeOut = int.Parse(ConfigurationManager.AppSettings["TimeOut"]);

                    if (ConfigurationManager.AppSettings["MaxServicePointIdleTime1"] != null)
                        maxServicePointIdleTime1 = int.Parse(ConfigurationManager.AppSettings["MaxServicePointIdleTime1"]);

                    if (ConfigurationManager.AppSettings["MaxServicePointIdleTime2"] != null)
                        maxServicePointIdleTime2 = int.Parse(ConfigurationManager.AppSettings["MaxServicePointIdleTime2"]);


                    //if (ConfigurationManager.AppSettings["OverrideProxy"] == null)
                    //    throw new NullReferenceException("OverrideProxy cannot be null.");

                    bool isOverrideProxy = Convert.ToBoolean(ConfigurationManager.AppSettings["OverrideProxy"]);
                    if (isOverrideProxy)
                    {
                        if (ConfigurationManager.AppSettings["ProxyEnable"] == null)
                            throw new NullReferenceException("ProxyEnable cannot be null.");
                        if (ConfigurationManager.AppSettings["ProxyAddress"] == null)
                            throw new NullReferenceException("ProxyAddress cannot be null.");
                        if (ConfigurationManager.AppSettings["ProxyPort"] == null)
                            throw new NullReferenceException("ProxyPort cannot be null.");
                        if (ConfigurationManager.AppSettings["ProxyUserName"] == null)
                            throw new NullReferenceException("ProxyUserName cannot be null.");
                        if (ConfigurationManager.AppSettings["ProxyPassword"] == null)
                            throw new NullReferenceException("ProxyPassword cannot be null.");

                        proxyEnable = int.Parse(ConfigurationManager.AppSettings["ProxyEnable"]);
                        proxyAddress = ConfigurationManager.AppSettings["ProxyAddress"];
                        proxyPort = int.Parse(ConfigurationManager.AppSettings["ProxyPort"]);
                        proxyUserName = ConfigurationManager.AppSettings["ProxyUserName"];
                        proxyPassword = ConfigurationManager.AppSettings["ProxyPassword"];

                    }
                }
                catch (Exception ex)
                {
                    throw new RMSWebException(this, "0500", "Initialize Web.config/App.config failed. " + ex.Message, ex, false);
                }

                /*** set initial ***/

                _LocationService.Endpoint.Address = new EndpointAddress(webServicURL);
                if (timeOut != null)
                    _LocationService.Endpoint.Binding.SendTimeout = new TimeSpan(0, 0, timeOut.Value);

                var b = _LocationService.Endpoint.Binding as System.ServiceModel.BasicHttpBinding;

                b.UseDefaultWebProxy = true;

                WebRequest.DefaultWebProxy = GetWebProxy();

                /********************/

            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "Initialize failed. " + ex.Message, ex, false);
            }
        }

        public WebProxy GetWebProxy()
        {
            try
            {
                if (proxyEnable != null && !string.IsNullOrEmpty(proxyAddress)
                    && proxyPort != null && proxyEnable.Value == 1)
                {
                    if (maxServicePointIdleTime1 != null)
                        ServicePointManager.MaxServicePointIdleTime = 1000 * maxServicePointIdleTime1.Value;
                    var proxy = new WebProxy(proxyAddress, proxyPort.Value);
                    proxy.Credentials = new NetworkCredential(proxyUserName, proxyPassword);
                    return proxy;
                }
                else
                {
                    if (maxServicePointIdleTime2 != null)
                        ServicePointManager.MaxServicePointIdleTime = 1000 * maxServicePointIdleTime2.Value;
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new RMSWebException(this, "0500", "GetWebProxy failed. " + ex.Message, ex, false);
            }
        }

        #endregion

    }
}
