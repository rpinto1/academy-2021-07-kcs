using KCSit.SalesforceAcademy.Lasagna.Business;
using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
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

            cacheKey = "portfolios" + userId;

            if (!_memoryCache.TryGetValue(cacheKey, out GenericReturn<List<PortfolioPoco>> poco))
            {
                poco = await _portfoliosBO.GetPortfolios(userId);

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

        
        [HttpGet("portfolioCompanies")]
        public async Task<IActionResult> GetCompaniesByPortfolio(Guid portfolioId)
        {

            cacheKey = "companiesByPortfolio" + portfolioId;

            if (!_memoryCache.TryGetValue(cacheKey, out GenericReturn<PortfolioPoco> poco))
            {
                //poco = await _portfoliosBO.GetCompaniesByPortfolio(portfolioId);
                poco = await _portfoliosBO.GetPortfolioWithCompanies(portfolioId);

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

        
        [HttpGet("portfolioCompanyValues")]
        public async Task<IActionResult> GetCompanyValuesByTicker(string ticker)
        {

            cacheKey = "companyValuesByTicker" + ticker;

            if (!_memoryCache.TryGetValue(cacheKey, out GenericReturn<PortfolioCompanyPoco> poco))
            {
                poco = await _portfoliosBO.GetValuesAndScoreByTicker(ticker);

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

            cacheKey = "portfolioByUuid" + Id;

            if (!_memoryCache.TryGetValue(cacheKey, out GenericReturn<List<PortfolioCompanyPoco>> poco))
            {
                poco = await _portfoliosBO.GetPortfolio(Id);

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

        // api/companies/deleteportfolio/{id}
        [HttpDelete("deleteportfolio/{id}")]
        public async Task<IActionResult> DeletePortfolio(Guid id)
        {
            var result = await _portfoliosBO.DeletePortfolio(id);

            return ReturnResult(result);
        }

        [HttpPost("updateportfolio")]
        public async Task<IActionResult> UpdatePortfolio([FromBody] PortfolioUpdatePoco data)
        {
            
            var result = await _portfoliosBO.UpdatePortfolioId(Guid.Parse(data.Uuid),data.Tickers,data.PortfolioName);

            return ReturnResult(result);
        }

    }
}
