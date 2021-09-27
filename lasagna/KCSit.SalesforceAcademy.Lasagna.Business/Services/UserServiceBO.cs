using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using KCSit.SalesforceAcademy.Lasagna.Business.Pocos;
using KCSit.SalesforceAcademy.Lasagna.EmailService.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.EmailService;
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
                var userSharedFeatures = new UserSharedFeatures(_userManager, _signInManager);

                return await userSharedFeatures.SignIn(model);
            });
        }


        public async Task<GenericReturn> SignOut(string userId)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {
                var userSharedFeatures = new UserSharedFeatures(_userManager, _signInManager);

                userSharedFeatures.SignOut(userId);
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
