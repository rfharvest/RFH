using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Infrastructure;
using RFH.Models;

namespace RFH.Controllers
{
    [Authorize]
    public class ManageSuperCategoryController : Controller
    {
        private DataContext _dataContext = new DataContext();

        public ActionResult Index()
        {
            var host = _dataContext.SuperCategories.OrderBy(m => m.Name).ToList();
            return View(host);
        }

        public ActionResult Edit(int id)
        {
            var superCategory = _dataContext.SuperCategories.Single(c => c.SuperCategoryId == id);

            return View(superCategory);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection form)
        {
            var superCategory = _dataContext.SuperCategories.Single(m => m.SuperCategoryId == id);

            if (TryUpdateModel(superCategory))
            {
                _dataContext.SaveChanges();
                return RedirectToAction("Detail", new { id = superCategory.SuperCategoryId });
            }

            var model = _dataContext.SuperCategories.Single(c => c.SuperCategoryId == id);
            return View(model);
        }

        public ActionResult Detail(int id)
        {
            var model = _dataContext.SuperCategories.Single(c => c.SuperCategoryId == id);

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SuperCategory superCategory)
        {
            if (ModelState.IsValid)
            {
                var defaultCategory = new Category
                                          {
                                              Name = "Default category under " + superCategory.Name,
                                              SuperCategory = superCategory,
                                              UrlFriendlyName = "default"+superCategory.UrlFriendlyName,
                                              Description = "Bacon ipsum dolor sit amet pig pancetta corned beef, strip steak tail spare ribs venison short ribs turkey pork t-bone."
                                          };
                _dataContext.SuperCategories.Add(superCategory);
                _dataContext.Categories.Add(defaultCategory);
                _dataContext.SaveChanges();

                return RedirectToAction("Detail", new { id = superCategory.SuperCategoryId });
            }

            return View(superCategory);
        }

        public ActionResult Delete(int id)
        {
            var model = _dataContext.SuperCategories.Single(c => c.SuperCategoryId == id);

            ViewBag.CategoriesUsingThisSuperCategory = _dataContext.Categories.Where(c => c.SuperCategoryId == id).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            SuperCategory model = null;

            try
            {
                model = _dataContext.SuperCategories.Single(m => m.SuperCategoryId == id);

                _dataContext.SuperCategories.Remove(model);
                _dataContext.SaveChanges();

                return RedirectToAction("Index", "ManageSuperCategory");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Id", "Unable to delete. Please confirm there are no items linked to this super category.");
                return View(model);
            }
        }
    }
}