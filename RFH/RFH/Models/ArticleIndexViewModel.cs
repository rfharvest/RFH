using System.Collections.Generic;

namespace RFH.Models 
{
    public class ArticleIndexViewModel 
    {
        public Models.Article Article { get; set; }

        public IList<RelatedArticle> RelatedArticles { get; set; }
    }
}