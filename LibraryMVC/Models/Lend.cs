using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class Lend
    {
        public int Id { get; set; }

        public DateTime DateBorrowed { get; set; }

        public DateTime DateReturn { get; set; }

        public string State { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Book Book { get; set; }
    }
}