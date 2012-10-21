using RFH.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RFH.Controllers
{
  public class ViewSitesController : Controller
  {
    private DataContext _dataContext = new DataContext();

    public ActionResult Index()
    {
      return View();
    }

    [ChildActionOnly]
    public ActionResult _HostSitesMap()
    {
      var model = from site in _dataContext.HostSites
                  orderby site.Name
                  where site.IsActive
                        && site.Address != null
                        && site.City != null
                        && site.State != null
                        && site.Zip != null
                  select site;

      return PartialView(model);
    }

  }
}
