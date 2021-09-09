using System;
using System.Collections.Generic;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Data.Pocos
{
    public class CompanyPoco
    {
        public string Name { get; set; }
        public string Ticker { get; set; }

        public Decimal? Price { get; set; }
    }
}
