using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class ExternalServicesController : ControllerBase
    {

        private readonly ILogger<ExternalServicesController> _logger;

        private readonly IExternalServicesBO _externalServicesBO;



        public ExternalServicesController(ILogger<ExternalServicesController> logger, IExternalServicesBO externalServicesBO)
        {
            _logger = logger;
            _externalServicesBO = externalServicesBO;
        }


        [HttpGet("gainlose")]
        public string Get()
        {


            var result = _externalServicesBO.FetchGainLoseData();

            if (!result.Succeeded)
            {
                Console.WriteLine("gainlose: ", result);
                return result.Result;
            }

            return result.Message;

        }
    }
}
