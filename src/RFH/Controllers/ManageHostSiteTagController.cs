using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using RFH.Infrastructure;
using RFH.Models;

namespace RFH.Controllers
{ 
    [Authorize]
    public class ManageHostSiteTagController : Controller
    {
        private DataContext _dataContext = new DataContext();

        public ViewResult Index()
        {
            var model = _dataContext.HostSiteTags
                .OrderBy(m => m.SortOrder)
                .ThenBy(m => m.Name)
                .ToList();
            
            return View(model);
        }

        public ViewResult Details(int id)
        {
            var model = new ManageHostSiteTagDetailViewModel();
            
            model.HostSiteTag = _dataContext.HostSiteTags.Find(id);

            model.HostSiteTagValues = _dataContext.HostSiteTagValues
                .Where(m => m.HostSiteTagId == id)
                .OrderBy(m => m.SortOrder)
                .ThenBy(m => m.Name)
                .ToList();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(HostSiteTag hostsitetag)
        {
            if (ModelState.IsValid)
            {
                _dataContext.HostSiteTags.Add(hostsitetag);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(hostsitetag);
        }
        
        public ActionResult Edit(int id)
        {
            HostSiteTag hostsitetag = _dataContext.HostSiteTags.Find(id);
            return View(hostsitetag);
        }

        [HttpPost]
        public ActionResult Edit(HostSiteTag hostsitetag)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Entry(hostsitetag).State = EntityState.Modified;
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hostsitetag);
        }

        public ActionResult Delete(int id)
        {
            HostSiteTag hostsitetag = _dataContext.HostSiteTags.Find(id);
            return View(hostsitetag);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            HostSiteTag model = null;

            try 
            {
                model = _dataContext.HostSiteTags.Find(id);
                _dataContext.HostSiteTags.Remove(model);
                _dataContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Id", "Unable to delete. Please confirm there are no Host Sites or Values linked to this item.");
                return View(model);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _dataContext.Dispose();
            base.Dispose(disposing);
        }
    }
}