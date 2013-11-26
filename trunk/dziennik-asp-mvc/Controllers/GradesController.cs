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
using Newtonsoft.Json;
using dziennik_asp_mvc.ViewModels;

namespace dziennik_asp_mvc.Controllers
{
    [Authorize(Roles = "Administrator,Wykładowca,Student")]
    public class GradesController : Controller
    {
        private IFinalGradesService finalGradesService;
        private IPartialGradesService partialGradesService;
        private IUsersService usersService;
        private ISubjectsService subjectsService;
        private IRolesService rolesService;
        private IGroupsService groupsService;
        private ICreditingFormService creditingFormService;

        public GradesController( ICreditingFormService creditingFormService, IFinalGradesService finalGradesService, IPartialGradesService partialGradesService, ISubjectsService subjectsService, IUsersService usersService, IGroupsService groupsService, IRolesService rolesService)
        {
            this.finalGradesService = finalGradesService;
            this.partialGradesService = partialGradesService;
            this.subjectsService = subjectsService;
            this.usersService = usersService;
            this.rolesService = rolesService;
            this.groupsService = groupsService;
            this.creditingFormService = creditingFormService;
        }

        [HttpGet]
        public String Subjects(int id)
        {
            var subjects = subjectsService.FindAllSubjectsForGroup(id);
            int count = subjects.ToList().Count;
            
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            return JsonConvert.SerializeObject(subjects, Formatting.Indented, settings);

        }

        [HttpGet]
        public String Students(int id)
        {
            var users = usersService.FindAllStudentsInGroup(id);
            int count = users.ToList().Count;

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            return JsonConvert.SerializeObject(users, Formatting.Indented, settings);

        }

        [HttpPost]
        public ActionResult Show(int id_group, int id_subject)
        {
            var groups = groupsService.FindAll;
            IQueryable<Subjects> subjects = null;
            if (groups != null && groups.ToList().Count() != 0)
            {
                subjects = subjectsService.FindAllSubjectsForGroup(id_group);
            }

            ViewBag.Groups = new SelectList(groups, "id_group", "full_name", id_group);
            ViewBag.Subjects = new SelectList(subjects, "id_subject", "subject_name", id_subject);

            var studentsList = usersService.FindAllStudentsInGroup(id_group);          

            List<Users> usersList = new List<Users>();
            int maxPartialGrades = 0;
            int maxFinalGrades = 0;

            foreach (Users user in studentsList)
            {
                foreach (Subjects subject in subjects)
                {
                    if (id_subject.CompareTo(subject.id_subject) == 0 &&
                        (subject.Partial_Grades.Any(m => m.id_subject == id_subject && m.Users.id_group == id_group) 
                        || subject.Final_Grades.Any(m => m.id_subject == id_subject && m.Users.id_group == id_group)))
                    {
                        if (user.Partial_Grades.Count > maxPartialGrades)
                        {
                            maxPartialGrades = user.Partial_Grades.Count;
                        }
                        if (user.Final_Grades.Count > maxFinalGrades)
                        {
                            maxFinalGrades = user.Final_Grades.Count;
                        }
                        usersList.Add(user);
                    }
                }
            }
            ViewBag.MaxPartialGrades = maxPartialGrades;
            ViewBag.MaxFinalGrades = maxFinalGrades;
            return View("List", usersList);
        }

        [HttpGet]
        public ActionResult List()
        {
            var groups = groupsService.FindAll;
            IQueryable<Subjects> subjects = null;
            if (groups != null && groups.ToList().Count() != 0)
            {
                subjects = subjectsService.FindAllSubjectsForGroup(groups.First().id_group);
            }

            ViewBag.Groups = new SelectList(groups, "id_group", "full_name");
            ViewBag.Subjects = new SelectList(subjects, "id_subject", "subject_name");

            return View();
        }

        
        public ActionResult CreatePartial()
        {
            ViewBag.Type = "create";

            var groups = groupsService.FindAll;
            var form = creditingFormService.FindAll;
            IQueryable<Subjects> subjects = null;
            IQueryable<Users> users = null;
            if (groups != null && groups.ToList().Count() != 0)
            {
                subjects = subjectsService.FindAllSubjectsForGroup(groups.First().id_group);
                users = usersService.FindAllStudentsInGroup(groups.First().id_group);
            }

            ViewBag.Groups = new SelectList(groups, "id_group", "full_name");
            ViewBag.Subjects = new SelectList(subjects, "id_subject", "subject_name");
            ViewBag.Users = new SelectList(users, "id_user", "full_name");
            ViewBag.CreditingForm = new SelectList(form, "id_crediting_form", "name");

            return View("Partial/Create", new PartialGradesViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePartial(PartialGradesViewModel gradesViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Type = "create";
                return RedirectToAction("CreatePartial");
            }

            try
            {
                partialGradesService.Add(gradesViewModel.grade);
                TempData["Status"] = "success";
                TempData["Msg"] = "Nowa ocena została dodana!";
            }
            catch (Exception e)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się dodać nowej oceny!";
            }
            return RedirectToAction("List");
        }

