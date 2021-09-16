using KCSit.SalesforceAcademy.Lasagna.Business;
using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Business.Pocos;
using KCSit.SalesforceAcademy.Lasagna.Business.Services;
using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KCSit.SalesforceAcademy.Lasagna.Controller.Controllers
{
    //[Route("api/[controller]")]
    //[ValidateAntiForgeryToken]
    public class UserController : GenericController
    {

        private readonly IUserServiceBO _userService;


        public UserController(IUserServiceBO userService)
        {
            this._userService = userService;
        }


        [Route("api/SignUp")]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpViewModel model)
        {
            var result = _userService.SignUp(model);

            return await ReturnResult(result);
        }



        [Route("api/SignIn")]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInViewModel model)
        {
            var result = await _userService.SignIn(model);

            return ReturnResult(result);
        }


        [Route("api/SignOut")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SignOut(string userId)
        {
            var signOutResult = await _userService.SignOut(userId);

            return ReturnResult(signOutResult);
        }


        [Route("api/Update")]
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(string userId, [FromBody] SignUpViewModel newModel)
        {
            var result = await _userService.Update(userId, newModel);

            return ReturnResult(result);
        }

        // DELETE /<>
        [HttpDelete("{id}")]
        public void Delete(int id) { }





        // --------------------------  PremiumUser  ---------------------------------------------------


        [HttpGet]
        [Route("api/GetAllUsers")]
        [Authorize(Policy = "PremiumUserPolicy")]
        public Task<IActionResult> GetAllUsers()
        {
            var result = _userService.GetAllUsers();

            return ReturnResult(result);
        }



        // --------------------------  Manager  ---------------------------------------------------

        [HttpPost]
        [Route("api/AddClaim")]
        [Authorize(Policy = "ManagerPolicy")]
        public async Task<IActionResult> AddClaim(string userId, [FromBody] Claim claim)
        {
            var result = await _userService.AddClaim(userId, claim);

            return ReturnResult(result);
        }

        [HttpPost]
        [Route("api/RemoveClaim")]
        [Authorize(Policy = "ManagerPolicy")]
        public async Task<IActionResult> RemoveClaim(string userId, [FromBody] Claim claim)
        {
            var result = await _userService.RemoveClaim(userId, claim);

            return ReturnResult(result);
        }


        [HttpGet]
        [Route("api/GetClaims")]
        [Authorize(Policy = "ManagerPolicy")]
        public async Task<IActionResult> GetClaims(string userId)
        {
            var result = await _userService.GetClaims(userId);

            return ReturnResult(result);
        }




        // --------------------------  ADMIN  ---------------------------------------------------

        [Route("api/DeleteUser")]
        [HttpDelete]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _userService.DeleteUser(userId);

            return ReturnResult(result);
        }



    }
}
