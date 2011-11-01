﻿using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using RFH.Infrastructure;
using RFH.Models;

namespace RFH.Controllers
{
    [Authorize]
    public class ManageCategoryController : Controller
    {
        private DataContext _dataContext = new DataContext();

        public ActionResult Index()
        {
            var model = _dataContext.Categories
                .OrderBy(m => m.Name)
                .ToList();

            return View(model);
        }

        public ActionResult Detail(int id)
        {
            var model = _dataContext.Categories.Single(m => m.Id == id);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var model = _dataContext.Categories.Single(m => m.Id == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            var model = _dataContext.Categories.Single(m => m.Id == id);

            if (TryUpdateModel(model))
            {
                model.UrlFriendlyName = Regex.Replace(model.Name, @"[^\w]+", "-", RegexOptions.IgnoreCase);
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
        public ActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                model.UrlFriendlyName = Regex.Replace(model.Name, @"[^\w]+", "-", RegexOptions.IgnoreCase);
                _dataContext.Categories.Add(model);
                _dataContext.SaveChanges();

                return RedirectToAction("Detail", new { model.Id });
            }

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var model = _dataContext.Categories.Single(m => m.Id == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            Category model = null;

            try 
            {
                model = _dataContext.Categories.Find(id);
                _dataContext.Categories.Remove(model);
                _dataContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Id", "Unable to delete. Please confirm there are no Articles linked to this category.");
                return View(model);
            }
        }
    }
}