using System;
using System.Collections.Generic;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Data.Pocos
{
    public class CompanyPocoAuthenticated
    {
        public string Name { get; set; }
        public string Ticker { get; set; }

        public Decimal? Price { get; set; }
        public Decimal? StickerPrice { get; set; }
        public Decimal? MarginSafety { get; set; }

        public double? Score { get; set; }
    }
}
