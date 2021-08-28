using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public class SignInViewModel
    {
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email Address is not valid")]
        public string EmailAddress { get; set; }

        [Required]
        [RegularExpression(@"((?=.*\d)(?=.*[A-Z])(?=.*\W).{8,100})",
            ErrorMessage = "Password must have at least 8 characters length, at least one capital letter, at least one digit and at least one special character")]
        public string Password { get; set; }
    }
}
