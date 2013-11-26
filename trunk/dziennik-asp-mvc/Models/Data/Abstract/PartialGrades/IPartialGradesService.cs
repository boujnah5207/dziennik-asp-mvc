using dziennik_asp_mvc.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik_asp_mvc.Models.Data.Abstract
{
    public interface IPartialGradesService
    {
        IQueryable<Partial_Grades> FindAllGradesForSubjectInGroup(int idGroup, int idSubject);
        IQueryable<Partial_Grades> FindAllGradesForUser(int id);
        Partial_Grades FindById(int id);
        void Add(Partial_Grades grade);
        void Edit(Partial_Grades grade);
        void Delete(int id);
        void Dispose();
    }
}
