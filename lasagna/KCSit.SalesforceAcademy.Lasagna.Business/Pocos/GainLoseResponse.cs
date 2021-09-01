using System;
using System.Collections.Generic;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Pocos
{
    class GainLoseResponse
    {
        public IEnumerable<GainLoseCompanyData> Quotes { get; set; }
    }
}
