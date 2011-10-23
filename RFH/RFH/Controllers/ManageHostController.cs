using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Infrastructure;
using RFH.Models;

namespace RFH.Controllers
{
    public class ManageHostController : Controller
    {
        private DataContext _dataContext = new DataContext();

        public ActionResult Detail(int id)
        {
            var host = _dataContext.HostSites.Single(h => h.Id == id);
            return View(host);
        }

        public ActionResult Edit(int id)
        {
            var host = _dataContext.HostSites.Single(h => h.Id == id);
            return View(host);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)   
        {
            var model = _dataContext.HostSites.Single(h => h.Id == id);

            if (TryUpdateModel(model))
            {
                _dataContext.SaveChanges();
                return RedirectToAction("Detail", new { model.Id });
            }

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(HostSite model)
        {
            if (ModelState.IsValid)
            {
                
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
            var host = _dataContext.HostSites.Single(h => h.Id == id);
            _dataContext.HostSites.Remove(host);
            _dataContext.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }
    }
}
