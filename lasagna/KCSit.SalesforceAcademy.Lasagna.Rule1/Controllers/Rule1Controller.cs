using KCSit.SalesforceAcademy.Lasagna.Controller.Controllers;
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

        [Route("admin/UpdateScore")]
        [HttpGet]
        public async Task<IActionResult> UpdateScore(string ticker)
        {
            var result = await _rule1BO.UpdateScore(ticker);

            return ReturnResult(result);
        }

        [Route("admin/UpdateAllScores")]
        [HttpGet]
        public async Task<IActionResult> UpdateAllScores()
        {
            var result = await _rule1BO.UpdateAllScores();

            return ReturnResult(result);
        }

    }
}
