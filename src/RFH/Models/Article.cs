using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RFH.Models 
{
    public class Article 
    {
        public int Id { get; set; }

        [DisplayName("Host Site Name")]
        public int? HostSiteId { get; set; }

        [ForeignKey("HostSiteId")]
        public HostSite HostSite { get; set; }

        [DisplayName("Category Name")]
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [DisplayName("Is Published")]
        public bool IsPublished { get; set; }

        [DisplayName("Title")]
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [DisplayName("Short Description")]
        [UIHint("TextArea")]
        [Required]
        public string ShortDescription { get; set; }

        [Required]
        [UIHint("HtmlContent")]
        [MaxLength]
        public string Content { get; set; }

        [UIHint("Image")]
        [MaxLength(100)]
        public string ImageFilename { get; set; }

        [DisplayName("Page Name")]
        public int? PageId { get; set; }

        [ForeignKey("PageId")]
        public Page Page { get; set; }

        public ICollection<Comment> Comments { get;set; }
    }
}