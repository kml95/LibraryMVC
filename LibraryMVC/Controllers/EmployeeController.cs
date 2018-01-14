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
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employee
        public ActionResult Index()
        {
            var roleId = db.Roles.Where(r => r.Name == "Employee").FirstOrDefault().Id;

            var users = db.Users.ToList();
            
            foreach(var user in users)
            {
            }
            
            
            
            
            //.Select(user => new EmployeeViewModels { Id = user.Id, Email = user.Email, Role = "Employee"}).ToList();
            //var huj = db.Users.Where(a => a.Roles.Equals("Employee"));
            return View();
        }
    }
}