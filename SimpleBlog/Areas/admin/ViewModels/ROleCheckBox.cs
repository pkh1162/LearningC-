using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Areas.admin.ViewModels
{
    public class RoleCheckBox
    {
        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public string Name { get; set; }
    }
}