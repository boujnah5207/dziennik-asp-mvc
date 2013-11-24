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

namespace dziennik_asp_mvc.Controllers
{
    public class StudentsController : Controller
    {
        private EFContext db = new EFContext();

        // GET: /Students/
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Groups).Include(u => u.Roles);
            return View(users.ToList());
        }

        // GET: /Students/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: /Students/Create
        public ActionResult Create()
        {
            ViewBag.id_group = new SelectList(db.Groups, "id_group", "group_name");
            ViewBag.id_role = new SelectList(db.Roles, "id_role", "role_name");
            return View();
        }

        // POST: /Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id_user,id_role,id_group,login,password,first_name,last_name,email,status,album_number")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_group = new SelectList(db.Groups, "id_group", "group_name", users.id_group);
            ViewBag.id_role = new SelectList(db.Roles, "id_role", "role_name", users.id_role);
            return View(users);
        }

        // GET: /Students/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_group = new SelectList(db.Groups, "id_group", "group_name", users.id_group);
            ViewBag.id_role = new SelectList(db.Roles, "id_role", "role_name", users.id_role);
            return View(users);
        }

        // POST: /Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id_user,id_role,id_group,login,password,first_name,last_name,email,status,album_number")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_group = new SelectList(db.Groups, "id_group", "group_name", users.id_group);
            ViewBag.id_role = new SelectList(db.Roles, "id_role", "role_name", users.id_role);
            return View(users);
        }

        // GET: /Students/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: /Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
