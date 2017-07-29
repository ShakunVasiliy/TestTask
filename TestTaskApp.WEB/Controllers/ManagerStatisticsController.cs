using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoMapper;
using PagedList;

using TestTaskApp.BLL.DTO;
using TestTaskApp.BLL.Interfases;
using TestTaskApp.BLL.Infranstructure;
using TestTaskApp.BLL.Util;
using TestTaskApp.WEB.Models;

namespace TestTaskApp.WEB.Controllers
{
    public class ManagerStatisticsController : Controller
    {
        private const int CountValuesOnPage = 3;

        private IManagerStatisticsService managerStatisticsService;

        public ManagerStatisticsController(IManagerStatisticsService managerStatisticsService)
        {
            this.managerStatisticsService = managerStatisticsService;
        }

        // GET: Manager
        public ActionResult Index(int? page, SortParameter sortParameter)
        {
            page = page ?? 1;

            var viewModel = new ManagerStatisticsListViewModel();
            var managersStatisticsDto = managerStatisticsService.GetManagersStatistics(sortParameter);
            
            viewModel.currentSortParameter = sortParameter;
            viewModel.SortParameters = SortParameter.CreateSortParameters(sortParameter,
                new string[] { "Name", "SubjectUserCount" });
            
            viewModel.List = MappingUtil.MapToCollection<ManagerStatisticsDTO, ManagerStatisticsViewModel>(managersStatisticsDto)
                .ToPagedList(page.Value, CountValuesOnPage);

            return Request.IsAjaxRequest() ? (ActionResult)PartialView(viewModel)
                : View(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            managerStatisticsService.Dispose();
            base.Dispose(disposing);
        }
    }
}