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
using dziennik_asp_mvc.Exceptions;
using System.Web.Security;
using PagedList;

namespace dziennik_asp_mvc.Controllers
{
    [Authorize(Roles = "Administrator,Wykładowca")]
    public class StudentsController : Controller
    {
        private IUsersService usersService;
        private IRolesService rolesService;
        private IGroupsService groupsService;

        public StudentsController(IUsersService usersService, IGroupsService groupsService, IRolesService rolesService)
        {
            this.usersService = usersService;
            this.rolesService = rolesService;
            this.groupsService = groupsService;
        }
        [HttpGet]
        public ActionResult List(int? page, int groupId = 1, string column = "login", string sort = "ASC")
        {
            ViewBag.CurrentColumn = column;
            ViewBag.CurrentSort = sort == "ASC" ? "DESC" : "ASC";
            ViewBag.Groups = new SelectList(groupsService.FindAll, "id_group", "full_name");

            var students = usersService.FindAllStudentsInGroup(groupId);
            students = Sort(column, sort, students);

            int pageSize = 5;
            int pageNumber = (page.HasValue ? page.Value : 1); // Jeśli page == null to page = 1

            return View(students.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Group(int? page, int groupId = 1, string column = "login", string sort = "ASC")
        {
            ViewBag.CurrentColumn = column;
            ViewBag.CurrentSort = sort == "ASC" ? "DESC" : "ASC";
            ViewBag.Groups = new SelectList(groupsService.FindAll, "id_group", "full_name");

            var students = usersService.FindAllStudentsInGroup(groupId);
            students = Sort(column, sort, students);

            int pageSize = 5;
            int pageNumber = (page.HasValue ? page.Value : 1); // Jeśli page == null to page = 1

            return View("List",students.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            ViewBag.Type = "create";
            ViewBag.Groups = new SelectList(groupsService.FindAll, "id_group", "full_name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_user,id_group,login,password,first_name,last_name,album_number,email,status")] Users users)
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
                users.Roles = rolesService.FindByName("Student");
                users.password = FormsAuthentication.HashPasswordForStoringInConfigFile(users.password, "md5");
                usersService.Add(users);
                TempData["Status"] = "success";
                TempData["Msg"] = "Nowy student został dodany!";
            }
            catch (Exception e)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się dodać nowego studenta!";
            }
            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Type = "edit";
            ViewBag.Groups = new SelectList(groupsService.FindAll, "id_group", "full_name");

            if (id == null)
            {
                throw new UserNotFoundException();
            }

            Users users = null;

            try
            {
                Users user = usersService.FindById(id);

                if (user.Roles.role_name == "Wykładowca" || user.Roles.role_name == "Administator")
                {
                    throw new UserNotFoundException();
                }

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
        public ActionResult Edit([Bind(Include = "id_user,id_group,id_role,login,password,first_name,last_name,email,status,album_number")] Users users)
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
                TempData["Msg"] = "Aktualizacja studenta przebiegła pomyślnie!";
            }
            catch (Exception e)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się zaktualizować studenta!";
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                Users user = usersService.FindById(id);

                if (user.Roles.role_name == "Wykładowca" || user.Roles.role_name == "Administator")
                {
                    throw new UserNotFoundException();
                }

                usersService.Delete(id);
                TempData["Status"] = "success";
                TempData["Msg"] = "Pomyślnie usunięto studenta!";
            }
            catch (Exception e)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się usunąć studenta!";
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
                        case "album_number":
                            users = users.OrderBy(s => s.album_number);
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
                        case "album_number":
                            users = users.OrderByDescending(s => s.album_number);
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
