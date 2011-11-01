using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RFH.Models 
{
    public class Article 
    {
        public int Id { get; set; }

        [DisplayName("Host Site Name")]
        public int HostSiteId { get; set; }

        public HostSite HostSite { get; set; }

        [DisplayName("Category Name")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [DisplayName("Is Published")]
        public bool IsPublished { get; set; }

        [DisplayName("Short Description")]
        [UIHint("TextArea")]
        [Required]
        public string ShortDescription { get; set; }

        [Required]
        [UIHint("HtmlContent")]
        [MaxLength]
        public string Content { get; set; }
    }
}