using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using RFH.Infrastructure;
using System.Data.Entity;
using RFH.Models;

namespace RFH.Controllers {
   public class ArticleController : Controller {

      private static class StaticRandom {
         private static int seed;
         private static ThreadLocal<Random> threadLocal = new ThreadLocal<Random>
             (() => new Random(Interlocked.Increment(ref seed)));
         static StaticRandom() { seed = Environment.TickCount; }
         public static Random Instance { get { return threadLocal.Value; } }
      }

      private DataContext _dataContext = new DataContext();

      public ActionResult Index(int id) {
         var article = _dataContext.Articles
             .Include(a => a.HostSite)
             .Include(a => a.Category)
             .Include(a => a.Page)
             .Include(a => a.Comments)
             .Where(a => a.IsPublished).FirstOrDefault(a => a.Id == id);



          List<RelatedArticle> relatedArticles = new List<RelatedArticle>();
          if (article.CategoryId.HasValue)
          {
              relatedArticles = _dataContext.Articles
                  .Where(m => m.CategoryId == article.CategoryId)
                  .Where(m => m.Id != article.Id)
                  .Where(m => m.IsPublished)
                  .Select(m => new RelatedArticle
                  {
                      ArticleId = m.Id,
                      HostSiteName = m.HostSite.Name,
                      Title = m.Title
                  }).ToList();
          }
          else if (article.PageId.HasValue)
          {
              relatedArticles = _dataContext.Articles
                                            .Where(m => m.PageId == article.PageId.Value)
                                            .Where(m => m.Id != article.Id)
                                            .Where(m => m.IsPublished)
                                            .Select(m => new RelatedArticle
                                                {
                                                    ArticleId = m.Id,
                                                    HostSiteName = m.Page.Name,
                                                    Title = m.Title
                                                }).ToList();


          }


         // TODO: Move the following filter execution from C# to the database
        List<HostSiteTagValue> selectedTagValues = new List<HostSiteTagValue>();
        List<HostSiteTag> hostSiteTags = new List<HostSiteTag>();
        List<int> distinctHostSiteTags = new List<int>();

          // Only grab host site info if this article is mapped to a host site
        if (article.HostSiteId.HasValue)
        {

            selectedTagValues = _dataContext.HostSiteToHostSiteTagValues
                                            .Where(m => m.HostSiteId == article.HostSiteId)
                                            .Select(m => m.HostSiteTagValue)
                                            .OrderBy(m => m.Name)
                                            .ToList();

            distinctHostSiteTags = selectedTagValues.Select(m => m.HostSiteTagId).Distinct().ToList();

            hostSiteTags = _dataContext.HostSiteTags
                                        .Include(m => m.HostSiteTagValues)
                                        .ToList()
                                        .Where(m => distinctHostSiteTags.Any(d => d == m.Id))
                                        .OrderBy(m => m.Name).ToList();
        }

          var model = new ArticleIndexViewModel {
            FromName = article.Page != null ? article.Page.Name : article.HostSite.Name,
            Article = article,
            RelatedArticles = relatedArticles,
            HostSiteTags = hostSiteTags,
            HostSiteTagValues = selectedTagValues,
            Comments = article != null ? article.Comments ?? new Collection<Comment>() : new Collection<Comment>(),
            NewComment = new CreateCommentViewModel { ArticleId = id }
         };

         return View(model);
      }

      public ActionResult Featured() {

         var allIds =
            (from a in _dataContext.Articles
             where a.IsPublished
             select a.Id).ToList();

         int id = allIds[StaticRandom.Instance.Next(allIds.Count) - 1];
         Article model = _dataContext.Articles.Where(a => a.Id == id).FirstOrDefault();
         return PartialView("_ArticleSummary", model ?? new Article());
      }
   }
}
