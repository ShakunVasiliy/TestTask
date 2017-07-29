using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using TestTaskApp.BLL.Interfases;

namespace TestTaskApp.WEB.Controllers
{
    public class SalutationValuesController : ApiController
    {
        private ISalutationService salutationService;

        public SalutationValuesController(ISalutationService salutationService)
        {
            this.salutationService = salutationService;
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var salutations = salutationService.GetSalutations();

            return Ok(salutations);
        }

        protected override void Dispose(bool disposing)
        {
            salutationService.Dispose();
            base.Dispose(disposing);
        }
    }
}