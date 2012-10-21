using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RFH.Models
{
    public class HomeStatisticsViewModel
    {
        public List<Statistics> StatisticsItems { get; set; }
    }

    public class Statistics
    {
        [Key]
        [DisplayName("Statistics ID")]
        public int StatisticId { get; set; }
        
        [DisplayName("Statistics Description")]
        
        public string Description { get; set; }

        [DisplayName("Statistics Value")]
        [Required]
        public int Value { get; set; }

        [DisplayName("Statistics Units")]
        [Required]
        public string Units { get; set; }

        [DisplayName("Sort Order")]
        [Required]
        public int SortOrder { get; set; }
    }
}
