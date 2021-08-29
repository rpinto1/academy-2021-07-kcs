using KCSit.SalesforceAcademy.Lasagna.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Interfaces
{
    public interface IUserServiceBO
    {
        public Task<GenericReturn> SignUp(SignUpViewModel model);

        public Task<GenericReturn> SignIn(SignInViewModel model);

        public Task<GenericReturn> SignOut(UserModel model);

        public Task<GenericReturn> Update(SignUpViewModel newModel);

        public Task<GenericReturn> Delete(UserModel model);
    }
}
