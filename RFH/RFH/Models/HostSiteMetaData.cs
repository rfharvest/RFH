using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFH.Models {
    public class HostSiteMetaData {

        public int Id { get; set; }

        public string Type { get; set; }

        public ICollection<string> Value { get; set; }

        public int ArticleId { get; set; }

        public Article Article { get; set; }
    }
}