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
        private ISubjectsService subjectsService;
        private IUsersService usersService;
        private IRolesService rolesService;
        private IPartialGradesService partialService;
        private IFinalGradesService finalService;
        private IGroupsService groupsService;

        public UserController(IUsersService usersService, IRolesService rolesService, ISubjectsService subjectsService, IPartialGradesService partialService, IFinalGradesService finalService,
            IGroupsService groupsService)
        {
            this.usersService = usersService;
            this.rolesService = rolesService;
            this.subjectsService = subjectsService;
            this.partialService = partialService;
            this.finalService = finalService;
            this.groupsService = groupsService;
        }

        public ActionResult Profile()
        {
            Users user = usersService.FindByName(User.Identity.Name);
            return View(user);
        }

        public ActionResult MyGrades()
        {
            Users user = usersService.FindByName(User.Identity.Name);
            int id_group = (int)user.id_group;

            Groups groups = groupsService.FindById(id_group);
            IEnumerable<Subjects> subjects = subjectsService.FindAllSubjectsForGroup(id_group);

            List<MyGradesViewModel> viewModel = new List<MyGradesViewModel>();
            int size = 0;

            foreach (Subjects sub in subjects)
            {
                MyGradesViewModel model = new MyGradesViewModel();

                model.user = sub.Users.full_name;
                model.subject = sub.subject_name;

                foreach (Partial_Grades partialGrade in sub.Partial_Grades)
                {
                    if (partialGrade.id_user.CompareTo(user.id_user) == 0)
                    {
                        model.partialGrades.Add(partialGrade);
                        if (model.partialGrades.Count > size)
                        {
                            size = model.partialGrades.Count;
                        }
                    }
                }

                if (model.partialGrades.Count == 0)
                {
                    model.partialGrades.Add(new Partial_Grades());
                }

                foreach (Final_Grades finalGrade in sub.Final_Grades)
                {
                    if (finalGrade.id_user.CompareTo(user.id_user) == 0)
                    {
                        model.finalGrades.Add(finalGrade);
                    }
                }

                if (model.finalGrades.Count == 0)
                {
                    model.finalGrades.Add(new Final_Grades());
                }

                viewModel.Add(model);
            }

            foreach(MyGradesViewModel mod in viewModel)
            {
                if(mod.partialGrades.Count < size){
                    while(size > mod.partialGrades.Count){
                        mod.partialGrades.Add(new Partial_Grades());
                    }
                }
            }

            ViewBag.Size = size;

            return View("MyGrades", viewModel);
        }

        public ActionResult MySubjects()
        {
            Users user = usersService.FindByName(User.Identity.Name);
            int id_group = (int)user.id_group;
            return View("MySubjects", subjectsService.FindAllSubjectsForGroup(id_group));
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

            return View("UserProfile", user);
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
            TempData["Msg"] = "Udało się pomyślnie zaktualizować profil!";

            return RedirectToAction("UserProfile", new { id = foundUser.id_user });
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
