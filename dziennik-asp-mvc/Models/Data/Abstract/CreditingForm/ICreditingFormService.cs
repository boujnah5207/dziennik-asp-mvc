using dziennik_asp_mvc.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik_asp_mvc.Models.Data.Abstract
{
    public interface ICreditingFormService
    {
        IQueryable<Crediting_Form> FindAll { get; }

        Crediting_Form FindByName(string name);
        Crediting_Form FindById(int id);

        void Add(Crediting_Form form);
        void Edit(Crediting_Form form);
        void Delete(int id);
        void Dispose();
    }
}
