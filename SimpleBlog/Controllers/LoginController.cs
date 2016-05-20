using SimpleBlog.DAL;
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

        private UserRoleContext db = new UserRoleContext();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(LoginIndex form, string returnUrl)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == form.Username);

            if (user == null)
            {
                SimpleBlog.Areas.admin.Models.User.FakeHash(); //Prevents timing attacks, see fakeHash declaration for more details.
            }

            if((user==null) || !user.CheckPassword(form.Password))
            {
                ModelState.AddModelError("Username", "Username or password is incorrect");
            }

            if (ModelState.IsValid)
            {

                FormsAuthentication.SetAuthCookie(user.Username, true);

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