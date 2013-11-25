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

    public partial class Subjects
    {
        public Subjects()
        {
            this.Final_Grades = new HashSet<Final_Grades>();
            this.Partial_Grades = new HashSet<Partial_Grades>();
            this.Groups = new HashSet<Groups>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id_subject { get; set; }

        [Display(Name = "Wykładowca")]
        public int id_user { get; set; }

        [StringLength(50)]
        [Display(Name = "Nazwa przedmiotu")]
        [Required(ErrorMessage = "Pole nie może być puste!")]
        public string subject_name { get; set; }

        public virtual ICollection<Final_Grades> Final_Grades { get; set; }
        public virtual ICollection<Partial_Grades> Partial_Grades { get; set; }
        public virtual Users Users { get; set; }
        public virtual ICollection<Groups> Groups { get; set; }
    }
}