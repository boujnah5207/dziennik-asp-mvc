using System.Data;
using System.Linq;
using dziennik_asp_mvc.Models.Data.Abstract;
using dziennik_asp_mvc.Models.Data.Concrete;
using dziennik_asp_mvc.Models.Entities;
using System.Data.Entity;

namespace dziennik_asp_mvc.Models.Data.Concrete
{
    public class SubjectsRepository : ISubjectsRepository
    {
        private readonly EFContext context;

        public SubjectsRepository(IUnitOfWork unitOfWork)
        {
            context = unitOfWork as EFContext;
        }

        public IQueryable<Subjects> FindAll()
        {
            return from subject in context.Subjects select subject;
        }

        public IQueryable<Subjects> FindAllSubjectsForGroup(int id) {
            return from subject in context.Subjects
                   where context.Groups.All(requiredId => subject.Groups.Any(groups=> groups.id_group == id))
                   select subject;
        }

        public Subjects FindByName(string name)
        {
            return context.Subjects.FirstOrDefault(u => u.subject_name == name);
        }

        public Subjects FindById(int id)
        {
            return context.Subjects.FirstOrDefault(u => u.id_subject == id);
        }

        public void Add(Subjects subject)
        {
            context.Subjects.Add(subject);
        }

        public void Edit(Subjects subject)
        {
            context.Entry(subject).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            context.Subjects.Remove(FindById(id));
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