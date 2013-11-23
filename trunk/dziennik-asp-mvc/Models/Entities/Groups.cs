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

    public partial class Groups
    {
        public Groups()
        {
            this.Users = new HashSet<Users>();
            this.Subjects = new HashSet<Subjects>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal id_group { get; set; }

        [StringLength(50)]
        [Display(Name = "Nazwa grupy")]
        [Required(ErrorMessage = "Pole nie może być puste!")]
        public string group_name { get; set; }
        
        [StringLength(50)]
        [Display(Name = "Identyfikator grupy")]
        public string numeric_group_name { get; set; }

        public virtual ICollection<Users> Users { get; set; }
        public virtual ICollection<Subjects> Subjects { get; set; }
    }
}