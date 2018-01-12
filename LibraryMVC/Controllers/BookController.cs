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
    [Authorize(Roles = "Reader")]
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
        public ActionResult Index()
        {
            return View(db.Books.ToList());
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
        
        public ActionResult SaveHisotry()
        {
            return View();
        }



    }
}
