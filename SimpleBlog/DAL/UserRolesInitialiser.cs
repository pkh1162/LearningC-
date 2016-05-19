using SimpleBlog.Areas.admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.DAL
{
    public class UserRoleInitialiser : System.Data.Entity.DropCreateDatabaseAlways<UserRoleContext>
    {

        //DropCreateDatabaseIfModelChanges<UserRoleContext>

        protected override void Seed(UserRoleContext context)
        {

           
            

            List<User> users = new List<User>
            {
                new User { Username = "pkh1162", Email = "pop@hop.com", Password = "nope"},
                new User { Username = "bob", Email = "too@hop.com", Password = "franc"},
                new User { Username = "mary", Email = "djdh@hop.com", Password = "blizzrd"},
                new User { Username = "Sue", Email = "dolly@hop.com", Password = "tango" },
                new User { Username = "hue", Email = "polly@hop.com", Password = "tango" },
                new User { Username = "Lue", Email = "popolly@hop.com", Password = "tango"}


            };

            
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
    





    



    
