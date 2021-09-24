using System;
using System.Collections.Generic;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Data.Pocos
{
    public class AdminRule1Parameters
    {

        public List<string> Countries { get; set; }
        public string IndexName { get; set; }
        public string SectorName { get; set; }
        public string IndustryName { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public string OrderByField { get; set; }
        public string OrderByDirection { get; set; }
    }
}
