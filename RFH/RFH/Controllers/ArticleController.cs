using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Infrastructure;
using System.Data.Entity;
using RFH.Views.Article;
using RFH.Models;

namespace RFH.Controllers
{
    public class ArticleController : Controller
    {

        private DataContext _dataContext = new DataContext();

        public ActionResult ListArticleInSite(int id) {
            //id==HostSiteId
            var article = (from a in _dataContext.Articles.Include(a => a.Category)
                           where a.HostSiteId == id && a.IsPublished == true
                           select a).ToList();

            return PartialView("List", article); 
        }
        public ActionResult Index(int id)
        {
            // Lookup the article by id (only include published articles)
            var article = (from a in _dataContext.Articles.Include(a => a.HostSite).Include(a=>a.Category).Include(a=>a.HostSite.MetaData.Select(m => m.Values))
                           where a.Id == id && a.IsPublished == true
                           select a).FirstOrDefault();

            // If you cannot find the article then redirect to Home
            if (article == null) {
                return RedirectToAction("Index", "Home");
            }


            // Get related articles
            var relatedArticles = (from a in _dataContext.Articles
                                   where a.CategoryId == article.Category.Id
                                   && a.Id != article.Id
                                   && a.IsPublished == true
                                   select new RelatedArticle { ArticleId = a.Id, HostSiteName = a.HostSite.Name }).ToList();


            var vm = new IndexViewModel {
                Article = article,
                RelatedArticles = relatedArticles
            };

            return View(vm);
        }

    }
}
