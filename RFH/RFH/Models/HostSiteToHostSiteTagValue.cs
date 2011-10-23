using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFH.Models
{
    public class HostSiteToHostSiteTagValue
    {
        public int Id { get; set; }

        public HostSite HostSite { get; set; }
        public int HostSiteId { get; set; }

        public HostSiteTagValue HostSiteTagValue { get; set; }
        public int HostSiteTagValueId { get; set; }
    }
}