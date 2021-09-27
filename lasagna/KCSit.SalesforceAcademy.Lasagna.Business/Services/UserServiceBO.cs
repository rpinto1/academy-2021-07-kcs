﻿using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using KCSit.SalesforceAcademy.Lasagna.DataAccess;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using KCSit.SalesforceAcademy.Lasagna.Business.Pocos;
using KCSit.SalesforceAcademy.Lasagna.EmailService.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.EmailService;
using System.Web;
using System.Text.Json;
using KCSit.SalesforceAcademy.Lasagna.Data.ViewModels;
using KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Services
{
    public class UserServiceBO : IUserServiceBO
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IPortfoliosDAO _portfoliosDAO;
        private readonly IGenericBusinessLogic _genericBusinessLogic;
        private readonly IGenericDAO _genericDAO;
        private readonly IEmailSender _emailSender;

        public UserServiceBO(IGenericBusinessLogic genericBusinessLogic, IGenericDAO genericDAO,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
             IEmailSender emailSender, IPortfoliosDAO portfoliosDAO)
        {
            this._genericBusinessLogic = genericBusinessLogic;
            this._genericDAO = genericDAO;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._portfoliosDAO = portfoliosDAO;
            _emailSender = emailSender;
        }

        public async Task<GenericReturn<UserPoco>> SignUp(SignUpViewModel model)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {
                var user = new ApplicationUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email,
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

                var claims = new List<Claim> { new Claim(ClaimTypes.Role, "Basic User") };

                await _userManager.AddClaimsAsync(user, claims);

                return new UserPoco { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email, Role = "Basic User" };
            });
        }

        public async Task<GenericReturn<IdToken>> SignIn(SignInViewModel model)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (!result.Succeeded)
                {
                    throw new Exception("Invalid Sign In credentials");
                }

                var user = await _userManager.FindByEmailAsync(model.Email);

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
                //// which user will this method logout???
                //await _signInManager.SignOutAsync();

            });
        }



        public async Task<GenericReturn> Update(EditUserViewModel newModel)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {
                var userId = newModel.Id;
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    throw new Exception("User does not exist");
                }

                if (user.Email != newModel.Email)
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

                if (!string.IsNullOrEmpty(newModel.NewPassword))
                {

                    var passwordResult = await _userManager.ChangePasswordAsync(user, newModel.OldPassword, newModel.NewPassword);

                    if (!passwordResult.Succeeded)
                    {
                        throw new Exception(passwordResult.Errors.First().Description.ToString());
                    }
                }
            });
        }


        public async Task<GenericReturn<UserPoco>> AdminCreate(AdminUpdateViewModel model)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {
                const string defaultPassword = "Test1234%";

                var user = new ApplicationUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email,
                };

                var result = await _userManager.CreateAsync(user, defaultPassword);

                if (!result.Succeeded)
                {
                    string errorMsg = "";
                    foreach (var error in result.Errors)
                    {
                        errorMsg = String.Concat(errorMsg, error.Description, " ");
                    }
                    throw new Exception(errorMsg);
                }

                var claims = new List<Claim> { new Claim(ClaimTypes.Role, model.Role) };

                await _userManager.AddClaimsAsync(user, claims);

                return new UserPoco { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email, Role = model.Role };
            });
        }

        public async Task<GenericReturn> AdminUpdate(string userId, AdminUpdateViewModel newModel)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    throw new Exception("User does not exist");
                }

                if (user.Email != newModel.Email)
                {
                    throw new Exception("User can not change email address");
                }

                // user data is ok. Update user
                user.FirstName = newModel.FirstName;
                user.LastName = newModel.LastName;

                var claims = await _userManager.GetClaimsAsync(user);
                await _userManager.RemoveClaimsAsync(user, claims);
                var newClaims = new List<Claim> { new Claim(ClaimTypes.Role, newModel.Role) };
                await _userManager.AddClaimsAsync(user, newClaims);

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description.ToString());
                }
                return userId;
            });
        }



        // --------------------------  PremiumUser  ---------------------------------------------------

        public async Task<GenericReturn<UserPocoList>> GetUsers(string queryString)
        {
            return await _genericBusinessLogic.GenericTransaction(() =>
           {
               if (String.IsNullOrEmpty(queryString))
               {
                   queryString = "?filter=%7B%7D&range=%5B0%2C9%5D&sort=%5B%22id%22%2C%22ASC%22%5D";
               }


               // ?filter={}&range=[0,9]&sort=["id","ASC"]
               // "?filter={"firstName":"joana","email":"joana@lasagna.pt"}&range=[0,24]&sort=["id","ASC"]"
               var filterStr = HttpUtility.ParseQueryString(queryString).Get("filter");

               var filter = new Filter();
               if (filterStr != null)
               {
                   filter = JsonSerializer.Deserialize<Filter>(filterStr);
                   filter.firstName ??= "";
                   filter.lastName ??= "";
                   filter.email ??= "";
                   filter.role ??= "";
               }

               // [0, 9],  [10, 19],  [20, 25] ...
               var skip = 0;
               var take = 0;
               var rangeStr = HttpUtility.ParseQueryString(queryString).Get("range");
               if (rangeStr != null)
               {
                   var first = int.Parse(rangeStr.Substring(1, rangeStr.IndexOf(",") - rangeStr.IndexOf("[") - 1));
                   var last = int.Parse(rangeStr.Substring(rangeStr.IndexOf(",") + 1, rangeStr.IndexOf("]") - rangeStr.IndexOf(",") - 1));
                   skip = first;
                   take = last - first + 1;
               }

               // sort=["id","ASC"]"
               var orderByField = "";
               var orderByDirection = "Ascending";
               var sortStr = HttpUtility.ParseQueryString(queryString).Get("sort");
               if (sortStr != null)
               {
                   orderByField = sortStr.Split("\"")[1];
                   orderByField = orderByField.ToUpper().ElementAt(0) + orderByField.Substring(1);
                   orderByDirection = sortStr.Contains("ASC") ? "Ascending" : "Descending";
               }



               //////var users = from user in _userManager.Users
               //////            .Where(u => u.FirstName.ToLower().Contains(filter.firstName.ToLower()) &&
               //////                        u.LastName.ToLower().Contains(filter.lastName.ToLower()) &&
               //////                        u.Email.ToLower().Contains(filter.email.ToLower()))
               //////            .OrderBy(orderByField, orderByDirection)
               //////            .Skip(skip)
               //////            .Take(take)
               //////            .ToList()

               //////            select new UserPoco
               //////            {
               //////                Id = user.Id,
               //////                FirstName = user.FirstName,
               //////                LastName = user.LastName,
               //////                Email = user.Email,
               //////            };


               //////var Total = _userManager.Users.Count(u => u.FirstName.ToLower().Contains(filter.firstName.ToLower()) &&
               //////                                          u.LastName.ToLower().Contains(filter.lastName.ToLower()) &&
               //////                                          u.Email.ToLower().Contains(filter.email.ToLower()));

               //////var usersList = users.ToList();
               //////for (int i = 0; i < usersList.Count; i++)
               //////{
               //////    var appUser = _userManager.FindByIdAsync(usersList[i].Id).Result;
               //////    var claims = _userManager.GetClaimsAsync(appUser).Result;
               //////    usersList[i].Role = claims.First().Value;
               //////}

               //////var result = new UserPocoList { Users = usersList.AsEnumerable(), Total = Total };

               //////return Task.FromResult(result);



               using (var context = new lasagnakcsContext())
               {
                   var users = (from user in context.Users
                                join claims in context.UserClaims
                                on user.Id equals claims.UserId
                                where user.FirstName.ToLower().Contains(filter.firstName.ToLower()) &&
                                      user.LastName.ToLower().Contains(filter.lastName.ToLower()) &&
                                      user.Email.ToLower().Contains(filter.email.ToLower()) &&
                                      claims.ClaimValue.Contains(filter.role)

                                select new UserPoco
                                {
                                    Id = user.Id,
                                    FirstName = user.FirstName,
                                    LastName = user.LastName,
                                    Email = user.Email,
                                    Role = claims.ClaimValue,
                                })

                                .OrderBy(orderByField, orderByDirection)
                                .Skip(skip)
                                .Take(take)
                                .ToList();

                   return Task.FromResult(new UserPocoList { Users = users, Total = users.Count() });
               }

           });
        }


        public async Task<GenericReturn<UserPoco>> GetUser(string userId)
        {
            return await _genericBusinessLogic.GenericTransaction(() =>
            {
                if (String.IsNullOrEmpty(userId))
                {
                    throw new Exception("User not found.");
                }

                var user = _userManager.Users.Where(u => u.Id == userId).SingleOrDefault();

                if (user == null)
                {
                    throw new Exception("User does not exist");
                }

                var userPoco = new UserPoco { 
                    Id = userId, 
                    FirstName = user.FirstName, 
                    LastName = user.LastName, 
                    Email = user.Email, 
                    Role = _userManager.GetClaimsAsync(user).Result.First().Value 
                };

                return Task.FromResult(userPoco);
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

        public async Task<GenericReturn> DeleteUser(Guid userId)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user == null)
                {
                    throw new Exception("User does not exist");
                }

                var portfolios = await _portfoliosDAO.GetPortfolios(userId);

                foreach (PortfolioPoco portfolio in portfolios)
                {
                    await _portfoliosDAO.DeletePortfolioId(portfolio.PortfolioId);
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

                return new UserPoco { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email };

            });
        }


        public async Task<GenericReturn> DeleteUsers(string queryString)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {
                var idStr = queryString.Substring(7, queryString.Length - 9).Split(",");

                var deletedUsers = new List<Guid>();

                foreach (string id in idStr)
                {
                    var uuid = Guid.Parse(JsonSerializer.Deserialize<string>(id));

                    var result = await DeleteUser(uuid);

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Message);
                    }

                    deletedUsers.Add(uuid);
                }

                return deletedUsers;
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
        public async Task<GenericReturn> ResetPassword(string email, string token, string password)
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
