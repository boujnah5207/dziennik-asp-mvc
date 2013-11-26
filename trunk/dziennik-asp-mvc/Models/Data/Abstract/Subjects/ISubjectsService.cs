using dziennik_asp_mvc.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik_asp_mvc.Models.Data.Abstract
{
    public interface ISubjectsService
    {
        IQueryable<Subjects> FindAll();
        IQueryable<Subjects> FindAllSubjectsForGroup(int id);
        Subjects FindByName(string name);
        Subjects FindById(int id);
        void Add(Subjects subject);
        void Edit(Subjects subject);
        void Delete(int id);
        void Dispose();
    }
}
