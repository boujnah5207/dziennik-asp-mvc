using System.Data;
using System.Linq;
using dziennik_asp_mvc.Models.Data.Abstract;
using dziennik_asp_mvc.Models.Data.Concrete;
using dziennik_asp_mvc.Models.Entities;
using System.Data.Entity;

namespace dziennik_asp_mvc.Models.Data.Concrete
{
    public class GroupsRepository : IGroupsRepository
    {
        private readonly EFContext context;

        public GroupsRepository(IUnitOfWork unitOfWork)
        {
            context = unitOfWork as EFContext;
        }

        public IQueryable<Groups> FindAll
        {
            get { return from g in context.Groups select g; }
        }

        public Groups FindByName(string name)
        {
            return (context.Groups.FirstOrDefault(g => g.group_name == name));
        }

        public Groups FindById(int id)
        {
            return context.Groups.FirstOrDefault(g => g.id_group == id);
        }

        public void Add(Groups group)
        {
            context.Groups.Add(group);
        }

        public void Edit(Groups group)
        {
           context.Entry(group).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            context.Groups.Remove(FindById(id));
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