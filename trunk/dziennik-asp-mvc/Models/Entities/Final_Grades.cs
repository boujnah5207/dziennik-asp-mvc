using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dziennik_asp_mvc.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Final_Grades
    {
        [Key]
        public decimal id_final_grade { get; set; }
        public decimal id_crediting_form { get; set; }
        public Nullable<decimal> id_user { get; set; }
        public decimal id_subject { get; set; }
        public decimal grade { get; set; }
        public System.DateTime date { get; set; }
        public string comment { get; set; }

        public virtual Crediting_Form Crediting_Form { get; set; }
        public virtual Subjects Subjects { get; set; }
        public virtual Users Users { get; set; }
    }
}