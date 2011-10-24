using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RFH.Infrastructure;
using RFH.Models;

namespace RFH.Controllers
{ 
    [Authorize]
    public class ManageHostSiteTagValueController : Controller
    {
        private DataContext _dataContext = new DataContext();

        //
        // GET: /ManageHostSiteTagValue/Details/5

        public ViewResult Details(int id)
        {
            HostSiteTagValue hostsitetagvalue = _dataContext.HostSiteTagValues.Find(id);
            return View(hostsitetagvalue);
        }

        //
        // GET: /ManageHostSiteTagValue/Create

        public ActionResult Create(int id)
        {
            var model = new HostSiteTagValue();
            model.HostSiteTagId = id;
            return View(model);
        } 

        //
        // POST: /ManageHostSiteTagValue/Create

        [HttpPost]
        public ActionResult Create(HostSiteTagValue hostsitetagvalue)
        {
            if (ModelState.IsValid)
            {
                _dataContext.HostSiteTagValues.Add(hostsitetagvalue);
                _dataContext.SaveChanges();
                return RedirectToAction("Details", "ManageHostSiteTag", new { id=hostsitetagvalue.HostSiteTagId});  
            }

            return View(hostsitetagvalue);
        }
        
        //
        // GET: /ManageHostSiteTagValue/Edit/5
 
        public ActionResult Edit(int id)
        {
            HostSiteTagValue hostsitetagvalue = _dataContext.HostSiteTagValues.Find(id);
            return View(hostsitetagvalue);
        }

        //
        // POST: /ManageHostSiteTagValue/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, HostSiteTagValue hostsitetagvalue)
        {
            var record = _dataContext.HostSiteTagValues.Single(m => m.Id == id);

            if (TryUpdateModel(record))
            {
                _dataContext.SaveChanges();
                return RedirectToAction("Details", "ManageHostSiteTag", new { id=hostsitetagvalue.HostSiteTagId });  
            }

            return View(hostsitetagvalue);
        }

        //
        // GET: /ManageHostSiteTagValue/Delete/5
 
        public ActionResult Delete(int id)
        {
            HostSiteTagValue hostsitetagvalue = _dataContext.HostSiteTagValues.Find(id);
            return View(hostsitetagvalue);
        }

        //
        // POST: /ManageHostSiteTagValue/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            HostSiteTagValue hostsitetagvalue = _dataContext.HostSiteTagValues.Find(id);
            _dataContext.HostSiteTagValues.Remove(hostsitetagvalue);
            _dataContext.SaveChanges();
            return RedirectToAction("Details", "ManageHostSiteTag", new { id = hostsitetagvalue.HostSiteTagId });  
        }
    }
}