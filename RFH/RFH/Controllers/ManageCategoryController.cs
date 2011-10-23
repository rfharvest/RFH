using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Infrastructure;
using RFH.Models;

namespace RFH.Controllers
{
    public class ManageCategoryController : Controller
    {
        private DataContext _dataContext = new DataContext();

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
            var model = _dataContext.Categories.Single(m => m.Id == id);
            _dataContext.Categories.Remove(model);
            _dataContext.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

    }
}
