using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFH.Models;

namespace RFH.Views.Article {
    public class IndexViewModel {

        public Models.Article Article { get; set; }

        public IList<RelatedArticle> RelatedArticles { get; set; }

    }
}