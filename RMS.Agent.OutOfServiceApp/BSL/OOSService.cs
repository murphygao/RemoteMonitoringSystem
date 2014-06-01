using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Common.Exception;

namespace RMS.Agent.OutOfServiceApp.BSL
{
    public class OOSService
    {
        public bool Authentication(string userName, string password)
        {
            return true;
        }

        public bool PrepareForClosing()
        {
            try
            {
                string maFilePath = ConfigurationManager.AppSettings["RMS.MAFilePath"];
                if (File.Exists(maFilePath))
                {
                    return true;
                }
                else
                {
                    using (File.Create(maFilePath)) ;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new RMSAppException(this, "0500", "PrepareForClosing failed. " + ex.Message, ex, false);
            }
        }
    }
}
