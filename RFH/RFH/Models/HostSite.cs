using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace RFH.Models {
    public class HostSite {

        public HostSite() {

            this.MetaData = new List<HostSiteMetaData>();
        }


        public int Id { get; set; }

        public string HostSiteUrl { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }


        public string Area { get; set; }


        public bool IsActive { get; set; }


        public ICollection<Article> Articles { get; set; }


        public ICollection<HostSiteMetaData> MetaData { get; set; }

        public ICollection<HostSiteTagValue> HostSiteTagValues { get; set; }
    }
}
