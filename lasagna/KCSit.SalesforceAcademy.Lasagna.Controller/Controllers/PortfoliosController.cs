using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfoliosController : GenericController
    {

        private IPortfoliosBO _portfoliosBO;
        private IGenericLogic _genericLogic;
        private readonly IMemoryCache _memoryCache;
        private string cacheKey;

        public PortfoliosController(IPortfoliosBO companiesBO, IGenericLogic genericLogic, IMemoryCache memoryCache)
        {
            _portfoliosBO = companiesBO;
            _genericLogic = genericLogic;
            _memoryCache = memoryCache;

        }

        [HttpGet("portfolio")]
        public async Task<IActionResult> GetPortfolios(Guid userId)
        {

            var genericReturn = await _portfoliosBO.GetPortfolios(userId);

            return Ok(genericReturn);
        }

        [ResponseCache(Duration = 200, Location = ResponseCacheLocation.Client, NoStore = true)]
        [HttpGet("portfolioCompanies")]
        public async Task<IActionResult> GetCompaniesByPortfolio(Guid portfolioId)
        {

            //var genericReturn = await _portfoliosBO.GetCompaniesByPortfolio(portfolioId);
            var genericReturn = await _portfoliosBO.GetPortfolioWithCompanies(portfolioId);

            return Ok(genericReturn);
        }

        [ResponseCache(Duration = 200, Location = ResponseCacheLocation.Client, NoStore = false)]
        [HttpGet("portfolioCompanyValues")]
        public async Task<IActionResult> GetCompanyValuesByTicker(string ticker)
        {
            var genericReturn = await _portfoliosBO.GetCompanyValuesByTicker(ticker);

            return Ok(genericReturn);
        }


        [HttpPost("createPortfolio")]
        public async Task<IActionResult> CreatePortfolio([FromBody] PortfolioViewModel portfolio)
        {

            var genericReturn = await _portfoliosBO.CreatePortfolio(
                portfolio.UserId.ToString(),
                portfolio.Name
                //replace with some form of DTO -- signature safe
                );

            return Ok(genericReturn);
        }


        [HttpPost("addCompanyToPortfolio")]
        public async Task<IActionResult> AddCompanyToPortfolio([FromBody] CompanyToPortfolioViewModel data)
        {

            var genericReturn = await _portfoliosBO.AddCompanyToPortfolio(
                data.PortfolioUuid,
                data.Ticker
                //DTO
                );

            return Ok(genericReturn);
        }



        //------------------------------------------Raúl----------------------------------------------


        // api/companies/portfolio/Id
        [HttpGet("editportfolio")]
        public async Task<IActionResult> GetPortfolio(Guid Id)
        {

            var genericReturn = await _portfoliosBO.GetPortfolio(Id);

            return Ok(genericReturn);
        }

        // api/companies/deleteportfolio/{id}
        [HttpDelete("deleteportfolio/{id}")]
        public async Task<IActionResult> DeletePortfolio(Guid id)
        {
            _portfoliosBO.DeletePortfolio(id);

            return Ok();
        }

        [HttpPost("updateportfolio")]
        public async Task<IActionResult> UpdatePortfolio( Guid Uuid,  List<string> Tickers,  String PortfolioName)
        {
            _portfoliosBO.UpdatePortfolioId(Uuid, Tickers, PortfolioName);

            return Ok();
        }



    }
}
