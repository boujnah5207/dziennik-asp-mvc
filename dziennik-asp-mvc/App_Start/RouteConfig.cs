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
                name: "groupsList",
                url: "Groups/",
                defaults: new { controller = "Groups", action = "List", id = UrlParameter.Optional},
                namespaces: new string[] { "dziennik-asp-mvc.Controllers" }
            );

            routes.MapRoute(
              name: "logout",
              url: "Logout",
              defaults: new { controller = "Account", action = "Logout", id = UrlParameter.Optional },
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
