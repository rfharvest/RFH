using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RFH.Models {
    public class Category {

        public int Id { get; set; }

        [DisplayName("Category Name")]
        [Required]
        public string Name { get; set; }

        [UIHint("TextArea")]
        [Required]
        public string Description { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}