using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RFH.Models
{
    public class ManagePageEditViewModel
    {
        public Page Page { get; set; }
        public IEnumerable<SelectListItem> SuperCategoryItems { get; set; }
    }
}