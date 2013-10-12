using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Infrastructure;

namespace RFH.Controllers
{
    public class PageController : Controller
    {

        private DataContext _dataContext = new DataContext();
        //
        // GET: /Page/

        public ActionResult Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Redirect("/");
            }

            var page = _dataContext.Pages.Include(p => p.Articles).Where(a => a.UrlFriendlyName == id).Single();

            page.Articles = page.Articles.Where(a => a.IsPublished).ToList();

            return View(page);
        }
    }
}
