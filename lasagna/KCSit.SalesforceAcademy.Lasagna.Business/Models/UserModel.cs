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
        public string FullName { get; set; }

        [Required]
        public string EmailId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
