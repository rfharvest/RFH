using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Infrastructure;

namespace RFH.Controllers
{
    public class HostMapController : Controller
    {

        private DataContext _dataContext = new DataContext();

        //
        // GET: /HostMap/

        public ActionResult Index()
        {
            var model = from site in _dataContext.HostSites
                        orderby site.Name
                        where site.IsActive
                              && site.Address != null
                              && site.City != null
                              && site.State != null
                              && site.Zip != null
                        select site;

            return View(model);
        }

    }
}
