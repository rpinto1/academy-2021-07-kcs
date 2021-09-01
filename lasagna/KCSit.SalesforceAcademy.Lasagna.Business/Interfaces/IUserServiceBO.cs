using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Interfaces
{
    public interface IUserServiceBO
    {
        public Task<GenericReturn> SignUp(SignUpViewModel model);

        public Task<GenericReturn<GuidToken>> SignIn(SignInViewModel model);

        public Task<GenericReturn> SignOut(ApplicationUser model);

        public Task<GenericReturn> Update(string id, SignUpViewModel newModel);

        public Task<GenericReturn> Delete(string id);
    }
}
