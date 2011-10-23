using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFH.Models
{
    public class HostSiteTagValue
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public HostSiteTag HostSiteTag { get; set; }
        public int HostSiteTagId { get; set; }
    }
}