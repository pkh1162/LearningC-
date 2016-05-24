using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleBlog.ViewModels
{
    public class BlogIndex //Convention: [Controller name][Action name]
    {
        public PagedData<Post> Posts { get; set; }
    }

    public class PostForms
    {
        public bool IsNew { get; set; }
        public int? BlogPostId { get; set; }
        public string Author { get; set; }
       

        [Required, MaxLength(128)]
        public string Title { get; set; }

        [Required, MaxLength(128)]
        public string Slug { get; set; }

        [Required, DataType(DataType.MultilineText)]
        public string Content { get; set; }


    }

}