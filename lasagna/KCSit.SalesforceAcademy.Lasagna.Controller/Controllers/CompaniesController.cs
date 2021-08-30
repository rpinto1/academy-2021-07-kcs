﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using Newtonsoft.Json;
using KCSit.SalesforceAcademy.Lasagna.Business;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KCSit.SalesforceAcademy.Lasagna.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private ICompaniesBO _companiesBO;
        private IGenericLogic _genericLogic;
        private GenericControllerReturn _genericCR;

        public CompaniesController(ICompaniesBO companiesBO, IGenericLogic genericLogic, GenericControllerReturn genericCR)
        {
            _companiesBO = companiesBO;
            _genericLogic = genericLogic;
            _genericCR = genericCR;
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
        // GET api/Companies/indexSector
        [HttpGet("indexSector")]
        public async Task<IActionResult> GetIndexAndSector()
        {
            var indexList = (await _genericLogic.GetAll<Data.Index>());
            var sectorList =  (await _genericLogic.GetAll<Sector>());
            var newResult = new IndexSector{ Indices = indexList.Result, Sectors = sectorList.Result };
            var returnList = new GenericReturn<IndexSector> { Succeeded = true, Message = indexList.Message, 
                Result = newResult };
            return _genericCR.ReturnResult(returnList);
        }
        // GET api/<CompaniesController>/industries/?
        [HttpGet("industries/{sector}")]
        public async Task<IActionResult> GetIndustries(string sector)
        {
            var industriesList =  _companiesBO.GetIndustries(sector);


            return await _genericCR.ReturnResult(industriesList);
        }
        // GET api/<CompaniesController>/industries/?
        [HttpGet("industries")]
        public async Task<IActionResult> GetIndustriesAll()
        {
            var industriesList = _companiesBO.GetIndustries("");


            return await _genericCR.ReturnResult(industriesList);
        }
        // POST api/<CompaniesController>
        [HttpPost("IIS")]
        public async Task<IActionResult> PostSearchCompaniesIIS([FromBody] DropDownParameters value)
        {

            var companies = _companiesBO.GetCompanyByIIS(value.SectorName,value.IndexName,value.IndustryName,value.Page);
            return await _genericCR.ReturnResult(companies);
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
        [HttpGet("search/{search}/{searchPageIndex}")]
        public string GetSearch(string search, int searchPageIndex)
        {
            var companies = _companiesBO.GetCompaniesNamesTickers(search).Result.Result.Skip(searchPageIndex * 10).Take(10);
            return JsonConvert.SerializeObject(companies);
        }
    }
}
