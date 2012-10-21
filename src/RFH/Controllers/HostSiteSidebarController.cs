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
            _dataContext.Categories.ToList();

            var hostSite = _dataContext.HostSites
                            .Include(m => m.Articles)
                            .Where(m => m.UrlFriendlyName == id)
                            .Where(m => m.IsActive)
                            .Single();

            // TODO: Move the following filter execution from C# to the database

            /*var selectedTagValues = _dataContext.HostSiteToHostSiteTagValues
                .Where(m => m.HostSiteId == hostSite.Id)
                .Select(m => m.HostSiteTagValue)
                .OrderBy(m => m.SortOrder)
                .ThenBy(m => m.Name)
                .ToList();

            var distinctHostSiteTags = selectedTagValues.Select(m => m.HostSiteTagId).Distinct();

            var hostSiteTags = _dataContext.HostSiteTags
                .Include(m => m.HostSiteTagValues)
                .ToList()
                .Where(m => distinctHostSiteTags.Any(d => d == m.Id))
                .OrderBy(m => m.SortOrder)
                .ThenBy(m => m.Name);
             */

            var model = new HostSiteSidebarIndexViewModel
            {
                HostSiteSidebar = hostSite
                /*,
                HostSiteTags = hostSiteTags,
                HostSiteTagValues = selectedTagValues
                 */ 
            };
            return View(model);
        }

    }
}
