using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class Queue
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        public Book Book { get; set; }
    }
}