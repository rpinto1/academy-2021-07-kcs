using System;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Pocos
{
    public class GainLoseCompanyData
    {
        public string DisplayName { get; set; }

        public string Symbol { get; set; }

        public Decimal RegularMarketChange { get; set; }

        public Decimal RegularMarketChangePercent { get; set; }
    }
}
