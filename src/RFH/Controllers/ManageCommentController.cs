using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using RFH.Infrastructure;
using RFH.Models;

namespace RFH.Controllers
{
    [Authorize]
    public class ManageCommentController : Controller
    {
        private DataContext _dataContext = new DataContext();

        public ActionResult Index()
        {
            var model = _dataContext.Comments
                .Include(c => c.Article)
                .OrderByDescending(m => m.PostDate)
                .ToList();

            return View(model);
        }

        public ActionResult Detail(int id)
        {
            var model = new ManageCommentDetailViewModel();
            model.Comment = _dataContext.Comments.Single(m=>m.CommentId == id);

            return View(model);
        }


        public ActionResult Create()
        {
            return View();
        }


        public ActionResult Delete(int id)
        {
            var model = _dataContext.Comments.Single(m => m.CommentId == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            Comment model = null;

            try 
            {
                model = _dataContext.Comments.Find(id);
                _dataContext.Comments.Remove(model);
                _dataContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Id", "Unable to delete. Please verify again if there are comments available in the list.");
                return View(model);
            }
        }
    }
}
