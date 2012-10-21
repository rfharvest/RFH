using System;
using System.Configuration;
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

            newComment.Text = newComment.Text + "\\n Article Category: " + articleData.Category + "\\n Click here to moderate this comment: " + ConfigurationManager.AppSettings["siteURL"] + "/ManageComment/Detail/" + comment.CommentId;
  
            List<string> emailRecipients = new List<string>() { ConfigurationManager.AppSettings["CommentAlertEmail"] };
            try
            {
                mailService.Send(emailRecipients, "RFH: A new comment has been added for the article - " + articleData.Title, newComment.Text, null);
            }
            catch (Exception)
            {
                
            }

            return  RedirectToAction("Index", "Article", new { id = newComment.ArticleId});
        }
    }
}
