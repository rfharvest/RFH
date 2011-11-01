using System.Linq;
using System.Web.Mvc;
using RFH.Infrastructure;
using RFH.Models;

namespace RFH.Controllers
{
    [Authorize]
	public class ManageContentController : Controller
	{
		private DataContext _dataContext = new DataContext();

		public ActionResult Index()
		{
			var model = _dataContext.ContentDatas.ToList();
			return View(model);
		}

		public ActionResult Detail(int id)
		{
			var model = _dataContext.ContentDatas.Single(m => m.Id == id);
			return View(model);
		}

		public ActionResult Edit(int id)
		{
			var model = _dataContext.ContentDatas.Single(m => m.Id == id);
			return View(model);
		}

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Edit(int id, ContentData model)
		{
			var contentData = _dataContext.ContentDatas.Single(m => m.Id == id);

			contentData.Content = model.Content;
			_dataContext.SaveChanges();

			return RedirectToAction("Detail", new {contentData.Id});
		}

	}
}
