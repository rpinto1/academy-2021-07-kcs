using KCSit.SalesforceAcademy.Lasagna.Business;
using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KCSit.SalesforceAcademy.Lasagna.Controller.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserServiceBO _userService;


        public UserController(IUserServiceBO userService)
        {
            _userService = userService;
        }


        [Route("api/user/authenticate")]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserModelAuthentication model)
        {
            Console.WriteLine("Authenticate request - USER: " + model.EmailAdress + " PASSWORD: " + model.Password);
            var user = _userService.Authenticate(model.EmailAdress, model.Password);

            if (user == null) return BadRequest(new { message = "Email address and/or password incorrect" });

            return Ok(user);
        }



        [Route("api/user")]
        [Authorize]
        [HttpGet]
        public IEnumerable<UserModel> Get()
        {
            return _userService.GetAll();
        }



        [Route("api/user/{id}")]
        [Authorize]
        [HttpGet("{id}")]
        public UserModel Get(int id)
        {
            return _userService.GetUser(id);
        }



        [Route("api/user")]
        [HttpPost]
        public IActionResult Post([FromBody] UserModel model)
        {
            GenericReturn addUserResult = _userService.AddUser(model);

            return ReturnResult(addUserResult);
        }


        [Route("api/<UserController>/{id}")]
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserModel model)
        {
            GenericReturn updateUserResult = _userService.UpdateUser(id, model);

            return ReturnResult(updateUserResult);
        }



        [Route("api/<UserController>/{id}")]
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            GenericReturn deleteResult = _userService.DeleteId(id);

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
