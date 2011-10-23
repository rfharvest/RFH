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
		private DataContext _dataContext = new DataContext();

        public ActionResult Index()
		{
            var contentItem = from content in _dataContext.ContentDatas
                              where content.ActionName == "Index" &&
                                    content.ControllerName == "Home"
                              select content;

			return View(contentItem.FirstOrDefault());
		}

        public ActionResult GeneralResource()
        {
            var contentItem = from content in _dataContext.ContentDatas
                              where content.ActionName == "Index" &&
                                    content.ControllerName == "GeneralResource"
                              select content;

            return View(contentItem.FirstOrDefault());
        }
	}
}
