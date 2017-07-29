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
    public class ManagerValuesController : ApiController
    {
        private IManagerService managerService;

        public ManagerValuesController(IManagerService managerService)
        {
            this.managerService = managerService;
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var managerDtos = managerService.GetManagers();
            var managers = MappingUtil.MapToCollection<ManagerDTO, ManagerViewModel>(managerDtos);

            return Ok(managers);
        }

        protected override void Dispose(bool disposing)
        {
            managerService.Dispose();
            base.Dispose(disposing);
        }
    }
}