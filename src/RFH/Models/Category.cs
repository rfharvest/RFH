 using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RFH.Models
{
    public class Category
    {
        public int Id { get; set; }

        [DisplayName("Category Name")]
        [Required]
        public string Name { get; set; }

        public string UrlFriendlyName { get; set; }

        [UIHint("TextArea")]
        [Required]
        public string Description { get; set; }

        public int SuperCategoryId { get; set; }

        public ICollection<Article> Articles { get; set; }

        [ForeignKey("SuperCategoryId")]
        public SuperCategory SuperCategory { get; set; }

        public string HeroFileName { get; set; }
    }
}