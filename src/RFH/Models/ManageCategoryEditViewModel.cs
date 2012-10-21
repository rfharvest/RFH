using System.Collections.Generic;
using System.Web.Mvc;

namespace RFH.Models
{
    public class ManageCategoryEditViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<SelectListItem> SuperCategories { get; set; } 
    }
}