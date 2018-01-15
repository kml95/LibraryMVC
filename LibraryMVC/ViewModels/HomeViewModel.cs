using LibraryMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMVC.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<New> News { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}