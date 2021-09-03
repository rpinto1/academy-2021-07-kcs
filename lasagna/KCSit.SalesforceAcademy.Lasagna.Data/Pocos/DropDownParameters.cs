using System;
using System.Collections.Generic;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Data.Pocos
{
    public class DropDownParameters
    {

        public string SectorName { get; set; }
        public string IndustryName { get; set; }
        public string IndexName { get; set; }

        public List<string> Countries { get; set; }

        public int Page { get; set; }
    }
}
