using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Web;
using ESN.LicenseManager.Helper;
using ESN.LicenseManager.Model;

namespace RMS.Centralize.WebService.BSL
{
    public class BaseService
    {
        private const string Lic1 = "jyh+b+xr3nOcz7emiKx1ujj0sD2xQQLV2CqPj69OyZGYWcw6p9N+I0TVPAltOnMf5/nTOkPgVS6xeAC9WBfMlOMpAa93zvour57nlVATiAOYfNlUFBK6JTG9+6WQW+Yk";
        private const string companyCode = "AIS";
        private const string assemblyName = "ESN.LicenseManager.Model";
        
        public static string Lic2 = "";

        private static LicenseInfo _license = null;
        public static LicenseInfo licenseInfo { get { return _license; } }

        public BaseService()
        {
            InitialLicense();
        }

        private void InitialLicense()
        {
            try
            {
                string licPath = ConfigurationManager.AppSettings["RMS.LicenseFile"];

                if (string.IsNullOrEmpty(licPath)) throw new Exception("Cannot find License file path. Please check web.config.");

                if (!File.Exists(licPath)) throw new Exception("Cannot find License file path. Please check web.config.");

                string _tmpLic2 = File.ReadAllText(licPath);

                if (string.IsNullOrEmpty(_tmpLic2)) throw new Exception("License cannot be null or empty. Please contact product owner.");

                if (Lic2 != _tmpLic2)
                {
                    Lic2 = _tmpLic2;

                    string Lic = Lic1 + Lic2;

                    ObjectHandle handle = Activator.CreateInstance(assemblyName, assemblyName + "." + companyCode);
                    BaseAESPassword aesPassword = (BaseAESPassword) handle.Unwrap();

                    string decrypted = Cryptography.Decrypt<RijndaelManaged>(Lic, aesPassword.password);
                    _license = Serializer.XML.DeserializeObject<LicenseInfo>(decrypted, true, "xmlns:MyNamespace");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Initial License errors. " + ex.Message, ex);
            }
        }
    }
}