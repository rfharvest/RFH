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

        private DataContext _dataContext = new DataContext();

        public ActionResult HostSiteSidebar(string id)
        {
            var hostSite = _dataContext.HostSites
                            .Where(m => m.UrlFriendlyName == id)
                            .Where(m => m.IsActive)
                            .Single();

            return View(hostSite);
        }
    }
}
