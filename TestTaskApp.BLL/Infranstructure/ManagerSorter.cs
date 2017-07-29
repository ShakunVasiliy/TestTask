using System;
using System.Collections.Generic;
using System.Linq;

using TestTaskApp.BLL.Interfases;
using TestTaskApp.DAL.Entities;

namespace TestTaskApp.BLL.Infranstructure
{
    public class ManagerSorter : ISorter<Manager>
    {
        private Dictionary<SortParameter, Func<IEnumerable<Manager>, IEnumerable<Manager>>> sortFuncs;

        public ManagerSorter()
        {
            sortFuncs = new Dictionary<SortParameter, Func<IEnumerable<Manager>, IEnumerable<Manager>>>(8);
            sortFuncs.Add(new SortParameter("Name", SortType.ASC), mss => mss.OrderBy(ms => ms.Name));
            sortFuncs.Add(new SortParameter("Name", SortType.DESC), mss => mss.OrderByDescending(ms => ms.Name));
        }

        public IEnumerable<Manager> Sort(IEnumerable<Manager> managers, SortParameter sortParameter)
        {
            var sortFunc = GetSortFunc(sortParameter);

            return sortFunc(managers);
        }

        private Func<IEnumerable<Manager>, IEnumerable<Manager>> GetSortFunc(SortParameter sortParameter)
        {
            Func<IEnumerable<Manager>, IEnumerable<Manager>> sortFunc;
            bool isExistsSort = sortFuncs.TryGetValue(sortParameter, out sortFunc);

            return isExistsSort ? sortFunc : GetDefaultSortFunc();
        }

        private Func<IEnumerable<Manager>, IEnumerable<Manager>> GetDefaultSortFunc()
        {
            return sortFuncs[new SortParameter("Name", SortType.DESC)];
        }
    }
}
