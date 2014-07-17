using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace RMS.Centralize.WebService.BSL
{
    public class WebDownload : WebClient
    {
        /// <summary>
        /// Time in milliseconds
        /// </summary>
        public int Timeout { get; set; }

        public WebDownload() : this(5) { }

        public WebDownload(int timeoutMinute)
        {
            this.Timeout = timeoutMinute * 1000 * 60;
            this.Encoding = System.Text.Encoding.UTF8;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request != null)
            {
                request.Timeout = this.Timeout;
            }
            return request;
        }
    }
}