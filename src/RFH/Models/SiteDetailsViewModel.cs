using System.Collections.Generic;

namespace RFH.Models
{
    public class SiteDetailsViewModel
    {
        public HostSite HostSite { get; set; }

        public IEnumerable<HostSiteTag> HostSiteTags { get; set; }

        public IEnumerable<HostSiteTagValue> HostSiteTagValues { get; set; }
    }
}