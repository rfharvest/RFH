using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using RFH.Infrastructure;
using RFH.Models;

namespace RFH.Controllers
{
	public class HomeController : Controller
	{
		//
		// GET: /Home/

		private DataContext _dataContext = new DataContext();


		public ActionResult Index()
		{
			var contentItem = from content in _dataContext.ContentDatas
							  where content.ActionName == "Index" &&  
									content.ControllerName == "Home"
							  select content;

			return View(contentItem.FirstOrDefault());
		}

        public ActionResult GeneralResource()
        {
            var contentItem = from content in _dataContext.ContentDatas
                              where content.ActionName == "GeneralResource" &&
                                    content.ControllerName == "Home"
                              select content;

            return View(contentItem.FirstOrDefault());
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
                                    url = "/site/" + m.Name,
                                    tagValues = tagValues.Where(t => t.HostSiteId == m.Id).Select(t => t.HostSiteTagValueId)
                                });

            return Json(model, "text/html", JsonRequestBehavior.AllowGet);
        }

	}
}
