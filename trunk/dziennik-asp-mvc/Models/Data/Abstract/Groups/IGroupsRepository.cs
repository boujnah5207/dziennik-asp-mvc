using System.Linq;
using dziennik_asp_mvc.Models.Entities;

namespace dziennik_asp_mvc.Models.Data.Abstract
{
    public interface IGroupsRepository
    {
        IQueryable<Groups> FindAll { get; }

        Groups FindByName(string name);
        Groups FindById(int id);

        void Add(Groups group);
        void Edit(Groups group);
        void Delete(int id);

        void Save();
        void Dispose();
    }
}
