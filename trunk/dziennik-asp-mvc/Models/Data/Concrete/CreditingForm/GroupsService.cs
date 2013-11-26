using System.Data;
using System.Linq;
using dziennik_asp_mvc.Models.Data.Abstract;
using dziennik_asp_mvc.Models.Data.Concrete;
using dziennik_asp_mvc.Models.Entities;
using System.Data.Entity;
using dziennik_asp_mvc.Exceptions;

namespace dziennik_asp_mvc.Models.Data.Concrete
{
    public class CreditingFormService : ICreditingFormService
    {
        private ICreditingFormRepository repo;

        public CreditingFormService(ICreditingFormRepository repo)
        {
            this.repo = repo;
        }

        IQueryable<Crediting_Form> ICreditingFormService.FindAll
        {
            get { return repo.FindAll; }
        }

        Crediting_Form ICreditingFormService.FindByName(string name)
        {
            Crediting_Form form = repo.FindByName(name);

            if (form == null)
            {
                throw new CreditingFormNotFoundException();
            }

            return form;
        }

        Crediting_Form ICreditingFormService.FindById(int id)
        {
            Crediting_Form group = repo.FindById(id);

            if (group == null)
            {
                throw new CreditingFormNotFoundException();
            }

            return group;
        }

        void ICreditingFormService.Add(Crediting_Form form)
        {
            repo.Add(form);
            repo.Save();
        }

        void ICreditingFormService.Edit(Crediting_Form form)
        {
            repo.Edit(form);
            repo.Save();
        }

        void ICreditingFormService.Delete(int id)
        {
            repo.Delete(id);
            repo.Save();
        }

        void ICreditingFormService.Dispose()
        {
            repo.Dispose();
        }
    }
}