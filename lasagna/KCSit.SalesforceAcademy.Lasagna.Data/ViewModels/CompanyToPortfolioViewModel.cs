using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Data.ViewModels
{
    public class CompanyToPortfolioViewModel
    {
        [Required]
        public Guid PortfolioUuid { get; set; }

        [Required]
        public string Ticker { get; set; }

    }
}
