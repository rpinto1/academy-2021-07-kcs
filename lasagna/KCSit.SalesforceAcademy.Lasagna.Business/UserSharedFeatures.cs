using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Business
{
    public class UserSharedFeatures
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;



        public UserSharedFeatures(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }


        public async Task<IdToken> SignIn(SignInViewModel model)
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
        }


        public async void SignOut(string userId)
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
        }


    }
}
