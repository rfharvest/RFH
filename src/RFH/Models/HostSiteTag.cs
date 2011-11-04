using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RFH.Models
{
    public class HostSiteTag
    {
        public int Id { get; set; }

        [DisplayName("Tag Name")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Sort Order")]
        [Required]
        public int SortOrder { get; set; }

        public ICollection<HostSiteTagValue> HostSiteTagValues { get; set; }
    }
}