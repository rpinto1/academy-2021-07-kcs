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
using KCSit.SalesforceAcademy.Lasagna.EmailService.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.EmailService;
using System.Web;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Services
{
    public class UserServiceBO : IUserServiceBO
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly GenericBusinessLogic _genericBusiness;
        private readonly GenericBusinessLogic _genericBusinessLogic;
        private readonly IEmailSender _emailSender;

        public UserServiceBO(GenericBusinessLogic genericBusinessLogic, IEmailSender emailSender, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._genericBusiness = genericBusinessLogic;
            this._userManager = userManager;
            this._signInManager = signInManager;
            _emailSender = emailSender;
        }

        public async Task<GenericReturn> SignUp(SignUpViewModel model)
        {
            return await _genericBusiness.GenericTransaction(async () =>
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
            return await _genericBusiness.GenericTransaction(async () =>
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
            return await _genericBusiness.GenericTransaction(async () =>
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
            return await _genericBusiness.GenericTransaction(async () =>
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

        public async Task<GenericReturn<IEnumerable<UserPoco>>> GetUsers(string queryString)
        {
            return await _genericBusiness.GenericTransaction(() =>
            {
                var firstNameFilter = "";
                var lastNameFilter = "";
                var emailFilter = "";

                if (String.IsNullOrEmpty(queryString))
                {
                    //queryString = "?filter=%7B%7D&range=%5B0%2C9%5D&sort=%5B%22id%22%2C%22ASC%22%5D";
                    queryString = "";
                }

                // "?filter={"firstName":"joan","email":"mmm"}&range=[0,24]&sort=["id","ASC"]"
                var filter = HttpUtility.ParseQueryString(queryString).Get("filter");

                var firstNameIndex = filter.IndexOf("firstName");
                if (firstNameIndex != -1)
                {
                    var firstNameValueStartIndex = filter.IndexOf(":", firstNameIndex) + 2;
                    var firstNameValueEndIndex = filter.IndexOf("\"", firstNameValueStartIndex);
                    firstNameFilter = filter.Substring(firstNameValueStartIndex, firstNameValueEndIndex - firstNameValueStartIndex);
                }

                var lastNameIndex = filter.IndexOf("lastName");
                if (lastNameIndex != -1)
                {
                    var lastNameValueStartIndex = filter.IndexOf(":", lastNameIndex) + 2;
                    var lastNameValueEndIndex = filter.IndexOf("\"", lastNameValueStartIndex);
                    lastNameFilter = filter.Substring(lastNameValueStartIndex, lastNameValueEndIndex - lastNameValueStartIndex);
                }

                var emailIndex = filter.IndexOf("email");
                if (emailIndex != -1)
                {
                    var emailValueStartIndex = filter.IndexOf(":", emailIndex) + 2;
                    var emailValueEndIndex = filter.IndexOf("\"", emailValueStartIndex);
                    emailFilter = filter.Substring(emailValueStartIndex, emailValueEndIndex - emailValueStartIndex);
                }

                // [0, 9],  [10, 19],  [20, 25] ...
                var range = HttpUtility.ParseQueryString(queryString).Get("range");
                var first = int.Parse(range.Substring(1, range.IndexOf(",") - range.IndexOf("[") - 1));
                var last = int.Parse(range.Substring(range.IndexOf(",") + 1, range.IndexOf("]") - range.IndexOf(",") - 1));
                var skip = first;
                var take = last - first + 1;

                var sort = HttpUtility.ParseQueryString(queryString).Get("sort");
                var orderByField = sort.Split("\"")[1];
                orderByField = orderByField.ToUpper().ElementAt(0) + orderByField.Substring(1);
                var orderByDirection = sort.Contains("ASC") ? "Ascending" : "Descending";



                var userInfo = from user in _userManager.Users
                                      .Where(u => u.FirstName.ToLower().Contains(firstNameFilter.ToLower()) &&
                                                  u.LastName.ToLower().Contains(lastNameFilter.ToLower()) &&
                                                  u.Email.ToLower().Contains(emailFilter.ToLower()))
                                      .OrderBy(orderByField, orderByDirection)
                                      .Skip(skip)
                                      .Take(take)
                                      .ToList()
                                      select new UserPoco
                                      {
                                          Id = user.Id,
                                          FirstName = user.FirstName,
                                          LastName = user.LastName,
                                          Email = user.Email
                                      };

                return Task.FromResult(userInfo);
            });
        }


        public async Task<GenericReturn<UserPoco>> GetUser(string userId)
        {
            return await _genericBusiness.GenericTransaction(() =>
            {
                if (String.IsNullOrEmpty(userId))
                {
                    throw new Exception("User not found.");
                }
                var user = _userManager.Users.Where(u => u.Id == userId).SingleOrDefault();

                var userPoco = new UserPoco { Id = userId, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email };

                return Task.FromResult(userPoco);
            });
        }

        // --------------------------  Manager  ---------------------------------------------------

        public async Task<GenericReturn> AddClaim(string userId, Claim claim)
        {
            return await _genericBusiness.GenericTransaction(async () =>
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
            return await _genericBusiness.GenericTransaction(async () =>
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
            return await _genericBusiness.GenericTransaction(async () =>
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
            return await _genericBusiness.GenericTransaction(async () =>
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
                    throw new Exception("");
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                
                var message = new Message(new string[] { email }, "Reset password", token, email);

                await _emailSender.SendEmailAsync(message);
               
            });
        }
        public async Task<GenericReturn> ResetPassword(string email,string token, string password)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                    throw new Exception("Error");
                var resetPassResult = await _userManager.ResetPasswordAsync(user, token, password);
            if (!resetPassResult.Succeeded)
            {
                    throw new Exception("Error");
            }
            });
        }
    }
}
