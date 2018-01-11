using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class Cart
    {
        public List<Book> Books { get; set; }

        public Cart()
        {
            Books = new List<Book>();
        }

        public bool AddBook(Book book)
        {
            Books.Add(book);

            return true;
        }

        public bool RemoveBook(Book book)
        {
            Books.Remove(book);

            return true;
        }
    }
}