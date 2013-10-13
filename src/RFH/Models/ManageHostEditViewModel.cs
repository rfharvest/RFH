using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RFH.Models
{
    public class ManageHostEditViewModel
    {
        public HostSite HostSite { get; set; }
        public IEnumerable<SelectListItem> Counties { get; set; }
    }
}