using System;
using System.Web.Security;
using dziennik_asp_mvc.Models.Data.Concrete;
using dziennik_asp_mvc.Models.Data.Concrete.Roles;
using dziennik_asp_mvc.Models.Entities;

namespace dziennik_asp_mvc.Helpers
{
    public class DziennikRoleProvider : RoleProvider
    {
        private RolesRepository roleRepo { get; set; }
        private UsersRepository userRepo { get; set; }

        public DziennikRoleProvider()
        {
            roleRepo = new RolesRepository(new EFContext());
            userRepo = new UsersRepository(new EFContext());
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            return new String[] { userRepo.FindByName(username).Roles.role_name };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string rolename)
        {
            Users user = userRepo.FindByName(username);

            if (user.Roles.role_name == rolename)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}