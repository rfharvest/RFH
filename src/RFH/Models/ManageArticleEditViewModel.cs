using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RFH.Models
{
    public class ManageArticleEditViewModel
    {
        public Article Article { get; set; }
        public IEnumerable<SelectListItem> HostSiteItems { get; set; }
        public IEnumerable<SelectListItem> CategoryItems { get; set; }
        public IEnumerable<SelectListItem> PageItems { get; set; }
    }
}