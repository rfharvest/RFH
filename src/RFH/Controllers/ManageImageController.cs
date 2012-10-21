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
    [Authorize]
    public class ManageImageController : Controller
    {
        public ActionResult Index()
        {
            var model = new ManageImageIndexViewModel();
            model.FolderUrl = ImageFolderUrl;
            model.ImageUrls = new List<string>();

            var directoryInfo = ImageFolderUrl.GetDirectoryInfo(Server);
            var files = directoryInfo.GetFiles().OrderBy(f => f.Name);

            foreach (var file in files)
            {
                model.ImageUrls.Add(file.Name);
            }

            return View(model);
        }

        public ActionResult Details(string fileName)
        {
            var url = GetImageUrl(fileName);
            var fileInfo = url.GetFileInfo(Server);
            var fileSize = (fileInfo.Length/1000).ToString("###,###,##0");

            var model = new ManageImageDetailsViewModel
                            {
                                FileName = fileName,
                                ImageUrl = url,
                                Size = string.Format("{0}KB", fileSize)
                            };

            return View(model);
        }

        public ActionResult Delete(string fileName)
        {
            var url = GetImageUrl(fileName);
            var fileInfo = url.GetFileInfo(Server);
            var fileSize = (fileInfo.Length / 1000).ToString("###,###,##0");

            var model = new ManageImageDetailsViewModel
            {
                FileName = fileName,
                ImageUrl = url,
                Size = string.Format("{0}KB", fileSize)
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(string fileName, FormCollection form)
        {
            var url = GetImageUrl(fileName);
            var physicalPath = Server.MapPath(url);

            if (System.IO.File.Exists(physicalPath))
            {
                System.IO.File.Delete(physicalPath);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase imageUrlFile)
        {
            if (imageUrlFile == null || imageUrlFile.ContentLength < 1)
            {
                ModelState.AddModelError("imageUrlFile", "Please select an image file to upload.");
                return View();
            }

            string ext = Path.GetExtension(imageUrlFile.FileName);
            var validExts = new List<string> {".gif", ".jpg", ".png"};
            var isValidExtension = validExts.Any(e => string.Equals(ext, e, StringComparison.InvariantCultureIgnoreCase));

            if(!isValidExtension)
            {
                ModelState.AddModelError("imageUrlFile", "Invalid image file. Please upload .gif, .jpg, or .png files.");
                return View();
            }

            if (ModelState.IsValid)
            {
                var url = string.Format("{0}/{1}", ImageFolderUrl, imageUrlFile.FileName);
                var physicalPath = Server.MapPath(url);

                imageUrlFile.SaveAs(physicalPath);

                return RedirectToAction("Index");    
            }

            return View();
        }

        private string ImageFolderUrl
        {
            get
            {
                var url = "/Content/cmsImages";
                return url;
            }
        }

        private string GetImageUrl(string fileName)
        {
            var url = string.Format("{0}/{1}", ImageFolderUrl, fileName);
            return url;
        }
    }
}
