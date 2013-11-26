using System.Data;
using System.Linq;
using dziennik_asp_mvc.Models.Data.Abstract;
using dziennik_asp_mvc.Models.Data.Concrete;
using dziennik_asp_mvc.Models.Entities;
using System.Data.Entity;
using dziennik_asp_mvc.Exceptions;

namespace dziennik_asp_mvc.Models.Data.Concrete
{
    public class SubjectsService : ISubjectsService
    {
        private ISubjectsRepository repo;

        public SubjectsService(ISubjectsRepository repo)
        {
            this.repo = repo;
        }

        public IQueryable<Subjects> FindAll()
        {
            return repo.FindAll();
        }

        IQueryable<Subjects> ISubjectsService.FindAllSubjectsForGroup(int id)
        {
            return repo.FindAllSubjectsForGroup(id);
        }

        Subjects ISubjectsService.FindByName(string name)
        {
            Subjects user = repo.FindByName(name);

            if (user == null)
            {
                throw new SubjectNotFoundException();
            }

            return user;
        }

        Subjects ISubjectsService.FindById(int id)
        {
            Subjects user = repo.FindById(id);

            if (user == null)
            {
                throw new SubjectNotFoundException();
            }

            return user;
        }

        public void Add(Subjects subject)
        {
            repo.Add(subject);
            repo.Save();
        }

        public void Edit(Subjects subject)
        {
            repo.Edit(subject);
            repo.Save();
        }

        public void Delete(int id)
        {
            repo.Delete(id);
            repo.Save();
        }

        public void Dispose()
        {
            repo.Dispose();
        }
    }
}