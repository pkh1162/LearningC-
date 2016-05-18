using SimpleBlog.Areas.admin.Models;
using SimpleBlog.Areas.admin.ViewModels;
using SimpleBlog.DAL;
using SimpleBlog.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Areas.admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTabAttribute("users")]
    public class UsersController : Controller
    {

        private UserRoleContext db = new UserRoleContext();

        // GET: admin/Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }


        public ActionResult New()
        {
            return View(new UsersNew { });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult New(UsersNew form)
        {
            if (db.Users.Any(u => u.Username == form.Username))
            {
                ModelState.AddModelError("Username", "Username provided already exists.");
            }

            if (db.Users.Any(u => u.Email == form.Email))
            {
                ModelState.AddModelError("Email", "Email provided already exists.");
            }


            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var user = new User
            {
                Username = form.Username,
                Email = form.Email,
            };
            user.SetPassword(form.Password);

                db.Users.Add(user);
                db.SaveChanges();
           
                                 
            return RedirectToAction("Index");
        }



        public ActionResult Edit(int? id)
        {
            User user = db.Users.Find(id);
            if(user == null)
            {
                return HttpNotFound();
            }
            return View(new UsersEdit
            {
                Username = user.Username,
                Email = user.Email
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(UsersEdit form, int id)
        {

            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            if (db.Users.Any(u => u.Username == form.Username && u.UserID != id))
            {
                ModelState.AddModelError("Username", "The username you provided has already been taken.");
            }

            if (db.Users.Any(u => u.Email == form.Email && u.UserID != id))
            {
                ModelState.AddModelError("Email", "The email address you provided has already been taken.");
            }

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            user.Username = form.Username;
            user.Email = form.Email;

            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult ResetPassword(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(new UsersResetPassword
            {
                Username = user.Username,
                Password = user.Password
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ResetPassword(int id, UsersResetPassword form)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

           // form.Username = user.Username;
           // if (form.ConfirmPassword != form.Password)
           // {
           //     ModelState.AddModelError("Password", "Passwords must match");
           // }

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            user.Password = form.Password;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var user = db.Users.Find(id);
            if(user == null)
            {
                return HttpNotFound();
            }

            
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

    }




}