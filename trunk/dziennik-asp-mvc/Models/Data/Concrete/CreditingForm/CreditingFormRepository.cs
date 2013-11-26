using System.Data;
using System.Linq;
using dziennik_asp_mvc.Models.Data.Abstract;
using dziennik_asp_mvc.Models.Data.Concrete;
using dziennik_asp_mvc.Models.Entities;
using System.Data.Entity;

namespace dziennik_asp_mvc.Models.Data.Concrete
{
    public class CreditingFormRepository : ICreditingFormRepository
    {
        private readonly EFContext context;

        public CreditingFormRepository(IUnitOfWork unitOfWork)
        {
            context = unitOfWork as EFContext;
        }

        public IQueryable<Crediting_Form> FindAll
        {
            get { return from g in context.Crediting_Form select g; }
        }

        public Crediting_Form FindByName(string name)
        {
            return (context.Crediting_Form.FirstOrDefault(g => g.name == name));
        }

        public Crediting_Form FindById(int id)
        {
            return context.Crediting_Form.FirstOrDefault(g => g.id_crediting_form == id);
        }

        public void Add(Crediting_Form form)
        {
            context.Crediting_Form.Add(form);
        }

        public void Edit(Crediting_Form form)
        {
            context.Entry(form).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            context.Crediting_Form.Remove(FindById(id));
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