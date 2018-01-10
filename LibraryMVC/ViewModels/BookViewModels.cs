using LibraryMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryMVC.ViewModels
{
    public class AddBook
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public List<Category> Categories { get; set; }

        public string[] SelectedValues { get; set; }

        public MultiSelectList BookTagsList { get; set; }
    }
}