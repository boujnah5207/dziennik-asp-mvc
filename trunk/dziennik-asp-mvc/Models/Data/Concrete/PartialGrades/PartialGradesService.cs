using System.Data;
using System.Linq;
using dziennik_asp_mvc.Models.Data.Abstract;
using dziennik_asp_mvc.Models.Data.Concrete;
using dziennik_asp_mvc.Models.Entities;
using System.Data.Entity;
using dziennik_asp_mvc.Exceptions;

namespace dziennik_asp_mvc.Models.Data.Concrete
{
    public class PartialGradesService : IPartialGradesService
    {
        private IPartialGradesRepository repo;

        public PartialGradesService(IPartialGradesRepository repo)
        {
            this.repo = repo;
        }

        public IQueryable<Partial_Grades> FindAllGradesForSubjectInGroup(int idGroup, int idSubject)
        {
            return repo.FindAllGradesForSubjectInGroup(idGroup, idSubject);
        }

        public IQueryable<Partial_Grades> FindAllGradesForUser(int id)
        {
            return repo.FindAll().Where( m => m.Users.id_user == id);
        }

        Partial_Grades IPartialGradesService.FindById(int id)
        {
            Partial_Grades grade = repo.FindById(id);

            if (grade == null)
            {
                throw new PartialGradesNotFoundException();
            }

            return grade;
        }

        void IPartialGradesService.Add(Partial_Grades grade)
        {
            repo.Add(grade);
            repo.Save();
        }

        void IPartialGradesService.Edit(Partial_Grades grade)
        {
            repo.Edit(grade);
            repo.Save();
        }

        void IPartialGradesService.Delete(int id)
        {
            repo.Delete(id);
            repo.Save();
        }

        void IPartialGradesService.Dispose()
        {
            repo.Dispose();
        }
    }
}