using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public partial class KeyStatistic
    {
        public KeyStatistic()
        {
            YearlyReports = new HashSet<YearlyReport>();
        }

        public int Id { get; set; }
        public decimal? Pe { get; set; }
        public decimal? Pb { get; set; }
        public decimal? Ps { get; set; }
        public decimal? Evs { get; set; }
        public decimal? Evebitda { get; set; }
        public decimal? Evebit { get; set; }
        public decimal? Evpretax { get; set; }
        public decimal? Evfcf { get; set; }
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
        public Guid Uuid { get; set; }

        public virtual ICollection<YearlyReport> YearlyReports { get; set; }
    }
}
