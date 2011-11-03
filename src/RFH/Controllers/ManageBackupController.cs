using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using RFH.Services;

namespace RFH.Controllers
{
    [Authorize]
    public class ManageBackupController : Controller
    {
        public ActionResult Index()
        {
            var files = Directory.GetFiles(AppDataFolder, "*.zip")
                .Select(m => Path.GetFileName(m));

            return View(files);
        }

        public FileResult Download(string id)
        {
            var filename = GetFileName(id);
            var fullFileName = Path.Combine(AppDataFolder, filename);
            return File(fullFileName, "application/zip", Server.UrlEncode(filename));
        }

        public ActionResult Delete(string id)
        {
            var filename = GetFileName(id);
            return View("Delete", null, filename);
        }

        [HttpPost]
        public ActionResult Delete(string id, FormCollection form)
        {
            var filename = GetFileName(id);
            var fullFileName = Path.Combine(AppDataFolder, filename);

            if (System.IO.File.Exists(fullFileName))
            {
                System.IO.File.Delete(fullFileName);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                var rootPath = Server.MapPath("/");
                var zipFile = string.Format("backup-{0}.zip", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                var fullZipFile = Path.Combine(AppDataFolder, zipFile);

                var backupService = new BackupService();
                backupService.ExecuteBackup(rootPath, fullZipFile);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("_form", ex.ToString());
                return View();
            }
        }

        private string AppDataFolder
        {
            get
            {
                return Server.MapPath("/App_Data");
            }
        }

        private static string GetFileName(string id)
        {
            var filename = id + ".zip";
            return filename;
        }
    }
}