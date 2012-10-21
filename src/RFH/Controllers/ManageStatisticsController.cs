using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Infrastructure;
using RFH.Models;

namespace RFH.Controllers
{ 
    [Authorize]
    public class ManageStatisticsController : Controller
    {
        private DataContext _dataContext = new DataContext();

        public ViewResult Index()
        {
            var model = _dataContext.StatisticsItemValues
                .OrderBy(m => m.SortOrder)
                .ThenBy(m => m.Description)
                .ToList();
            
            return View(model);
        }

        public ViewResult Details(int id)
        {
            var model = new ManageStatisticsViewModel();
            
            model.Statistics = _dataContext.StatisticsItemValues.Find(id);

            /*model.Statistics = _dataContext.StatisticsItemValues
                .Where(m => m.StatisticId == id)
                .OrderBy(m => m.SortOrder)
                ThenBy(m => m.Description)
                .ToList();*/

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(Statistics statistics)
        {
            if (ModelState.IsValid)
            {
                _dataContext.StatisticsItemValues.Add(statistics);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(statistics);
        }
        
        public ActionResult Edit(int id)
        {
            Statistics statistics = _dataContext.StatisticsItemValues.Find(id);
            return View(statistics);

            /*HostSiteTag hostsitetag = _dataContext.HostSiteTags.Find(id);
            return View(hostsitetag);*/
        }

        [HttpPost]
        public ActionResult Edit(Statistics statistics)
        {
            
            if (ModelState.IsValid)
            {
                _dataContext.Entry(statistics).State = EntityState.Modified;
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(statistics);
        }

        public ActionResult Delete(int id)
        {
            Statistics statistics = _dataContext.StatisticsItemValues.Find(id);
            return View(statistics);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            Statistics model = null;

            try 
            {
                model = _dataContext.StatisticsItemValues.Find(id);
                _dataContext.StatisticsItemValues.Remove(model);
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
