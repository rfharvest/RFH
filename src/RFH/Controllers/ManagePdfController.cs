using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Extensions;
using RFH.Models;

namespace RFH.Controllers
{
    public class ManagePdfController : Controller
    {
        public ActionResult Index()
        {
            var model = new ManagePdfIndexViewModel();
            model.FolderUrl = PdfFolderUrl;
            model.PdfUrls = new List<string>();

            var directoryInfo = PdfFolderUrl.GetDirectoryInfo(Server);;
            var files = directoryInfo.GetFiles().OrderBy(f => f.Name);

            foreach (var file in files)
            {
                model.PdfUrls.Add(file.Name);
            }

            return View(model);
        }

        private string PdfFolderUrl
        {
            get { return "/Content/cmsPdfs"; }
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase pdfUrlFile)
        {
            if (pdfUrlFile == null || pdfUrlFile.ContentLength < 1)
            {
                ModelState.AddModelError("pdfUrlFile", "Please select a PDF file to upload.");
                return View();
            }

            string ext = Path.GetExtension(pdfUrlFile.FileName);
            var validExts = new List<string> { ".pdf" };
            var isValidExtension = validExts.Any(e => string.Equals(ext, e, StringComparison.InvariantCultureIgnoreCase));

            if (!isValidExtension)
            {
                ModelState.AddModelError("pdfUrlFile", "Invalid PDF file. Please upload a .pdf file.");
                return View();
            }

            if (ModelState.IsValid)
            {
                var url = string.Format("{0}/{1}", PdfFolderUrl, pdfUrlFile.FileName);
                var physicalPath = Server.MapPath(url);

                pdfUrlFile.SaveAs(physicalPath);

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
