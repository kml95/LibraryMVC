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
    public class BookTagController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BookTag
        public ActionResult Index()
        {
            return View(db.BookTags.ToList());
        }

        // GET: BookTag/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTag bookTag = db.BookTags.Find(id);
            if (bookTag == null)
            {
                return HttpNotFound();
            }
            return View(bookTag);
        }

        // GET: BookTag/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookTag/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Text")] BookTag bookTag)
        {
            if (ModelState.IsValid)
            {
                db.BookTags.Add(bookTag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookTag);
        }

        // GET: BookTag/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTag bookTag = db.BookTags.Find(id);
            if (bookTag == null)
            {
                return HttpNotFound();
            }
            return View(bookTag);
        }

        // POST: BookTag/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text")] BookTag bookTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookTag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookTag);
        }

        // GET: BookTag/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTag bookTag = db.BookTags.Find(id);
            if (bookTag == null)
            {
                return HttpNotFound();
            }
            return View(bookTag);
        }

        // POST: BookTag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookTag bookTag = db.BookTags.Find(id);
            db.BookTags.Remove(bookTag);
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
