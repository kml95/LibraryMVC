using LibraryMVC.DAL;
using LibraryMVC.Models;
using LibraryMVC.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LibraryMVC.Controllers
{
    [Authorize(Roles = "Employee")]
    public class ReaderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        protected UserManager<ApplicationUser> UserManager;

        public ReaderController()
        {
            this.db = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
        }

        // GET: Employee
        public ActionResult Index()
        {
            var roleId = db.Roles.Where(r => r.Name == "Reader").FirstOrDefault().Id;

            var model = db.Users.Where(r => r.Roles.Where(a => a.RoleId == roleId).Any()).Select(u => new EmployeeViewModels { Id = u.Id, Email = u.Email, Role = "Reader" }).ToList();

            return View(model);
        }


        public ActionResult Delete(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeViewModels user = db.Users.Where(u => u.Id == id).Select(u => new EmployeeViewModels { Id = u.Id, Email = u.Email, Role = "Reader" }).First();

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeViewModels user = db.Users.Where(u => u.Id == id).Select(u => new EmployeeViewModels { Id = u.Id, Email = u.Email, Role = "Reader" }).First();
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeViewModels user)
        {
            if (ModelState.IsValid)
            {
                var editedUser = db.Users.Where(u => u.Id == user.Id).First();
                editedUser.Email = user.Email;

                db.Entry(editedUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            EmployeeViewModels model = db.Users.Where(u => u.Id == user.Id).Select(u => new EmployeeViewModels { Id = u.Id, Email = u.Email, Role = "Reader" }).First();
            return View(model);
        }
    }
}