using RFH.Infrastructure;
using RFH.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RFH.Controllers
{
    [Authorize]
    public class ManageNewsFeedController : Controller
    {
        private DataContext _dataContext = new DataContext();

        public ViewResult Index()
        {
            var model = _dataContext.NewsFeedItemValues.ToList<NewsFeed>();

            return View(model);
        }

        public ViewResult Details(int id)
        {
            var model = new NewsFeed();

            model = _dataContext.NewsFeedItemValues.Find(id);

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NewsFeed newsFeed)
        {
            if (ModelState.IsValid)
            {
                _dataContext.NewsFeedItemValues.Add(newsFeed);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newsFeed);
        }

        public ActionResult Edit(int id)
        {
            NewsFeed newsFeed = _dataContext.NewsFeedItemValues.Find(id);
            return View(newsFeed);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(NewsFeed newsFeed)
        {

            if (ModelState.IsValid)
            {
                _dataContext.Entry(newsFeed).State = EntityState.Modified;
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newsFeed);
        }

        public ActionResult Delete(int id)
        {
            NewsFeed newsFeed = _dataContext.NewsFeedItemValues.Find(id);
            return View(newsFeed);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            NewsFeed model = null;

            try
            {
                model = _dataContext.NewsFeedItemValues.Find(id);
                _dataContext.NewsFeedItemValues.Remove(model);
                _dataContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Id", "Unable to delete. Please verify.");
                return View(model);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _dataContext.Dispose();
            base.Dispose(disposing);
        }
    }

}

