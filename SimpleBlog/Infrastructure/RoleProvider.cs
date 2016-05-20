﻿using SimpleBlog.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Infrastructure
{
    public class RoleProvider : System.Web.Security.RoleProvider
    {
        private UserRoleContext db = new UserRoleContext();

        public override string[] GetRolesForUser(string username)
        {

            return Auth.User.Roles.Select(r => r.RoleName).ToArray();


            /*
            var user = db.Users.FirstOrDefault(u => username == u.Username);
            var roleArray = user.Roles.Select(r => r.RoleName).ToArray();
            //return roleArray;
            

            if (roleArray.Contains("admin"))
            {
                return new string[] { "admin" };
            }


            else if (roleArray.Contains("author"))
            {
                return new string[] { "author" };
            }
            else if (roleArray.Contains("generaluser"))
            {
                return new string[] { "generalUser" };
            }
            else
                return new string[] { };
                */



        }


        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public string[] RolesForUser(string username)
        {
            var user = db.Users.FirstOrDefault(u => username == u.Username);
            var roleArray = user.Roles.Select(r => r.RoleName).ToArray();
            return roleArray;
        }
    }
}