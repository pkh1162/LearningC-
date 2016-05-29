using SimpleBlog.DAL;
using SimpleBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    public class LayoutController : Controller
    {
        UserRoleContext db = new UserRoleContext();
        // GET: Layout
       
        [ChildActionOnly]
        public ActionResult Sidebar()
        {
            return View(new LayoutSidebar
            {
                IsLoggedIn = Auth.User != null,
                Username = (Auth.User != null) ? Auth.User.Username : "",
                IsAdmin = User.IsInRole("admin"),
                
                Tags = db.Tags.Select(tag => new
                {
                    tag.TagID,
                    tag.Name,
                    tag.Slug,
                    PostCount = tag.Posts.Count
                }).Where(t => t.PostCount > 0).ToList().OrderByDescending(p =>p.PostCount).Select(
                    tag => new SidebarTag(tag.TagID, tag.Name, tag.Slug, tag.PostCount)).ToList()          


            });
        }

    }
}