using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Data.ViewModels
{
    public class PortfolioViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
