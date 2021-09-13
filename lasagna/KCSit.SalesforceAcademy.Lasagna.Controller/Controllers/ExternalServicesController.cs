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
        private readonly GenericController _genericControllerReturn;


        public ExternalServicesController(ILogger<ExternalServicesController> logger, IExternalServicesBO externalServicesBO, GenericController genericControllerReturn)
        {
            _logger = logger;
            _externalServicesBO = externalServicesBO;
            _genericControllerReturn = genericControllerReturn;
        }


        [HttpGet("gainlose")]
        public async Task<IActionResult> GetGainLose()
        {

            var result = _externalServicesBO.FetchGainLoseData();

            return await _genericControllerReturn.ReturnResult(result);

        }

        [HttpGet("news")]
        public async Task<IActionResult> GetNews()
        {

            var result = _externalServicesBO.FetchNewsData();

            return await _genericControllerReturn.ReturnResult(result);

        }

    }
}
