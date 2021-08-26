using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public class UserModelAuthentication
    {
        [Required]
        public string EmailAdress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
