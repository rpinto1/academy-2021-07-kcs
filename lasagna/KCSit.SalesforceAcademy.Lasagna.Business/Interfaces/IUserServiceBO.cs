using KCSit.SalesforceAcademy.Lasagna.Business.Pocos;
using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using KCSit.SalesforceAcademy.Lasagna.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Interfaces
{
    public interface IUserServiceBO
    {
        public Task<GenericReturn<UserPoco>> SignUp(SignUpViewModel model);

        public Task<GenericReturn<IdToken>> SignIn(SignInViewModel model);

        public Task<GenericReturn> SignOut(string userId);

        public Task<GenericReturn> Update(EditUserViewModel newModel);




        // --------------------------  ADMIN  ---------------------------------------------------
        public Task<GenericReturn<UserPocoList>> GetUsers(string queryString);

        public Task<GenericReturn<UserPoco>> GetUser(string userId);

        public Task<GenericReturn> AddClaim(string userId, Claim claim);

        public Task<GenericReturn> RemoveClaim(string userId, Claim claim);

        public Task<GenericReturn<IList<Claim>>> GetClaims(string userId);

        public Task<GenericReturn> DeleteUser(string userId);

        public Task<GenericReturn> DeleteUsers(string queryString);



        // --------------------------  Email  ---------------------------------------------------

        public Task<GenericReturn> SendEmail(string email);
        public Task<GenericReturn> ResetPassword(string email, string token, string password);

    }

}
