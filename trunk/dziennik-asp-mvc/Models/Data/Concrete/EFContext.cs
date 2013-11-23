using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dziennik_asp_mvc.Models.Data.Concrete
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using dziennik_asp_mvc.Models.Entities;
    using dziennik_asp_mvc.Models.Data.Abstract;

    public partial class EFContext : DbContext, IUnitOfWork
    {
        public EFContext() : base("name=EFDbContext")
        {
        }

        public virtual DbSet<Crediting_Form> Crediting_Form { get; set; }
        public virtual DbSet<Final_Grades> Final_Grades { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Partial_Grades> Partial_Grades { get; set; }
        public virtual DbSet<dziennik_asp_mvc.Models.Entities.Roles> Roles { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}