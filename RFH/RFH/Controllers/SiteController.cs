using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Infrastructure;

namespace RFH.Controllers
{
    public class SiteController : Controller
    {
        private DataContext _dataContext = new DataContext();
        public ActionResult Details(String id)
        {
                var siteQuery = from s in _dataContext.HostSites
                                  where s.Name == id
                                  select s;
                return View(siteQuery.FirstOrDefault());
        }
    }
}
