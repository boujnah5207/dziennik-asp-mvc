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
using dziennik_asp_mvc.ViewModels;

namespace dziennik_asp_mvc.Controllers
{
    [Authorize(Roles = "Administrator,Wykładowca,Student")]
    public class UserController : Controller
    {
        private IUsersService usersService;
        private IRolesService rolesService;

        public UserController(IUsersService usersService, IRolesService rolesService)
        {
            this.usersService = usersService;
            this.rolesService = rolesService;
        }

        public ActionResult Profile()
        {
            Users user = usersService.FindByName(User.Identity.Name);
            return View(user);
        }

        public ActionResult UserProfile(int id)
        {
            Users user = null;
            try
            {
                user = usersService.FindById(id);

            }
            catch (UserNotFoundException ex)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Taki użytkownik już istnieje!";
                return RedirectToAction("Profile");
            }

            return View("Profile", user);
        }

        [HttpPost]
        public ActionResult ChangeProfile(ProfileViewModel model)
        {

            Users foundUser = null;
            try
            {
                foundUser = usersService.FindById(model.Users.id_user);
            }
            catch (UserNotFoundException ex)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Taki użytkownik już istnieje!";
                return RedirectToAction("Profile");
            }

            try
            {
                foundUser.id_role = model.Users.id_role;
                foundUser.login = model.Users.login;
                foundUser.first_name = model.Users.first_name;
                foundUser.last_name = model.Users.last_name;
                foundUser.email = model.Users.email;
                usersService.Edit(foundUser);
            }
            catch (Exception ex)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się zaktualizować profilu!";
                return RedirectToAction("Profile");
            }

            TempData["Status"] = "success";
            TempData["Msg"] = "Udało się pomyślnie zaktualizować profilu!";

            return RedirectToAction("UserProfile", new { id = foundUser.id_user});
        }

        [HttpPost]
        public ActionResult EditProfile(Users user)
        {
            ProfileViewModel model = new ProfileViewModel();
            if (user != null)
            {
                model.Users = usersService.FindById(user.id_user);
            }
            else
            {
                model.Users = usersService.FindByName(User.Identity.Name);
            }

            ViewBag.Roles = new SelectList(rolesService.FindAll, "id_role", "role_name");

            return View(model);
        }

        [HttpPost]
        public ActionResult ChangePassword(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Taki użytkownik już istnieje!";
                return RedirectToAction("Profile");
            }

            if (String.IsNullOrEmpty(model.ActualPassword) || String.IsNullOrEmpty(model.NewPassword) || String.IsNullOrEmpty(model.RepeatedPassword))
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się zmienić hasła!";

                return RedirectToAction("Profile");
            }

            Users user = null;

            try
            {
                user = usersService.FindById(model.Users.id_user);

                string OldPassword = user.password;
                string HashedActualPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(model.ActualPassword, "md5");
                string HashedNewPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(model.NewPassword, "md5");

                if (!OldPassword.Equals(HashedActualPassword, StringComparison.OrdinalIgnoreCase))
                {
                    TempData["Status"] = "invalid";
                    TempData["Msg"] = "Podane aktualne hasło nie jest zgodne z zapisanym w bazie!";
                    return RedirectToAction("Profile");
                }

                if (!model.NewPassword.Equals(model.RepeatedPassword))
                {
                    TempData["Status"] = "invalid";
                    TempData["Msg"] = "Nowe hasło oraz potwierdzenie hasła się nie zgadzają!";
                    return RedirectToAction("Profile");
                }

                user.password = HashedNewPassword;
                usersService.Edit(user);

                TempData["Status"] = "success";
                TempData["Msg"] = "Pomyślnie zaktualizowano hasło!";

                return RedirectToAction("Profile");
            }
            catch (UserNotFoundException ex)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie znaleziono użytkownika!";
                return RedirectToAction("Profile");
            }



            return View();
        }
    }
}
