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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LibraryMVC.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class BookManageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        protected UserManager<ApplicationUser> UserManager;

        public BookManageController()
        {
            this.db = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
        }

        // GET: BookManage
        public ActionResult Index()
        {
            return View(db.Books.ToList());
        }
    
        // GET: BookManage/Create
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(db.Categories, "Id", "Name");
            ViewBag.BookTags = new MultiSelectList(db.BookTags, "Id", "Text");
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Author,ISBN")] Book book, int categories)
        {
            if (ModelState.IsValid)
            {
                var model = db.Categories.Where(a => a.Id == categories).FirstOrDefault();
                book.Category = model;
                book.Available = true;
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: BookManage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: BookManage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Author,ISBN,Available")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: BookManage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: BookManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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

        public ActionResult Filters(string title, string author, string isbn)
        {
            var model = db.Books.AsQueryable();
            if (!string.IsNullOrEmpty(title))
            {
                model = model.Where(a => a.Title.ToLower().Equals(title.ToLower()));
            }
            if (!string.IsNullOrEmpty(author))
            {
                model = model.Where(a => a.Author.ToLower().Equals(author.ToLower()));
            }
            if (!string.IsNullOrEmpty(isbn))
            {
                model = model.Where(a => a.ISBN.ToLower().Equals(isbn.ToLower()));
            }


            return View("Search", model.ToList());
        }

        [HttpGet]
        public ActionResult Search(List<Book> SearchList)
        {
            return View(SearchList);
        }
        [HttpGet]
        public ActionResult BookTag(int id)
        {
            ViewBag.Tags = new SelectList(db.BookTags, "Id", "Text");
            TempData["Bookid"] = id;
            var model = db.Books.Where(a => a.Id == id).First();
            return View(model);
        }
        [HttpPost]
        public ActionResult BoogTag(int Tags, int bookId)
        {
            var model = db.Books.Where(a => a.Id == bookId).First();
            var tag = db.BookTags.Where(a => a.Id == Tags).First();
            model.BookTags.Add(tag);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }

    }
}
