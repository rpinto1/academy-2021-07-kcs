using System;
using System.Collections.Generic;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Data.Pocos
{
    public class BalanceSheetPoco
    {
        public string Ticker { get; set; }

        public int Year { get; set; }

        public Decimal Equity { get; set; }

        public Decimal Cash { get; set; }


    }
}
