﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dziennik_asp_mvc.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Crediting_Form
    {
        public Crediting_Form()
        {
            this.Final_Grades = new HashSet<Final_Grades>();
            this.Partial_Grades = new HashSet<Partial_Grades>();
        }

        [Key]
        public decimal id_crediting_form { get; set; }
        public string name { get; set; }

        public virtual ICollection<Final_Grades> Final_Grades { get; set; }
        public virtual ICollection<Partial_Grades> Partial_Grades { get; set; }
    }
}