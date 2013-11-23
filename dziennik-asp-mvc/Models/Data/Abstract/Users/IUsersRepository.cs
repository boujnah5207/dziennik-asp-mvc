using System.Linq;
using dziennik_asp_mvc.Models.Entities;

namespace dziennik_asp_mvc.Models.Data.Abstract
{
    public interface IUsersRepository
    {
        IQueryable<Users> FindAll();
        Users FindByName(string name);
        Users FindById(int id);
    }
}
