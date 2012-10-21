using System;
using System.Web.Mvc;
using RFH.Infrastructure;
using RFH.Models;

namespace RFH.Controllers
{
    public class CommentController : Controller
    {
        private DataContext _dataContext = new DataContext();

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateComment(CreateCommentViewModel newComment)
        {
            var comment = new Comment
                              {
                                  Name = newComment.Name,
                                  Email = newComment.Email,
                                  Location = newComment.Location,
                                  Text = newComment.Text,
                                  ArticleId = newComment.ArticleId,
                                  PostDate = DateTime.UtcNow
                              };

            _dataContext.Comments.Add(comment);
            _dataContext.SaveChanges();

            return  RedirectToAction("Index", "Article", new { id = newComment.ArticleId});
        }
    }
}
