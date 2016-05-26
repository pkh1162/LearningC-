using SimpleBlog.DAL;
using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using SimpleBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    //[Authorize(Roles = "admin")]

    public class BlogController : Controller
    {
        private const int PostsPerPage = 5;
        UserRoleContext db = new UserRoleContext();
       
        public ActionResult Index(int page=1)   //This means, this action accepts an int parameter called page, and if it isn't supplied, it will default to one.
        {
            //var baseQuery = db.Posts.Where(t => t.DeletedAt == null).OrderByDescending(t => t.CreatedAt).Count();
            var totalPostCount = db.Posts.Where(t => t.DeletedAt == null).OrderByDescending(t => t.CreatedAt).Count();
          

            var currentPostsPage = db.Posts              //Lists posts by creation date, finds current page("page")                                                 
              .OrderByDescending(p => p.CreatedAt)
              .Where(y => y.DeletedAt==null)
              .Include(f => f.Tags)                   //and minuses 1. Skips this ammount multiplied by posts per page.                                                         
              .Skip((page - 1) * PostsPerPage)        //Takes posts from there and converts it to a list. This returns the 
              .Take(PostsPerPage)                     //posts on the current page numebr you are on.
              .ToList();

            return View(new PostsIndex
            {
                Posts = new PagedData<Post>(currentPostsPage, totalPostCount, page, PostsPerPage)
            });
        }


        public ActionResult Tag(int? id, string slug, int page=1)
        {

            if (id == null)
            {
                return HttpNotFound();
            }

            var tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }

            var totalPostCount = tag.Posts.Count();
            var postIds = tag.Posts
                .OrderByDescending(g => g.CreatedAt)
                .Skip((page - 1) * PostsPerPage)
                .Take(PostsPerPage)
                .Where(t => t.DeletedAt == null)
                .Select(t => t.PostID)
                .ToArray();

            var posts = db.Posts
                .OrderByDescending(p => p.CreatedAt)
                .Where(t => postIds.Contains(t.PostID))
                .Include(f => f.Tags)
                .ToList();


            return View(new PostsTag
            {
                Tag = tag,
                Posts = new PagedData<Post>(posts, totalPostCount, page, PostsPerPage)
            });

            /*
            var parts = SeperateIdAndSlug(idAndSlug);
            if (parts == null)
            {
                return HttpNotFound();
            }



            var tag = db.Tags.Find(parts.Item1);
            if (tag == null)
            {
                return HttpNotFound();
            }

            if (!tag.Slug.Equals(parts.Item2, StringComparison.CurrentCultureIgnoreCase))
            {
                return RedirectToRoutePermanent("Tag", new { id=parts.Item1, slug=tag.Slug });
            }

            var totalPostCount = tag.Posts.Count();
            var postIds = tag.Posts
                .OrderByDescending(g => g.CreatedAt)
                .Skip((page - 1)* PostsPerPage)
                .Take(PostsPerPage)
                .Where(t => t.DeletedAt == null)
                .Select(t => t.PostID)
                .ToArray();

            var posts = db.Posts
                .OrderByDescending(p => p.CreatedAt)
                .Where(t => postIds.Contains(t.PostID))
                .Include(f => f.Tags)
                .ToList();

            return View(new PostsTag
            {
                Tag = tag,
                Posts = new PagedData<Post>(posts, totalPostCount, page, PostsPerPage)
            });
            */
        }



      //  public ActionResult Show(string idAndSlug)
        public ActionResult Show(int? id, string slug)
        {

            if (slug ==null)
                return HttpNotFound();


            var post = db.Posts.Find(id);
            if (post == null || post.IsDeleted)
            {
                return HttpNotFound();
            }


            if (!post.Slug.Equals(slug, StringComparison.CurrentCultureIgnoreCase))
            {
                return RedirectToRoutePermanent("Post", new { id = id, slug = post.Slug });
            }

            return View(new PostsShow
            {
                Post = post
            });

           


/*            
                        var parts = SeperateIdAndSlug(idAndSlug);
                        if (parts == null)
                        {
                            return HttpNotFound();
                        }

                        var post = db.Posts.Find(parts.Item1);
                        if (post == null || post.IsDeleted)
                        {
                            return HttpNotFound();
                        }

                        if (!post.Slug.Equals(parts.Item2, StringComparison.CurrentCultureIgnoreCase))
                        //If slug from database post does not equal Url slug, then use the database slug instead. So if someone types
                        //in the incorrect slug in the Url (but the id is right) then the correct slug, taken from the post in the db using
                        //its id, will be used to route to the correct page permanently.
                        {
                            return RedirectToRoutePermanent("Post", new { id = parts.Item1, slug = post.Slug });
                        }

                        return View(new PostsShow
                        {
                            Post = post
                        });
*/
            
        }

        private System.Tuple<int, string> SeperateIdAndSlug(string idAndSlug)
        {
            var matches = Regex.Match(idAndSlug, @"^(\d+)\-(.*)?$");   //Breaks idAndSlug into "groups" (represented by () in expression) The first group is (d+) which is "many or more digits. The second (.*) is "anything after the dash after the frist group".
            if (!matches.Success)
                return null;

            var id = int.Parse(matches.Result("$1"));   //Gets the int given from the first group.
            var slug = matches.Result("$2");            //Gets the string from the second group.
            return Tuple.Create(id, slug);              //returns them both.

        }

    }
}