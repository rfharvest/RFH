using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using RFH.Infrastructure;
using RFH.Models;

namespace RFH.Controllers
{
    public class SiteController : Controller
    {
        private DataContext _dataContext = new DataContext();
        public ActionResult Details(string id)
        {
            _dataContext.Categories.ToList();

            var hostSite = _dataContext.HostSites
                            .Include(m => m.Articles)
                            .Where(m => m.UrlFriendlyName == id)
                            .Where(m => m.IsActive)
                            .Single();

            // TODO: Move the following filter execution from C# to the database

            var selectedTagValues = _dataContext.HostSiteToHostSiteTagValues
                .Where(m => m.HostSiteId == hostSite.Id)
                .Select(m => m.HostSiteTagValue)
                .OrderBy(m => m.Name)
                .ToList();

            var distinctHostSiteTags = selectedTagValues.Select(m => m.HostSiteTagId).Distinct();

            var hostSiteTags = _dataContext.HostSiteTags
                .Include(m => m.HostSiteTagValues)
                .ToList()
                .Where(m => distinctHostSiteTags.Any(d => d == m.Id))
                .OrderBy(m => m.Name);

            var model = new SiteDetailsViewModel
                            {
                                HostSite = hostSite,
                                HostSiteTags = hostSiteTags,
                                HostSiteTagValues = selectedTagValues
                            };

            return View(model);
        }
    }
}
