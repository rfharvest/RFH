﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFH.Infrastructure;

namespace RFH.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        private DataContext _dataContext = new DataContext();


        public ActionResult Index()
        {

            var categories = _dataContext.Categories.ToList();

            return View(categories);
        }

    }
}
