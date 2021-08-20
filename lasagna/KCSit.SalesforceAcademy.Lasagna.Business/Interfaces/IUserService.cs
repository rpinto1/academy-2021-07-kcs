using KCSit.SalesforceAcademy.Lasagna.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Interfaces
{
    public interface IUserService
    {
        UserModel Authenticate(string username, string password);
    }
}
