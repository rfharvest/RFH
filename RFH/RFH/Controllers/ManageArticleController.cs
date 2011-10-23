using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Infrastructure;
using RFH.Models;

namespace RFH.Controllers
{
    public class ManageArticleController : Controller
    {
        private DataContext _dataContext = new DataContext();

        public ActionResult Detail(int id)
        {
            var model = new ManageArticleDetailViewModel();
            model.Article = _dataContext.Articles.Single(m => m.Id == id);
            model.HostSite = _dataContext.HostSites.Single(m => m.Id == model.Article.HostSiteId);

            return View(model);
        }


        public ActionResult Edit(int id)
        {
            ViewBag.HostSites = _dataContext.HostSites.ToList()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });

            var model = _dataContext.Articles.Single(m => m.Id == id);
            var host = _dataContext.HostSites.Single(m => m.Id == model.HostSiteId);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection form)
        {
            var model = _dataContext.Articles.Single(m => m.Id == id);
            
            if (TryUpdateModel(model))
            {
                _dataContext.SaveChanges();
                return RedirectToAction("Detail", new {model.Id});
            }

            var host = _dataContext.HostSites.Single(m => m.Id == model.HostSiteId);

            ViewBag.HostSites = _dataContext.HostSites.ToList()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });

            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.HostSites = _dataContext.HostSites.ToList()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });

            var model = new Article()
                            {
                                Content = "Please enter content"
                            };
            
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Article model) {

            if (ModelState.IsValid) {
                _dataContext.Articles.Add(model);
                _dataContext.SaveChanges();

                return RedirectToAction("Detail", new { model.Id });
            }

            ViewBag.HostSites = _dataContext.HostSites.ToList()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var model = _dataContext.Articles.Single(m => m.Id == id);
            var host = _dataContext.HostSites.Single(m => m.Id == model.HostSiteId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            var model = _dataContext.Articles.Single(m => m.Id == id);

            _dataContext.Articles.Remove(model);
            _dataContext.SaveChanges();

            return RedirectToAction("Detail", "ManageHost", new { model.Id });
        }

    }
}
