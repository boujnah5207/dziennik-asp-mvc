using System.Linq;
using dziennik_asp_mvc.Models.Entities;

namespace dziennik_asp_mvc.Models.Data.Abstract
{
    public interface IFinalGradesRepository
    {
        IQueryable<Final_Grades> FindAll { get; }

        Final_Grades FindById(int id);

        void Add(Final_Grades grade);
        void Edit(Final_Grades grade);
        void Delete(int id);

        void Save();
        void Dispose();
    }
}
