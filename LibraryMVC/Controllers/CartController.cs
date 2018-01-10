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

        public ActionResult AddToChart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            var book = db.Books.Where(a => a.Id == id).First();
            cart.addBook(book);

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