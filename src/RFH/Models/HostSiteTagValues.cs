using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RFH.Models
{
    public class HostSiteTagValue
    {
        public int Id { get; set; }

        [DisplayName("Tag Value")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Sort Order")]
        [Required]
        public int SortOrder { get; set; }

        public HostSiteTag HostSiteTag { get; set; }
        public int HostSiteTagId { get; set; }
    }
}