using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public partial class IncomeStatement
    {
        public IncomeStatement()
        {
            YearlyReports = new HashSet<YearlyReport>();
        }

        public int Id { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? CostOfGoodsSold { get; set; }
        public decimal? GrossProfit { get; set; }
        public decimal? SalesGeneralAdministrative { get; set; }
        public decimal? OtherOperatingExpense { get; set; }
        public decimal? OperatingProfit { get; set; }
        public decimal? NetInterestIncome { get; set; }
        public decimal? OtherNonOperatingIncome { get; set; }
        public decimal? PreTaxIncome { get; set; }
        public decimal? IncomeTax { get; set; }
        public decimal? NetIncome { get; set; }
        public decimal? Epsbasic { get; set; }
        public decimal? Epsdiluted { get; set; }
        public decimal? SharesBasic { get; set; }
        public decimal? SharesDiluted { get; set; }
        public Guid Uuid { get; set; }
        public decimal? Development { get; set; }
        public decimal? TotalOperatingExpenses { get; set; }
        public decimal? TotalInterestIncome { get; set; }
        public decimal? TotalInterestExpense { get; set; }
        public decimal? NetInterestIncomeBank { get; set; }
        public decimal? TotalNoninterestRevenue { get; set; }
        public decimal? CreditLossesProvision { get; set; }
        public decimal? NetInterestAclp { get; set; }
        public decimal? TotalNonInterestExpense { get; set; }
        public decimal? TotalPremiums { get; set; }
        public decimal? NetIvestmentIncome { get; set; }
        public decimal? FeesOtherIncome { get; set; }
        public decimal? PolicyClaims { get; set; }
        public decimal? PolicyExpense { get; set; }
        public decimal? InterestExpense { get; set; }

        public virtual ICollection<YearlyReport> YearlyReports { get; set; }
    }
}
