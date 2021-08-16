using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public partial class Score
    {
        public int Id { get; set; }
        public int? ScoringMethodId { get; set; }
        public int? CompanyId { get; set; }
        public double? Score1 { get; set; }
        public decimal? StickerPrice { get; set; }
        public decimal? MarginOfSafety { get; set; }
        public Guid Uuid { get; set; }

        public virtual Company Company { get; set; }
        public virtual ScoringMethod ScoringMethod { get; set; }
    }
}
