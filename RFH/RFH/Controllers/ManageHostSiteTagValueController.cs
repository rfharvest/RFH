using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using RFH.Infrastructure;
using RFH.Models;

namespace RFH.Controllers
{ 
    [Authorize]
    public class ManageHostSiteTagValueController : Controller
    {
        private DataContext _dataContext = new DataContext();

        public ActionResult Create(int id)
        {
            var model = new HostSiteTagValue();
            model.HostSiteTagId = id;
            return View(model);
        } 

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
        
        public ActionResult Edit(int id)
        {
            var model = _dataContext.HostSiteTagValues
                .Include(m => m.HostSiteTag)
                .Single(m => m.Id == id);

            return View(model);
        }

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

        public ActionResult Delete(int id)
        {
            HostSiteTagValue hostsitetagvalue = _dataContext.HostSiteTagValues.Find(id);
            return View(hostsitetagvalue);
        }

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