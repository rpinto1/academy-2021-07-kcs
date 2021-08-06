using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public partial class KeyRatio
    {
        public KeyRatio()
        {
            YearlyReports = new HashSet<YearlyReport>();
        }

        public int Id { get; set; }
        public decimal? ReturnOnAssets { get; set; }
        public decimal? ReturnOnEquity { get; set; }
        public decimal? ReturnOnInvestedCapital { get; set; }
        public decimal? ReturnOnCapitalEmployed { get; set; }
        public decimal? GrossMargin { get; set; }
        public decimal? Ebidtamargin { get; set; }
        public decimal? OperatingMargin { get; set; }
        public decimal? PretaxMargin { get; set; }
        public decimal? NetMargin { get; set; }
        public decimal? FreeCashMargin { get; set; }
        public decimal? AssetsToEquity { get; set; }
        public decimal? EquityToAssets { get; set; }
        public decimal? DebtToEquity { get; set; }
        public decimal? DebtToAssets { get; set; }
        public decimal? RevenueGrowth { get; set; }
        public decimal? GrossProfitGrowth { get; set; }
        public decimal? Ebidtagrowth { get; set; }
        public decimal? OperatingIncomeGrowth { get; set; }
        public decimal? PretaxIncomeGrowth { get; set; }
        public decimal? NetIncomeGrowth { get; set; }
        public decimal? DilutedEpsgrowth { get; set; }
        public decimal? DilutedSharesGrowth { get; set; }
        public decimal? Ppegrowth { get; set; }
        public decimal? TotalAssetsGrowth { get; set; }
        public decimal? EquityGrowth { get; set; }
        public decimal? CashFromOperationsGrowth { get; set; }
        public decimal? CapitalExpendituresGrowth { get; set; }
        public decimal? FreeCashFlowGrowth { get; set; }
        public decimal? FreeCashFlow { get; set; }
        public decimal? BookValue { get; set; }
        public decimal? TangibleBookValue { get; set; }
        public decimal? RevenuePerShare { get; set; }
        public decimal? EbidtaperShare { get; set; }
        public decimal? OperatingIncomePerShare { get; set; }
        public decimal? FreeCashFlowPerShare { get; set; }
        public decimal? BookValuePerShare { get; set; }
        public decimal? TangibleBookValuePerShare { get; set; }
        public decimal? MarketCapitalization { get; set; }
        public decimal? PriceToEarnings { get; set; }
        public decimal? PriceToBook { get; set; }
        public decimal? PriceToSales { get; set; }
        public decimal? DividendsPerShare { get; set; }
        public decimal? PayoutRatio { get; set; }
        public Guid Uuid { get; set; }

        public virtual ICollection<YearlyReport> YearlyReports { get; set; }
    }
}
