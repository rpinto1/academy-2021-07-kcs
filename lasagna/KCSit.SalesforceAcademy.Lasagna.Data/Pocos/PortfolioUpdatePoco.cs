using System;
using System.Collections.Generic;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Data.Pocos
{
    public class PortfolioUpdatePoco
    {
        public string Uuid  { get; set; }
        public string PortfolioName { get; set; }
        public List<string> Tickers{ get; set; }
    }
}
