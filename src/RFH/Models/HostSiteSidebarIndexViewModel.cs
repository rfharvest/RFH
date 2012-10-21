using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RFH.Models 
{
    public class HostSiteSidebarIndexViewModel 
    {
        public HostSite HostSiteSidebar { get; set; }

        public IEnumerable<RelatedArticle> RelatedArticles { get; set; }

        public IEnumerable<HostSiteTag> HostSiteTags { get; set; }

        public IEnumerable<HostSiteTagValue> HostSiteTagValues { get; set; }
    }

    public class HostSiteSidebar
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

        public ICollection<Comment> Comments { get; set; }

    }
}