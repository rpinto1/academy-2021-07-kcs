using KCSit.SalesforceAcademy.Lasagna.Controller.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Rule1
{
    public class Rule1Controller : ControllerBase
    {
        private readonly IScore _rule1BO;
        private readonly GenericControllerReturn _genericControllerReturn;

        public Rule1Controller(IScore rule1BO, GenericControllerReturn genericControllerReturn)
        {
            _rule1BO = rule1BO;
            _genericControllerReturn = genericControllerReturn;
        }

        public async Task<IActionResult> UpdateScore(string ticker)
        {
            var result = _rule1BO.UpdateScore(ticker);

            return await _genericControllerReturn.ReturnResult(result);
        }

        public async Task<IActionResult> UpdateAllScores()
        {
            var result = _rule1BO.UpdateAllScores();

            return await _genericControllerReturn.ReturnResult(result);
        }

    }
}
