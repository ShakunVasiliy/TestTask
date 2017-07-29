using Ninject.Modules;

using TestTaskApp.DAL.Entities;
using TestTaskApp.DAL.Interfaces;
using TestTaskApp.DAL.Repositories;
using TestTaskApp.BLL.Infranstructure;
using TestTaskApp.BLL.Interfases;
using TestTaskApp.BLL.Util;

namespace TestTaskApp.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;

        public ServiceModule(string connection)
        {
            connectionString = connection;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
            Bind<IValidator<UserProfile>>().To<UserProfileValidator>();
            Bind<ISorter<UserProfile>>().To<UserProfileSorter>();
            Bind<ISorter<ManagerStatistics>>().To<ManagerSpatisticsSorter>();
            Bind<ISorter<Manager>>().To<ManagerSorter>();
        }
    }
}
