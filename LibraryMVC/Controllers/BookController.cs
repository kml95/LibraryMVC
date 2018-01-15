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

        [HttpGet]
        public ActionResult SearchCategory()
        {
            ViewBag.MainCategories = new SelectList(db.Categories, "Id", "Name");
            if (TempData["List"] != null)
            {
                return View(TempData["List"]);
            }
            var model = db.Books.ToList();
             return View(model);
        }

        [HttpPost]
        public ActionResult SearchCategory(int categories)
        {
            List<Book> ListBook = new List<Book>();

            var model = db.Books.Where(a => a.Category.Id == categories).FirstOrDefault();

            ListBook = FindBookRootID(categories, ListBook);

            ListBook.Add(model);
            TempData["List"] = ListBook;
            return RedirectToAction("SearchCategory");
        }

        public List<Book> FindBookRootID(int rootId, List<Book> ListBook)
        {
            var model = db.Books.Where(a => a.Category.RootCategoryId == rootId).FirstOrDefault();
            if (model != null)
            {
                ListBook.Add(model);
                return FindBookRootID(model.Category.Id, ListBook);
            }

                return ListBook;
            
        }

        [HttpGet]
        public ActionResult SearchByTag()
        {
            ViewBag.Tags = new SelectList(db.BookTags, "Id", "Text");
            if (TempData["BookList"] != null)
            {
                return View(TempData["BookList"]);
            }
            var model = db.Books.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult SearchByTag(int BookTags)
        {
            var model = db.BookTags.Where(a => a.Id == BookTags).First();
            List<Book> ListBook = new List<Book>();
            foreach(Book b in db.Books)
            {
                foreach (BookTag bookTag in b.BookTags)
                {
                    if (bookTag.Id == BookTags)
                    {
                        ListBook.Add(b);
                    }
                }
            }

            TempData["BookList"] = ListBook;
            return RedirectToAction("SearchByTag");
        }



    }
}
