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
using LibraryMVC.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LibraryMVC.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext db;
        protected UserManager<ApplicationUser> UserManager;


        public BookController()
        {
            this.db = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
        }

        // GET: Book
        [Authorize(Roles = "Admin, Employee")]
        public ActionResult Index()
        {
            return View(db.Books.ToList());
        }

        // GET: Book/Details/5
        [Authorize(Roles = "Admin, Employee")]
        public ActionResult Details(int? id)
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

        // GET: Book/Create
        [Authorize(Roles = "Admin, Employee")]
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(db.Categories, "Id", "Name");
            ViewBag.BookTags = new MultiSelectList(db.BookTags, "Id", "Text");
            return View();
        }

        public ActionResult Create2()
        {
            var model = new AddBook();
            model.Categories = db.Categories.ToList();
            model.BookTagsList = new MultiSelectList(db.BookTags.ToList().OrderBy(i => i.Text), "Id", "Text");
            return View(model);
        }

        // POST: Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Employee")]
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

        // GET: Book/Edit/5
        [Authorize(Roles = "Admin, Employee")]
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

        // POST: Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Employee")]
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

        // GET: Book/Delete/5
        [Authorize(Roles = "Admin, Employee")]
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

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Employee")]
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

        //[HttpPost]
        [Authorize(Roles = "Reader")]
        public ActionResult AddToQueue(int id)
        {
            Book book = db.Books.Where(b => b.Id == id).FirstOrDefault();

            var user = UserManager.FindById(User.Identity.GetUserId());

            var queue = new Queue();
            queue.Book = book;
            queue.User = user;

            book.Queues.Add(queue);
            db.SaveChanges();


            TempData["Success"] = "Added Successfully!";
            return RedirectToAction("Index");
        }
    }
}
