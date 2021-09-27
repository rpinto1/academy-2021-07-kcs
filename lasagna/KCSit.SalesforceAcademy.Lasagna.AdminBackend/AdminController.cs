using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Controller.Controllers;
using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using KCSit.SalesforceAcademy.Lasagna.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;


namespace KCSit.SalesforceAcademy.Lasagna.AdminBackend
{
    //[ApiController]
    public class AdminController : GenericController
    {
        private readonly IAdminBO _adminBO;
        private readonly IRule1BO _rule1BO;

        public AdminController(IAdminBO adminBO, IRule1BO rule1BO )
        {
            this._adminBO = adminBO;
            this._rule1BO = rule1BO;
        }


        [Route("api/Admin/SignIn")]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInViewModel model)
        {
            var result = await _adminBO.SignIn(model);

            return ReturnResult(result);
        }


        [Route("api/Admin/SignOut")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SignOut(string userId)
        {
            var signOutResult = await _adminBO.SignOut(userId);

            return ReturnResult(signOutResult);
        }


        [Route("api/Admin/CreateUser")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] AdminUpdateViewModel model)
        {
            var result = _adminBO.CreateUser(model);

            return await ReturnResult(result);
        }



        [Route("api/admin/users/{userId}")]
        [HttpGet]
        //[Authorize(Policy = "PremiumUserPolicy")]
        public Task<IActionResult> GetUser(string userId)
        {
            var result = _adminBO.GetUser(userId);

            return ReturnResult(result);
        }


        /// http://localhost:3011/api/users?filter={}&range=[0,9]&sort=["id","DESC"]  
        [Route("api/admin/users")]
        [HttpGet]
        //[Authorize(Policy = "ManagerPolicy")]
        //[Authorize(Policy = "AdminPolicy")]
        public Task<IActionResult> GetUsers()
        {
            var queryString = HttpContext.Request.QueryString.Value;

            var result = _adminBO.GetUsers(queryString);

            return ReturnResult(result);
        }



        [Route("api/Admin/UpdateUser")]
        [HttpPut]
        //[Authorize]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] AdminUpdateViewModel newModel)
        {
            var result = await _adminBO.UpdateUser(userId, newModel);

            return ReturnResult(result);
        }



        [Route("api/Admin/DeleteUser")]
        [HttpDelete]
        //[Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var result = await _adminBO.DeleteUser(userId);

            return ReturnResult(result);
        }

        [Route("api/Admin/DeleteUsers")]
        [HttpDelete]
        //[Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteUsers(string filter)
        {
            var result = await _adminBO.DeleteUsers(filter);

            return ReturnResult(result);
        }





        [HttpPost]
        [Route("api/Admin/Users/AddClaim")]
        //[Authorize(Policy = "ManagerPolicy")]
        public async Task<IActionResult> AddClaim(string userId, [FromBody] Claim claim)
        {
            var result = await _adminBO.AddClaim(userId, claim);

            return ReturnResult(result);
        }

        [HttpPost]
        [Route("api/Admin/Users/RemoveClaim")]
        //[Authorize(Policy = "ManagerPolicy")]
        public async Task<IActionResult> RemoveClaim(string userId, [FromBody] Claim claim)
        {
            var result = await _adminBO.RemoveClaim(userId, claim);

            return ReturnResult(result);
        }


        [HttpGet]
        [Route("api/Admin/Users/GetClaims")]
        //[Authorize(Policy = "ManagerPolicy")]
        public async Task<IActionResult> GetClaims(string userId)
        {
            var result = await _adminBO.GetClaims(userId);

            return ReturnResult(result);
        }




        // -------------------------------------------------------- Rule1 --------------------------------------------------------



        [Route("api/Admin/Rule1/GetInfo")]
        [HttpGet]
        public async Task<IActionResult> GetRule1Info()
        {
            var queryString = HttpContext.Request.QueryString.Value;

            var result = await _rule1BO.GetRule1Info(queryString);

            return ReturnResult(result);
        }


        [Route("api/Admin/Rule1/UpdateOneScore")]
        [HttpGet]
        public async Task<IActionResult> UpdateOneScore(string ticker)
        {
            var result = await _rule1BO.UpdateOneScore(ticker);

            return ReturnResult(result);
        }


        [Route("api/Admin/Rule1/UpdateManyScores")]
        [HttpGet]
        public async Task<IActionResult> UpdateManyScores(string tickers)
        {
            var result = await _rule1BO.UpdateManyScores(tickers);

            return ReturnResult(result);
        }



        [Route("api/Admin/Rule1/UpdateAllScores")]
        [HttpGet]
        public async Task<IActionResult> UpdateAllScores()
        {
            var result = await _rule1BO.UpdateAllScores();

            return ReturnResult(result);
        }





    }
}
