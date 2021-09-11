using KCSit.SalesforceAcademy.Lasagna.Controller.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Rule1
{
    [ApiController]
    public class Rule1Controller : ControllerBase
    {
        private readonly IRule1BO _rule1BO;
        private readonly GenericControllerReturn _genericControllerReturn;

        public Rule1Controller(IRule1BO rule1BO, GenericControllerReturn genericControllerReturn)
        {
            _rule1BO = rule1BO;
            _genericControllerReturn = genericControllerReturn;
        }

        [Route("admin/UpdateScore")]
        [HttpGet]
        public async Task<IActionResult> UpdateScore(string ticker)
        {
            var result = _rule1BO.UpdateScore(ticker);

            return await _genericControllerReturn.ReturnResult(result);
        }

        [Route("admin/UpdateAllScores")]
        [HttpPost]
        public async Task<IActionResult> UpdateAllScores()
        {
            var result = _rule1BO.UpdateAllScores();

            return await _genericControllerReturn.ReturnResult(result);
        }

    }
}
