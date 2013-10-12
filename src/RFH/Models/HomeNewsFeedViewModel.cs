using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RFH.Models
{
    public class HomeNewsFeedViewModel
    {
        public List<NewsFeed> NewsFeedItems { get; set; }
    }

    public class NewsFeed
    {
        [Key]
        [DisplayName("NewsFeed Id")]
        public int NewsFeedId { get; set; }

        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }
    }
}