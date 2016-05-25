using SimpleBlog.Areas.admin.Models;
using SimpleBlog.DAL;
using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using SimpleBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Areas.Admin.Controllers
{
  //  [Authorize(Roles = "admin")]
    [SelectedTabAttribute("posts")]
    public class PostsController : Controller
    {

        private const int PostsPerPage = 5;
        UserRoleContext db = new UserRoleContext();



        public ActionResult Index(int page = 1)   //This means, this action accepts an int parameter called page, and if it isn't supplied, it will default to one.
        {
            var totalPostCount = db.Posts.Count();

            var currentPostsPage = db.Posts              //Lists posts by creation date, finds current page("page")                                                 
                .OrderByDescending(p => p.CreatedAt)
                .Include(f => f.Tags)                   //and minuses 1. Skips this ammount multiplied by posts per page.                                                         
                .Skip((page - 1) * PostsPerPage)        //Takes posts from there and converts it to a list. This returns the 
                .Take(PostsPerPage)                     //posts on the current page numebr you are on.
                .ToList();                              //The include line is important here, see problems encountered.

            return View(new BlogIndex
            {
                Posts = new PagedData<Post>(currentPostsPage, totalPostCount, page, PostsPerPage)
            });
        }


        public ActionResult New()
        {
            return View("Form", new PostForms()     //Return View called form, with the PostForm view model data types attached.
            {
                IsNew = true,
                Tags = db.Tags.Select(tag => new TagCheckBox
                {
                    Id = tag.TagID,
                    Name = tag.Name,
                    IsChecked = false
                }).ToList()               
            });
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Form(PostForms form)
        {
            form.IsNew = (form.BlogPostId == null);     //determines whether there is a post id (ie. is the post new or not)

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var selectedTags = ReconcileTags(form.Tags);

            Post post;

            //   User user1 = Auth.User;
           // User user3 = User.Identity.Name;
            
            //string blah = User.Identity.Name;
            //User user2 = db.Users.First(u => u.Username == blah);
        //    user1 = db.Users.First(u => u.Username == User.Identity.Name);
        //   user1.Username = User.Identity.Name;

            if (form.IsNew)
            {
                post = new Post
                {
                    CreatedAt = DateTime.UtcNow,
                  //  User = user2,
                    PostingUsername = User.Identity.Name                 
                };

                foreach (var tag in selectedTags)
                    post.Tags.Add(tag);
            }
            else
            {
                post = db.Posts.Find(form.BlogPostId);  //Finds post with form.BlogPostId and loads it into post.

                if (post == null)
                    return HttpNotFound();

                post.UpdatedAt = DateTime.UtcNow;

                foreach (var toAdd in selectedTags.Where(t => !post.Tags.Contains(t)))
                    post.Tags.Add(toAdd);

                foreach (var toRemove in post.Tags.Where(t => !selectedTags.Contains(t)).ToList())
                    post.Tags.Remove(toRemove);
            }

            post.Title = form.Title;
            post.Slug = form.Slug;
            post.Content = form.Content;
            //    post.PostID = form.BlogPostId;


            if (form.IsNew)
            {
                db.Posts.Add(post);
            }
            else
            {
                db.Entry(post).State = EntityState.Modified;
            }

                db.SaveChanges();
            
            
            

            return RedirectToAction("index");
            

        }


        public ActionResult Edit(int? id)
        {
            //var post = db.Posts.FirstOrDefault(u => u.PostID==id);
            var post = db.Posts.Find(id);

            if (post == null)
                return RedirectToAction("Index");


            ///////////////////////////////////
            var postTagNames = post.Tags.Select(pt => pt.Name); 
            ///////////////////////////////////

            return View("form", new PostForms
            {
                IsNew = false,
                BlogPostId = post.PostID,
                Content = post.Content,
                Slug = post.Slug,
                Title = post.Title,
                Author = post.PostingUsername,
                Tags = db.Tags.Select(tag => new TagCheckBox
                {
                    Id = tag.TagID,
                    Name = tag.Name,
                    IsChecked = postTagNames.Contains(tag.Name)
//                  IsChecked = (post.Tags.Contains(tag))           :/See problems encountered. same issue as problem one.
                }).ToList()

    });
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Trash(int id)
        {
            var post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            post.DeletedAt = DateTime.UtcNow;
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var post = db.Posts.Find(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            db.Posts.Remove(post);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Restore(int id)
        {
            var post = db.Posts.Find(id);
            if (post == null)
                return HttpNotFound();

            post.DeletedAt = null;
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }



        public IEnumerable<Tag> ReconcileTags(IEnumerable<TagCheckBox> tags)
        {
            foreach (var tag in tags.Where(t => t.IsChecked))
            {

                if (tag.Id != null)
                {
                    yield return db.Tags.Find(tag.Id);
                    continue;
                }

                var existingTag = db.Tags.FirstOrDefault(u => u.Name == tag.Name);
                if (existingTag != null)
                {
                    yield return existingTag;
                    continue;
                }

                var newTag = new Tag
                {
                    Name = tag.Name,
                    Slug = tag.Name.Slugify()
                };

                db.Tags.Add(newTag);
                db.SaveChanges();

                yield return newTag;
          }

        }


    }
}