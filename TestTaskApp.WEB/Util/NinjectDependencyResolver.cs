using System;
using System.Web.Mvc;
using System.Collections.Generic;
using Ninject;

using TestTaskApp.BLL.Interfases;
using TestTaskApp.BLL.Services;

namespace TestTaskApp.WEB.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IUserProfileService>().To<UserProfileService>()
                .WithConstructorArgument("~/Images/defaultImage.jpeg");
            kernel.Bind<IManagerService>().To<ManagerService>();
            kernel.Bind<ISalutationService>().To<SalutationService>();
            kernel.Bind<IManagerStatisticsService>().To<ManagerStatisticsService>();
        }
    }
}