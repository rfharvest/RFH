using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Infrastructure;
using RFH.Models;

namespace RFH.Controllers
{ 
    [Authorize]
    public class ManageHostSiteTagController : Controller
    {
        private DataContext _dataContext = new DataContext();

        //
        // GET: /ManageHostSiteTag/

        public ViewResult Index()
        {
            return View(_dataContext.HostSiteTags.ToList());
        }

        //
        // GET: /ManageHostSiteTag/Details/5

        public ViewResult Details(int id)
        {
            var model = new ManageHostSiteTagDetailViewModel();
            model.HostSiteTag = _dataContext.HostSiteTags.Find(id);
            model.HostSiteTagValues = _dataContext.HostSiteTagValues.Where(m => m.HostSiteTagId == id).ToList();

            return View(model);
        }

        //
        // GET: /ManageHostSiteTag/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ManageHostSiteTag/Create

        [HttpPost]
        public ActionResult Create(HostSiteTag hostsitetag)
        {
            if (ModelState.IsValid)
            {
                _dataContext.HostSiteTags.Add(hostsitetag);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(hostsitetag);
        }
        
        //
        // GET: /ManageHostSiteTag/Edit/5
 
        public ActionResult Edit(int id)
        {
            HostSiteTag hostsitetag = _dataContext.HostSiteTags.Find(id);
            return View(hostsitetag);
        }

        //
        // POST: /ManageHostSiteTag/Edit/5

        [HttpPost]
        public ActionResult Edit(HostSiteTag hostsitetag)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Entry(hostsitetag).State = EntityState.Modified;
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hostsitetag);
        }

        //
        // GET: /ManageHostSiteTag/Delete/5
 
        public ActionResult Delete(int id)
        {
            HostSiteTag hostsitetag = _dataContext.HostSiteTags.Find(id);
            return View(hostsitetag);
        }

        //
        // POST: /ManageHostSiteTag/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            HostSiteTag hostsitetag = _dataContext.HostSiteTags.Find(id);
            _dataContext.HostSiteTags.Remove(hostsitetag);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _dataContext.Dispose();
            base.Dispose(disposing);
        }
    }
}