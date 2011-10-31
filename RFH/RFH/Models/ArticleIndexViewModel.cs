using System.Collections.Generic;

namespace RFH.Models 
{
    public class ArticleIndexViewModel 
    {
        public Article Article { get; set; }

        public IEnumerable<RelatedArticle> RelatedArticles { get; set; }

        public IEnumerable<HostSiteTag> HostSiteTags { get; set; }

        public IEnumerable<HostSiteTagValue> HostSiteTagValues { get; set; }
    }
}