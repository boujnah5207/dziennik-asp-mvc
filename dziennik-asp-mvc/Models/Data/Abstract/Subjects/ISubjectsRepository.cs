using System.Linq;
using dziennik_asp_mvc.Models.Entities;

namespace dziennik_asp_mvc.Models.Data.Abstract
{
    public interface ISubjectsRepository
    {
        IQueryable<Subjects> FindAll();
        IQueryable<Subjects> FindAllSubjectsForGroup(int id);
        Subjects FindByName(string name);
        Subjects FindById(int id);

        void Add(Subjects subject);
        void Edit(Subjects subject);
        void Delete(int id);

        void Save();
        void Dispose();
    }
}
