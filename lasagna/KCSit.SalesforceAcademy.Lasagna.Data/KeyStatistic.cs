using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public partial class KeyStatistic
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public decimal? Roamedian { get; set; }
        public decimal? Roemedian { get; set; }
        public decimal? Roicmedian { get; set; }
        public decimal? RevenueCagr { get; set; }
        public decimal? AssetsCagr { get; set; }
        public decimal? Fcfcagr { get; set; }
        public decimal? Epscagr { get; set; }
        public decimal? GrossProfitMedian { get; set; }
        public decimal? Ebitmedian { get; set; }
        public decimal? PreTaxIncomeMedian { get; set; }
        public decimal? Fcfmedian { get; set; }
        public decimal? AssetsEquityMedian { get; set; }
        public decimal? DebtEquityMedian { get; set; }
        public decimal? DebtAssetsMedian { get; set; }
        public decimal? PermiumCagr { get; set; }
        public decimal? UnderwritingMedian { get; set; }
        public decimal? Roimedian { get; set; }
        public Guid Uuid { get; set; }
        public decimal? NetInterestIncomeCagr { get; set; }
        public decimal? GrossLoansCagr { get; set; }
        public decimal? EarningAssetsCagr { get; set; }
        public decimal? DepositsCagr { get; set; }
        public decimal? Nimmedian { get; set; }
        public decimal? EarningAemedian { get; set; }
        public decimal? LoanLossRtlmedian { get; set; }
        public decimal? EquityAssetsMedian { get; set; }

        public virtual Company Company { get; set; }
    }
}
