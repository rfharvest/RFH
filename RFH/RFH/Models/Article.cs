using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RFH.Models {
    public class Article {



        public int Id { get; set; }



        [Required]
        public string Content { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        


        public int HostSiteId { get; set; }

        public HostSite HostSite { get; set; }


        public int CategoryId { get; set; }

        public Category Category { get; set; }


        public bool IsPublished { get; set; }



    }
}