using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using RFH.Infrastructure;
using RFH.Models;
using System.Data.Entity.Infrastructure;

namespace RFH.Controllers
{
    [Authorize]
    public class ManageHostController : Controller
    {
        private DataContext _dataContext = new DataContext();

        public ActionResult Index()
        {
            var host = _dataContext.HostSites.OrderBy(m => m.Name).ToList();
            return View(host);
        }

        public ActionResult Detail(int id)
        {
            var model = new ManageHostDetailViewModel();

            model.HostSite = this._dataContext.HostSites.Include(h => h.County)
                .Single(h => h.Id == id);

            model.Articles = this._dataContext.Articles
                .Include(i => i.Category)
                .Where(m => m.HostSiteId == id)
                .OrderBy(m => m.Category.Name)
                .ToList();

            model.HostSiteTags = this._dataContext.HostSiteTags
                .OrderBy(m => m.SortOrder)
                .ThenBy(m => m.Name)
                .ToList();

            model.HostSiteTagValues = this._dataContext.HostSiteTagValues
                .OrderBy(m => m.SortOrder)
                .ThenBy(m => m.Name)
                .ToList();

            model.HostSiteToHostSiteTagValues = this._dataContext.HostSiteToHostSiteTagValues
                .Where(m => m.HostSiteId == model.HostSite.Id)
                .ToList();

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var host = _dataContext.HostSites.Single(h => h.Id == id);

            var vm = new ManageHostEditViewModel
                {
                    HostSite = host,
                    Counties = GetCountySelectListItem(_dataContext.Counties)
                };

            return View(vm);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection form)   
        {
            var model = _dataContext.HostSites.Single(h => h.Id == id);

            model.UrlFriendlyName = Regex.Replace(model.Name, @"[^\w]+", "-", RegexOptions.IgnoreCase);

            if (TryUpdateModel(model, "HostSite"))
            {
                _dataContext.SaveChanges();
                return RedirectToAction("Detail", new { model.Id });
            }

            var vm = new ManageHostEditViewModel
                {
                    HostSite = model,
                    Counties = GetCountySelectListItem(_dataContext.Counties)
                };

            return View(vm);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(HostSite model)
        {
            if (ModelState.IsValid)
            {
                model.UrlFriendlyName = Regex.Replace(model.Name, @"[^\w]+", "-", RegexOptions.IgnoreCase);
                _dataContext.HostSites.Add(model);
                _dataContext.SaveChanges();

                return RedirectToAction("Detail", new {model.Id});
            }

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var host = _dataContext.HostSites.Single(h => h.Id == id);
            return View(host);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            HostSite model = null;

            try
            {
                model = _dataContext.HostSites.Single(h => h.Id == id);
                _dataContext.HostSites.Remove(model);
                _dataContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Id", "Unable to delete. Please confirm there are no articles or tags linked to this item.");
                return View(model);
            }
        }

        public string EditTag(int hostSiteId, int hostSiteTagValueId, bool isChecked)
        {
            HostSiteToHostSiteTagValue value = _dataContext.HostSiteToHostSiteTagValues
                .Where(m => m.HostSiteId == hostSiteId)
                .Where(m => m.HostSiteTagValueId == hostSiteTagValueId)
                .SingleOrDefault();

            if (isChecked)
            {
                if (value == null)
                {
                    value = new HostSiteToHostSiteTagValue
                                {
                                    HostSiteId = hostSiteId,
                                    HostSiteTagValueId = hostSiteTagValueId
                                };
                    _dataContext.HostSiteToHostSiteTagValues.Add(value);
                }
            }
            else
            {
                if (value != null)
                {
                    _dataContext.HostSiteToHostSiteTagValues.Remove(value);
                }
            }

            _dataContext.SaveChanges();

            return string.Format("hostSiteId='{0}', hostSiteTagValueId='{1}', isChecked='{2}'",
                                 hostSiteId, hostSiteTagValueId, isChecked);
        }

        private IEnumerable<SelectListItem> GetCountySelectListItem(IEnumerable<County> items)
        {
            return items.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
        } 
    }
}
