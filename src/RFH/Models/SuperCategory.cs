using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RFH.Models
{
    public class SuperCategory
    {
        [Key]
        public int SuperCategoryId { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string UrlFriendlyName { get; set; }

        
        public ICollection<Category> Categories { get; set; }
    }
}