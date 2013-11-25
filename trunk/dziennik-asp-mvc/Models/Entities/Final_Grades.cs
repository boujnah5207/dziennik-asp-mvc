using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dziennik_asp_mvc.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Final_Grades
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id_final_grade { get; set; }
        public int id_crediting_form { get; set; }
        public int id_user { get; set; }
        public int id_subject { get; set; }

        [Display(Name = "Ocena")]
        public decimal grade { get; set; }

        public String date { get; set; }

        [Display(Name = "Uwagi")]
        public string comment { get; set; }

        [NotMapped]
        public string description
        {
            get { return "Ocena: " + this.grade + "\nData: " + this.date +"\nUwagi: " + this.comment; }
        }
        public virtual Crediting_Form Crediting_Form { get; set; }
        public virtual Subjects Subjects { get; set; }
        public virtual Users Users { get; set; }
    }
}