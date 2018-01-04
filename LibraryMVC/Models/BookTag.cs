using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class BookTag
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}