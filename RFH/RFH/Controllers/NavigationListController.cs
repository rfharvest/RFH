using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Infrastructure;

namespace RFH.Controllers
{
    public class NavigationListController : Controller
    { 
        
        private DataContext _dataContext = new DataContext();


        public ActionResult ListCategory()
        {
           return PartialView(_dataContext.Categories.OrderBy(c=>c.Name).ToList());
        }


        public ActionResult ListSite() {
            return PartialView(_dataContext.HostSites.OrderBy(s => s.Name).ToList());
        }


    }
}
