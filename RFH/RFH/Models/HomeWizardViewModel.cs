using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RFH.Models
{
    public class HomeWizardViewModel
    {
        public List<TagItem> TagItems { get; set; }
    }

    public class TagItem
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public IEnumerable<SelectListItem> TagValues { get; set; }
    }
}