using KCSit.SalesforceAcademy.Lasagna.Business.Pocos;
using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Interfaces
{
    public interface IUserServiceBO
    {
        public Task<GenericReturn> SignUp(SignUpViewModel model);

        public Task<GenericReturn<IdToken>> SignIn(SignInViewModel model);

        public Task<GenericReturn> SignOut(string userId);

        public Task<GenericReturn> Update(string userId, SignUpViewModel newModel);




        // --------------------------  ADMIN  ---------------------------------------------------




        public Task<GenericReturn<IEnumerable<GetUsersPoco>>> GetAllUsers();

        public Task<GenericReturn> AddClaim(string userId, Claim claim);

        public Task<GenericReturn> RemoveClaim(string userId, Claim claim);

        public Task<GenericReturn<IList<Claim>>> GetClaims(string userId);

        public Task<GenericReturn> DeleteUser(string userId);

        // --------------------------  Email  ---------------------------------------------------

        public Task<GenericReturn> SendEmail(string email);
        public Task<GenericReturn> ResetPassword(string email, string token, string password);

    }
}
