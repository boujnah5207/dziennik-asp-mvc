using dziennik_asp_mvc.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik_asp_mvc.Models.Data.Abstract
{
    public interface IGroupsService
    {
        IQueryable<Groups> FindAll { get; }

        Groups FindByName(string name);
        Groups FindById(int id);

        void Add(Groups group);
        void Edit(Groups group);
        void Delete(int id);
        void Dispose();
    }
}
