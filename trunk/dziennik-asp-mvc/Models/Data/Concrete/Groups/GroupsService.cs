using System.Data;
using System.Linq;
using dziennik_asp_mvc.Models.Data.Abstract;
using dziennik_asp_mvc.Models.Data.Concrete;
using dziennik_asp_mvc.Models.Entities;
using System.Data.Entity;
using dziennik_asp_mvc.Exceptions;

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
            Groups group = repo.FindByName(name);

            if (group == null)
            {
                throw new GroupNotFoundException();
            }

            return group;
        }

        Groups IGroupsService.FindById(int id)
        {
            Groups group = repo.FindById(id);

            if (group == null)
            {
                throw new GroupNotFoundException();
            }

            return group;
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