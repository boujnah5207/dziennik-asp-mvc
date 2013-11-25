using System.Data;
using System.Linq;
using dziennik_asp_mvc.Models.Data.Abstract;
using dziennik_asp_mvc.Models.Data.Concrete;
using dziennik_asp_mvc.Models.Entities;
using System.Data.Entity;
using dziennik_asp_mvc.Exceptions;

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

        public IQueryable<Users> FindAllTeachers()
        {
            return repo.FindAll().Where(u => u.Roles.role_name == "Wykładowca");
        }

        public IQueryable<Users> FindAllStudentsInGroup(int id)
        {
            return repo.FindAll().Where(u => u.Roles.role_name == "Student").Where(u => u.Groups.id_group == id);
        }

        Users IUsersService.FindByName(string name)
        {
            Users user = repo.FindByName(name);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return user;
        }

        Users IUsersService.FindById(int id)
        {
            Users user = repo.FindById(id);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return user;
        }

        public void Add(Users users)
        {
            repo.Add(users);
            repo.Save();
        }

        public void Edit(Users users)
        {
            repo.Edit(users);
            repo.Save();
        }

        public void Delete(int id)
        {
            repo.Delete(id);
            repo.Save();
        }

        public void Dispose()
        {
            repo.Dispose();
        }
    }
}