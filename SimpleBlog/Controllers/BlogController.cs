using SimpleBlog.DAL;
using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using SimpleBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTabAttribute("posts")]
    public class BlogController : Controller
    {
        private const int PostsPerPage = 5;
        UserRoleContext db = new UserRoleContext();
       
        public ActionResult Index(int page=1)   //This means, this action accepts an int parameter called page, and if it isn't supplied, it will default to one.
        {
            var totalPostCount = db.Posts.Count();

            var currentPostsPage = db.Posts              //Lists posts by creation date, finds current page("page")                                                 
                .OrderByDescending(p => p.CreatedAt)    //and minuses 1. Skips this ammount multiplied by posts per page.                                                         
                .Skip((page - 1) * PostsPerPage)        //Takes posts from there and converts it to a list. This returns the 
                .Take(PostsPerPage)                     //posts on the current page numebr you are on.
                .ToList();

            return View(new BlogIndex
            {
                Posts = new PagedData<Post>(currentPostsPage, totalPostCount, page, PostsPerPage)
            });
        }
    }
}