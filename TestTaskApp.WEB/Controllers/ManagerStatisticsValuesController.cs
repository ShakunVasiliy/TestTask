using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using TestTaskApp.BLL.DTO;
using TestTaskApp.BLL.Interfases;
using TestTaskApp.BLL.Util;
using TestTaskApp.WEB.Models;

namespace TestTaskApp.WEB.Controllers
{
    public class ManagerStatisticsValuesController : ApiController
    {
        private IManagerStatisticsService managerStatisticsService;

        public ManagerStatisticsValuesController(IManagerStatisticsService managerStatisticsService)
        {
            this.managerStatisticsService = managerStatisticsService;
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var managerStatisticsDto = managerStatisticsService.GetManagersStatistics();
            var managerStatistics = MappingUtil.MapToCollection<ManagerStatisticsDTO, ManagerStatisticsViewModel>(managerStatisticsDto);

            return Ok(managerStatistics);
        }

        protected override void Dispose(bool disposing)
        {
            managerStatisticsService.Dispose();
            base.Dispose(disposing);
        }
    }
}