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
              name: "createFinal",
              url: "Grades/Students/Create/Final",
              defaults: new { controller = "Grades", action = "CreateFinal" },
              namespaces: new string[] { "dziennik-asp-mvc.Controllers" }
          );

            routes.MapRoute(
              name: "editFinal",
              url: "Grades/Students/Edit/Final/{id}",
              defaults: new { controller = "Grades", action = "EditFinal" },
              namespaces: new string[] { "dziennik-asp-mvc.Controllers" }
          );

            routes.MapRoute(
              name: "deleteFinal",
              url: "Grades/Students/Delete/Final/{id}",
              defaults: new { controller = "Grades", action = "DeleteFinal" },
              namespaces: new string[] { "dziennik-asp-mvc.Controllers" }
          );

            routes.MapRoute(
               name: "createPartial",
               url: "Grades/Students/Create/Partial",
               defaults: new { controller = "Grades", action = "CreatePartial" },
               namespaces: new string[] { "dziennik-asp-mvc.Controllers" }
           );

            routes.MapRoute(
              name: "editPartial",
              url: "Grades/Students/Edit/Partial/{id}",
              defaults: new { controller = "Grades", action = "EditPartial" },
              namespaces: new string[] { "dziennik-asp-mvc.Controllers" }
          );

            routes.MapRoute(
              name: "deletePartial",
              url: "Grades/Students/Delete/Partial/{id}",
              defaults: new { controller = "Grades", action = "DeletePartial" },
              namespaces: new string[] { "dziennik-asp-mvc.Controllers" }
          );

            routes.MapRoute(
                name: "creditingFormList",
                url: "CreditingForm/",
                defaults: new { controller = "CreditingForm", action = "List" },
                namespaces: new string[] { "dziennik-asp-mvc.Controllers" }
            );

            routes.MapRoute(
                name: "teachersList",
                url: "Users/Teachers/",
                defaults: new { controller = "Teachers", action = "List" },
                namespaces: new string[] { "dziennik-asp-mvc.Controllers" }
            );

            routes.MapRoute(
             name: "manageTeachers",
             url: "Users/Teachers/{action}/{id}",
             defaults: new { controller = "Teachers", action = "List", id = UrlParameter.Optional },
             namespaces: new string[] { "dziennik-asp-mvc.Controllers" }
           );

            routes.MapRoute(
             name: "studentsList",
             url: "Users/Students/",
             defaults: new { controller = "Students", action = "List" },
             namespaces: new string[] { "dziennik-asp-mvc.Controllers" }
           );

            routes.MapRoute(
             name: "manageStudents",
             url: "Users/Students/{action}/{id}",
             defaults: new { controller = "Students", action = "List", id = UrlParameter.Optional },
             namespaces: new string[] { "dziennik-asp-mvc.Controllers" }
           );

            routes.MapRoute(
               name: "subjectsList",
               url: "Subjects/",
               defaults: new { controller = "Subjects", action = "List" },
               namespaces: new string[] { "dziennik-asp-mvc.Controllers" }
           );

            routes.MapRoute(
               name: "subjectsInGroupList",
               url: "Groups/Subjects/{id}",
               defaults: new { controller = "Groups", action = "SubjectsInGroup" },
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
