using LibraryMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMVC.ViewModels
{
    public class HomeViewModel
    {
        public List<New> News { get; set; }
        public List<Book> Books { get; set; }
    }
}