using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Business;
using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KCSit.SalesforceAcademy.Lasagna.WebApp.Controllers
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
        public string GetIndexAndSector()
        {
            var indexList = _genericLogic.GetAll<Data.Index>().Result;
            var sectorList = _genericLogic.GetAll<Sector>().Result;
            return JsonConvert.SerializeObject( new {index = indexList.Result , sector = sectorList.Result });
        }
        // GET api/<CompaniesController>/industries/?
        [HttpGet("industries/{sector}")]
        public string GetIndustries(string sector)
        {
            var industriesList = _companiesBO.GetIndustries(sector).Result;


            return  JsonConvert.SerializeObject(industriesList);
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
        [HttpGet("search/{search}")]
        public string GetSearch(string search)
        {
            return search;
        }
    }
}
