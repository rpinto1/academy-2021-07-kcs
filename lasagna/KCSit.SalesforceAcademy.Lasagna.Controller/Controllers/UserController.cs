using KCSit.SalesforceAcademy.Lasagna.Business;
using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Business.Pocos;
using KCSit.SalesforceAcademy.Lasagna.Business.Services;
using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using KCSit.SalesforceAcademy.Lasagna.Data.ViewModels;
using KCSit.SalesforceAcademy.Lasagna.EmailService;
using KCSit.SalesforceAcademy.Lasagna.EmailService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

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
        //[Authorize]
        public async Task<IActionResult> Update([FromBody] EditUserViewModel newModel)
        {
            var result = await _userService.Update(newModel);

            return ReturnResult(result);
        }

        [Route("api/AdminUpdate")]
        [HttpPut]
        //[Authorize]
        public async Task<IActionResult> AdminUpdate(string userId, [FromBody] AdminUpdateViewModel newModel)
        {
            var result = await _userService.AdminUpdate(userId, newModel);

            return ReturnResult(result);
        }

        [Route("api/AdminCreate")]
        [HttpPost]
        public async Task<IActionResult> AdminCreate([FromBody] AdminUpdateViewModel model)
        {
            var result = _userService.AdminCreate(model);

            return await ReturnResult(result);
        }



        // --------------------------  PremiumUser  ---------------------------------------------------

        /// http://localhost:3010/api/users?filter={}&range=[0,9]&sort=["id","DESC"]  
        [HttpGet]
        [Route("api/Users")]
        [HttpGet]
        //[Authorize(Policy = "ManagerPolicy")]
        //[Authorize(Policy = "AdminPolicy")]
        public Task<IActionResult> GetUsers()
        {
            var queryString = HttpContext.Request.QueryString.Value;

            var result = _userService.GetUsers(queryString);

            return ReturnResult(result);
        }

        [HttpGet]
        [Route("api/Users/{userId}")]
        [HttpGet]
        //[Authorize(Policy = "PremiumUserPolicy")]
        public Task<IActionResult> GetUser(string userId)
        {
            var result = _userService.GetUser(userId);

            return ReturnResult(result);
        }


        // --------------------------  Manager  ---------------------------------------------------

        [HttpPost]
        [Route("api/AddClaim")]
        //[Authorize(Policy = "ManagerPolicy")]
        public async Task<IActionResult> AddClaim(string userId, [FromBody] Claim claim)
        {
            var result = await _userService.AddClaim(userId, claim);

            return ReturnResult(result);
        }

        [HttpPost]
        [Route("api/RemoveClaim")]
        //[Authorize(Policy = "ManagerPolicy")]
        public async Task<IActionResult> RemoveClaim(string userId, [FromBody] Claim claim)
        {
            var result = await _userService.RemoveClaim(userId, claim);

            return ReturnResult(result);
        }


        [HttpGet]
        [Route("api/GetClaims")]
        //[Authorize(Policy = "ManagerPolicy")]
        public async Task<IActionResult> GetClaims(string userId)
        {
            var result = await _userService.GetClaims(userId);

            return ReturnResult(result);
        }




        // --------------------------  ADMIN  ---------------------------------------------------

        [Route("api/DeleteUser")]
        [HttpDelete]
        //[Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var result = await _userService.DeleteUser(userId);

            return ReturnResult(result);
        }

        [Route("api/DeleteUsers")]
        [HttpDelete]
        //[Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteUsers(string filter)
        {
            var result = await _userService.DeleteUsers(filter);

            return ReturnResult(result);
        }

        //----------------------- Portfolios

        [Route("api/GetPortfolio")]
        [HttpDelete]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetPortfolio(string userId)
        {
            //var result = await _userService.DeleteUser(userId);
            var result = await _userService.DeleteUser(Guid.Parse(userId));

            return ReturnResult(result);
        }




        // --------------------------  Email  ---------------------------------------------------


        [Route("api/SendEmail/{email}")]
        [HttpGet]
        public async Task<IActionResult> ForgotPassword(string email)
        {


            try
            {
                MailAddress m = new MailAddress(email);
            }
            catch (Exception)
            {

                return NotFound();
            }
            
            var resetToken = await _userService.SendEmail(email);
            if (resetToken.Succeeded == false)
            {
                return NotFound();
            }

            return Ok();
        }

        [Route("api/recover")]
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel resetPasswordModel)
        {
            var token = HttpUtility.UrlDecode(resetPasswordModel.Token);
            var resetToken = await _userService.ResetPassword(resetPasswordModel.Email,token,resetPasswordModel.Password);
            if (resetToken.Succeeded == false)
            {
                return NotFound();
            }

            return Ok();
        }


    }
}
