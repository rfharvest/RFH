using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RFH.Models 
{
    public class HostSite 
    {
        public HostSite() 
        {
            this.MetaData = new List<HostSiteMetaData>();
        }

        [Key]
        public int Id { get; set; }

        [DisplayName("Host Site Url")]
        public string HostSiteUrl { get; set; }

        [DisplayName("Host Site Name")]
        [Required]
        public string Name { get; set; }

        public string UrlFriendlyName { get; set; }

        [UIHint("HtmlContent")]
        [Required]
        [MaxLength]
        public string Description { get; set; }

        public string Area { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        public ICollection<Article> Articles { get; set; }

        public ICollection<HostSiteMetaData> MetaData { get; set; }

        public ICollection<HostSiteTagValue> HostSiteTagValues { get; set; }
    }
}
