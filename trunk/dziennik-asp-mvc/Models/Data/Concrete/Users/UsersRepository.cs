using System.Data;
using System.Linq;
using dziennik_asp_mvc.Models.Data.Abstract;
using dziennik_asp_mvc.Models.Data.Concrete;
using dziennik_asp_mvc.Models.Entities;
using System.Data.Entity;

namespace dziennik_asp_mvc.Models.Data.Concrete
{
    public class UsersRepository : IUsersRepository
    {
        private readonly EFContext context;

        public UsersRepository(IUnitOfWork unitOfWork)
        {
            context = unitOfWork as EFContext;
        }

        public IQueryable<Users> FindAll()
        {
            return from user in context.Users select user;
        }

        public Users FindByName(string name)
        {
            return context.Users.FirstOrDefault(u => u.login == name);
        }

        public Users FindById(int id)
        {
            return context.Users.FirstOrDefault(u => u.id_user == id);
        }

        public void Add(Users user)
        {
            context.Users.Add(user);
        }

        public void Edit(Users user)
        {
            context.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            context.Users.Remove(FindById(id));
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}