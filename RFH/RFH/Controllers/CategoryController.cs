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
            var category = _dataContext.Categories.Single(m => m.Name == id);

            var articles = _dataContext.Articles
                .Include(m => m.HostSite)
                .Include(m => m.Category)
                .Where(m => m.Category.Id == category.Id)
                .Where(m => m.IsPublished)
                .Where(m => m.HostSite.IsActive).ToList();

            var model = new CategoryIndexViewModel
                            {
                                Category = category,
                                Articles = articles
                            };

            return View(model);
        }
    }
}
