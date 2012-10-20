using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Extensions;
using RFH.Models;

namespace RFH.Controllers
{
    public class ManageDocumentController : Controller
    {
        public ActionResult Index()
        {
            var model = new ManageDocumentIndexViewModel();
            model.FolderUrl = DocumentFolderUrl;
            model.DocumentUrls = new List<string>();

            var directoryInfo = DocumentFolderUrl.GetDirectoryInfo(Server);;
            var files = directoryInfo.GetFiles().OrderBy(f => f.Name);

            foreach (var file in files)
            {
                model.DocumentUrls.Add(file.Name);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(string fileName, FormCollection form)
        {
            var url = GetDocumentUrl(fileName);
            var physicalPath = Server.MapPath(url);

            if (System.IO.File.Exists(physicalPath))
            {
                System.IO.File.Delete(physicalPath);
            }

            return RedirectToAction("Index");
        }

        private string DocumentFolderUrl
        {
            get { return "/Content/cmsDocuments"; }
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase documentUrlFile)
        {
            if (documentUrlFile == null || documentUrlFile.ContentLength < 1)
            {
                ModelState.AddModelError("documentUrlFile", "Please select a document to upload.");
                return View();
            }

            if (ModelState.IsValid)
            {
                var url = string.Format("{0}/{1}", DocumentFolderUrl, documentUrlFile.FileName);
                var physicalPath = Server.MapPath(url);

                documentUrlFile.SaveAs(physicalPath);

                return RedirectToAction("Index");
            }

            return View();
        }

        private string GetDocumentUrl(string fileName)
        {
            var url = string.Format("{0}/{1}", DocumentFolderUrl, fileName);
            return url;
        }
    }
}
