using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using KCSit.SalesforceAcademy.Lasagna.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mail;
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
