using System.Linq;

namespace dziennik_asp_mvc.Models.Data.Abstract.Roles
{
    public interface IRolesRepository
    {
        IQueryable<dziennik_asp_mvc.Models.Entities.Roles> FindAll { get; }
        dziennik_asp_mvc.Models.Entities.Roles FindByName(string name);

    }
}
