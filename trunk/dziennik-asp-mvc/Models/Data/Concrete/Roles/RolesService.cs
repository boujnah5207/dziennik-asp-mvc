using dziennik_asp_mvc.Models.Data.Abstract.Roles;
using System.Linq;

namespace dziennik_asp_mvc.Models.Data.Concrete.Roles
{
    public class RolesService : IRolesService
    {
        private IRolesRepository repo;

        public RolesService(IRolesRepository repo)
        {
            this.repo = repo;
        }

        IQueryable<Entities.Roles> IRolesService.FindAll
        {
           get { return repo.FindAll; }
        }

        public Entities.Roles FindByName(string name)
        {
            return repo.FindByName(name);
        }
    }
}