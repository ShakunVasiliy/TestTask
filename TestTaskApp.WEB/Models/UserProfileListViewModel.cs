using System;
using System.Collections.Generic;
using PagedList.Mvc;

using TestTaskApp.BLL.Infranstructure;

namespace TestTaskApp.WEB.Models
{
    public class UserProfileListViewModel
    {
        public PagedList.IPagedList<UserProfileForListViewModel> List { get; set; }
        public IEnumerable<SortParameter> SortParameters { get; set; }
        public SortParameter currentSortParameter { get; set; }
    }
}