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
using dziennik_asp_mvc.Exceptions;

namespace dziennik_asp_mvc.Controllers
{
    [Authorize(Roles = "Administrator,Wykładowca")]
    public class CreditingFormController : Controller
    {
        private ICreditingFormService creditingService;

        public CreditingFormController(ICreditingFormService creditingService)
        {
            this.creditingService = creditingService;
        }

        public ActionResult List()
        {         
            var forms = creditingService.FindAll;      
            return View(forms);
        }
        
        public ActionResult Create()
        {
            ViewBag.Type = "create";

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_crediting_form,name")] Crediting_Form form)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Type = "create";
                return View(form);
            }

            try
            {
                creditingService.Add(form);
                TempData["Status"] = "success";
                TempData["Msg"] = "Nowa forma zaliczenia została dodana!";
            }
            catch (Exception e)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się dodać nowej formy zaliczenia!";
            }

            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Type = "edit";
            if (id == null)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie odnaleziono takiej formy zaliczenia!";
                return View();
            }

            Crediting_Form forms = null;
            try
            {
                forms = creditingService.FindById(id);
            }
            catch (CreditingFormNotFoundException ex)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie odnaleziono takiej formy zaliczenia!";
            }
            return View(forms);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_crediting_form,name")] Crediting_Form forms)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Type = "edit";
                return View(forms);
            }

            try
            {
                creditingService.Edit(forms);
                TempData["Status"] = "success";
                TempData["Msg"] = "Aktualizacja formy zaliczenia przebiegła pomyślnie!";
            }
            catch (Exception e)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się zaktualizować formy zaliczenia!";
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                creditingService.Delete(id);
                TempData["Status"] = "success";
                TempData["Msg"] = "Pomyślnie usunięto formę zaliczenia!";
            }
            catch (Exception e)
            {
                TempData["Status"] = "invalid";
                TempData["Msg"] = "Nie udało się usunąć formy zaliczenia!";
            }
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                creditingService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}