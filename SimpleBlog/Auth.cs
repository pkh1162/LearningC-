using SimpleBlog.Areas.admin.Models;
using SimpleBlog.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog
{
    public static class Auth
    {

        private const string Userkey = "SimpleBlog.Auth.UserKey";
        private static UserRoleContext db = new UserRoleContext();


        public static User User {

            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;

                var user = HttpContext.Current.Items[Userkey] as User;
                if (user == null)
                {
                    user = db.Users.FirstOrDefault(u => u.Username == HttpContext.Current.User.Identity.Name);

                    if (user == null)
                        return null;

                    HttpContext.Current.Items[Userkey] = user;
                }

                return user;
            }

        }



    }
}