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

        private IExternalServicesBO _externalServicesBO;
        private GenericControllerReturn _genericControllerReturn;



        public ExternalServicesController(ILogger<ExternalServicesController> logger, IExternalServicesBO externalServicesBO)
        {
            _logger = logger;
            _externalServicesBO = externalServicesBO;
        }


        [HttpGet("gainlose")]
        public IActionResult GetGainLose()
        {

            var result = _externalServicesBO.FetchGainLoseData();

            return _genericControllerReturn.ReturnResult(result);

        }

        [HttpGet("news")]
        public IActionResult GetNews()
        {

            var result = _externalServicesBO.FetchNewsData();

            return _genericControllerReturn.ReturnResult(result);

        }

    }
}
