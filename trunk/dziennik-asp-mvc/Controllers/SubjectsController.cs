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
using PagedList;
using dziennik_asp_mvc.ViewModels;
using dziennik_asp_mvc.Exceptions;

namespace dziennik_asp_mvc.Controllers
{
    [Authorize(Roles = "Administrator,Wykładowca,Student")]
    public class SubjectsController : Controller
    {
        private IUsersService usersService;
        private IGroupsService groupsService;
        private ISubjectsService subjectService;

        public SubjectsController(IUsersService usersService, IGroupsService groupsService, ISubjectsService subjectService)
        {
            this.usersService = usersService;
            this.groupsService = groupsService;
            this.subjectService = subjectService;
        }

        public ActionResult List(int? page, string column = "subjectName", string sort = "ASC", string searchString = "", string currentFilter = "")
        {
            ViewBag.CurrentColumn = column;
            ViewBag.CurrentSort = sort == "ASC" ? "DESC" : "ASC";

            if (String.IsNullOrEmpty(searchString))
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var subjects = subjectService.FindAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                subjects = subjects.Where(s => s.subject_name.ToUpper().Contains(searchString.ToUpper())
                    || s.Users.first_name.ToUpper().Contains(searchString.ToUpper())
                    || s.Users.last_name.ToUpper().Contains(searchString.ToUpper()));
            }

            subjects = Sort(column, sort, subjects);

            int pageSize = 5;
            int pageNumber = (page.HasValue ? page.Value : 1); // Jeśli page == null to page = 1

            return View(subjects.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            ViewBag.Type = "create";
            ViewBag.Teachers = new SelectList(usersService.FindAllTeachers(), "id_user", "full_name");
            ViewBag.Groups = new SelectList(groupsService.FindAll, "id_group", "full_name");

            return View(new SubjectsViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubjectsViewModel subjectsViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Type = "create";
                return View(subjectsViewModel);
            }

            try
            {
                List<Groups> groups = new List<Groups>();

                foreach (string value in subjectsViewModel.SelectedGroups.ToList())
                {
                    groups.Add(groupsService.FindById(Convert.ToInt32(value)));
                }

                Subjects subject = subjectsViewModel.subject;
                subject.id_user = Convert.ToInt32(subjectsViewModel.SelectedUser);
                subject.Groups = groups;

                subjectService.Add(subject);
                TempData["Status"] = "success";
                TempData["Msg"] = "Nowy przedmiot został dodany!";
            }
            catch (Exception e)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się dodać nowego przedmiotu!";
            }
            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Type = "edit";

            if (id == null)
            {
                throw new SubjectNotFoundException();
            }

            Subjects subject = null;

            try
            {
                ViewBag.Teachers = new SelectList(usersService.FindAllTeachers(), "id_user", "full_name");
                ViewBag.Groups = new SelectList(groupsService.FindAll, "id_group", "full_name");

                subject = subjectService.FindById(id);

                List<string> selectedGroups = new List<string>();

                foreach (Groups group in subject.Groups)
                {
                    selectedGroups.Add(Convert.ToString(group.id_group));
                }

                SubjectsViewModel viewModel = new SubjectsViewModel();
                viewModel.subject = subject;
                viewModel.SelectedUser = Convert.ToString(subject.id_user);
                viewModel.SelectedGroups = selectedGroups.ToArray();

                return View(viewModel);
            }
            catch (SubjectNotFoundException ex)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie znaleziono przedmiotu!";
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubjectsViewModel subjectsViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Type = "edit";
                return View(subjectsViewModel);
            }

            try
            {
                Subjects subjectFromRepo = subjectService.FindById(subjectsViewModel.subject.id_subject);
                subjectFromRepo.id_user = Convert.ToInt32(subjectsViewModel.SelectedUser);
                subjectFromRepo.subject_name = subjectsViewModel.subject.subject_name;

                if (subjectsViewModel.SelectedGroups == null || subjectsViewModel.SelectedGroups.Length == 0)
                {
                    foreach (Groups group in subjectFromRepo.Groups.ToList())
                    {
                        subjectFromRepo.Groups.Remove(group);
                    }
                }
                else {
                    subjectFromRepo.Groups.Clear();
                    foreach (string value in subjectsViewModel.SelectedGroups.ToList())
                    {
                        Groups group = groupsService.FindById(Convert.ToInt32(value));
                        subjectFromRepo.Groups.Add(group);
                    }
                }
                subjectService.Edit(subjectFromRepo);
                TempData["Status"] = "success";
                TempData["Msg"] = "Aktualizacja przemdiotu przebiegła pomyślnie!";
            }
            catch (Exception e)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się zaktualizować przedmiotu!";
            }
            return RedirectToAction("List");
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                subjectService.Delete(id);
                TempData["Status"] = "success";
                TempData["Msg"] = "Pomyślnie usunięto przedmiot!";
            }
            catch (Exception e)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się usunąć przedmiotu!";
            }
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                subjectService.Dispose();
            }
            base.Dispose(disposing);
        }

        private static IQueryable<Subjects> Sort(string column, string sortOrder, IQueryable<Subjects> subjects)
        {
            switch (sortOrder)
            {
                case "ASC":
                    switch (column)
                    {
                        case "subjectName":
                            subjects = subjects.OrderBy(s => s.subject_name);
                            break;
                        case "fullName":
                            subjects = subjects.OrderBy(s => s.Users.first_name).ThenBy(s => s.Users.last_name);
                            break;
                    }
                    break;
                case "DESC":
                    switch (column)
                    {
                        case "subjectName":
                            subjects = subjects.OrderByDescending(s => s.subject_name);
                            break;
                        case "fullName":
                            subjects = subjects.OrderByDescending(s => s.Users.first_name).ThenBy(s => s.Users.last_name);
                            break;
                    }
                    break;
                default:
                    subjects = subjects.OrderBy(s => s.subject_name);
                    break;
            }
            return subjects;
        }
    }
}
