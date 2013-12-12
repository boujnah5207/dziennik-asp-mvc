using dziennik_asp_mvc.Models.Data.Abstract;
using dziennik_asp_mvc.Models.Data.Abstract.Roles;
using dziennik_asp_mvc.Models.Data.Concrete;
using dziennik_asp_mvc.Models.Data.Concrete.Roles;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dziennik_asp_mvc.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        private void AddBindings()
        {
            kernel.Bind<IUnitOfWork>().To<EFContext>().InRequestScope();
            AddPartialGradesBindings();
            AddFinalGradesBindings();
            AddGroupsBindings();
            AddCreditingFormBindings();
            AddSubjectsBindings();
            AddUsersBindings();
            AddRolesBindings();
        }

        private void AddGroupsBindings()
        {
            kernel.Bind<IGroupsRepository>().To<GroupsRepository>();
            kernel.Bind<IGroupsService>().To<GroupsService>();
        }

        private void AddUsersBindings()
        {
            kernel.Bind<IUsersRepository>().To<UsersRepository>();
            kernel.Bind<IUsersService>().To<UsersService>();
        }
        private void AddRolesBindings()
        {
            kernel.Bind<IRolesRepository>().To<RolesRepository>();
            kernel.Bind<IRolesService>().To<RolesService>();
        }
        private void AddSubjectsBindings()
        {
            kernel.Bind<ISubjectsRepository>().To<SubjectsRepository>();
            kernel.Bind<ISubjectsService>().To<SubjectsService>();
        }
        private void AddCreditingFormBindings()
        {
            kernel.Bind<ICreditingFormRepository>().To<CreditingFormRepository>();
            kernel.Bind<ICreditingFormService>().To<CreditingFormService>();
        }
        private void AddPartialGradesBindings()
        {
            kernel.Bind<IPartialGradesRepository>().To<PartialGradesRepository>();
            kernel.Bind<IPartialGradesService>().To<PartialGradesService>();
        }
        private void AddFinalGradesBindings()
        {
            kernel.Bind<IFinalGradesRepository>().To<FinalGradesRepository>();
            kernel.Bind<IFinalGradesService>().To<FinalGradesService>();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }       
    }
}