using System.Data;
using System.Linq;
using dziennik_asp_mvc.Models.Data.Abstract;
using dziennik_asp_mvc.Models.Data.Concrete;
using dziennik_asp_mvc.Models.Entities;
using System.Data.Entity;

namespace dziennik_asp_mvc.Models.Data.Concrete
{
    public class GroupsService : IGroupsService
    {
        private IGroupsRepository repo;

        public GroupsService(IGroupsRepository repo)
        {
            this.repo = repo;
        }

        IQueryable<Groups> IGroupsService.FindAll
        {
            get { return repo.FindAll; }
        }

        Groups IGroupsService.FindByName(string name)
        {
            return repo.FindByName(name);
        }

        Groups IGroupsService.FindById(int id)
        {
            return repo.FindById(id);
        }

        void IGroupsService.Add(Groups group)
        {
            repo.Add(group);
            repo.Save();
        }

        void IGroupsService.Edit(Groups group)
        {
            repo.Edit(group);
            repo.Save();
        }

        void IGroupsService.Delete(int id)
        {
            repo.Delete(id);
            repo.Save();
        }

        void IGroupsService.Dispose()
        {
            repo.Dispose();
        }
    }
}