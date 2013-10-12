using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RFH.Models
{
    public class Page
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Page Name")]
        [Required]
        public string Name { get; set; }

        public string UrlFriendlyName { get; set; }

        public bool IsActive { get; set; }

        [DisplayName("Super Category")]
        public int SuperCategoryId { get; set; }

        [ForeignKey("SuperCategoryId")]
        public SuperCategory SuperCategory { get; set; }

        [DisplayName("Hero FileName")]
        [Required]
        public string HeroFileName { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}