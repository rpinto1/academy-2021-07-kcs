using KCSit.SalesforceAcademy.Lasagna.Business;
using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Business.Pocos;
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
    public class ExternalServicesController : ControllerBase
    {

        private readonly ILogger<ExternalServicesController> _logger;

        private readonly IExternalServicesBO _externalServicesBO;
        private readonly GenericController _genericControllerReturn;

        private readonly IMemoryCache _memoryCache;
        private string cacheKey;


        public ExternalServicesController(ILogger<ExternalServicesController> logger, IExternalServicesBO externalServicesBO, GenericController genericControllerReturn, IMemoryCache cache)
        {
            _logger = logger;
            _externalServicesBO = externalServicesBO;
            _genericControllerReturn = genericControllerReturn;
            _memoryCache = cache;
        }


        [HttpGet("gainlose")]
        public async Task<IActionResult> GetGainLose()
        {
             cacheKey = "gainLoseData";

            if (!_memoryCache.TryGetValue(cacheKey, out GenericReturn<GainLosePoco> poco))
            {
                poco = await _externalServicesBO.FetchGainLoseData();

                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(10),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };

                _memoryCache.Set(cacheKey, poco, cacheExpiryOptions);
            }
            return Ok(poco);
            
        }

        [HttpGet("news")]
        public async Task<IActionResult> GetNews()
        {
            cacheKey = "news";

            if (!_memoryCache.TryGetValue(cacheKey, out GenericReturn<NewsPoco> poco))
            {
                poco = await _externalServicesBO.FetchNewsData();

                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(10),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };

                _memoryCache.Set(cacheKey, poco, cacheExpiryOptions);
            }
            return Ok(poco);


        }

    }
}
