using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public partial class Company
    {
        public Company()
        {
            CompanyIndices = new HashSet<CompanyIndex>();
            Scores = new HashSet<Score>();
            YearlyReports = new HashSet<YearlyReport>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Qfsticker { get; set; }
        public int IndustryId { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; }
        public string Symbol { get; set; }
        public int ExchangeId { get; set; }
        public string CompanyType { get; set; }
        public string Qfssymbol { get; set; }
        public int? DailyInfoId { get; set; }
        public Guid Uuid { get; set; }

        public virtual Country Country { get; set; }
        public virtual DailyInfo DailyInfo { get; set; }
        public virtual Exchange Exchange { get; set; }
        public virtual Industry Industry { get; set; }
        public virtual ICollection<CompanyIndex> CompanyIndices { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
        public virtual ICollection<YearlyReport> YearlyReports { get; set; }
    }
}
