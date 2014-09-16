using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using RMS.Centralize.DAL;

namespace RMS.Centralize.WebService.Model
{
    public class ListOfValueInfo : RmsListOfValue
    {
        public string PItemValue { get; set; }
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