using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMS.Centralize.DAL;

namespace RMS.Centralize.WebService.Model
{
    public class LocationInfo : RmsLocation
    {
        public long? RowNum { get; set; } // RowNum

    }
}