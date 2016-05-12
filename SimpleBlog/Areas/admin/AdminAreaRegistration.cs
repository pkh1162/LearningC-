using System.Web.Mvc;

namespace SimpleBlog.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get {return "admin";}
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute("adminHome","admin/home/index");   //Just an example of easily giving a area route a name.
        }
    }
}