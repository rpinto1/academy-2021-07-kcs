using System;
using System.Collections.Generic;

namespace KCSit.SalesforceAcademy.Lasagna.Data.Pocos
{
    public class PortfolioCompanyPoco
    {
        //public Guid PortfolioUuid { get; set; }

        public string Name { get; set; }

        public string Ticker { get; set; }

        public string Score { get; set; }

        public List<PortfolioCompanyValuesPoco> Values { get; set; }

    }
}