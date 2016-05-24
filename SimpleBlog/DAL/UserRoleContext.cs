using SimpleBlog.Areas.admin.Models;
using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SimpleBlog.DAL
{
    public class UserRoleContext : DbContext
    {
        public UserRoleContext() : base("UserRoleContext")
        {
        }

       
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder) //This method just prevents tables names being pluralised in our database.
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}