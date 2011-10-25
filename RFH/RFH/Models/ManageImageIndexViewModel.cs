using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFH.Models
{
    public class ManageImageIndexViewModel
    {
        public string FolderUrl { get; set; }
        public IList<string> ImageUrls { get; set; } 
    }
}