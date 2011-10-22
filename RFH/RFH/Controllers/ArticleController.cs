using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Infrastructure;
using System.Data.Entity;

namespace RFH.Controllers
{
    public class ArticleController : Controller
    {

        private DataContext _dataContext = new DataContext();

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


            return View(article);
        }

    }
}
