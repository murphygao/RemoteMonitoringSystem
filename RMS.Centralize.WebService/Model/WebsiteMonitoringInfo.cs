using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMS.Centralize.DAL;
using System.Reflection;

namespace RMS.Centralize.WebService.Model
{
    public class WebsiteMonitoringInfo : RmsWebsiteMonitoring
    {
        public string ClientCode { get; set; }
        public string ClientIPAddress { get; set; }
        public string WebsiteMonitoringTypeValue { get; set; }
        public string WebsiteMonitoringProtocolValue { get; set; }

        /// <summary> copy base class instance's property values to this object. </summary>
        public void InitInhertedProperties(object baseClassInstance)
        {
            foreach (PropertyInfo propertyInfo in baseClassInstance.GetType().GetProperties())
            {
                object value = propertyInfo.GetValue(baseClassInstance, null);
                if (null != value) propertyInfo.SetValue(this, value, null);
            }
        }
    }
}