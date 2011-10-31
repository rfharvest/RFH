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
        private string urlFriendlyName;

        public HostSite() 
        {
            this.MetaData = new List<HostSiteMetaData>();
        }

        public int Id { get; set; }

        [DisplayName("Host Site Url")]
        public string HostSiteUrl { get; set; }

        [DisplayName("Host Site Name")]
        [Required]
        public string Name { get; set; }


        [DisplayName("Url Friendly Name")]
        public string UrlFriendlyName
        {
            get { return Regex.Replace(Name, @"[^\w]+", "-", RegexOptions.IgnoreCase); }
            set { urlFriendlyName = Regex.Replace(Name, @"[^\w]+", "-", RegexOptions.IgnoreCase); }
        }

        [UIHint("HtmlContent")]
        [Required]
        public string Description { get; set; }

        public string Area { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        public ICollection<Article> Articles { get; set; }

        public ICollection<HostSiteMetaData> MetaData { get; set; }

        public ICollection<HostSiteTagValue> HostSiteTagValues { get; set; }
    }
}
