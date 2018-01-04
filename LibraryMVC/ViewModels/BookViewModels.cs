using LibraryMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMVC.ViewModels
{
    public class AddBook
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }
        
        public string[] Tags { get; set; }

        public List<Category> Categories { get; set; }
    }
}