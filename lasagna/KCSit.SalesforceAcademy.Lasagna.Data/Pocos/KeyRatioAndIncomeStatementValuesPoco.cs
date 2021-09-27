using System;
using System.Collections.Generic;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Data.Pocos
{
    public class KeyRatioAndIncomeStatementValuesPoco
    {
        public decimal Year { get; set; }
        public decimal? Revenue { get; set; }

        public decimal? RevenueGrowth { get; set; }

        public decimal? GrossProfit { get; set; }

        public decimal? GrossMargin { get; set; }

        public decimal? OperatingProfit { get; set; }

        public decimal? OperatingMargin { get; set; }

        public decimal? EarningsPerShare { get; set; }

        public decimal? EPSGrowth { get; set; }

        //EarningsPerShare

        //DividendsPerShare

        public decimal? DividendsPerShare { get; set; }

        //dividendGrowth

        public decimal? ReturnOnAssets { get; set; }

        public decimal? ReturnOnEquity { get; set; }

        public decimal? ReturnOnInvestedCapital { get; set; }

    }
}
