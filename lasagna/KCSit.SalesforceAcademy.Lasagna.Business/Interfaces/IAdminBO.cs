using KCSit.SalesforceAcademy.Lasagna.Business;
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
    public interface IAdminBO
    {
        public Task<GenericReturn<IdToken>> SignIn(SignInViewModel model);

        public Task<GenericReturn> SignOut(string userId);

        public Task<GenericReturn<UserPoco>> CreateUser(AdminUpdateViewModel model);

        public Task<GenericReturn<UserPoco>> GetUser(string userId);

        public Task<GenericReturn<UserPocoList>> GetUsers(string queryString);

        public Task<GenericReturn> UpdateUser(string userId, AdminUpdateViewModel newModel);

        public Task<GenericReturn> DeleteUser(Guid userId);

        public Task<GenericReturn> DeleteUsers(string queryString);


        public Task<GenericReturn> AddClaim(string userId, Claim claim);

        public Task<GenericReturn> RemoveClaim(string userId, Claim claim);

        public Task<GenericReturn<IList<Claim>>> GetClaims(string userId);





    }
}
