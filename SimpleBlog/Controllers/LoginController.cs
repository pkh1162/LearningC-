using SimpleBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SimpleBlog.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginIndex form, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                FormsAuthentication.SetAuthCookie(form.Username, true);

                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToRoute("HomePage");
                }      
            }

            else
            {
                form = new LoginIndex();
                return View(form);
            }
           
        }


    }
}