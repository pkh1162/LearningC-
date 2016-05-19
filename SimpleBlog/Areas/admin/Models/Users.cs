using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleBlog.Areas.admin.Models
{
    public class User
    {

        public User()
        {
            Roles = new List<Role>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual IList<Role> Roles { get; set; }


        public void SetPassword(string password)
        {
            Password = password;
        }



    }
}