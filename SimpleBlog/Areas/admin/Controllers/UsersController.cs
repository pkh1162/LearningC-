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
            return View(new UsersNew {

                CheckBoxRoles = db.Roles.Select(role => new RoleCheckBox
                {
                    Id = role.RoleID,
                    IsChecked = false,
                    Name = role.RoleName
                }).ToList()

            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult New(UsersNew form)
        {
            var user = new User();
            SyncRoles(form.CheckBoxRoles, user.Roles);


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


            //Roles.Add(db.Roles.Where(r => r.RoleName == "admin"),
            user.Username = form.Username;
            user.Email = form.Email;
          

                   
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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
           
           var userRoleNames = user.Roles.Select(us => us.RoleName); //need this line here. See ProblemsEncountered.P


            //////////////////////////////////////////////////////////////////////////////////////////////////////////

            return View(new UsersEdit
            {
                Email = user.Email,
                Username = user.Username,
                CheckBoxRoles = db.Roles.Select(role => new RoleCheckBox
                {
                    Id = role.RoleID,
                    IsChecked = userRoleNames.Contains(role.RoleName),  //If user.Roles contains a role with the same name(from database) in its Roles list, then return true(false), so check(uncheck) box. Not working as expected. Even if all checkboxes are set to true.
                    Name = role.RoleName
                }).ToList()
            });
        }

      /*  IsChecked = post.Tags.Select(x => x.Id).Contains(tag.Id)
            IsChecked = post.Tags.Contains(tag)
            IsChecked = user.Roles.Contains(role)
            */

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(UsersEdit form, int id)
        {

            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            SyncRoles(form.CheckBoxRoles, user.Roles);


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
              //  Password = user.PasswordHash
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

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            user.SetPassword(form.Password);
            //user.PasswordHash = form.Password;
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

       private void SyncRoles(IList<RoleCheckBox> checkboxes, IList<Role> roles)
        {
            var selectedRoles = new List<Role>();

            foreach (var role in db.Roles)
            {
                var checkbox = checkboxes.Single(c => c.Id == role.RoleID);
                checkbox.Name = role.RoleName;

                if (checkbox.IsChecked)
                    selectedRoles.Add(role);
            }

            foreach (var toAdd in selectedRoles.Where(t => !roles.Contains(t))) //If roles(from user) does not contain selected roles,
                roles.Add(toAdd);                                               //add selectedRoles to roles(of user

            foreach (var toRemove in roles.Where(t => !selectedRoles.Contains(t)).ToList()) //If roles(from user) does not contain selected roles,
                roles.Remove(toRemove);                                               //add selectedRoles to roles(of user

            db.SaveChanges();
        }

       

    }




}