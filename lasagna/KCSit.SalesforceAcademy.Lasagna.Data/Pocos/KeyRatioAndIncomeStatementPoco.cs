using System;
using System.Collections.Generic;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Data.Pocos
{
    public class KeyRatioAndIncomeStatementPoco
    {
        public string Ticker { get; set; }

        public int Year { get; set; }

        public decimal? Revenue { get; set; }

        public decimal? RevenueGrowth { get; set; }

        public decimal? GrossProfit { get; set; }

        public decimal? GrossMargin { get; set; }

        public decimal? OperatingProfit { get; set; }

        public decimal? OperatingMargin { get; set; }

        public decimal? EarningsPerShare { get; set; }

        public decimal? EPSGrowth { get; set; }

        public decimal? DividendsPerShare { get; set; }

        //dividend growth again

        public decimal? ReturnOnAssets { get; set; }

        public decimal? ReturnOnEquity { get; set; }

        public decimal? ReturnOnInvestedCapital { get; set; }
    }
}
