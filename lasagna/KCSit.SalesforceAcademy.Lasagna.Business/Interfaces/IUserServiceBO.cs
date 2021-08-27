using KCSit.SalesforceAcademy.Lasagna.Data;
using System.Collections.Generic;


namespace KCSit.SalesforceAcademy.Lasagna.Business.Interfaces
{
    public interface IUserServiceBO
    {
        public UserModel Authenticate(string emailAdress, string password);

        public IEnumerable<UserModel> GetAll();

        public UserModel GetUser(int id);

        public GenericReturn AddUser(UserModel model);

        public GenericReturn UpdateUser(int id, UserModel model);

        public GenericReturn DeleteId(int id);
    }
}
