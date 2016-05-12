﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class TestController : Controller
    {
        // GET: Test/Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}