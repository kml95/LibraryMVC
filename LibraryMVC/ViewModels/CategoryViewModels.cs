using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryMVC.ViewModels
{
    public class IndexCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "From Category")]
        public string ParrentName { get; set; }
    }
}