using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Models;

namespace RFH.Controllers
{
    [Authorize]
    public class ManageImageController : Controller
    {
        public ActionResult Index()
        {
            var folderVirtualPath = "/Content/images/cms";
            var folderPhysicalPath = Server.MapPath(folderVirtualPath);
            var directoryInfo = new System.IO.DirectoryInfo(folderPhysicalPath);

            var model = new ManageImageIndexViewModel();
            model.ImageFolderPath = folderVirtualPath;
            model.Images = new List<string>();

            foreach (var file in directoryInfo.GetFiles())
            {
                model.Images.Add(file.Name);
            }

            return View(model);
        }

        public ActionResult Details(string url)
        {
            var folderVirtualPath = "/Content/images/cms";

            var virtualPath = string.Format("{0}/{1}", folderVirtualPath, url);
            var physicalPath = Server.MapPath(virtualPath);
            var file = new FileInfo(physicalPath);
            var fileSize = (file.Length/1000).ToString("###,###,##0");

            var model = new ManageImageDetailsViewModel
                            {
                                Name = url,
                                VirtualPath = virtualPath,
                                Size = string.Format("{0}KB", fileSize)
                            };

            return View(model);
        }
    }
}
