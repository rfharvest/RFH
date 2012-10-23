using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using RFH.Models;

namespace RFH.Controllers {
   public class ImageSliderController : Controller {

      // GET: /ImageSlider/
      public ActionResult Index() {
         ImageSliderModel model = new ImageSliderModel();
         string imgFolder = ConfigurationManager.AppSettings["ImageSliderFolder"];
         List<string> files = Directory.EnumerateFiles(Server.MapPath(imgFolder)).ToList();

          // Sort
         files.Sort();

         foreach (string f in files) {
            string fileName = Path.GetFileName(f);
            model.Slides.Add(new Slide() { Src = string.Format("~/{0}/{1}", imgFolder, fileName), Alt = fileName});
         }
         return PartialView("_ImageSlider", model);
      }
   }
}
