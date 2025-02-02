﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace SimpleBlog.App_Start
{
	public class BundleConfig 
	{
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/admin/styles")
                .Include("~/content/styles/bootstrap.css")
                .Include("~/content/styles/admin.css")
                );


            bundles.Add(new StyleBundle("~/styles")
                .Include("~/content/styles/bootstrap.css")
                .Include("~/content/styles/site.css"));


            bundles.Add(new ScriptBundle("~/admin/scripts")
                .Include("~/scripts/jquery-2.2.3.js")
                .Include("~/scripts/jquery.validate.js")
                .Include("~/scripts/jquery.validate.unobtrusive.js")
                .Include("~/scripts/bootstrap.js")
                .Include("~/areas/admin/Scripts/forms.js")
                );

            bundles.Add(new ScriptBundle("~/admin/post/scripts")
               .Include("~/areas/admin/Scripts/posteditor.js")
               );

            bundles.Add(new ScriptBundle("~/scripts")
             .Include("~/scripts/jquery-2.2.3.js")
             .Include("~/scripts/jquery.validate.js")
             .Include("~/scripts/jquery.validate.unobtrusive.js")
             .Include("~/scripts/bootstrap.js")
             .Include("~/scripts/jquery.timeago.js")
             .Include("~/scripts/frontend.js")
             );
        }
	}
}