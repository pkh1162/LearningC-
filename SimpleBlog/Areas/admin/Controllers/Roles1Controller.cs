using SimpleBlog.DAL;
using SimpleBlog.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SimpleBlog.Areas.admin.Controllers
{

    [Authorize(Roles = "admin")]
    [SelectedTabAttribute("roles")]
    public class Roles1Controller : Controller
    {

        private UserRoleContext db = new UserRoleContext();

        // GET: admin/Roles_
        public ActionResult Index()
        {

            return View(db.Roles.ToList());
        }


    }
}