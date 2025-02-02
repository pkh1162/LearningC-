﻿using SimpleBlog.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTabAttribute("test")]
    public class TestController : Controller
    {
        // GET: Test/Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}