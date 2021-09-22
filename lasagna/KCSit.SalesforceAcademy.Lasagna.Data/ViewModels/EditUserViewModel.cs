using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCSit.SalesforceAcademy.Lasagna.Data.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(maximumLength: 15, MinimumLength = 2, ErrorMessage = "First Name must have 2 to 15 characters")]
        [RegularExpression(@"^([a-zA-Z]+)$", ErrorMessage = "First Name has invalid characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 15, MinimumLength = 2, ErrorMessage = "Last Name must have 2 to 15 characters")]
        [RegularExpression(@"^([a-zA-Z]+)$", ErrorMessage = "Last Name has invalid characters")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid Email Address")]
        //[DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        //not required because the user won't always want to change passwords... consult.
        
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }
    }
}
