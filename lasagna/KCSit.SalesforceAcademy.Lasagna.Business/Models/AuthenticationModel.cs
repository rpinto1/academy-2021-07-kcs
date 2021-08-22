using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Models
{
    public class AuthenticationModel
    {
        [Required]
        public string EmailAdress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
