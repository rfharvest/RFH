using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFH.Models
{
    public class ManageHostDetailViewModel
    {
        public HostSite HostSite { get; set; }
        public List<Article> Articles { get; set; }
        public List<HostSiteTag> HostSiteTags { get; set; }
        public List<HostSiteTagValue> HostSiteTagValues { get; set; }
        public List<HostSiteToHostSiteTagValue> HostSiteToHostSiteTagValues { get; set; }
    }
}