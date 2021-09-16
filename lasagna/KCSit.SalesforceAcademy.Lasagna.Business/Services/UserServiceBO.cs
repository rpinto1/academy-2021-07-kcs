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
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using KCSit.SalesforceAcademy.Lasagna.Business.Pocos;
using Microsoft.AspNetCore.Http;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Services
{
    public class UserServiceBO : IUserServiceBO
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly GenericBusinessLogic _genericBusinessLogic;


        public UserServiceBO(GenericBusinessLogic genericBusinessLogic, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
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

                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Role, "BasicUser"),
                    //new Claim(ClaimTypes.Role, "PremiumUser"),
                    //new Claim(ClaimTypes.Role, "Manager"),
                    //new Claim(ClaimTypes.Role, "Admin")
                };
                await _userManager.AddClaimsAsync(user, claims);


            });
        }

        public async Task<GenericReturn<IdToken>> SignIn(SignInViewModel model)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {
                var result = await _signInManager.PasswordSignInAsync(model.EmailAddress, model.Password, false, false);

                if (!result.Succeeded)
                {
                    throw new Exception("Invalid Sign In credentials");
                }

                var user = await _userManager.FindByEmailAsync(model.EmailAddress);

                var AuthenticationToken = await _userManager.GenerateUserTokenAsync(user, "LasagnaApp", "AuthenticationToken");
                await _userManager.SetAuthenticationTokenAsync(user, "LasagnaApp", "AuthenticationToken", AuthenticationToken);

                await _userManager.ResetAccessFailedCountAsync(user);
                
                return new IdToken { Id = user.Id, Token = AuthenticationToken };
            });
        }


        public async Task<GenericReturn> SignOut(string userId)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {

                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    throw new Exception("User does not exist");
                }

                await _userManager.RemoveAuthenticationTokenAsync(user, "LasagnaApp", "AuthenticationToken");


                //// How is this supposed to work?????? SignOutAsync doesn't receive any parameter and it doesn't return anything!!!
                //// wich user will this method logout???
                //await _signInManager.SignOutAsync();

            });
        }



        public async Task<GenericReturn> Update(string userId, SignUpViewModel newModel)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {


                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    throw new Exception("User does not exist");
                }

                // make sure user is not allowed to change is email address
                if (user.Email != newModel.EmailAddress)
                {
                    throw new Exception("User can not change email address");
                }

                // user data is ok. Update user
                user.FirstName = newModel.FirstName;
                user.LastName = newModel.LastName;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description.ToString());
                }

                // Current Password must come as input parameter from a new model !!!!!!!!!!!!
                //var passwordResult = await _userManager.ChangePasswordAsync(user, "Test1234%", newModel.Password);
                var passwordResult = await _userManager.ChangePasswordAsync(user, user.PasswordHash, newModel.Password);
                if (!passwordResult.Succeeded)
                {
                    throw new Exception(passwordResult.Errors.First().Description.ToString());
                }


            });

        }





        // --------------------------  PremiumUser  ---------------------------------------------------

        public async Task<GenericReturn<IEnumerable<GetUsersPoco>>> GetAllUsers()
        {
            return await _genericBusinessLogic.GenericTransaction(() =>
            {
                var userInfo = from user in _userManager.Users.ToList()
                               select new GetUsersPoco { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email };

                return Task.FromResult(userInfo);
            });
        }




        // --------------------------  Manager  ---------------------------------------------------

        public async Task<GenericReturn> AddClaim(string userId, Claim claim)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {

                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    throw new Exception("User does not exist");
                }

                await _userManager.AddClaimAsync(user, claim);

            });
        }

        public async Task<GenericReturn> RemoveClaim(string userId, Claim claim)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {

                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    throw new Exception("User does not exist");
                }

                await _userManager.RemoveClaimAsync(user, claim);

            });
        }

        public async Task<GenericReturn<IList<Claim>>> GetClaims(string userId)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {

                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    throw new Exception("User does not exist");
                }

                var claims = await _userManager.GetClaimsAsync(user);

                return claims;

            });
        }



        // --------------------------  ADMIN  ---------------------------------------------------

        public async Task<GenericReturn> DeleteUser(string userId)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    throw new Exception("User does not exist");
                }

                var result = await _userManager.DeleteAsync(user);

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

        // --------------------------  Email  ---------------------------------------------------

        public async Task<GenericReturn> SendEmail(string email)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                    return null;
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                return token;


            });
        }


    }
}
