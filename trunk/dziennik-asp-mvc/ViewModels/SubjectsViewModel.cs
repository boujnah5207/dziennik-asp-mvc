using dziennik_asp_mvc.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dziennik_asp_mvc.ViewModels
{
    public class SubjectsViewModel
    {
        public Subjects subject { get; set; }
        public string SelectedUser { get; set; }
        public string[] SelectedGroups { get; set; } 
    }
}