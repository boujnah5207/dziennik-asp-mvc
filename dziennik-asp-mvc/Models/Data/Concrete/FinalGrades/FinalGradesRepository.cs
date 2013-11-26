using System.Data;
using System.Linq;
using dziennik_asp_mvc.Models.Data.Abstract;
using dziennik_asp_mvc.Models.Data.Concrete;
using dziennik_asp_mvc.Models.Entities;
using System.Data.Entity;

namespace dziennik_asp_mvc.Models.Data.Concrete
{
    public class FinalGradesRepository : IFinalGradesRepository
    {
        private readonly EFContext context;

        public FinalGradesRepository(IUnitOfWork unitOfWork)
        {
            context = unitOfWork as EFContext;
        }

        public IQueryable<Final_Grades> FindAll
        {
            get { return from g in context.Final_Grades select g; }
        }


        public Final_Grades FindById(int id)
        {
            return context.Final_Grades.FirstOrDefault(g => g.id_final_grade == id);
        }

        public void Add(Final_Grades grade)
        {
            context.Final_Grades.Add(grade);
        }

        public void Edit(Final_Grades grade)
        {
            context.Entry(grade).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            context.Final_Grades.Remove(FindById(id));
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