using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFH.Models {
    public class RelatedArticle {

        public int Id { get; set; }

        public string HostSiteName { get; set; }

        public int ArticleId { get; set; }

    }
}