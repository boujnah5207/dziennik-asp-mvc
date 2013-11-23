using dziennik_asp_mvc.Models.Data.Abstract;
using System.Linq;

namespace dziennik_asp_mvc.Models.Data.Concrete.Roles
{
    public class RolesRepository
    {
        private readonly EFContext context;

        public RolesRepository(IUnitOfWork unitOfWork)
        {
            context = unitOfWork as EFContext;
        }

        public IQueryable<dziennik_asp_mvc.Models.Entities.Roles> FindAll
        {
            get { return from r in context.Roles select r; }
        }

        public dziennik_asp_mvc.Models.Entities.Roles FindByName(string name)
        {
            return context.Roles.FirstOrDefault(u => u.role_name == name);
        }
    }
}