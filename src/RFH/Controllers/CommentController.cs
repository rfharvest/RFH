using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using RFH.Infrastructure;
using RFH.Models;
using RFH.Services;
using System.Linq;
using System.Net.Configuration;
using System.Web.Configuration;


namespace RFH.Controllers
{
    public class CommentController : Controller
    {
        private DataContext _dataContext  = new DataContext();
        

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateComment(CreateCommentViewModel newComment)
        {
            var articleData = _dataContext.Articles.Where(article => article.Id == newComment.ArticleId).SingleOrDefault();

            MailSettingsSectionGroup GetMailSettings = new MailSettingsSectionGroup();
            var mailService = new MailService();

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

            newComment.Text = newComment.Text + "\\n Article Category: " + articleData.Category;

            List<string> stringEmail = new List<string>(new string[] { GetMailSettings.Smtp.From });
            IEnumerable<string> recep = Combine(stringEmail);
            try
            {
                mailService.Send(recep, "RFH: A new comment has been added for the article - " + articleData.Title, newComment.Text, null);
            }
            catch (Exception)
            {
                
            }

            return  RedirectToAction("Index", "Article", new { id = newComment.ArticleId});
        }

        static IEnumerable<T> Combine<T>(params IEnumerable<T>[] enumerables)
        {
            foreach (IEnumerable<T> enumerable in enumerables)
            {
                foreach (T item in enumerable)
                {
                    yield return item;
                }
            }
        }
    }
}
