using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SimpleBlog.Infrastructure
{
    public static class Extensions
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

        public static string Slugify(this string that)
        {
            that = Regex.Replace(that, @"[^a-zA-Z0-9\s]", "");
            that = that.ToLower();
            that = Regex.Replace(that, @"\s", " ");
            return that;
        }
        
            
        }


    }
