using LibraryMVC.DAL;
using LibraryMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryMVC.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Cart

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
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            
            //TODO  - napisać logikę
            


            //zwrocic dalej liste ksizek
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