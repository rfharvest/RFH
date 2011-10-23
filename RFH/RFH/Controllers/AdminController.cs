using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Infrastructure;
using RFH.Models;

namespace RFH.Controllers
{
    public class AdminController : Controller
    {
        private DataContext _dataContext = new DataContext();

        public ActionResult Index()
        {
            var model = new AdminIndexViewModel
                            {
                                HostSites = _dataContext.HostSites.ToList(),
                                Categories = _dataContext.Categories.ToList()
                            };

            return View(model);
        }

    }
}
