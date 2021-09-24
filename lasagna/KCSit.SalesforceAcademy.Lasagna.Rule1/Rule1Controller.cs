using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Controller.Controllers;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Rule1
{
    public class Rule1Controller : GenericController
    {
        private readonly IRule1BO _rule1BO;

        public Rule1Controller(IRule1BO rule1BO)
        {
            _rule1BO = rule1BO;
        }

        [Route("api/Rule1/GetInfo")]
        [HttpPost]
        public async Task<IActionResult> GetRule1Info([FromBody] AdminRule1Parameters parameters)
        {
            var result = await _rule1BO.GetRule1Info(parameters);

            return ReturnResult(result);
        }

        [Route("api/Rule1/UpdateScore")]
        [HttpGet]
        public async Task<IActionResult> UpdateScore(string ticker)
        {
            var result = await _rule1BO.UpdateScore(ticker);

            return ReturnResult(result);
        }

        [Route("api/Rule1/UpdateAllScores")]
        [HttpGet]
        public async Task<IActionResult> UpdateAllScores()
        {
            var result = await _rule1BO.UpdateAllScores();

            return ReturnResult(result);
        }

    }
}
