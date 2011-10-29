using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RFH.Models {
    public class Category {

        private string urlFriendlyName;

        public int Id { get; set; }

        [DisplayName("Category Name")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Url Friendly Name")]
        public string UrlFriendlyName
        {
            get { return Regex.Replace(Name, @"[^\w]+", "-", RegexOptions.IgnoreCase); }
            set { urlFriendlyName = Regex.Replace(Name, @"[^\w]+", "-", RegexOptions.IgnoreCase); }
        }

        [UIHint("TextArea")]
        [Required]
        public string Description { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}