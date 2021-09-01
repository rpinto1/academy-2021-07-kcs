using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public partial class PortfolioCompany
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Portfolio Portfolio { get; set; }
    }
}
