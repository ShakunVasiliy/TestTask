using System;
using System.Collections.Generic;

using TestTaskApp.DAL.Entities;
using TestTaskApp.DAL.Interfaces;
using TestTaskApp.BLL.DTO;
using TestTaskApp.BLL.Interfases;
using TestTaskApp.BLL.Infranstructure;
using TestTaskApp.BLL.Util;

namespace TestTaskApp.BLL.Services
{
    public class ManagerService : IManagerService
    {
        private IUnitOfWork dataset;
        private ISorter<Manager> managerSorter;

        public ManagerService(IUnitOfWork unitOfWork, ISorter<Manager> managerSorter)
        {
            this.dataset = unitOfWork;
            this.managerSorter = managerSorter;
        }

        #region IManagerService

        public IEnumerable<ManagerDTO> GetManagers()
        {
            return GetWithSortBy(new SortParameter());
        }

        #region IDisposable

        public void Dispose()
        {
            dataset.Dispose();
        }

        #endregion IDisposable

        #endregion IManagerService

        #region IServiceWithSort

        public IEnumerable<ManagerDTO> GetWithSortBy(SortParameter sortParameter)
        {
            var managers = dataset.Managers.GetAll();
            managers = managerSorter.Sort(managers, sortParameter);

            return MappingUtil.MapToCollection<Manager, ManagerDTO>(managers);
        }

        #endregion IServiceWithSort
    }
}
