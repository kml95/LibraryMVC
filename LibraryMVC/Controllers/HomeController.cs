using LibraryMVC.DAL;
using LibraryMVC.Models;
using LibraryMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryMVC.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            IEnumerable<New> news = new List<New>();
            IEnumerable<Book> books = new List<Book>();
            news = db.News.OrderByDescending(n => n.Id).Take(3).ToList();
            books = db.Books.OrderByDescending(n => n.Id).Take(3).ToList();

            var model = db.News.Select(s => s.)
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}