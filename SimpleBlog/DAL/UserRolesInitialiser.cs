using SimpleBlog.Areas.admin.Models;
using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.DAL
{
    public class UserRoleInitialiser : System.Data.Entity.DropCreateDatabaseIfModelChanges<UserRoleContext>
    {
       // private PostsTagsContext context2 = new PostsTagsContext();
        //DropCreateDatabaseIfModelChanges<UserRoleContext>

        protected override void Seed(UserRoleContext context)
        {

            List<Tag> tags = new List<Tag>
            {
                new Tag {Name = "design", Slug = "dddd"  }
            };

            tags.ForEach(t => context.Tags.Add(t));
            context.SaveChanges();
           

            List<User> users = new List<User>
            {
                new User { Username = "pkh1162", Email = "pop@hop.com", PasswordHash = "test"},
                new User { Username = "bob", Email = "too@hop.com", PasswordHash = "franc"},
                new User { Username = "sue", Email = "tio@hop.com", PasswordHash = "you"},
             //   new User { Username = "mary", Email = "djdh@hop.com", PasswordHash = "blizzrd"},
             //   new User { Username = "Sue", Email = "dolly@hop.com", PasswordHash = "tango" },
             //   new User { Username = "hue", Email = "polly@hop.com", PasswordHash = "tango" },
             //   new User { Username = "Lue", Email = "popolly@hop.com", PasswordHash = "tango"}
            };

            users.ForEach(u => u.SetPassword("test"));

            users.ForEach(user => context.Users.Add(user));
            context.SaveChanges();


            List<Role> roles = new List<Role>
            {
                 new Role { RoleName = "admin" },
                 new Role { RoleName = "generalUser" },
                 new Role { RoleName = "author" }
        };

            roles.ForEach(role => context.Roles.Add(role));
            context.SaveChanges();



        }
    }
}
    





    



    
