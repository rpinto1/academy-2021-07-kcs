using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Data.ViewModels
{
    public class AdminUpdateViewModel
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


    }
}
