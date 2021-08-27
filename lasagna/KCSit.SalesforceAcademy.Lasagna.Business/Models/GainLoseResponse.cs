using System;
using System.Collections.Generic;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Models
{
    class GainLoseResponse
    {
        public IEnumerable<CompanyData> Quotes { get; set; }
    }
}
