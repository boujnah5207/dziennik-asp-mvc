using System.Data;
using System.Linq;
using dziennik_asp_mvc.Models.Data.Abstract;
using dziennik_asp_mvc.Models.Data.Concrete;
using dziennik_asp_mvc.Models.Entities;
using System.Data.Entity;
using dziennik_asp_mvc.Exceptions;

namespace dziennik_asp_mvc.Models.Data.Concrete
{
    public class FinalGradesService : IFinalGradesService
    {
        private IFinalGradesRepository repo;

        public FinalGradesService(IFinalGradesRepository repo)
        {
            this.repo = repo;
        }

        Final_Grades IFinalGradesService.FindById(int id)
        {
            Final_Grades grade = repo.FindById(id);

            if (grade == null)
            {
                throw new FinalGradesNotFoundException();
            }

            return grade;
        }

        void IFinalGradesService.Add(Final_Grades grade)
        {
            repo.Add(grade);
            repo.Save();
        }

        void IFinalGradesService.Edit(Final_Grades grade)
        {
            repo.Edit(grade);
            repo.Save();
        }

        void IFinalGradesService.Delete(int id)
        {
            repo.Delete(id);
            repo.Save();
        }

        void IFinalGradesService.Dispose()
        {
            repo.Dispose();
        }
    }
}