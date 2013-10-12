using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Infrastructure;

namespace RFH.Controllers
{
    public class HarvestDirectoryController : Controller
    {
        private DataContext _dataContext = new DataContext();
        //
        // GET: /HarvestDirectory/

        public ActionResult Index()
        {
            // This is the ide of the real page which is marked inactive and contains the stories
            var harvestDirectoryPageId = 5;

            var stateArticleMapping = _dataContext.Articles.Where(p => p.PageId == harvestDirectoryPageId).ToList().ToDictionary(a => a.ShortDescription, a=> a.Id);

            var stateJson = Newtonsoft.Json.JsonConvert.SerializeObject(stateArticleMapping);

            return View((object) stateJson);
        }

    }
}
