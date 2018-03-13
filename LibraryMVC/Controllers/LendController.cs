using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryMVC.DAL;
using LibraryMVC.Models;

namespace LibraryMVC.Controllers
{
    [Authorize(Roles ="Employee")]
    public class LendController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Lend
        public ActionResult Index()
        {
            return View(db.Borrows.ToList());
        }

        // GET: Lend/Create
        public ActionResult Create()
        {
            var roleId = db.Roles.Where(r => r.Name == "Reader").FirstOrDefault().Id;

            ViewBag.Readers = new SelectList(db.Users.Where(u => u.Roles.Where(r => r.RoleId == roleId).Any()).ToList(), "Id", "Email");
            ViewBag.Books = new SelectList(db.Books.Where(b => b.Available == true).ToList(), "Id", "Title");
            return View();
        }

        // POST: Lend/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DateBorrowed,DateReturn,State")] Lend lend)
        {
            if (ModelState.IsValid)
            {
                db.Borrows.Add(lend);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lend);
        }

        // GET: Lend/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lend lend = db.Borrows.Find(id);
            if (lend == null)
            {
                return HttpNotFound();
            }
            return View(lend);
        }

        // POST: Lend/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateBorrowed,DateReturn,State")] Lend lend)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lend).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lend);
        }

        // GET: Lend/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lend lend = db.Borrows.Find(id);
            if (lend == null)
            {
                return HttpNotFound();
            }
            return View(lend);
        }

        // POST: Lend/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lend lend = db.Borrows.Find(id);
            lend.Book.Available = true;
            db.Borrows.Remove(lend);
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
