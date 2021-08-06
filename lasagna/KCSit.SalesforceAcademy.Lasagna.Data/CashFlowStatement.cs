using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public partial class CashFlowStatement
    {
        public CashFlowStatement()
        {
            YearlyReports = new HashSet<YearlyReport>();
        }

        public int Id { get; set; }
        public decimal? NetIncome { get; set; }
        public decimal? DepreciationAmortization { get; set; }
        public decimal? ChangeInWorkingCapital { get; set; }
        public decimal? ChangeInDeferredTax { get; set; }
        public decimal? StockBasedCompensation { get; set; }
        public decimal? OtherOperations { get; set; }
        public decimal? CashFromOperations { get; set; }
        public decimal? PropertyPlantEquipment { get; set; }
        public decimal? Acquisitions { get; set; }
        public decimal? OtherInvesting { get; set; }
        public decimal? CashFromInvesting { get; set; }
        public decimal? NetIssuanceOfCommonStock { get; set; }
        public decimal? NetIssuanceOfDebt { get; set; }
        public decimal? CashPaidForDividends { get; set; }
        public decimal? OtherFinancing { get; set; }
        public decimal? CashFinancing { get; set; }
        public decimal? Investements { get; set; }
        public Guid Uuid { get; set; }

        public virtual ICollection<YearlyReport> YearlyReports { get; set; }
    }
}
