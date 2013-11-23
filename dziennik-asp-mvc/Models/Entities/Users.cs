using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dziennik_asp_mvc.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Users
    {
        public Users()
        {
            this.Final_Grades = new HashSet<Final_Grades>();
            this.Partial_Grades = new HashSet<Partial_Grades>();
            this.Subjects = new HashSet<Subjects>();
        }

        [Key]
        public decimal id_user { get; set; }
        public decimal id_role { get; set; }
        public Nullable<decimal> id_group { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public bool status { get; set; }
        public Nullable<decimal> album_number { get; set; }

        public virtual ICollection<Final_Grades> Final_Grades { get; set; }
        public virtual Groups Groups { get; set; }
        public virtual ICollection<Partial_Grades> Partial_Grades { get; set; }
        public virtual Roles Roles { get; set; }
        public virtual ICollection<Subjects> Subjects { get; set; }
    }
}