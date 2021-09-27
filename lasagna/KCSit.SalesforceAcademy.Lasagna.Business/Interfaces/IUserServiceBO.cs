using KCSit.SalesforceAcademy.Lasagna.Business.Pocos;
using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using KCSit.SalesforceAcademy.Lasagna.Data.ViewModels;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Interfaces
{
    public interface IUserServiceBO
    {
        public Task<GenericReturn<UserPoco>> SignUp(SignUpViewModel model);

        public Task<GenericReturn<IdToken>> SignIn(SignInViewModel model);

        public Task<GenericReturn> SignOut(string userId);

        public Task<GenericReturn> Update(EditUserViewModel newModel);


        // --------------------------  Email  ---------------------------------------------------

        public Task<GenericReturn> SendEmail(string email);
        public Task<GenericReturn> ResetPassword(string email, string token, string password);

    }

}
