using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Business.Settings;
using KCSit.SalesforceAcademy.Lasagna.Business;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using KCSit.SalesforceAcademy.Lasagna.DataAccess;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Services
{
    public class UserServiceBO : IUserServiceBO
    {
        private readonly AppSettings _appSettings;
        private readonly UserManager<UserModel> userManager;
        private readonly SignInManager<UserModel> signInManager;
        private GenericBusinessLogic genericBusinessLogic;


        public UserServiceBO(IOptions<AppSettings> appSettings, UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            _appSettings = appSettings.Value;
            this.userManager = userManager;
            this.signInManager = signInManager;

            this.genericBusinessLogic = new GenericBusinessLogic();
        }

        //public UserModel Authenticate(string emailAddress, string password)
        //{
        //    var user = _users.SingleOrDefault(x => x.EmailAddress == emailAddress && x.Password == password);

        //    if (user == null) return null;

        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[] {
        //            new Claim(ClaimTypes.Name, user.UserInfoId.ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    user.Token = tokenHandler.WriteToken(token);

        //    return user;
        //}

        public async Task<GenericReturn> SignUp(SignUpViewModel model)
        {
            return await genericBusinessLogic.GenericTransaction(async () => 
            {
                var user = new UserModel()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.EmailAddress,
                    Email = model.EmailAddress
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    string errorMsg = "";
                    foreach (var error in result.Errors)
                    {
                        errorMsg = String.Concat(errorMsg, error.Description, " ");
                    }
                    return new GenericReturn { Succeeded = false, Message = errorMsg };
                }

                return new GenericReturn { Succeeded = true, Message = "User created successfully" };
            });
        }

        public async Task<GenericReturn> SignIn(SignInViewModel model)
        {
            return await genericBusinessLogic.GenericTransaction(async () =>
            {
                //// check if user is already logged in

                var result = await signInManager.PasswordSignInAsync(model.EmailAddress, model.Password, false, false);

                if (!result.Succeeded)
                {
                    return new GenericReturn { Succeeded = false, Message = "Invalid Sign In credentials" };
                }

                return new GenericReturn { Succeeded = true, Message = "User signed in successfully" };
            });
        }



        public async Task<GenericReturn> SignOut(UserModel model)
        {
            await signInManager.SignOutAsync();

            return new GenericReturn { Succeeded = true, Message = "User signed out successfully" };
        }


        public async Task<GenericReturn> Update(SignUpViewModel newModel)
        {
            var userModel = await userManager.FindByEmailAsync(newModel.EmailAddress);

            if (userModel == null)
            {
                return new GenericReturn { Succeeded = false, Message = "User does not exist" };
            }

            //// make sure user is not allowed to change is email address
            //if (userModel.Email != newModel.EmailAddress)
            //    return new GenericReturn { Succeeded = false, Message = "User can not change email address" };

            // user data is ok. Update user
            userModel.FirstName = newModel.FirstName;
            userModel.LastName = newModel.LastName;
            var identityResult = await userManager.ChangePasswordAsync(userModel, userModel.PasswordHash, newModel.Password);
            

            var newUserModel = await userManager.UpdateAsync(userModel);

            if (!newUserModel.Succeeded)
            {
                return new GenericReturn { Succeeded = false, Message = "User could not be updated" };
            }

            return new GenericReturn { Succeeded = true, Message = "User updated successfully" };
        }


        public async Task<GenericReturn> Delete(UserModel model)
        {
            // check if user exists
            var userModel = await userManager.FindByEmailAsync(model.Email);

            if (userModel == null)
            {
                return new GenericReturn { Succeeded = false, Message = "User does not exist" };
            }

            var identityResult = await userManager.DeleteAsync(userModel);

            if (!identityResult.Succeeded)
            {
                return new GenericReturn { Succeeded = false, Message = "Could not delete this User" };
            }

            return new GenericReturn { Succeeded = true, Message = "User deleted successfully" };
        }




    }
}
