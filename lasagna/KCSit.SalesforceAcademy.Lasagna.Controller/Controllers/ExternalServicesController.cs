using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Controller.Controllers
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
        public string GetGainLose()
        {


            var result = _externalServicesBO.FetchGainLoseData();

            if (!result.Succeeded)
            {
                return result.Message;
            }

            return result.Result;

        }

        [HttpGet("news")]
        public string GetNews()
        {


            var result = _externalServicesBO.FetchNewsData();

            if (!result.Succeeded)
            {
                return result.Message;
            }

            return result.Result;

        }

    }
}
