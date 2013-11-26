using dziennik_asp_mvc.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dziennik_asp_mvc.ViewModels
{
    public class StudentsViewModel
    {
        public IEnumerable<Users> Users { get; set; }

        public string SelectedGroup { get; set; } 
    }
}