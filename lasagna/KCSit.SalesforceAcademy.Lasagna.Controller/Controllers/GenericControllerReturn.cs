using KCSit.SalesforceAcademy.Lasagna.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Controller.Controllers
{
    public class GenericControllerReturn : ControllerBase
    {
        public IActionResult ReturnResult(GenericReturn action)
        {
            if (action.Succeeded)
            {
                return Ok(action);
            }

            return BadRequest(action);
        }

        public IActionResult ReturnResult<T>(GenericReturn<T> action) where T :class
        {
            if (action.Succeeded)
            {
                return Ok(action);
            }

            return BadRequest(action);
        }

        public async Task<IActionResult> ReturnResult(Task<GenericReturn> action) 
        {
            if ((await action).Succeeded)
            {
                return Ok(action.Result);
            }

            return BadRequest(action.Result);
        }

        public async Task<IActionResult> ReturnResult<T>(Task<GenericReturn<T>> action) where T : class
        {
            if ((await action).Succeeded)
            {
                return Ok(action.Result);
            }

            return BadRequest(action.Result);
        }
    }
}
