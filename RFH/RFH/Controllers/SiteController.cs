using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using RFH.Infrastructure;

namespace RFH.Controllers
{
    public class SiteController : Controller
    {
        private DataContext _dataContext = new DataContext();
        public ActionResult Details(string id)
        {
            _dataContext.Categories.ToList();

            var model = _dataContext.HostSites
                            .Include(m => m.Articles)
                            .Where(m => m.Name == id)
                            .Where(m => m.IsActive)
                            .Single();
            
            return View(model);
        }
    }
}
