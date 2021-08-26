using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Business.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using KCSit.SalesforceAcademy.Lasagna.DataAccess;
using Microsoft.AspNetCore.Identity;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Services
{
    public class UserServiceBO : IUserServiceBO
    {
        private List<UserModel> _users = new List<UserModel> {
            new UserModel{ UserInfoId=Guid.NewGuid(), FirstName="Pete", LastName="Selvas", EmailAddress="pete@selvas.com", Password="test", ConfirmPassword="test"},
            new UserModel{ UserInfoId=Guid.NewGuid(), FirstName="Joana", LastName="Zerpa", EmailAddress="joana@zerpa.com", Password="test", ConfirmPassword="test"},
            new UserModel{ UserInfoId=Guid.NewGuid(), FirstName="Rui", LastName="Costa", EmailAddress="rui@costa.com", Password="test", ConfirmPassword="test"},
            new UserModel{ UserInfoId=Guid.NewGuid(), FirstName="Vitor", LastName="Costa", EmailAddress="vitor@costa.com", Password="test", ConfirmPassword="test"},
            new UserModel{ UserInfoId=Guid.NewGuid(), FirstName="Raul", LastName="Ribeiro", EmailAddress="raul@ribeiro.com", Password="test", ConfirmPassword="test"}
        };

        private readonly AppSettings _appSettings;

        public UserServiceBO(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public UserModel Authenticate(string emailAddress, string password)
        {
            var user = _users.SingleOrDefault(x => x.EmailAddress == emailAddress && x.Password == password);

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


        public GenericReturn AddUser(UserModel model)
        {
            // check if EmailAddress already exist
            foreach (UserModel user in _users)
            {
                if (user.EmailAddress == model.EmailAddress)
                    return new GenericReturn { Succeeded = false, Message = "User already exists" };
            }

            // user data is OK. Add user
            _users.Add(new UserModel
            {
                UserInfoId = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.EmailAddress,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword
            });

            //var test = new IdentityUser();

            //model.UserName = model.EmailAddress;
            //model.Email = model.EmailAddress;
            //model.NormalizedEmail = model.EmailAddress;
            //model.PasswordHash = model.Password;

            ////model.PhoneNumber = "123456789";

            ////model.EmailConfirmed = true;
            ////model.PhoneNumberConfirmed = true;
            ////model.TwoFactorEnabled = false;
            ////model.LockoutEnabled = false;
            ////model.AccessFailedCount = 0;


            ////var addUserResult = new GenericDAO().Add<IdentityUser>(test);
            //var addUserResult = new GenericDAO().Add<IdentityUser>(model);

            //if (addUserResult == null)
            //{
            //    return new GenericReturn { Succeeded = false, Message = "Error while adding this User" };
            //}

            return new GenericReturn { Succeeded = true, Message = "User added successfully" };
        }

        public GenericReturn UpdateUser(int id, UserModel model)
        {
            var currentUser = GetUser(id);

            // make sure user is not allowed to change is email address
            if (currentUser.EmailAddress != model.EmailAddress)
                return new GenericReturn { Succeeded = false, Message = "User can not change is email address" };

            // user data is ok. Update user
            _users[id].FirstName = model.FirstName;
            _users[id].LastName = model.LastName;
            _users[id].Password = model.Password;
            _users[id].ConfirmPassword = model.ConfirmPassword;

            return new GenericReturn { Succeeded = true, Message = "User updated successfully" };
        }


        public GenericReturn DeleteId(int id)
        {
            if (id < 0 || id >= _users.Count)
            {
                return new GenericReturn { Succeeded = false, Message = "Invalid Id number" };
            }

            _users.RemoveAt(id);
            return new GenericReturn { Succeeded = true, Message = "UserId " + id + " was deleted" };
        }




    }
}
