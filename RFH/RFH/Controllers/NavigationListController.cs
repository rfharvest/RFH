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
           return PartialView(_dataContext.Categories.ToList());
        }


        public ActionResult ListSite() {
            return PartialView(_dataContext.HostSites.ToList());
        }


    }
}
