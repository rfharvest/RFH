using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Models;
using System.Web.Security;

namespace RFH.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(UserCredentials credentials, String returnUrl){
            Boolean b = FormsAuthentication.Authenticate(credentials.UserName, credentials.Password);
            if (!b) {
                ModelState.AddModelError("AuthenticationFailure", "Invalid username and password");
                return View(credentials);
            }

            FormsAuthentication.SetAuthCookie(credentials.UserName, false);
            return Redirect(returnUrl ?? "/Admin");
        }

        public ActionResult LogOut() {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("LogOn");
        }
    }
}
