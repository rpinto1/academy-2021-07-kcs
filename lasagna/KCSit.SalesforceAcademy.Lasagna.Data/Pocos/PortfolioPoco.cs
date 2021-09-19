using System;
using System.Collections.Generic;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Data.Pocos
{
    public class PortfolioPoco
    {

        public Guid PortfolioId{ get; set; }

        public string PortfolioName { get; set; }

        public List<PortfolioCompanyPoco> PortfolioCompanies { get; set; }



    }
}
