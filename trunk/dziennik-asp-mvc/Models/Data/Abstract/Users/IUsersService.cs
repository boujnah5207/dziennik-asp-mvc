using dziennik_asp_mvc.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik_asp_mvc.Models.Data.Abstract
{
    public interface IUsersService
    {
        IQueryable<Users> FindAll();
        IQueryable<Users> FindAllTeachers();
        IQueryable<Users> FindAllStudentsInGroup(int id);
        Users FindByName(string name);
        Users FindById(int id);
        void Add(Users users);
        void Edit(Users users);
        void Delete(int id);
        void Dispose();
    }
}
