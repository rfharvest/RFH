using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFH.Models
{
    public class ManageArticleDetailViewModel
    {
        public Article Article { get; set; }
        public HostSite HostSite { get; set; }
        public Page Page { get; set; }
    }
}