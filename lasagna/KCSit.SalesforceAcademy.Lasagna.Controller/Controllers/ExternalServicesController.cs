using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Controller.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class ExternalServicesController : GenericController
    {

        private readonly ILogger<ExternalServicesController> _logger;
        private IMemoryCache _cache;

        private readonly IExternalServicesBO _externalServicesBO;
        


        public ExternalServicesController(ILogger<ExternalServicesController> logger, IMemoryCache cache, IExternalServicesBO externalServicesBO)
        {
            _logger = logger;
            _cache = cache;
            _externalServicesBO = externalServicesBO;
            
        }


        [HttpGet("gainlose")]
        public async Task<IActionResult> GetGainLose()
        {

            var result = _externalServicesBO.FetchGainLoseData();

            return await ReturnResult(result);

        }

        [HttpGet("news")]
        public async Task<IActionResult> GetNews()
        {

            var result = _externalServicesBO.FetchNewsData();

            return await ReturnResult(result);

        }

    }
}
