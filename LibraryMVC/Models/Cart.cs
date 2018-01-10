using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class Cart
    {
        public List<Book> books { get; set; }

        public Cart()
        {
            books = new List<Book>();
        }

        public bool addBook(Book book)
        {
            books.Add(book);

            return true;
        }

        public bool removeBook(Book book)
        {
            books.Remove(book);

            return true;
        }
    }
}