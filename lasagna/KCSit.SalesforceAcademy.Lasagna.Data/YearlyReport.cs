using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public partial class YearlyReport
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int CompanyId { get; set; }
        public int IncomeStatementId { get; set; }
        public int BalanceSheetId { get; set; }
        public int CashFlowStatementId { get; set; }
        public int KeyRatioId { get; set; }
        public int KeyStatisticId { get; set; }
        public Guid Uuid { get; set; }

        public virtual BalanceSheet BalanceSheet { get; set; }
        public virtual CashFlowStatement CashFlowStatement { get; set; }
        public virtual Company Company { get; set; }
        public virtual IncomeStatement IncomeStatement { get; set; }
        public virtual KeyRatio KeyRatio { get; set; }
        public virtual KeyStatistic KeyStatistic { get; set; }
    }
}
