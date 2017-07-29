using System;
using System.Collections.Generic;
using System.Linq;

using TestTaskApp.BLL.Interfases;
using TestTaskApp.DAL.Entities;

namespace TestTaskApp.BLL.Infranstructure
{
    public class ManagerSpatisticsSorter : ISorter<ManagerStatistics>
    {
        private Dictionary<SortParameter, Func<IEnumerable<ManagerStatistics>, IEnumerable<ManagerStatistics>>> sortFuncs;

        public ManagerSpatisticsSorter()
        {
            sortFuncs = new Dictionary<SortParameter, Func<IEnumerable<ManagerStatistics>, IEnumerable<ManagerStatistics>>>(8);
            sortFuncs.Add(new SortParameter("Name", SortType.ASC), mss => mss.OrderBy(ms => ms.Name));
            sortFuncs.Add(new SortParameter("Name", SortType.DESC), mss => mss.OrderByDescending(ms => ms.Name));
            sortFuncs.Add(new SortParameter("SubjectUserCount", SortType.ASC), mss => mss.OrderBy(ms => ms.SubjectUserCount));
            sortFuncs.Add(new SortParameter("SubjectUserCount", SortType.DESC), mss => mss.OrderByDescending(ms => ms.SubjectUserCount));
        }

        public IEnumerable<ManagerStatistics> Sort(IEnumerable<ManagerStatistics> managersStatistics, SortParameter sortParameter)
        {
            var sortFunc = GetSortFunc(sortParameter);

            return sortFunc(managersStatistics);
        }

        private Func<IEnumerable<ManagerStatistics>, IEnumerable<ManagerStatistics>> GetSortFunc(SortParameter sortParameter)
        {
            Func<IEnumerable<ManagerStatistics>, IEnumerable<ManagerStatistics>> sortFunc;
            bool isExistsSort = sortFuncs.TryGetValue(sortParameter, out sortFunc);

            return isExistsSort ? sortFunc : GetDefaultSortFunc();
        }

        private Func<IEnumerable<ManagerStatistics>, IEnumerable<ManagerStatistics>> GetDefaultSortFunc()
        {
            return sortFuncs[new SortParameter("Name", SortType.DESC)];
        }
    }
}
