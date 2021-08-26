using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;


namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public class UserModel 
    {
        [Key]
        public Guid UserInfoId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 15, MinimumLength = 2, ErrorMessage = "First Name must have 2 to 15 characters")]
        [RegularExpression(@"^([a-zA-Z]+)$", ErrorMessage = "First Name has invalid characters")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 15, MinimumLength = 2, ErrorMessage = "Last Name must have 2 to 15 characters")]
        [RegularExpression(@"^([a-zA-Z]+)$", ErrorMessage = "Last Name has invalid characters")]
        public string LastName { get; set; }

        [Required]
        [Key]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email Address is not valid")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"((?=.*\d)(?=.*[A-Z])(?=.*\W).{8,100})", 
            ErrorMessage = "Password must have at least 8 characters length, at least one capital letter, at least one digit and at least one special character")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and ConfirmPassword do not match")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
