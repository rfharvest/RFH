using System;
using System.Linq;
using System.Web.Mvc;
using RFH.Infrastructure;
using RFH.Models;
using RFH.Services;

namespace RFH.Controllers
{
    [Authorize]
    public class ManageBackupRecipientController : Controller
    {
        private DataContext _dataContext = new DataContext();

        public ActionResult Index()
        {
            var model = _dataContext.BackupRecipients
                .OrderBy(m => m.EmailAddress)
                .ToList();

            return View(model);
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BackupRecipient model)
        {
            if (ModelState.IsValid)
            {
                _dataContext.BackupRecipients.Add(model);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var model = _dataContext.BackupRecipients.Single(m => m.Id == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            var model = _dataContext.BackupRecipients.Single(m => m.Id == id);
            _dataContext.BackupRecipients.Remove(model);
            _dataContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult BackupWebsite()
        {
            var rootPath = Server.MapPath("/");
            var now = DateTime.Now;
            var tempZipPath = Server.MapPath(
                string.Format(
                    "/app_data/backup-{0}.zip",
                    now.ToString("yyyyMMdd-HH-mm-ss")));

            var backupService = new BackupService();
            backupService.ExecuteBackup(rootPath, tempZipPath);

            return Json(new { success = "true" });
        }
    }
}