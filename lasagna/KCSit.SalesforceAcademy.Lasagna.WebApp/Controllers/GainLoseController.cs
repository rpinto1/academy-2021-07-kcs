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
    [Route("[controller]")] 
    public class GainLoseController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<GainLoseController> _logger;
        private readonly IExternalServicesBO _externalServicesBO;

        public GainLoseController(ILogger<GainLoseController> logger, IExternalServicesBO externalServicesBO)
        {
            _logger = logger;
            _externalServicesBO = externalServicesBO;
        }

        [HttpGet]
        public IEnumerable<CompanyData> Get()
        {

            //fetch
            //business inst has a service returns op result
            

            return Enumerable.Range(1, 5).Select(index => new CompanyData
            {
                DisplayName = "",
                Symbol = "",
                MarketChange = 0.00,
                MarketChangePercent = 0.00
            })
            .ToArray();
        }
    }
}
