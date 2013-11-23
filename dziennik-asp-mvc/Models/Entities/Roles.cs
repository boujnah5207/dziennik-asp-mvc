using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dziennik_asp_mvc.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Roles
    {
        public Roles()
        {
            this.Users = new HashSet<Users>();
        }

        [Key]
        public decimal id_role { get; set; }
        public string role_name { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}