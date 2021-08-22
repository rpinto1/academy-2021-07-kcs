using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Models
{
    public class UserModel
    {
        [Key]
        public Guid UserInfoId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 15, ErrorMessage = "First Name maximum length is 15 characters")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 15, ErrorMessage = "Last Name maximum length is 15 characters")]
        public string LastName { get; set; }

        [Required]
        [Key]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string EmailAdress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 100, MinimumLength = 8, ErrorMessage = "Password should have at least 8 characters")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and ConfirmPassword do not match")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
