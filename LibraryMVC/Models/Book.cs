using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public bool Available { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<BookTag> BookTags { get; set; }

        public virtual ICollection<Lend> Borrows { get; set; }

        public virtual ICollection<Queue> Queues { get; set; }

    }
}