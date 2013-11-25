using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using dziennik_asp_mvc.Models.Entities;
using dziennik_asp_mvc.Models.Data.Concrete;
using dziennik_asp_mvc.Models.Data.Abstract;
using dziennik_asp_mvc.Models.Data.Abstract.Roles;
using System.Web.Security;
using dziennik_asp_mvc.Exceptions;
using PagedList;

namespace dziennik_asp_mvc.Controllers
{
    [Authorize(Roles = "Administrator,Wykładowca")]
    public class TeachersController : Controller
    {
        private IUsersService usersService;
        private IRolesService rolesService;
        private IGroupsService groupsService;

        public TeachersController(IUsersService usersService, IGroupsService groupsService, IRolesService rolesService)
        {
            this.usersService = usersService;
            this.rolesService = rolesService;
            this.groupsService = groupsService;
        }

        public ActionResult List(int? page, string column = "login", string sort = "ASC", string searchString = "", string currentFilter = "")
        {
            ViewBag.CurrentColumn = column;
            ViewBag.CurrentSort = sort == "ASC" ? "DESC" : "ASC";

            if (String.IsNullOrEmpty(searchString))
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var teachers = usersService.FindAllTeachers();

            if (!String.IsNullOrEmpty(searchString))
            {
                teachers = teachers.Where(s => s.login.ToUpper().Contains(searchString.ToUpper()) 
                    || s.first_name.ToUpper().Contains(searchString.ToUpper())
                    || s.last_name.ToUpper().Contains(searchString.ToUpper())
                    || s.email.ToUpper().Contains(searchString.ToUpper()));
            }

            teachers = Sort(column, sort, teachers);

            int pageSize = 5;
            int pageNumber = (page.HasValue ? page.Value : 1); // Jeśli page == null to page = 1

            return View(teachers.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            ViewBag.Type = "create";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_user,login,password,first_name,last_name,email,status")] Users users)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Type = "create";
                return View(users);
            }

            try
            {
                Users foundUser = usersService.FindByName(users.login);

                if (foundUser != null)
                {
                    throw new UserNameAlreadyExistsException();
                }
            }
            catch (UserNameAlreadyExistsException ex)
            {
                ViewBag.Type = "create";
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Taki użytkownik już istnieje!";
                return View(users);
            }
            catch (UserNotFoundException ex) { }

            try
            {
                users.Roles = rolesService.FindByName("Wykładowca");
                users.password = FormsAuthentication.HashPasswordForStoringInConfigFile(users.password, "md5");
                usersService.Add(users);
                TempData["Status"] = "success";
                TempData["Msg"] = "Nowy wykładowca został dodany!";
            }
            catch (Exception e)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się dodać nowego wykładowcy!";
            }
            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Type = "edit";

            if (id == null)
            {
                throw new UserNotFoundException();
            }

            Users users = null;

            try
            {
                users = usersService.FindById(id);

                return View(users);
            }
            catch (UserNotFoundException ex)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie znaleziono użytkownika!";
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_user,id_role,login,password,first_name,last_name,email,status,album_number")] Users users)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Type = "edit";
                return View(users);
            }

            try
            {
                users.password = FormsAuthentication.HashPasswordForStoringInConfigFile(users.password, "md5");
                usersService.Edit(users);
                TempData["Status"] = "success";
                TempData["Msg"] = "Aktualizacja wykładowcy przebiegła pomyślnie!";
            }
            catch (Exception e)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się zaktualizować wykładowcy!";
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                usersService.Delete(id);
                TempData["Status"] = "success";
                TempData["Msg"] = "Pomyślnie usunięto wykładowcę!";
            }
            catch (Exception e)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się usunąć wykładowcy!";
            }
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                usersService.Dispose();
            }
            base.Dispose(disposing);
        }

        private static IQueryable<Users> Sort(string column, string sortOrder, IQueryable<Users> users)
        {
            switch (sortOrder)
            {
                case "ASC":
                    switch (column)
                    {
                        case "login":
                            users = users.OrderBy(s => s.login);
                            break;
                        case "firstName":
                            users = users.OrderBy(s => s.first_name);
                            break;
                        case "lastName":
                            users = users.OrderBy(s => s.last_name);
                            break;
                        case "email":
                            users = users.OrderBy(s => s.email);
                            break;
                        case "status":
                            users = users.OrderBy(s => s.status);
                            break;
                    }
                    break;
                case "DESC":
                    switch (column)
                    {
                        case "login":
                            users = users.OrderByDescending(s => s.login);
                            break;
                        case "firstName":
                            users = users.OrderByDescending(s => s.first_name);
                            break;
                        case "lastName":
                            users = users.OrderByDescending(s => s.last_name);
                            break;
                        case "email":
                            users = users.OrderByDescending(s => s.email);
                            break;
                        case "status":
                            users = users.OrderByDescending(s => s.status);
                            break;
                    }
                    break;
                default:
                    users = users.OrderBy(s => s.login);
                    break;
            }
            return users;
        }
    }
}
