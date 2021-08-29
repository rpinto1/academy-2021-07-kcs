using KCSit.SalesforceAcademy.Lasagna.Business;
using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Data;
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

        private IUserServiceBO userService;


        public UserController(IUserServiceBO userService)
        {
            this.userService = userService;
        }


        [Route("api/SignUp")]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpViewModel model)
        {
            GenericReturn signUpResult = await userService.SignUp(model);

            return ReturnResult(signUpResult);
        }


        [Route("api/SignIn")]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInViewModel model)
        {
            GenericReturn signInResult = await userService.SignIn(model);

            return ReturnResult(signInResult);
        }


        [Route("api/SignOut")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> SignOut([FromBody] UserModel model)
        {
            GenericReturn signOutResult = await userService.SignOut(model);

            return ReturnResult(signOutResult);
        }



        [Route("api/Update")]
        //[Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SignUpViewModel newModel)
        {
            GenericReturn updateUserResult = await userService.Update(newModel);

            return ReturnResult(updateUserResult);
        }



        [Route("api/Delete")]
        //[Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] UserModel model)
        {
            GenericReturn deleteResult = await userService.Delete(model);

            return ReturnResult(deleteResult);
        }



        private IActionResult ReturnResult(GenericReturn action)
        {
            if (action.Succeeded)
            {
                return Ok(new { message = action.Message });
            }

            return BadRequest(new { message = action.Message });
        }


    }
}
