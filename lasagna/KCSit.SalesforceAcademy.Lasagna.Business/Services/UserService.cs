using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Business.Models;
using KCSit.SalesforceAcademy.Lasagna.Business.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Services
{
    public class UserService : IUserService
    {
        private List<UserModel> _users = new List<UserModel> {
            new UserModel{ UserInfoId=Guid.NewGuid(), FirstName="Pete", LastName="Selvas", EmailAdress="pete@selvas.com", Password="test", ConfirmPassword="test"},
            new UserModel{ UserInfoId=Guid.NewGuid(), FirstName="Joana", LastName="Zerpa", EmailAdress="joana@zerpa.com", Password="test", ConfirmPassword="test"},
            new UserModel{ UserInfoId=Guid.NewGuid(), FirstName="Rui", LastName="Costa", EmailAdress="rui@costa.com", Password="test", ConfirmPassword="test"},
            new UserModel{ UserInfoId=Guid.NewGuid(), FirstName="Vitor", LastName="Costa", EmailAdress="vitor@costa.com", Password="test", ConfirmPassword="test"},
            new UserModel{ UserInfoId=Guid.NewGuid(), FirstName="Raul", LastName="Ribeiro", EmailAdress="raul@ribeiro.com", Password="test", ConfirmPassword="test"}
        };

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public UserModel Authenticate(string emailAdress, string password)
        {
            var user = _users.SingleOrDefault(x => x.EmailAdress == emailAdress && x.Password == password);

            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.UserInfoId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }


        public IEnumerable<UserModel> GetAll()
        {
            return _users;
        }

        public UserModel GetUser(int id)
        {
            if (id < 0 || id >= _users.Count)
            {
                return null;
            }

            return _users[id];
        }


        public UserServiceResultMessage AddUser(UserModel model)
        {
            // check if EmailAddress already exist
            foreach (UserModel user in _users)
            {
                if (user.EmailAdress == model.EmailAdress)
                    return new UserServiceResultMessage { Success = false, Message = "User already exists" };
            }

            // check if model data is valid (FirstName, LastName and Password)
            var dataValidationReusltMessage = UserModelDataValidator.CheckModelData(model);
            if (!dataValidationReusltMessage.Success)
            {
                return dataValidationReusltMessage;
            }

            // user data is OK. Add user
            _users.Add(new UserModel
            {
                UserInfoId = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAdress = model.EmailAdress,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword
            });

            return new UserServiceResultMessage { Success = true, Message = "User added successfully" };
        }

        public UserServiceResultMessage UpdateUser(int id, UserModel model)
        {
            var currentUser = GetUser(id);

            // make sure user is not allowed to change is email address
            if (currentUser.EmailAdress != model.EmailAdress)
                return new UserServiceResultMessage { Success = false, Message = "User can not change is email address" };

            // check if model data is valid (FirstName, LastName and Password)
            var dataValidationReusltMessage = UserModelDataValidator.CheckModelData(model);
            if (!dataValidationReusltMessage.Success)
            {
                return dataValidationReusltMessage;
            }

            // user data is ok. Update user
            _users[id].FirstName = model.FirstName;
            _users[id].LastName = model.LastName;
            _users[id].Password = model.Password;
            _users[id].ConfirmPassword = model.ConfirmPassword;

            return new UserServiceResultMessage { Success = true, Message = "User updated successfully" };
        }


        public UserServiceResultMessage DeleteId(int id)
        {
            if (id < 0 || id >= _users.Count)
            {
                return new UserServiceResultMessage { Success = false, Message = "Invalid Id number" };
            }

            _users.RemoveAt(id);
            return new UserServiceResultMessage { Success = true, Message = "UserId " + id + " was deleted" };
        }




    }
}
