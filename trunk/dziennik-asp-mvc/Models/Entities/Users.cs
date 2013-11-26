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
    using DataAnnotationsExtensions;

    public partial class Users
    {
        public Users()
        {
            this.Final_Grades = new HashSet<Final_Grades>();
            this.Partial_Grades = new HashSet<Partial_Grades>();
            this.Subjects = new HashSet<Subjects>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Wykładowca")]
        public int id_user { get; set; }
        public int id_role { get; set; }
        public Nullable<int> id_group { get; set; }

        [StringLength(25)]
        [Display(Name = "Nazwa użytkownika")]
        [Required(ErrorMessage = "Pole nie może być puste!")]
        public string login { get; set; }

        [StringLength(255)]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Pole nie może być puste!")]
        public string password { get; set; }

        [StringLength(30)]
        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Pole nie może być puste!")]
        public string first_name { get; set; }

        [StringLength(30)]
        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Pole nie może być puste!")]
        public string last_name { get; set; }

        [StringLength(30)]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Pole nie może być puste!")]
        public string email { get; set; }
        [Display(Name = "Status konta")]
        public bool status { get; set; }

        [Range(1, 100000, ErrorMessage = "Nie wprowadzono poprawnego numeru albumu!")]
        [Display(Name = "Numer albumu")]
        public Nullable<int> album_number { get; set; }

        [NotMapped]
        public string full_name
        {
            get { return this.first_name +" " + last_name; }
        }

        public virtual ICollection<Final_Grades> Final_Grades { get; set; }
        public virtual Groups Groups { get; set; }
        public virtual ICollection<Partial_Grades> Partial_Grades { get; set; }
        public virtual Roles Roles { get; set; }
        public virtual ICollection<Subjects> Subjects { get; set; }
    }
}