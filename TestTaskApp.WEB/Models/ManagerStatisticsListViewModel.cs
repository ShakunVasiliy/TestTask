using System;
using System.Collections.Generic;

using TestTaskApp.BLL.Infranstructure;

namespace TestTaskApp.WEB.Models
{
    public class ManagerStatisticsListViewModel
    {
        public PagedList.IPagedList<ManagerStatisticsViewModel> List { get; set; }
        public IEnumerable<SortParameter> SortParameters { get; set; }
        public SortParameter currentSortParameter { get; set; }
    }
}