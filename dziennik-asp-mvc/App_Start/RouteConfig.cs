using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace dziennik_asp_mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "teachersList",
              url: "Users/Teachers/",
              defaults: new { controller = "Teachers", action = "List" },
              namespaces: new string[] { "dziennik-asp-mvc.Controllers" }
            );

            routes.MapRoute(
             name: "manageTeachers",
             url: "Users/{controller}/{action}/{id}",
             defaults: new { controller = "Teachers", action = "List", id = UrlParameter.Optional },
             namespaces: new string[] { "dziennik-asp-mvc.Controllers" }
           );

            routes.MapRoute(
                name: "groupsList",
                url: "Groups/",
                defaults: new { controller = "Groups", action = "List" },
                namespaces: new string[] { "dziennik-asp-mvc.Controllers" }
            );

            routes.MapRoute(
              name: "logout",
              url: "Logout",
              defaults: new { controller = "Account", action = "Logout" },
              namespaces: new string[] { "dziennik-asp-mvc.Controllers" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "dziennik-asp-mvc.Controllers" }

            );
        }
    }
}
