using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public class UserModel : IdentityUser
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
