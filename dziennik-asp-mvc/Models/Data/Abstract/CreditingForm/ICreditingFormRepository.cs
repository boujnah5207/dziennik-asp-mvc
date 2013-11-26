using System.Linq;
using dziennik_asp_mvc.Models.Entities;

namespace dziennik_asp_mvc.Models.Data.Abstract
{
    public interface ICreditingFormRepository
    {
        IQueryable<Crediting_Form> FindAll { get; }

        Crediting_Form FindByName(string name);
        Crediting_Form FindById(int id);

        void Add(Crediting_Form form);
        void Edit(Crediting_Form form);
        void Delete(int id);

        void Save();
        void Dispose();
    }
}
