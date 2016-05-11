using SimpleBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View(new LoginVM());
        }

        [HttpPost]
        public ActionResult Index(LoginVM form)
        {
            if (ModelState.IsValid)
                return Content(
                    "Username and Password are valid: "
                    + "   Username: " + form.UserName
                    + ",   Password: " + form.Password
                    );

            else
            {
                ModelState.Clear();
                form = new LoginVM();
                return View(form);
            }
           
        }


    }
}