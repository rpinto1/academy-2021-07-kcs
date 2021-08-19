using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public partial class BalanceSheet
    {
        public BalanceSheet()
        {
            YearlyReports = new HashSet<YearlyReport>();
        }

        public int Id { get; set; }
        public decimal? CashAndEquivalents { get; set; }
        public decimal? ShortTermInvestments { get; set; }
        public decimal? AccountsReceivable { get; set; }
        public decimal? Inventories { get; set; }
        public decimal? OtherCurrentAssets { get; set; }
        public decimal? Investments { get; set; }
        public decimal? PropertyPlantAndEquipment { get; set; }
        public decimal? Goodwill { get; set; }
        public decimal? OtherIntangibleAssets { get; set; }
        public decimal? OtherAssets { get; set; }
        public decimal? AccountsPayable { get; set; }
        public decimal? TaxPayable { get; set; }
        public decimal? ShortTermDebt { get; set; }
        public decimal? CurrentDeferredRevenue { get; set; }
        public decimal? OtherCurrentLabilities { get; set; }
        public decimal? LongTermDebt { get; set; }
        public decimal? CapitalLeases { get; set; }
        public decimal? NonCurrentDeferredRevenue { get; set; }
        public decimal? OtherLiabilities { get; set; }
        public decimal? RetainedEarnings { get; set; }
        public decimal? CommonStock { get; set; }
        public decimal? Aoci { get; set; }
        public decimal? ShareholdersEquity { get; set; }
        public decimal? TotalCurrentAssets { get; set; }
        public decimal? TotalAssets { get; set; }
        public decimal? TotalCurrentLiabilities { get; set; }
        public decimal? TotalLiabilities { get; set; }
        public decimal? TotalLiabilitiesAndEquity { get; set; }
        public Guid Uuid { get; set; }
        public decimal? DeferredPolicyCost { get; set; }
        public decimal? UnearnedPremiums { get; set; }
        public decimal? FuturePolicyBenefits { get; set; }
        public decimal? TotalInvestments { get; set; }
        public decimal? GrossLoans { get; set; }
        public decimal? AllowanceLoanLosses { get; set; }
        public decimal? UnearnedIncome { get; set; }
        public decimal? NetLoans { get; set; }
        public decimal? DepositsLiability { get; set; }

        public virtual ICollection<YearlyReport> YearlyReports { get; set; }
    }
}
