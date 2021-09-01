using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() 
        {
            Portfolios = new HashSet<Portfolio>();
        }

        [Required]
        [StringLength(maximumLength: 15, MinimumLength = 2, ErrorMessage = "First Name must have 2 to 15 characters")]
        [RegularExpression(@"^([a-zA-Z]+)$", ErrorMessage = "First Name has invalid characters")]
        public string FirstName { get; set; }


        [Required]
        [StringLength(maximumLength: 15, MinimumLength = 2, ErrorMessage = "Last Name must have 2 to 15 characters")]
        [RegularExpression(@"^([a-zA-Z]+)$", ErrorMessage = "Last Name has invalid characters")]
        public string LastName { get; set; }

        public virtual ICollection<Portfolio> Portfolios { get; set; }
    }
}
