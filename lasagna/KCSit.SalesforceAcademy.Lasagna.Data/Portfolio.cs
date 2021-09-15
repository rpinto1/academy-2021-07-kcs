using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public partial class Portfolio
    {
        public Portfolio()
        {
            PortfolioCompanies = new HashSet<PortfolioCompany>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public Guid Uuid { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<PortfolioCompany> PortfolioCompanies { get; set; }
    }
}
