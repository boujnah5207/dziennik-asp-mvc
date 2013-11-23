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
using PagedList;

namespace dziennik_asp_mvc.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class GroupsController : Controller
    {
        private IGroupsService groupsService;

        public GroupsController(IGroupsService groupsService)
        {
            this.groupsService = groupsService;
        }

        public ActionResult List(int? page, string column = "groupName", string sort = "ASC", string searchString = "", string currentFilter="")
        {
            ViewBag.CurrentColumn = column;
            ViewBag.CurrentSort = sort == "ASC" ? "DESC" : "ASC";

            if (String.IsNullOrEmpty(searchString))
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var groups = groupsService.FindAll;

            if (!String.IsNullOrEmpty(searchString))
            {
                groups = groups.Where(s => s.group_name.ToUpper().Contains(searchString.ToUpper()) || s.numeric_group_name.ToUpper().Contains(searchString.ToUpper()));
            }

            groups = Sort(column, sort, groups);

            int pageSize = 5;
            int pageNumber = (page.HasValue ? page.Value : 1); // Jeśli page == null to page = 1

            return View(groups.ToPagedList(pageNumber, pageSize));
        }

        private static IQueryable<Groups> Sort(string column, string sortOrder, IQueryable<Groups> groups)
        {
            switch (sortOrder)
            {
                case "ASC":
                    switch (column)
                    {
                        case "groupName":
                            groups = groups.OrderBy(s => s.group_name);
                            break;
                        case "numericGroupName":
                            groups = groups.OrderBy(s => s.numeric_group_name);
                            break;
                    }
                    break;
                case "DESC":
                    switch (column)
                    {
                        case "groupName":
                            groups = groups.OrderByDescending(s => s.group_name);
                            break;
                        case "numericGroupName":
                            groups = groups.OrderByDescending(s => s.numeric_group_name);
                            break;
                    }
                    break;
                default:
                    groups = groups.OrderBy(s => s.group_name);
                    break;
            }
            return groups;
        }

        public ActionResult Create()
        {
            ViewBag.Type = "create";

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_group,group_name,numeric_group_name")] Groups groups)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Type = "create";
                return View(groups);
            }

            try
            {
                groupsService.Add(groups);
                TempData["Status"] = "success";
                TempData["Msg"] = "Nowa grupa została dodana!";
            }
            catch (Exception e)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się dodać nowej grupy!";
            }

            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Type = "edit";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groups groups = groupsService.FindById(id);
            if (groups == null)
            {
                return HttpNotFound();
            }
            return View(groups);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_group,group_name,numeric_group_name")] Groups groups)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Type = "create";
                return View(groups);
            }

            try
            {
                groupsService.Edit(groups);
                TempData["Status"] = "success";
                TempData["Msg"] = "Aktualizacja grupy przebiegła pomyślnie!";
            }
            catch (Exception e)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się zaktualizować grupy!";
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                groupsService.Delete(id);
                TempData["Status"] = "success";
                TempData["Msg"] = "Pomyślnie usunięto grupę!";
            }
            catch (Exception e)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się usunąć grupy!";
            }
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                groupsService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
