using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KCSit.SalesforceAcademy.Lasagna.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private ICompaniesBO _companiesBO;
        private IGenericLogic _genericLogic;

        public CompaniesController(ICompaniesBO companiesBO, IGenericLogic genericLogic)
        {
            _companiesBO = companiesBO;
            _genericLogic = genericLogic;

        }




        // GET: api/<CompaniesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CompaniesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        // GET api/<CompaniesController>/indexSector
        [HttpGet("indexSector")]
        public async Task<string> GetIndexAndSector()
        {
            var indexList = (await _genericLogic.GetAll<Data.Index>()).Result;
            var sectorList = (await _genericLogic.GetAll<Sector>()).Result;
            return JsonConvert.SerializeObject(new { index = indexList, sector = sectorList });
        }
        // GET api/<CompaniesController>/industries/?
        [HttpGet("industries/{sector}")]
        public async Task<string> GetIndustries(string sector)
        {
            var industriesList = (await _companiesBO.GetIndustries(sector)).Result;


            return JsonConvert.SerializeObject(industriesList);
        }
        // GET api/<CompaniesController>/industries/?
        [HttpGet("industries")]
        public async Task<string> GetIndustriesAll()
        {
            var industriesList = (await _companiesBO.GetIndustries("")).Result;


            return JsonConvert.SerializeObject(industriesList);
        }
        // POST api/<CompaniesController>
        [HttpPost("IIS")]
        public async Task<string> PostSearchCompaniesIIS([FromBody] DropDownParameters value)
        {

            var companies = (await _companiesBO.GetCompanyByIIS(value.SectorName, value.IndexName, value.IndustryName, value.Page)).Result;
            return JsonConvert.SerializeObject(companies);
        }

        // POST api/<CompaniesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CompaniesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CompaniesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // POST api/<CompaniesController>
        /*[HttpGet("search/{search}/{searchPageIndex}")]
        public string GetSearchTest(string search, int searchPageIndex)
        {
            var companies = _companiesBO.GetCompaniesNamesTickers(search).Result.Result.Skip(searchPageIndex * 10).Take(10);
            return JsonConvert.SerializeObject(companies);
        } */

        // POST api/<CompaniesController>
        [HttpGet("search/{search}/{searchPageIndex}")]
        public async Task<IActionResult> GetSearch(string search, int searchPageIndex)
        {
            //passar o searchPageIndex como parâmetro
            //devolver o GenericReturn
            //mudar a assinatura do método para ser um IActionResult
            //return Ok(genericReturn)
            var genericReturn = await _companiesBO.GetCompaniesNamesTickers(search, searchPageIndex);
            return Ok(genericReturn);
        }
    }
}
