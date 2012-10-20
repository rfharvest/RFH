using System;
using System.Collections.Generic;
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
        public int StatisticId { get; set; }
        public string Description { get; set; }
        public long Value { get; set; }
        public string Units { get; set; }
        public byte SortOrder { get; set; }
    }
}
