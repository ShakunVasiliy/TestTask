using System;
using System.Collections.Generic;
using System.Linq;

using TestTaskApp.BLL.Interfases;
using TestTaskApp.DAL.Entities;

namespace TestTaskApp.BLL.Infranstructure
{
    public class UserProfileSorter : ISorter<UserProfile>
    {
        private Dictionary<SortParameter, Func<IEnumerable<UserProfile>, IEnumerable<UserProfile>>> sortFuncs;

        public UserProfileSorter()
        {
            sortFuncs = new Dictionary<SortParameter, Func<IEnumerable<UserProfile>, IEnumerable<UserProfile>>>(8);
            sortFuncs.Add(new SortParameter("Name", SortType.ASC), ups => ups.OrderBy(up => up.Name));
            sortFuncs.Add(new SortParameter("Name", SortType.DESC), ups => ups.OrderByDescending(up => up.Name));
            sortFuncs.Add(new SortParameter("Email", SortType.ASC), ups => ups.OrderBy(up => up.Email));
            sortFuncs.Add(new SortParameter("Email", SortType.DESC), ups => ups.OrderByDescending(up => up.Email));
            sortFuncs.Add(new SortParameter("Title", SortType.ASC), ups => ups.OrderBy(up => up.Title));
            sortFuncs.Add(new SortParameter("Title", SortType.DESC), ups => ups.OrderByDescending(up => up.Title));
            sortFuncs.Add(new SortParameter("ManagerName", SortType.ASC), ups => ups.OrderBy(up => up.Manager?.Name));
            sortFuncs.Add(new SortParameter("ManagerName", SortType.DESC), ups => ups.OrderByDescending(up => up.Manager?.Name));
        }

        public IEnumerable<UserProfile> Sort(IEnumerable<UserProfile> userProfiles, SortParameter sortParameter)
        {
            var sortFunc = GetSortFunc(sortParameter);

            return sortFunc(userProfiles);
        }

        private Func<IEnumerable<UserProfile>, IEnumerable<UserProfile>> GetSortFunc(SortParameter sortParameter)
        {
            Func<IEnumerable<UserProfile>, IEnumerable<UserProfile>> sortFunc;
            bool isExistsSort = sortFuncs.TryGetValue(sortParameter, out sortFunc);

            return isExistsSort ? sortFunc : GetDefaultSortFunc();
        }

        private Func<IEnumerable<UserProfile>, IEnumerable<UserProfile>> GetDefaultSortFunc()
        {
            return sortFuncs[new SortParameter("Name", SortType.DESC)];
        }
    }
}
