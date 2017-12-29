using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? RootCategoryId { get; set; }

        public virtual Category RootCategory { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}