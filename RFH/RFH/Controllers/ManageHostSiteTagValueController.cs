using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
            var model = new HostSiteTagValue
                            {
                                HostSiteTag = _dataContext.HostSiteTags.Single(m => m.Id == id),
                                HostSiteTagId = id
                            };
            
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
        public ActionResult Edit(int id, HostSiteTagValue postedModel)
        {
            var model = _dataContext.HostSiteTagValues.Single(m => m.Id == id);

            if (TryUpdateModel(model))
            {
                _dataContext.SaveChanges();
                return RedirectToAction("Details", "ManageHostSiteTag", new { id=postedModel.HostSiteTagId });  
            }

            return View(postedModel);
        }

        public ActionResult Delete(int id)
        {
            var model = _dataContext.HostSiteTagValues
                .Include(m => m.HostSiteTag)
                .Single(m => m.Id == id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            HostSiteTagValue model = null;

            try 
            {
                model = _dataContext.HostSiteTagValues.Find(id);
                _dataContext.HostSiteTagValues.Remove(model);
                _dataContext.SaveChanges();

                return RedirectToAction("Details", "ManageHostSiteTag", new { id = model.HostSiteTagId });
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Id", "Unable to delete. Please confirm there are no items are linked to this value.");
                return View(model);
            }
        }
    }
}