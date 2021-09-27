using System;
using System.Collections.Generic;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Data.Pocos
{
    public class AdminRule1Poco
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Exchange { get; set; }
        public string Sector { get; set; }
        public string Industry { get; set; }
        public Decimal? Price { get; set; }
        public string Currency { get; set; }
        public double? Score { get; set; }
        public Decimal? StickerPrice { get; set; }
        public Decimal? MarginSafety { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
}
