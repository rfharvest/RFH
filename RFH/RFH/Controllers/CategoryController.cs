using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Infrastructure;
using RFH.Models;
using System.Data.Entity;

namespace RFH.Controllers
{
    public class CategoryController : Controller
    {
        private DataContext _dataContext = new DataContext();

     
        public ActionResult Index(string id)
        {
            ViewBag.Title = id;


            var articles = _dataContext.Articles
                                .Include(h => h.HostSite)
                                .Include(c => c.Category)
                                .Where(a => a.Category.Name == id)
                                .Where(a => a.IsPublished == true).ToList();

            if (articles.Count() > 0)
            {
                ViewBag.Description = articles[0].Category.Description;
            }

            return View(articles);
        }
    }
}
