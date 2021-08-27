using System;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Models
{
    public class CompanyData
    {
        public string DisplayName { get; set; }

        public string Symbol { get; set; }

        public Decimal RegularMarketChange { get; set; }

        public Decimal RegularMarketChangePercent { get; set; }
    }
}
