using System.Data;
using System.Linq;
using dziennik_asp_mvc.Models.Data.Abstract;
using dziennik_asp_mvc.Models.Data.Concrete;
using dziennik_asp_mvc.Models.Entities;
using System.Data.Entity;

namespace dziennik_asp_mvc.Models.Data.Concrete
{
    public class PartialGradesRepository : IPartialGradesRepository
    {
        private readonly EFContext context;

        public PartialGradesRepository(IUnitOfWork unitOfWork)
        {
            context = unitOfWork as EFContext;
        }

        public IQueryable<Partial_Grades> FindAll()
        {
             return from g in context.Partial_Grades select g;
        }

        IQueryable<Partial_Grades> IPartialGradesRepository.FindAllGradesForSubjectInGroup(int idGroup, int idSubject)
        {
            return from grade in context.Partial_Grades
                   where grade.id_subject == idSubject
                   join groups in context.Groups
                      on idGroup equals groups.id_group
                   select grade;
        }
        
        public Partial_Grades FindById(int id)
        {
            return context.Partial_Grades.FirstOrDefault(g => g.id_grade == id);
        }

        public void Add(Partial_Grades grade)
        {
            context.Partial_Grades.Add(grade);
        }

        public void Edit(Partial_Grades grade)
        {
            context.Entry(grade).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            context.Partial_Grades.Remove(FindById(id));
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