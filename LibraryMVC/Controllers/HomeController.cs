using LibraryMVC.DAL;
using LibraryMVC.Models;
using LibraryMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LibraryMVC.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            List<New> news = new List<New>();
            List<Book> books = new List<Book>();
            news = db.News.OrderByDescending(n => n.Id).Take(3).ToList();
            books = db.Books.OrderByDescending(n => n.Id).Take(3).ToList();
            var model = new HomeViewModel { News = news, Books = books };
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Contact")]
        public async Task<ActionResult> Contact_Post()
        {
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress("8special20140@proprice.co"));  // replace with valid value 
            message.From = new MailAddress("mailtomvc@gmail.com");  // replace with valid value
            message.Subject = "Your email subject";
            message.Body = string.Format(body, "Admin", "mailtomvc@gmail.com", "Ty łachu działa");
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                await smtp.SendMailAsync(message);
                return RedirectToAction("Sent");
            }

        }

        public ActionResult Sent()
        {
            return View();
        }

    }
}