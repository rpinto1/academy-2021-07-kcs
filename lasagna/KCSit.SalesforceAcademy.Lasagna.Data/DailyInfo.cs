using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public partial class DailyInfo
    {
        public DailyInfo()
        {
            Companies = new HashSet<Company>();
        }

        public int Id { get; set; }
        public decimal? StockPrice { get; set; }
        public decimal? PreviousClose { get; set; }
        public Guid Uuid { get; set; }
        public decimal ForwardPe { get; set; }
        public decimal EpsTTM { get; set; }

        public DateTime UpdatedOn { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}
