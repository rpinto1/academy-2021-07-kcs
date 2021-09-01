﻿using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
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
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Services
{
    public class UserServiceBO : IUserServiceBO
    {
        private readonly AppSettings _appSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly GenericBusinessLogic _genericBusinessLogic;


        public UserServiceBO(IOptions<AppSettings> appSettings, GenericBusinessLogic genericBusinessLogic,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._appSettings = appSettings.Value;
            this._genericBusinessLogic = genericBusinessLogic;
            this._userManager = userManager;
            this._signInManager = signInManager;

        }

        public async Task<GenericReturn> SignUp(SignUpViewModel model)
        {
            return await _genericBusinessLogic.GenericTransaction(async () => 
            {
                var user = new ApplicationUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.EmailAddress,
                    Email = model.EmailAddress
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    string errorMsg = "";
                    foreach (var error in result.Errors)
                    {
                        errorMsg = String.Concat(errorMsg, error.Description, " ");
                    }
                    throw new Exception(errorMsg);
                }
            });
        }

        public async Task<GenericReturn<GuidToken>> SignIn(SignInViewModel model)
        {
            return await _genericBusinessLogic.GenericTransaction<GuidToken>(async () =>
            {
                var result = await _signInManager.PasswordSignInAsync(model.EmailAddress, model.Password, false, false);

                if (!result.Succeeded)
                {
                    throw new Exception("Invalid Sign In credentials");
                }

                var user = await _userManager.FindByEmailAsync(model.EmailAddress);

                await _userManager.RemoveAuthenticationTokenAsync(user, "LasagnaApp", "AccessToken");
                var newAccessToken = await _userManager.GenerateUserTokenAsync(user, "LasagnaApp", "AccessToken");
                await _userManager.SetAuthenticationTokenAsync(user, "LasagnaApp", "AccessToken", newAccessToken);

                return new GuidToken { Guid = user.Id, Token = newAccessToken };
            });
        }


        public async Task<GenericReturn> SignOut(ApplicationUser model)
        {
            await _signInManager.SignOutAsync();

            return new GenericReturn { Succeeded = true, Message = "User signed out successfully" };
        }


        public async Task<GenericReturn> Update(string id, SignUpViewModel newModel)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return new GenericReturn { Succeeded = false, Message = "User does not exist" };
            }

            // make sure user is not allowed to change is email address
            if (user.Email != newModel.EmailAddress)
                return new GenericReturn { Succeeded = false, Message = "User can not change email address" };

            // user data is ok. Update user
            user.FirstName = newModel.FirstName;
            user.LastName = newModel.LastName;

            var userIdentityResult = await _userManager.UpdateAsync(user);
            if (!userIdentityResult.Succeeded)
            {
                return new GenericReturn { Succeeded = false, Message = userIdentityResult.Errors.First().Description.ToString() };
            }

            var pass = _userManager.PasswordHasher.HashPassword(user, user.PasswordHash);
            if (await _userManager.CheckPasswordAsync(user, pass))
            {
                return new GenericReturn { Succeeded = true, Message = "Passwords match" };
            }

            var passwordIdentityResult = await _userManager.ChangePasswordAsync(user, user.PasswordHash, newModel.Password);            
            if (!passwordIdentityResult.Succeeded)
            {
                return new GenericReturn { Succeeded = false, Message = passwordIdentityResult.Errors.First().Description.ToString() };
            }

            return new GenericReturn { Succeeded = true, Message = "User updated successfully" };
        }


        public async Task<GenericReturn> Delete(string id)
        {
            // check if user exists
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return new GenericReturn { Succeeded = false, Message = "User does not exist" };
            }

            var identityResult = await _userManager.DeleteAsync(user);

            if (!identityResult.Succeeded)
            {
                return new GenericReturn { Succeeded = false, Message = "Could not delete this User" };
            }

            return new GenericReturn { Succeeded = true, Message = "User deleted successfully" };
        }




        private string GenerateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                        new Claim("token", "value")
                    }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }

    }
}
