using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using RFH.Infrastructure;
using RFH.Models;

namespace RFH.Controllers
{
    public class ManagePageController : Controller
    {
        private DataContext _dataContext = new DataContext();
        //
        // GET: /ManagePage/

        public ActionResult Index()
        {
            var pages = _dataContext.Pages.ToList();
            return View(pages);
        }

        public ActionResult Detail(int id)
        {
            var page = _dataContext.Pages.Include(p => p.Articles).Where(p => p.Id == id).Single();
            return View(page);
        }

        public ActionResult Edit(int id)
        {
            var page = _dataContext.Pages.Where(p => p.Id == id).Single();
            return View(page);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            var model = _dataContext.Pages.Single(h => h.Id == id);

            model.UrlFriendlyName = Regex.Replace(model.Name, @"[^\w]+", "-", RegexOptions.IgnoreCase);

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
        public ActionResult Create(Page model)
        {
            if (ModelState.IsValid)
            {
                model.UrlFriendlyName = Regex.Replace(model.Name, @"[^\w]+", "-", RegexOptions.IgnoreCase);
                _dataContext.Pages.Add(model);
                _dataContext.SaveChanges();

                return RedirectToAction("Detail", new { model.Id });
            }

            return View(model);

        }

        public ActionResult Delete(int id)
        {
            var page = _dataContext.Pages.Single(h => h.Id == id);
            return View(page);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            Page model = null;

            try
            {
                model = _dataContext.Pages.Single(h => h.Id == id);
                _dataContext.Pages.Remove(model);
                _dataContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Id", "Unable to delete. Please confirm there are no articles or tags linked to this item.");
                return View(model);
            }
        }

    }
}
