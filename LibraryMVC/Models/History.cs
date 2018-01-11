using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class History
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public ApplicationUser User { get; set; }
    }
}