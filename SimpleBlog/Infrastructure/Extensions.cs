using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Infrastructure
{
    public class Extensions
    {
        public static string SelectedTabHelper(string viewBag, string selectedTab)
        {
            if (viewBag == selectedTab)
            {
                return "active";
            }
            else
            {
                return "";
            }
        }
    }
}