using System.Data;
using System.Linq;
using dziennik_asp_mvc.Models.Data.Abstract;
using dziennik_asp_mvc.Models.Data.Concrete;
using dziennik_asp_mvc.Models.Entities;
using System.Data.Entity;

namespace dziennik_asp_mvc.Models.Data.Concrete
{
    public class UsersService : IUsersService
    {
        private IUsersRepository repo;

        public UsersService(IUsersRepository repo)
        {
            this.repo = repo;
        }

        public IQueryable<Users> FindAll()
        {
            return repo.FindAll();
        }

        Users IUsersService.FindByName(string name)
        {
            return repo.FindByName(name);
        }

        Users IUsersService.FindById(int id)
        {
            return repo.FindById(id);
        }

    }
}