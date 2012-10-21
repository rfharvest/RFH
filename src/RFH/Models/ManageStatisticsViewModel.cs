using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFH.Models
{
    public class ManageStatisticsViewModel
    {
        public Statistics Statistics { get; set;  }
        public HostSiteTag HostSiteTag { get; set; }
        public List<HostSiteTagValue> HostSiteTagValues { get; set; }
    }
}