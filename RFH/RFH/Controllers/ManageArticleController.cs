﻿using System;
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
	public class ManageArticleController : Controller
	{
		private DataContext _dataContext = new DataContext();

		public ActionResult Detail(int id)
		{
			var model = new ManageArticleDetailViewModel();
			model.Article = _dataContext.Articles.Include(m => m.Category).Single(m => m.Id == id);
			model.HostSite = _dataContext.HostSites.Find(model.Article.HostSiteId);

			return View(model);
		}

		public ActionResult Edit(int id)
		{
		    var article = _dataContext.Articles
		        .Include(m => m.HostSite)
		        .Include(m => m.Category)
		        .Single(m => m.Id == id);

		    var model = GetManageArticleEditViewModel(article);
			return View(model);
		}

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Edit(int id, FormCollection form)
		{
			var article = _dataContext.Articles.Single(m => m.Id == id);

			if (TryUpdateModel(article, "Article"))
			{
				_dataContext.SaveChanges();
                return RedirectToAction("Detail", new { article.Id });
			}

            var model = GetManageArticleEditViewModel(article);
            return View(model);
		}


		public ActionResult Create()
		{
            var model = GetManageArticleEditViewModel(null);
			return View(model);
		}

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Create(Article article)
		{
            if (ModelState.IsValid)
            {
                _dataContext.Articles.Add(article);
                _dataContext.SaveChanges();

                return RedirectToAction("Detail", new { article.Id });
            }

            var model = GetManageArticleEditViewModel(article);
			return View(model);
		}

		public ActionResult Delete(int id)
		{
            var model = _dataContext.Articles
                            .Include(m => m.HostSite)
                            .Include(m => m.Category)
                            .Single(m => m.Id == id);

			return View(model);
		}

		[HttpPost]
		public ActionResult Delete(int id, FormCollection form)
		{
		    Article model = null;

		    try
		    {
		        model = _dataContext.Articles
		            .Include(m => m.HostSite)
		            .Include(m => m.Category)
		            .Single(m => m.Id == id);

			    _dataContext.Articles.Remove(model);
			    _dataContext.SaveChanges();

			    return RedirectToAction("Detail", "ManageHost", new { Id = model.HostSiteId });
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Id", "Unable to delete. Please confirm there are no items linked to this article.");
                return View(model);
            }
		}

		protected IEnumerable<SelectListItem> GetCategoryListItems(List<Category> items)
		{
			return items.Select(x => new SelectListItem
				{
					Text = x.Name,
					Value = x.Id.ToString()
				});
		}

		protected IEnumerable<SelectListItem> GetHostSiteListItems(List<HostSite> items)
		{
			return items.Select(x => new SelectListItem
				{
					Text = x.Name,
					Value = x.Id.ToString()
				});
		}

        private ManageArticleEditViewModel GetManageArticleEditViewModel(Article article)
        {
            var model = new ManageArticleEditViewModel
            {
                Article = article,
                HostSiteItems = GetHostSiteListItems(_dataContext.HostSites.ToList()),
                CategoryItems = GetCategoryListItems(_dataContext.Categories.ToList())
            };

            return model;
        }
	}
}
