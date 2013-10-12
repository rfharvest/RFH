using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Infrastructure;
using System.Data.Entity;

namespace RFH.Controllers
{
    public class NavigationListController : Controller
    { 
        
        private DataContext _dataContext = new DataContext();


        public ActionResult ListCategory()
        {
           return PartialView(_dataContext.SuperCategories.Include("Categories").Include("Pages").Where(sc => sc.IsActive).OrderBy(c=>c.SortOrder).ToList());
        }


        public ActionResult ListSite() {
            return PartialView(_dataContext.HostSites.Where(s => s.IsActive == true).OrderBy(s => s.Name).ToList());
        }


    }
}
