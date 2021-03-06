﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
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
            var model = new ManageCategoryDetailViewModel();
            model.Category = _dataContext.Categories.Include(m => m.SuperCategory).Single(m => m.Id == id);
            model.SuperCategory = _dataContext.SuperCategories.Find(model.Category.SuperCategoryId);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var model = new ManageCategoryEditViewModel
                            {
                                Category = _dataContext.Categories.Single(m => m.Id == id),
                                SuperCategories = GetSuperCategoryListItems(_dataContext.SuperCategories.ToList())
                            };

            return View(model);
        }

        private IEnumerable<SelectListItem> GetSuperCategoryListItems(List<SuperCategory> items)
        {
            return items.Select(x => new SelectListItem
                                         {
                                             Text = x.Name,
                                             Value = x.SuperCategoryId.ToString()
                                         });
        }

        private ManageCategoryEditViewModel GetManageCategoryViewModel(Category category)
        {
            var model = new ManageCategoryEditViewModel
                            {
                                Category = category,
                                SuperCategories = GetSuperCategoryListItems(_dataContext.SuperCategories.ToList())
                            };

            return model;
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            var category = _dataContext.Categories.Single(m => m.Id == id);

            if (TryUpdateModel(category, "Category"))
            {
                category.UrlFriendlyName = Regex.Replace(category.Name, @"[^\w]+", "-", RegexOptions.IgnoreCase);
                _dataContext.SaveChanges();
                return RedirectToAction("Detail", new { category.Id });
            }

            var model = GetManageCategoryViewModel(category);

            return View(model);
        }

        public ActionResult Create()
        {
            var model = new ManageCategoryEditViewModel
            {
                SuperCategories = GetSuperCategoryListItems(_dataContext.SuperCategories.ToList())
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            Category category = new Category();
            if (TryUpdateModel(category, "Category"))
            {
                category.UrlFriendlyName = Regex.Replace(category.Name, @"[^\w]+", "-", RegexOptions.IgnoreCase);
                _dataContext.Categories.Add(category);
                _dataContext.SaveChanges();

                return RedirectToAction("Detail", new { category.Id });
            }


            var viewModel = GetManageCategoryViewModel(category);
            return View(viewModel);
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
