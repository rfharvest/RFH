﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using RFH.Infrastructure;
using RFH.Models;

namespace RFH.Controllers
{
	public class HomeController : Controller
	{
		private DataContext _dataContext = new DataContext();

		public ActionResult Index()
		{
            var model = GetContentData("Index", "Home");
            return View(model);
		}

        public ActionResult GeneralResource()
        {
            var model = GetContentData("GeneralResource", "Home");
            return View(model);
        }

        public ActionResult GiveCamp()
        {
            var model = GetContentData("GiveCamp", "Home");
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult Wizard()
        {
            var model = new HomeWizardViewModel();

            model.TagItems = new List<TagItem>();

            var tags = _dataContext.HostSiteTags
                .Include(m => m.HostSiteTagValues)
                .OrderBy(m => m.Name)
                .ToList();

            foreach (var tag in tags)
            {
                model.TagItems.Add(
                    new TagItem
                        {
                            TagId = tag.Id,
                            TagName = tag.Name,
                            TagValues = tag.HostSiteTagValues
                                            .OrderBy(m => m.Name)
                                            .Select(m => new SelectListItem
                                                {
                                                    Text = m.Name, 
                                                    Value = m.Id.ToString()
                                                })
                        });
            }

            return PartialView(model);
        }

        [ChildActionOnly]
        public JsonResult WizardJsonData()
        {
            var tagValues = _dataContext.HostSiteToHostSiteTagValues
                                .OrderBy(m => m.HostSiteTagValueId)
                                .ToList();

            var model = _dataContext.HostSites
                            .Where(m => m.IsActive)
                            .OrderBy(m => m.Name)
                            .ToList()
                            .Select(m => new 
                                {
                                    id = m.Id,
                                    name = m.Name,
                                    url = "/site/" + m.UrlFriendlyName,
                                    tagValues = tagValues.Where(t => t.HostSiteId == m.Id).Select(t => t.HostSiteTagValueId)
                                });

            return Json(model, "text/html", JsonRequestBehavior.AllowGet);
        }

        private ContentData GetContentData(string actionName, string controllerName)
        {
            var model = _dataContext.ContentDatas
                    .Where(m => m.ActionName == actionName)
                    .Where(m => m.ControllerName == controllerName)
                    .SingleOrDefault();

            if (model == null)
            {
                model = new ContentData { ActionName = actionName, ControllerName = controllerName, Name = actionName };
                _dataContext.ContentDatas.Add(model);
                _dataContext.SaveChanges();
            }

            return model;
        }
	}
}
