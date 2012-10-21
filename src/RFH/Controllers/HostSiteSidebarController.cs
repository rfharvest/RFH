using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Models;
using RFH.Infrastructure;
using System.Data.Entity;

namespace RFH.Controllers
{
    public class HostSiteSidebarController : Controller
    {
        //
        // GET: /HostSiteSidebar/

        public ActionResult Index()
        {
            return View();
        }

        private DataContext _dataContext = new DataContext();

        public ActionResult Index(int id)
        {
            var article = _dataContext.Articles
                .Include(a => a.HostSite)
                .Include(a => a.Category)
                .Where(a => a.IsPublished)
                .Where(a => a.Id == id)
                .FirstOrDefault();

            var relatedArticles = _dataContext.Articles
                .Where(m => m.CategoryId == article.CategoryId)
                .Where(m => m.Id != article.Id)
                .Where(m => m.IsPublished)
                .Select(m => new RelatedArticle
                {
                    ArticleId = m.Id,
                    HostSiteName = m.HostSite.Name
                }).ToList();

            // TODO: Move the following filter execution from C# to the database

            var selectedTagValues = _dataContext.HostSiteToHostSiteTagValues
                .Where(m => m.HostSiteId == article.HostSiteId)
                .Select(m => m.HostSiteTagValue)
                .OrderBy(m => m.Name)
                .ToList();

            var distinctHostSiteTags = selectedTagValues.Select(m => m.HostSiteTagId).Distinct();

            var hostSiteTags = _dataContext.HostSiteTags
                .Include(m => m.HostSiteTagValues)
                .ToList()
                .Where(m => distinctHostSiteTags.Any(d => d == m.Id))
                .OrderBy(m => m.Name);

            var model = new ArticleIndexViewModel
            {
                Article = article,
                RelatedArticles = relatedArticles,
                HostSiteTags = hostSiteTags,
                HostSiteTagValues = selectedTagValues
            };

            return View(model);
        }

    }
}