        public ActionResult EditPartial(int id)
        {
            ViewBag.Type = "edit";

            var groups = groupsService.FindAll;
            var form = creditingFormService.FindAll;
            IQueryable<Subjects> subjects = null;
            IQueryable<Users> users = null;

            if (groups != null && groups.ToList().Count() != 0)
            {
                subjects = subjectsService.FindAllSubjectsForGroup(groups.First().id_group);
                users = usersService.FindAllStudentsInGroup(groups.First().id_group);
            }

            if (id == null)
            {
                throw new PartialGradesNotFoundException();
            }

            Partial_Grades grade = null;

            try
            {
                grade = partialGradesService.FindById(id);

                PartialGradesViewModel model = new PartialGradesViewModel();
                model.grade = grade;
                model.SelectedGroup = usersService.FindById(grade.id_user).id_group.ToString();
                model.SelectedSubject = grade.id_subject.ToString();
                model.SelectedUser = grade.id_user.ToString();

                ViewBag.Groups = new SelectList(groups, "id_group", "full_name");
                ViewBag.Subjects = new SelectList(subjects, "id_subject", "subject_name");
                ViewBag.Users = new SelectList(users, "id_user", "full_name");
                ViewBag.CreditingForm = new SelectList(form, "id_crediting_form", "name");
                
                return View("Partial/Edit",model);
            }
            catch (PartialGradesNotFoundException ex)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie znaleziono oceny!";
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPartial(PartialGradesViewModel gradesViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Type = "edit";

                string errorString = "";

                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        errorString += error.ErrorMessage + "\n";
                    }
                }
                TempData["Status"] = "invalid";
                TempData["Msg"] = errorString;

                return RedirectToAction("EditPartial", new { id = gradesViewModel.grade.id_grade });
            }

            try
            {
                partialGradesService.Edit(gradesViewModel.grade);
                TempData["Status"] = "success";
                TempData["Msg"] = "Aktualizacja oceny przebiegła pomyślnie!";
            }
            catch (Exception e)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się zaktualizować oceny!";
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult DeletePartial(int id)
        {
            try
            {
                partialGradesService.Delete(id);
                TempData["Status"] = "success";
                TempData["Msg"] = "Pomyślnie usunięto ocenę!";
            }
            catch (Exception e)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się usunąć ocenę!";
            }
            return RedirectToAction("List");
        }

        public ActionResult CreateFinal()
        {
            ViewBag.Type = "create";

            var groups = groupsService.FindAll;
            var form = creditingFormService.FindAll;
            IQueryable<Subjects> subjects = null;
            IQueryable<Users> users = null;
            if (groups != null && groups.ToList().Count() != 0)
            {
                subjects = subjectsService.FindAllSubjectsForGroup(groups.First().id_group);
                users = usersService.FindAllStudentsInGroup(groups.First().id_group);
            }

            ViewBag.Groups = new SelectList(groups, "id_group", "full_name");
            ViewBag.Subjects = new SelectList(subjects, "id_subject", "subject_name");
            ViewBag.Users = new SelectList(users, "id_user", "full_name");
            ViewBag.CreditingForm = new SelectList(form, "id_crediting_form", "name");

            return View("Final/Create", new FinalGradesViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFinal(FinalGradesViewModel gradesViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Type = "create";
                return RedirectToAction("EditFinal", new { id = gradesViewModel.grade.id_final_grade });
            }

            try
            {
                finalGradesService.Add(gradesViewModel.grade);
                TempData["Status"] = "success";
                TempData["Msg"] = "Nowa ocena została dodana!";
            }
            catch (Exception e)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się dodać nowej oceny!";
            }
            return RedirectToAction("List");
        }

        public ActionResult EditFinal(int id)
        {
            ViewBag.Type = "edit";

            var groups = groupsService.FindAll;
            var form = creditingFormService.FindAll;
            IQueryable<Subjects> subjects = null;
            IQueryable<Users> users = null;

            if (groups != null && groups.ToList().Count() != 0)
            {
                subjects = subjectsService.FindAllSubjectsForGroup(groups.First().id_group);
                users = usersService.FindAllStudentsInGroup(groups.First().id_group);
            }

            if (id == null)
            {
                throw new FinalGradesNotFoundException();
            }

            Final_Grades grade = null;

            try
            {
                grade = finalGradesService.FindById(id);

                FinalGradesViewModel model = new FinalGradesViewModel();
                model.grade = grade;
                model.SelectedGroup = usersService.FindById(grade.id_user).id_group.ToString();
                model.SelectedSubject = grade.id_subject.ToString();
                model.SelectedUser = grade.id_user.ToString();

                ViewBag.Groups = new SelectList(groups, "id_group", "full_name");
                ViewBag.Subjects = new SelectList(subjects, "id_subject", "subject_name");
                ViewBag.Users = new SelectList(users, "id_user", "full_name");
                ViewBag.CreditingForm = new SelectList(form, "id_crediting_form", "name");

                return View("Final/Edit", model);
            }
            catch (FinalGradesNotFoundException ex)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie znaleziono oceny!";
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFinal(FinalGradesViewModel gradesViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Type = "edit";

                string errorString = "";

                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        errorString += error.ErrorMessage + "\n";
                    }
                }
                TempData["Status"] = "invalid";
                TempData["Msg"] = errorString;

                return RedirectToAction("EditFinal", new { id = gradesViewModel.grade.id_final_grade });
            }

            try
            {
                finalGradesService.Edit(gradesViewModel.grade);
                TempData["Status"] = "success";
                TempData["Msg"] = "Aktualizacja oceny przebiegła pomyślnie!";
            }
            catch (Exception e)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się zaktualizować oceny!";
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult DeleteFinal(int id)
        {
            try
            {
                finalGradesService.Delete(id);
                TempData["Status"] = "success";
                TempData["Msg"] = "Pomyślnie usunięto ocenę!";
            }
            catch (Exception e)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się usunąć ocenę!";
            }
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                partialGradesService.Dispose();
                finalGradesService.Dispose();
            }
            base.Dispose(disposing);
        }               
    }
}
