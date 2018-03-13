using LibraryMVC.DAL;
using LibraryMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LibraryMVC.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> userManager;

        // GET: Cart
        public CartController()
        {
            this.db = new ApplicationDbContext();
            this.userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddToCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            var book = db.Books.Where(a => a.Id == id).First();
            cart.AddBook(book);

            //zwrocic dalej liste ksizek
            return RedirectToAction("ShowCart");
        }

        public ActionResult DeleteFromCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            var b = cart.Books.Where(a => a.Id == id).First();
            //var book = db.Books.Where(a => a.Id == id).First();
            cart.RemoveBook(b);

            //zwrocic dalej liste ksizek
            return RedirectToAction("ShowCart");
        }

        public ActionResult ClearCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            cart.Books.Clear();

            //zwrocic dalej liste ksizek
            return RedirectToAction("ShowCart");
        }

        public ActionResult LendBooks()
        {
            var userModel = userManager.FindById(User.Identity.GetUserId());

            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            else
            {
                foreach (Book b in cart.Books)
                {
                    Book book = db.Books.Where(a => a.Id == b.Id).First();
                    book.Available = false;
                    Lend LendBook = new Lend();
                    LendBook.DateBorrowed = DateTime.Now;
                    LendBook.DateReturn = DateTime.Now.AddDays(7);
                    LendBook.User = userModel;
                    LendBook.Book = book;
                    LendBook.State = "Borrowed";
                    book.Borrows.Add(LendBook);
                    
                }
                TempData["Success"] = "Added Successfully!";
                db.SaveChanges();

            }

            
            return RedirectToAction("ShowCart");
        }

        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
            {
                //pusta lista 
            }
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
    }
}