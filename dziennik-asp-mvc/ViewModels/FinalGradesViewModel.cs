using dziennik_asp_mvc.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dziennik_asp_mvc.ViewModels
{
    public class FinalGradesViewModel
    {
        public Final_Grades grade { get; set; }
        public string SelectedUser { get; set; }
        public string SelectedGroup { get; set; }
        public string SelectedSubject { get; set; } 
    }
}