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
    public class ManagerStatisticsService : IManagerStatisticsService
    {
        private IUnitOfWork dataset;
        private ISorter<ManagerStatistics> managerStatisticsSorter;

        public ManagerStatisticsService(IUnitOfWork unitOfWork, ISorter<ManagerStatistics> managerStatisticsSorter)
        {
            this.dataset = unitOfWork;
            this.managerStatisticsSorter = managerStatisticsSorter;
        }
        #region IManagerStatisticsService

        public IEnumerable<ManagerStatisticsDTO> GetManagersStatistics()
        {
            return GetManagersStatistics(new SortParameter());
        }

        public IEnumerable<ManagerStatisticsDTO> GetManagersStatistics(SortParameter sortParameter)
        {
            return GetWithSortBy(sortParameter);
        }

        #endregion IManagerStatisticsService

        #region IServiceWithSort

        public IEnumerable<ManagerStatisticsDTO> GetWithSortBy(SortParameter sortParameter)
        {
            var managersStatistics = dataset.Managers.GetStatistics();
            managersStatistics = managerStatisticsSorter.Sort(managersStatistics, sortParameter);

            return MappingUtil.MapToCollection<ManagerStatistics, ManagerStatisticsDTO>(managersStatistics);
        }

        #endregion IServiceWithSort

        #region IDisposable

        public void Dispose()
        {
            dataset.Dispose();
        }

        #endregion IDisposable
    }
}
