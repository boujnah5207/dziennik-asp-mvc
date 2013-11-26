using System.Linq;
using dziennik_asp_mvc.Models.Entities;

namespace dziennik_asp_mvc.Models.Data.Abstract
{
    public interface IPartialGradesRepository
    {
        IQueryable<Partial_Grades> FindAll();

        IQueryable<Partial_Grades> FindAllGradesForSubjectInGroup(int idGroup, int idSubject);

        Partial_Grades FindById(int id);
        void Add(Partial_Grades grade);
        void Edit(Partial_Grades grade);
        void Delete(int id);

        void Save();
        void Dispose();
    }
}
