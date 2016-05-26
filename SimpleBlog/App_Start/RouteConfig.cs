using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Linq;
using SimpleBlog.Controllers;

/*
 * For some reason my links are not working the way I expected them to. If I link to the route names "Home" or "Blog",
 * with {controller}{action} and {id} tokens, the link seemingly just takes you to the page that you are currently on.
 * I think the link is just taking in the current controller, action and id for some reason.
 * I temporarily fixed this problem by specifying route names which end with Page (eg. HomePage, BlogPage), these routes
 * don't have tokens and I think this is why they work. The defaults for these routes do not matter.
*/


namespace SimpleBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
 
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            var nsControllersMain = new[] {typeof(HomeController).Namespace};
            


            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            routes.MapRoute(
             name: "Home",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional},
             namespaces: nsControllersMain             
             );

             routes.MapRoute(
             name: "HomePage",
             url: "Home/Index/{id}",                                // Need to specify the url exactly because the link would just take you to the page you were already on. The defaults are redundant in this case.
             defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional},
             namespaces: nsControllersMain             
             );
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            routes.MapRoute(
             name: "Blog",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional},
             namespaces: nsControllersMain
             );

            routes.MapRoute(
            name: "BlogPage",
            url: "Blog/Index/{id}",
            defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional },
            namespaces: nsControllersMain
            );
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            routes.MapRoute(
                name: "RealPost",
                url: "post/Show/{idAndSlug}",
                defaults: new { controller = "Blog", action = "Show", id = UrlParameter.Optional },
                namespaces: nsControllersMain);

            routes.MapRoute(
                name: "Post", 
                url: "post/Show/{id}/{slug}",
                defaults: new { controller = "Blog", action = "Show", id = UrlParameter.Optional },
                namespaces: nsControllersMain);

            routes.MapRoute(
                name: "Tag",
                url: "post/Tag/{id}/{slug}",
                defaults: new { controller = "Blog", action = "Tag", id = UrlParameter.Optional },
                namespaces: nsControllersMain);

          

           // routes.MapRoute("Tag1", "tag/{id}-{slug}", new { controller = "Blog", action = "Tag" }, nsControllersMain);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////







            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            routes.MapRoute(
            name: "Login",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional },
            namespaces: nsControllersMain
            );

            routes.MapRoute(
            name: "LoginPage",
            url: "Login/Index/{id}",
            defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional },
            namespaces: nsControllersMain
            );
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            routes.MapRoute(
            name: "Logout",
            url: "logout",
            defaults: new { controller = "Logout", action = "Index", id = UrlParameter.Optional },
            namespaces: nsControllersMain
            );




        }
    }
}
