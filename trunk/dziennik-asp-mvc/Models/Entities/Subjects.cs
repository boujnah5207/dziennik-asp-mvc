using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dziennik_asp_mvc.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Subjects
    {
        public Subjects()
        {
            this.Final_Grades = new HashSet<Final_Grades>();
            this.Partial_Grades = new HashSet<Partial_Grades>();
            this.Groups = new HashSet<Groups>();
        }

        [Key]
        public decimal id_subject { get; set; }
        public decimal id_user { get; set; }
        public string subject_name { get; set; }

        public virtual ICollection<Final_Grades> Final_Grades { get; set; }
        public virtual ICollection<Partial_Grades> Partial_Grades { get; set; }
        public virtual Users Users { get; set; }
        public virtual ICollection<Groups> Groups { get; set; }
    }
}