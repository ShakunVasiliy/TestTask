using System;
using System.Collections.Generic;

using TestTaskApp.BLL.DTO;
using TestTaskApp.BLL.Infranstructure;

namespace TestTaskApp.BLL.Interfases
{
    public interface IManagerStatisticsService : IServiceWithSort<ManagerStatisticsDTO>, IDisposable
    {
        IEnumerable<ManagerStatisticsDTO> GetManagersStatistics();
        IEnumerable<ManagerStatisticsDTO> GetManagersStatistics(SortParameter sortParameter);
    }
}
