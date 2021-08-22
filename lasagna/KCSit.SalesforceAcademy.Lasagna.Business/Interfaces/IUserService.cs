using KCSit.SalesforceAcademy.Lasagna.Business.Models;
using KCSit.SalesforceAcademy.Lasagna.Business.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Interfaces
{
    public interface IUserService
    {
        public UserModel Authenticate(string emailAdress, string password);

        public IEnumerable<UserModel> GetAll();

        public UserModel GetUser(int id);

        public UserServiceResultMessage AddUser(UserModel model);

        public UserServiceResultMessage UpdateUser(int id, UserModel model);

        public UserServiceResultMessage DeleteId(int id);
    }
}
