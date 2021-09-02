using KCSit.SalesforceAcademy.Lasagna.Business;
using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KCSit.SalesforceAcademy.Lasagna.Controller.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserServiceBO _userService;
        private readonly GenericControllerReturn _genericControllerReturn;


        public UserController(IUserServiceBO userService, GenericControllerReturn genericControllerReturn)
        {
            this._userService = userService;
            this._genericControllerReturn = genericControllerReturn;
        }


        [Route("api/SignUp")]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpViewModel model)
        {
            var signUpResult = _userService.SignUp(model);

            return await _genericControllerReturn.ReturnResult(signUpResult);
        }


        [Route("api/SignIn")]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInViewModel model)
        {
            GenericReturn signInResult = await _userService.SignIn(model);

            return _genericControllerReturn.ReturnResult(signInResult);
        }


        [Route("api/SignOut")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> SignOut([FromBody] ApplicationUser model)
        {
            GenericReturn signOutResult = await _userService.SignOut(model);

            return _genericControllerReturn.ReturnResult(signOutResult);
        }



        [Route("api/Update")]
        //[Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(string id, [FromBody] SignUpViewModel newModel)
        {
            GenericReturn updateUserResult = await _userService.Update(id, newModel);

            return _genericControllerReturn.ReturnResult(updateUserResult);
        }



        [Route("api/Delete")]
        //[Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            
            GenericReturn deleteResult = await _userService.Delete(id);

            return _genericControllerReturn.ReturnResult(deleteResult);
        }



    }
}
