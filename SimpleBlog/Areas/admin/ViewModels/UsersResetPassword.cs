using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleBlog.Areas.admin.ViewModels
{
    public class UsersResetPassword
    {
        
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !"),
            Required, DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


    }
}